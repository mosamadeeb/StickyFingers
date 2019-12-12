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
using static StickyFingers.Load;

namespace StickyFingers
{
    static class Save
    {
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
                    if (newNud.Last() != 0x00) // Remove extra group bytes
                    {
                        int l = 0x02 + (mesh2.GroupCount * 0x04);
                        newNud.RemoveRange(newNud.Count - l, l);
                    }
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
                    if (newNud.Last() != 0x00) // Remove extra group bytes
                    {
                        int l = 0x02 + (mesh2.GroupCount * 0x04);
                        newNud.RemoveRange(newNud.Count - l, l);
                    }
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
    }
}
