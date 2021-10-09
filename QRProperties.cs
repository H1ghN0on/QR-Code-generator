using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections;
using System.IO;

namespace Холст_для_QR
{
    static class QRProperties
    {
        public static class ByteCorrection
        {
            public static int[][] Size { get; set; }
            public static int[][] Polynomial { get; set; }
        }
        public static int[][] Size { get; set; }
        public static int[][] AlignmentPattern { get; set; }
        public static int[] HeaderSize { get; set; }
        public static int[][] Blocks { get; set; }
        public static int[][] Galois { get; set; }
        public static ArrayList[] VersionCode { get; set; }
        public static string[][] Mask { get; set; }
        static QRProperties()
        {
            using (StreamReader r = new StreamReader("info.json"))
            {
                List<Item> items;
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<Item>>(json);
                Size = items[0].size;
                HeaderSize = items[0].headerSize;
                Blocks = items[0].blocks;
                ByteCorrection.Size = items[0].byteCorrection;
                ByteCorrection.Polynomial = items[0].polynomial;
                Galois = items[0].galois;
                AlignmentPattern = items[0].alignmentPattern;
                VersionCode = items[0].versionCode;
                Mask = items[0].mask;
            }

        }
    }
    class Item
    {
        public int[][] size = null; //works
        public int[] headerSize; //works;
        public int[][] blocks; //works
        public int[][] byteCorrection;
        public int[][] polynomial;
        public int[][] galois; //works
        public int[][] alignmentPattern;
        public ArrayList[] versionCode; //works;
        public string[][] mask; //works
    }
}
