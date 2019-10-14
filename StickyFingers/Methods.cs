using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utils;
using static StickyFingers.Variables;
using static StickyFingers.MainForm;

namespace StickyFingers
{
    static class Methods
    {
        public static MainForm MainF;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(MainF = new MainForm());
        }
        public static bool XfbinOpen(int xfbinNo, string xfbinPath)
        {
            MainF.XfbinClose(xfbinNo);
            byte[] fileBytes = File.ReadAllBytes(xfbinPath);
            List<NUD> meshList = new List<NUD>();
            List<byte> nudFile = new List<byte>();
            int meshCount = 0;
            searchResults = SearchForByte("NDP3", fileBytes, 0, fileBytes.Length, 0);
            if (!searchResults.Any())
            {
                MessageBox.Show($"Xfbin doesn't contain any meshes. Please select a valid xfbin.", $"Error");
                return false;
            }
            for (int i = 0; i < searchResults.Count; i++)
            {
                int x = searchResults[i];
                int fileStart;
                int fileSize;
                int meshIndex;
                if (fileBytes[x - 4] != 0x00)
                {
                    fileStart = x - 0x40;
                    fileSize = BigBitConverter.ToInt32(fileBytes, fileStart) + 0x08;
                    meshIndex = fileBytes[x - 0x39];
                }
                else
                {
                    fileStart = x - 0x28;
                    fileSize = BigBitConverter.ToInt32(fileBytes, fileStart) + 0x08;
                    meshIndex = fileBytes[x - 0x21];
                }
                int ndp3Size = BigBitConverter.ToInt32(fileBytes, x + 0x04);
                int sec1Size = BigBitConverter.ToInt32(fileBytes, x + 0x10);
                int sec1Index = x + 0x30;
                int triSize = BigBitConverter.ToInt32(fileBytes, x + 0x14);
                int triIndex = sec1Index + sec1Size;
                int uvSize = BigBitConverter.ToInt32(fileBytes, x + 0x18);
                int uvIndex = triIndex + triSize;
                int vertSize = BigBitConverter.ToInt32(fileBytes, x + 0x1C);
                int vertIndex = uvIndex + uvSize;
                int vertFormat = fileBytes[sec1Index + 0x27]; // either 0x14 or 0x00, for stating if there are vertices or not
                int groupCount = fileBytes[sec1Index + 0x2B];
                int matIndex = x + BigBitConverter.ToInt16(fileBytes, sec1Index + 0x42);
                bool mirrorState = false;
                int mirrorByte = BigBitConverter.ToInt16(fileBytes, matIndex + 0x12);
                if (mirrorByte == 0x00) mirrorState = true; // true for ASB models with no bod1_f, else false
                string matName = BitConverter.ToString(fileBytes, matIndex + 1, 3).Replace('-', ' ');
                matName = matName.TrimStart('0', '0', ' ');
                string meshName = Encoding.Default.GetString(fileBytes.ToArray(), vertIndex + vertSize, 0x20);
                meshName = meshName.Remove(meshName.IndexOf("\0"));
                //string meshFormat = NDP3Format(fileBytes[x + 0x23]);

                for (int a = 0; a < fileSize; a++)
                {
                    nudFile.Add(fileBytes[fileStart + a]);
                }
                meshList.Add(new NUD
                {
                    MeshName = meshName,
                    NudIndex = x,
                    MeshIndex = meshIndex,
                    FileSize = fileSize,
                    NDP3Size = ndp3Size,
                    TriIndex = triIndex,
                    TriSize = triSize,
                    UVIndex = uvIndex,
                    UVSize = uvSize,
                    VertIndex = vertIndex,
                    VertSize = vertSize,
                    VertFormat = vertFormat,
                    GroupCount = groupCount,
                    Material = matName,
                    Mirror = mirrorState,
                    NudFile = nudFile
                });
                meshCount++;
            }
            if (xfbinNo == 1)
            {
                //xfbin1Box.Text = xfbinPath;
                xfbin1Open = true;
                xfbin1Path = xfbinPath;
                file1Bytes = fileBytes.ToList();
                meshCount1 = meshCount;
                meshList1 = meshList;
            }
            else if (xfbinNo == 2)
            {
                //xfbin2Box.Text = xfbinPath;
                xfbin2Open = true;
                xfbin2Path = xfbinPath;
                file2Bytes = fileBytes.ToList();
                meshCount2 = meshCount;
                meshList2 = meshList;
            }
            return true;
        }
        public static void ExportNud(List<byte> fileBytes, List<NUD> meshList, int index)
        {
            int start = meshList[index].NudIndex;
            int count = meshList[index].NDP3Size + 0x02 + (meshList[index].GroupCount * 4);
            byte[] newNud = new byte[count];
            fileBytes.CopyTo(start, newNud, 0, count);
            File.WriteAllBytes(meshList[index].MeshName + ".nud", newNud);
        }
        public static List<int> SearchForByte(string search, byte[] file, int start, int end, int count)
        {
            bool reverse = false;
            if (start > end)
            {
                reverse = true; // Search in the opposite direction
            }
            List<byte> searchList = new List<byte>();
            if (search.Contains("null")) // Search for 0x00 bytes
            {
                int zeroes = (int)Char.GetNumericValue(search[4]);
                for (int z = 0; z < zeroes; z++)
                {
                    searchList.Add(0x00);
                }
            }
            else searchList = Encoding.ASCII.GetBytes(search).ToList();
            List<int> indices = new List<int>();
            int[] searchIndex = new int[searchList.Count()];
            int x = start;
            int n = 0;
            while (reverse && x < start + 1 || x < end)
            {
                if (file[x] == searchList[n])
                {
                    if (n == 0 || searchIndex[n - 1] == x - 1)
                    {
                        searchIndex[n] = x;
                        n++;
                        if (n == searchList.Count())
                        {
                            indices.Add(searchIndex[0]);
                            if (indices.Count == count && !reverse) x = end;
                            else if (reverse) x = start + 2;
                            n = 0;
                        }
                    }
                    else
                    {
                        n = 0;
                        Array.Clear(searchIndex, 0, searchIndex.Length);
                    }
                }
                else n = 0;
                if (reverse) x--;
                else x++;
            }
            return indices;
        }
    }
}
