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
            List<string> Lines = new List<string>();
            int meshCount = 0;
            searchResults = SearchForByte("NDP3", fileBytes, 0, fileBytes.Length, 0);
            if (!searchResults.Any())
            {
                MessageBox.Show($"Xfbin doesn't contain any meshes. Please select a valid xfbin.", $"Error");
                return false;
            }

            // Find the group names + arrange them
            if (SearchForByte("trall", fileBytes, 0, fileBytes.Length, 1).Any())
            {
                int end = SearchForByte("trall", fileBytes, 0, fileBytes.Length, 1)[0];
                string modelName = "";
                int t = 0x20;
                int boneStart = 0;
                while (t <= 0x20 && t > 0)
                {
                    modelName = Encoding.Default.GetString(fileBytes, end - t, 0x20);
                    modelName = modelName.Remove(modelName.IndexOf("\0"));
                    if (modelName.Contains("trall"))
                    {
                        for (int n = 0; n <= 0x20; n++)
                        {
                            if (fileBytes[end - t - n] == 0x00)
                            {
                                boneStart = end - t - n + 1;
                                modelName = Encoding.Default.GetString(fileBytes, boneStart, 0x20);
                                modelName = modelName.Remove(modelName.IndexOf("t0 trall")) + "bod1"; // xchr01bod1
                                n = 0x21;
                            }
                        }
                        t = 0x21;
                    }
                    else t -= 0x08;
                }
                int groupStart = 0;
                int x = 0;
                for (int n = 0; n >= 0; n++)
                {
                    groupStart = SearchForByte(modelName, fileBytes, boneStart - x, 0, 1)[0];
                    x = boneStart - groupStart + 1;
                    if (fileBytes[groupStart + modelName.Length] == 0)
                        n = -2;
                }
                byte[] groupNames = new byte[boneStart - groupStart];
                Array.Copy(fileBytes, groupStart, groupNames, 0, boneStart - groupStart);
                for (int o = 0; o < groupNames.Length; o++)
                {
                    if (groupNames[o] == 0x00)
                    {
                        groupNames[o] = 0x0A;
                    }
                }
                string tx = Encoding.ASCII.GetString(groupNames);
                Lines = tx.Split('\n').ToList();
                Lines.RemoveAt(0);
                Lines.RemoveAt(Lines.Count - 1);
                foreach (string s in Lines.ToList())
                {
                    if (s.Contains(" "))
                        Lines.Remove(s);
                    else Lines[Lines.IndexOf(s)] = s.Remove(0, modelName.Length + 1);
                }
            }

            // Read each mesh in the xfbin
            for (int i = 0; i < searchResults.Count; i++)
            {
                meshList.Add(LoadNud(fileBytes, searchResults[i], true));
                meshCount++;
            }

            // Set each group name to its byte
            List<Group> groupBytes = new List<Group>();
            foreach (NUD mesh in meshList)
            {
                for (int a = 0; a < mesh.GroupCount; a++)
                {
                    if (!groupBytes.Any(g => g.EndByte == mesh.GroupBytes[a].EndByte))
                        groupBytes.Add(mesh.GroupBytes[a]);
                }
            }
            if (Lines.Any())
            {
                foreach (Group g in groupBytes)
                {
                    g.Name = Lines[groupBytes.IndexOf(g)];
                }
            }
            List<int> BoneIDs3 = BoneIDs;

            // Set the bone names obtained from the animation file
            if (BoneNames.Any())
            {
                List<string> names = new List<string>();
                for (int b = 0; b < BoneIDs.Last(); b++)
                {
                    names.Add(BoneNames[b]);
                }
                BoneNames = names;
            }

            // Dynamically set the properties for each xfbin
            if (xfbinNo == 1)
            {
                xfbin1Open = true;
                xfbin1Path = xfbinPath;
                file1Bytes = fileBytes.ToList();
                meshCount1 = meshCount;
                meshList1 = meshList;
                xfbin1Groups = groupBytes;
            }
            else if (xfbinNo == 2)
            {
                xfbin2Open = true;
                xfbin2Path = xfbinPath;
                file2Bytes = fileBytes.ToList();
                meshCount2 = meshCount;
                meshList2 = meshList;
                xfbin2Groups = groupBytes;
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
            List<Group> groupBytes = new List<Group>();
            List<Polygon> poly = new List<Polygon>();
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
                meshIndex = nudFile[0x07];
            }
            else
            {
                fileSize = headerSize + ndp3Size + 0x02;
                nudFile = fileBytes;
            }
            for (int a = 0; a < groupCount; a++)
            {
                int matIndexp = x + BigBitConverter.ToInt16(nudFile, sec1Index + 0x42 + (a * 0x30));
                string matNamep = BitConverter.ToString(nudFile, matIndexp + 1, 3).Replace('-', ' ');
                if (matNamep[1] == '0') matNamep = matNamep.TrimStart('0', '0', ' ');
                poly.Add(new Polygon
                {
                    VertCount = BigBitConverter.ToInt16(nudFile, sec1Index + 0x3C + (a * 0x30)),
                    FormatByte = nudFile[sec1Index + 0x3E + (a * 0x30)],
                    MatName = matNamep,
                    EndByte = 0
                });
                vertCount += poly[a].VertCount;
                if (header)
                {
                    poly[a].EndByte = nudFile[(x - 1) + ndp3Size + 0x06 + (a * 4)];
                    groupBytes.Add(new Group
                    {
                        EndByte = poly[a].EndByte,
                        Name = ""
                    });
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

            string meshFormatName = "";
            foreach (Polygon p in poly)
            {
                switch (p.FormatByte)
                {
                    case 0x06: // Storm teeth/eyes
                        p.MeshFormat = 0x1C;
                        break;
                    case 0x20: // Storm effects (not ready)
                        p.MeshFormat = 0x20;
                        break;
                    case 0x07: // JoJo teeth
                        p.MeshFormat = 0x2C;
                        break;
                    case 0x30: // JoJo teeth (not ready)
                        p.MeshFormat = 0x30;
                        break;
                    case 0x11: // Storm most meshes + JoJo eyes
                        p.MeshFormat = 0x40;
                        p.BoneOffset = 0x20;
                        break;
                    case 0x13: // JoJo most meshes
                        p.MeshFormat = 0x60;
                        p.BoneOffset = 0x40;
                        break;
                    default:
                        if (vertSize == 0)
                            p.MeshFormat = uvSize / vertCount;
                        else p.MeshFormat = vertSize / vertCount;
                        break;
                }
                // Only last format for now
                meshFormatName = "0x" + BitConverter.ToString(BitConverter.GetBytes(p.MeshFormat)).Substring(0, 2);
            }

            int vertSum = 0;
            int maxBone = 0;
            List<BoneBytes> bones = new List<BoneBytes>();
            for (int a = 0; a < poly.Count; a++)
            {
                if (poly[a].BoneOffset != 0)
                {
                    for (int i = 0; i < poly[a].VertCount; i++)
                    {
                        BoneBytes bytes = new BoneBytes()
                        {
                            Id1 = nudFile[vertIndex + vertSum + poly[a].BoneOffset + 0x03 + (i * poly[a].MeshFormat)],
                            Id2 = nudFile[vertIndex + vertSum + poly[a].BoneOffset + 0x07 + (i * poly[a].MeshFormat)],
                            Id3 = nudFile[vertIndex + vertSum + poly[a].BoneOffset + 0x0B + (i * poly[a].MeshFormat)]
                        };
                        int IdMax1 = Math.Max(bytes.Id1, bytes.Id2);
                        int IdMax2 = Math.Max(bytes.Id3, IdMax1);
                        if (IdMax2 > maxBone) maxBone = IdMax2;
                        if (!BoneIDs.Contains(bytes.Id1)) BoneIDs.Add(bytes.Id1);
                        if (!BoneIDs.Contains(bytes.Id2)) BoneIDs.Add(bytes.Id2);
                        if (!BoneIDs.Contains(bytes.Id3)) BoneIDs.Add(bytes.Id3);
                        bones.Add(bytes);
                    }
                    vertSum += poly[a].VertCount * poly[a].MeshFormat;
                }
            }
            BoneIDs = BoneIDs.OrderBy(l => l).ToList();

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
                GroupBytes = groupBytes,
                Bones = bones,
                MaxBone = maxBone,
                Polygons = poly
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
            int groups = meshList1[index1].GroupCount;
            int ndp3Start = meshList1[index1].NudIndex;
            int ndp3Size = meshList1[index1].NDP3Size + 0x02 + (groups * 0x04);
            int fileEnd = ndp3Start + ndp3Size;
            int xfbinEnd = file1Bytes.Count - fileEnd;
            byte[] temp = new byte[xfbinEnd];
            file1Bytes.CopyTo(fileEnd, temp, 0, xfbinEnd);
            file1Bytes.RemoveRange(ndp3Start, file1Bytes.Count - ndp3Start);
            file1Bytes.AddRange(FixGroupBytes(index1, externalMesh));
            externalMesh.NDP3Size += nameSizeOffset;
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
            nameSizeOffset = 0;
        }
        public static List<byte> FixGroupBytes(int index1, NUD mesh2)
        {
            List<byte> newNud = mesh2.NudFile;

            // Replace mesh name (might need manual size fix if name sizes are different)
            int nameIndex = mesh2.VertIndex + mesh2.VertSize;
            string meshName = meshList1[index1].MeshName;
            int length1 = 0x10;
            int length2 = 0x10;
            if (meshName.Length >= 0x10)
                length1 = 0x20;
            else if (meshName.Length >= 0x20)
                length1 = 0x30;
            if (mesh2.MeshName.Length >= 0x10)
                length2 = 0x20;
            else if (mesh2.MeshName.Length >= 0x20)
                length2 = 0x30;
            if (length1 < length2)
            {
                byte[] temp = new byte[newNud.Count - (nameIndex + length2)];
                newNud.CopyTo(nameIndex + length2, temp, 0, newNud.Count - (nameIndex + length2));
                newNud.RemoveRange(nameIndex + length1, length2 - length1);
                newNud.AddRange(temp);
                nameSizeOffset = -(length2 - length1);
            }
            else if (length1 > length2)
            {
                byte[] empty = new byte[length1 - length2];
                for (int x = 0; x < empty.Length; x++)
                {
                    empty[x] = 0x00;
                }
                byte[] temp = new byte[newNud.Count - (nameIndex + length2)];
                newNud.CopyTo(nameIndex + length2, temp, 0, newNud.Count - (nameIndex + length2));
                newNud.RemoveRange(nameIndex + length2, length1 - length2);
                newNud.AddRange(empty);
                newNud.AddRange(temp);
                nameSizeOffset = length1 - length2;
            }
            for (int x = 0; x < length1; x++)
            {
                newNud[nameIndex + x] = 0x00;
            }
            for (int x = 0; x < meshName.Length; x++)
            {
                newNud[nameIndex + x] = BitConverter.GetBytes(meshName[x])[0];
            }

            // Change mesh index
            newNud[0x07] = BitConverter.GetBytes(meshList1[index1].MeshIndex)[0];

            // Fix group bytes (could use a rewrite)
            int groups1 = meshList1[index1].GroupCount;
            int groups2 = mesh2.GroupCount;
            int groupStart = mesh2.HeaderSize + mesh2.NDP3Size + 0x05;
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
                        newNud.Add(meshList1[index1].GroupBytes[x].EndByte); // Needs some changes
                    }
                }
                else
                {
                    for (int x = 0; x < groups2; x++)
                    {
                        newNud[groupStart + (x * 4)] = meshList1[index1].GroupBytes[x].EndByte; // Needs some changes
                    }
                }
            }
            else // This gets the groups it can from the old mesh and
            {    // adds the rest from the mesh with the most groups
                List<byte> tempgroup1Bytes = new List<byte>();
                foreach (Group g in xfbin1Groups)
                {
                    tempgroup1Bytes.Add(g.EndByte);  // Needs some changes
                }
                if (allGroupBytes)
                {
                    for (int x = 0; x < groups1; x++)
                    {
                        tempgroup1Bytes.Remove(meshList1[index1].GroupBytes[x].EndByte); // Needs some changes
                    }
                }
                if (nudOpen)
                {
                    newNud.Add(0x00);
                    newNud.Add(BitConverter.GetBytes(groups2)[0]);
                    for (int x = 0; x < groups1; x++)
                    {
                        newNud.Add(0x00);
                        newNud.Add(0x00);
                        newNud.Add(0x00);
                        newNud.Add(meshList1[index1].GroupBytes[x].EndByte); // Needs some changes
                    }
                    if (tempgroup1Bytes.Count >= groups2)
                    {
                        for (int x = 0; x < groups2 - groups1; x++)
                        {
                            newNud.Add(0x00);
                            newNud.Add(0x00);
                            newNud.Add(0x00);
                            newNud.Add(tempgroup1Bytes[x + groups1]);
                        }
                    }
                    else MessageBox.Show($"Some end bytes need manual fixing...", $"Warning");
                }
                else
                {
                    int i = 0;
                    for (int x = 0; x < groups1; x++)
                    {
                        newNud[groupStart + (x * 4)] = meshList1[index1].GroupBytes[x].EndByte; // Needs some changes
                        i = x;
                    }
                    if (tempgroup1Bytes.Count >= groups2)
                    {
                        for (int x = i; x < groups2 - groups1; x++)
                        {
                            newNud[groupStart + ((x + 1) * 4)] = tempgroup1Bytes[x + groups1];
                        }
                    }
                    else MessageBox.Show($"Some end bytes need manual fixing...", $"Warning");
                }
            }
            return newNud;
        }
        // Read bone names from an animation file
        public static void LoadBones(int xfbinNo, string motPath)
        {
            byte[] fileBytes = File.ReadAllBytes(motPath);
            List<string> Lines = new List<string>();

            if (SearchForByte("trall", fileBytes, 0, fileBytes.Length, 1).Any())
            {
                int end = SearchForByte("trall", fileBytes, 0, fileBytes.Length, 1)[0];
                int t = 0x20;
                int boneStart = 0;
                int boneEnd = 0;
                while (t <= 0x20 && t > 0)
                {
                    modelName = Encoding.Default.GetString(fileBytes, end - t, 0x20);
                    modelName = modelName.Remove(modelName.IndexOf("\0"));
                    if (modelName.Contains("trall"))
                    {
                        for (int n = 0; n <= 0x20; n++)
                        {
                            if (fileBytes[end - t - n] == 0x00)
                            {
                                boneStart = end - t - n + 1;
                                modelName = Encoding.Default.GetString(fileBytes, boneStart, 0x20);
                                modelName = modelName.Remove(modelName.IndexOf("t0 trall")); // xchr01
                                n = 0x21;
                            }
                        }
                        t = 0x21;
                    }
                    else t -= 0x08;
                }
                int x = 0;
                bool skipped = false;
                for (int n = 0; n >= 0; n++)
                {
                    boneEnd = SearchForByte(modelName, fileBytes, boneStart + x, fileBytes.Length, 1)[0];
                    x = boneEnd - boneStart + 1;
                    if (fileBytes[boneEnd + modelName.Length + 2] != 0x20) // space
                    {
                        if (skipped)
                        {
                            n = -2;
                        }
                        else skipped = true;
                    }
                }

                byte[] boneNames = new byte[boneEnd - boneStart];
                Array.Copy(fileBytes, boneStart, boneNames, 0, boneEnd - boneStart);
                for (int o = 0; o < boneNames.Length; o++)
                {
                    if (boneNames[o] == 0x00)
                    {
                        boneNames[o] = 0x0A;
                    }
                }
                string tx = Encoding.ASCII.GetString(boneNames);
                Lines = tx.Split('\n').ToList();
                Lines.RemoveAt(1);
                Lines.RemoveAt(Lines.Count - 1);
                foreach (string s in Lines.ToList())
                {
                    Lines[Lines.IndexOf(s)] = s.Remove(0, modelName.Length + 3);
                }
                BoneNames = Lines;
            }
            else MessageBox.Show($"Xfbin doesn't contain bone names. Please select a valid xfbin.", $"Error");
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
            int i = 0;
            while (reverse && x > end || !reverse && x < end)
            {
                if (file[x + i] == searchList[n])
                {
                    if (n == 0 || searchIndex[n - 1] == x - 1 || reverse && searchIndex[n - 1] == x + i - 1)
                    {
                        searchIndex[n] = x + i;
                        n++;
                        if (reverse) i+= 2;
                        //if (i == 1) i++;
                        if (n == searchList.Count())
                        {
                            indices.Add(searchIndex[0]);
                            if (indices.Count == count) x = end;
                            else if (reverse) x = start + 2;
                            n = 0;
                            i = 0;
                        }
                    }
                    else
                    {
                        n = 0;
                        i = 0;
                        Array.Clear(searchIndex, 0, searchIndex.Length);
                    }
                }
                else
                {
                    n = 0;
                    i = 0;
                    Array.Clear(searchIndex, 0, searchIndex.Length);
                }
                if (reverse) x--;
                else x++;
            }
            return indices;
        }
    }
}
