using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace StickyFingers
{
    public class Variables
    {
        public static string xfbin1Path;
        public static string xfbin2Path;
        public static string meshFormat;
        public static string dragFilePath;
        public static bool xfbin1Open;
        public static bool xfbin2Open;
        public static bool noesisStarted = false;
        public static List<byte> file1Bytes;
        public static List<byte> file2Bytes;
        public static List<int> searchResults;
        public static List<NUD> meshList1;
        public static List<NUD> meshList2;
        public static int meshCount1;
        public static int meshCount2;
        public static Process Noesis;

        public static string ProgramVersion
        {
            get { return "1.0"; }
        }

        public static string NDP3Format(int formatByte)
        {
            if (formatByte == 0x0)
            {
                return "DXT1";
            }
            else if (formatByte == 0x2)
            {
                return "DXT5";
            }
            else return "Unknown format";
        }

        public static string appData = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "StickyFingers"
            );
    }

    public class NUD
    {
        public string MeshName { get; set; }
        public int NudIndex { get; set; }
        public int FileSize { get; set; }
        public int NDP3Size { get; set; }
        public int MeshIndex { get; set; }
        public int TriIndex { get; set; }
        public int TriSize { get; set; }
        public int UVIndex { get; set; }
        public int UVSize { get; set; }
        public int VertIndex { get; set; }
        public int VertSize { get; set; }
        public int VertFormat { get; set; }
        public int GroupCount { get; set; }
        public string Material { get; set; }
        public bool Mirror { get; set; }
        public List<byte> TriFile { get; set; }
        public List<byte> UVFile { get; set; }
        public List<byte> VertFile { get; set; }
        public List<byte> NudFile { get; set; }
    }
}