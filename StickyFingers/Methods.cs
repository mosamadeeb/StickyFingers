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
            byte[] fileBytes = File.ReadAllBytes(xfbinPath);
            List<NUD> meshList = new List<NUD>();
            int meshCount = 0;
            searchResults = SearchForByte("NDP3", fileBytes, 0, fileBytes.Length, 0);
            if (!searchResults.Any())
            {
                MessageBox.Show($"Xfbin doesn't contain any meshes. Please select a valid xfbin.", $"Error");
                return false;
            }
            for (int i = 0; i < searchResults.Count; i++)
            {
                meshList.Add(LoadNud(fileBytes, searchResults[i], true));
                meshCount++;
            }
            int groupNo = 0;
            foreach (NUD mesh in meshList)
            {
                if (mesh.GroupCount > groupNo)
                {
                    groupNo = mesh.GroupCount;
                    group1Bytes = mesh.GroupBytes;
                }
            }
            if (xfbinNo == 1)
            {
                xfbin1Open = true;
                xfbin1Path = xfbinPath;
                file1Bytes = fileBytes.ToList();
                meshCount1 = meshCount;
                meshList1 = meshList;
            }
            else if (xfbinNo == 2)
            {
                xfbin2Open = true;
                xfbin2Path = xfbinPath;
                file2Bytes = fileBytes.ToList();
                meshCount2 = meshCount;
                meshList2 = meshList;
            }
            return true;
        }
        public static NUD LoadNud(byte[] fileBytes, int index, bool header)
        {
            int x = index;
            int headerSize = 0;
            int fileStart = index;
            int fileSize;
            int meshIndex = 0;
            int ndp3Size = fileBytes[x + 0x05] * 0x10000 + fileBytes[x + 0x06] * 0x100 + fileBytes[x + 0x07];
            int sec1Index = x + 0x30;
            int groupCount = fileBytes[sec1Index + 0x2B];
            int vertCount = 0;
            byte[] nudFile;
            List<byte> groupBytes = new List<byte>();
            if (header)
            {
                if (fileBytes[x - 4] != 0x00)
                {
                    headerSize = 0x40;
                    fileStart = x - headerSize;
                }
                else
                {
                    headerSize = 0x28;
                    fileStart = x - headerSize;
                }

                fileSize = headerSize + ndp3Size + 0x02 + (groupCount * 0x04);
                nudFile = new byte[fileSize];
                Array.Copy(fileBytes, fileStart, nudFile, 0, fileSize);
                x = headerSize;
                sec1Index = x + 0x30;
                for (int a = 0; a < groupCount; a++)
                {
                    vertCount += BigBitConverter.ToInt16(nudFile, sec1Index + 0x3C + (a * 0x30));
                    groupBytes.Add(nudFile[(x - 1) + ndp3Size + 0x06 + (a * 4)]);
                }
                meshIndex = nudFile[0x07];
            }
            else
            {
                fileSize = headerSize + ndp3Size + 0x02;
                nudFile = fileBytes;
                for (int a = 0; a < groupCount; a++)
                {
                    vertCount += BigBitConverter.ToInt16(nudFile, sec1Index + 0x3C + (a * 0x30));
                }
            }

            int sec1Size = BigBitConverter.ToInt32(nudFile, x + 0x10);
            int triSize = BigBitConverter.ToInt32(nudFile, x + 0x14);
            int triIndex = sec1Index + sec1Size;
            int uvSize = BigBitConverter.ToInt32(nudFile, x + 0x18);
            int uvIndex = triIndex + triSize;
            int vertSize = BigBitConverter.ToInt32(nudFile, x + 0x1C);
            int vertIndex = uvIndex + uvSize;
            int vertFormat = nudFile[sec1Index + 0x27]; // either 0x14 or 0x00, for stating if there are vertices or not


            int matIndex = x + BigBitConverter.ToInt16(nudFile, sec1Index + 0x42);
            bool mirrorState = false;
            int mirrorByte = BigBitConverter.ToInt16(nudFile, matIndex + 0x12);
            if (mirrorByte == 0x00) mirrorState = true; // true for ASB models with no bod1_f, else false
            string matName = BitConverter.ToString(nudFile, matIndex + 1, 3).Replace('-', ' ');
            if (matName[1] == '0') matName = matName.TrimStart('0', '0', ' ');
            string meshName = Encoding.Default.GetString(nudFile.ToArray(), vertIndex + vertSize, 0x20);
            meshName = meshName.Remove(meshName.IndexOf("\0"));

            int meshFormat;
            if (vertSize == 0)
            {
                meshFormat = uvSize / vertCount;
            }
            else meshFormat = vertSize / vertCount;


            string meshFormatName = "0x" + BitConverter.ToString(BitConverter.GetBytes(meshFormat)).Substring(0, 2);

            switch (meshFormat)
            {
                case 0x1C: // Storm teeth/eyes
                    break;
                case 0x20: // Storm effects
                    break;
                case 0x30: // JoJo teeth
                    break;
                case 0x40: // Storm most meshes + JoJo eyes
                    break;
                case 0x60: // JoJo most meshes
                    break;
                default:
                    break;
            }

            return new NUD
            {
                MeshName = meshName,
                MeshFormat = meshFormatName,
                NudIndex = fileStart + headerSize,
                FileStart = fileStart,
                HeaderSize = headerSize,
                FileSize = fileSize,
                NDP3Size = ndp3Size,
                MeshIndex = meshIndex,
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
                NudFile = nudFile.ToList(),
                GroupBytes = groupBytes
            };
        }
        public static void ExportNud(List<byte> fileBytes, List<NUD> meshList, int index)
        {
            string fileName = meshList[index].MeshName + ".nud";
            int start = meshList[index].NudIndex;
            int count = meshList[index].NDP3Size + 0x02 + (meshList[index].GroupCount * 4);
            byte[] newNud = new byte[count];
            fileBytes.CopyTo(start, newNud, 0, count);
            File.WriteAllBytes(fileName, newNud);
            MessageBox.Show($"File saved as \"" + fileName + "\" in the program's directory.", $"Success");
        }
        public static void ReplaceMesh(int index1, int index2)
        {
            int fileStart = meshList1[index1].FileStart;
            int fileSize = meshList1[index1].FileSize;
            int fileEnd = fileStart + fileSize;
            int xfbinEnd = file1Bytes.Count - fileEnd;
            byte[] temp = new byte[xfbinEnd];
            file1Bytes.CopyTo(fileEnd, temp, 0, xfbinEnd);
            file1Bytes.RemoveRange(fileStart, file1Bytes.Count - fileStart);
            file1Bytes.AddRange(FixGroupBytes(index1, meshList2[index2]));
            file1Bytes.AddRange(temp);
        }
        public static void ReplaceExternalMesh(int index1)
        {
            // Should have different behavior with lack of header
            int groups = meshList1[index1].GroupCount;
            int ndp3Start = meshList1[index1].NudIndex;
            int ndp3Size = meshList1[index1].NDP3Size + 0x02 + (groups * 0x04);
            int fileEnd = ndp3Start + ndp3Size;
            int xfbinEnd = file1Bytes.Count - fileEnd;
            byte[] temp = new byte[xfbinEnd];
            file1Bytes.CopyTo(fileEnd, temp, 0, xfbinEnd);
            file1Bytes.RemoveRange(ndp3Start, file1Bytes.Count - ndp3Start);
            file1Bytes.AddRange(FixGroupBytes(index1, externalMesh));
            int header = meshList1[index1].HeaderSize;
            int fileSizeInt = header + externalMesh.NDP3Size - 0x02 + (externalMesh.GroupCount * 4);
            byte[] size = BigBitConverter.GetBytes(externalMesh.NDP3Size);
            byte[] fileSize = BigBitConverter.GetBytes(fileSizeInt - 0x08);
            file1Bytes[ndp3Start - 3] = size[1];
            file1Bytes[ndp3Start - 2] = size[2];
            file1Bytes[ndp3Start - 1] = size[3];
            file1Bytes[ndp3Start - (header - 1)] = fileSize[1];
            file1Bytes[ndp3Start - (header - 2)] = fileSize[2];
            file1Bytes[ndp3Start - (header - 3)] = fileSize[3];
            file1Bytes.AddRange(temp);
        }
        public static List<byte> FixGroupBytes(int index1, NUD mesh2)
        {
            int groups1 = meshList1[index1].GroupCount;
            int groups2 = mesh2.GroupCount;
            int groupStart = mesh2.HeaderSize + mesh2.NDP3Size + 0x05;
            List<byte> newNud = mesh2.NudFile;

            // Change mesh index
            newNud[0x07] = BitConverter.GetBytes(meshList1[index1].MeshIndex)[0];

            if (groups1 >= groups2)
            {
                if (nudOpen)
                {
                    newNud.Add(0x00);
                    newNud.Add(BitConverter.GetBytes(groups2)[0]);
                    for (int x = 0; x < groups2; x++)
                    {
                        newNud.Add(0x00);
                        newNud.Add(0x00);
                        newNud.Add(0x00);
                        newNud.Add(meshList1[index1].GroupBytes[x]);
                    }
                }
                else
                {
                    for (int x = 0; x < groups2; x++)
                    {
                        newNud[groupStart + (x * 4)] = meshList1[index1].GroupBytes[x];
                    }
                }
            }
            else // This gets the groups it can from the old mesh and
            {    // adds the rest from the mesh with the most groups
                if (nudOpen)
                {
                    newNud.Add(0x00);
                    newNud.Add(BitConverter.GetBytes(groups2)[0]);
                    for (int x = 0; x < groups1; x++)
                    {
                        newNud.Add(0x00);
                        newNud.Add(0x00);
                        newNud.Add(0x00);
                        newNud.Add(meshList1[index1].GroupBytes[x]);
                    }
                    if (group1Bytes.Count >= groups2)
                    {
                        for (int x = 0; x < groups2 - groups1; x++)
                        {
                            newNud.Add(0x00);
                            newNud.Add(0x00);
                            newNud.Add(0x00);
                            newNud.Add(group1Bytes[x + groups1]);
                        }
                    }
                    else MessageBox.Show($"Some end bytes need manual fixing...", $"Warning");
                }
                else
                {
                    int i = 0;
                    for (int x = 0; x < groups1; x++)
                    {
                        newNud[groupStart + (x * 4)] = meshList1[index1].GroupBytes[x];
                        i = x;
                    }
                    if (group1Bytes.Count >= groups2)
                    {
                        for (int x = i; x < groups2 - groups1; x++)
                        {
                            newNud[groupStart + ((x + 1) * 4)] = group1Bytes[x + groups1];
                        }
                    }
                    else MessageBox.Show($"Some end bytes need manual fixing...", $"Warning");
                }
            }
            return newNud;
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
