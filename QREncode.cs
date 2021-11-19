using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Холст_для_QR
{
    static class QREncode
    {

        struct Block
        {
            public List<int> data;
            public int size;
        }
        private static int BlockNumber { get; set; }
        private static string[] data;
        private static string ByteString = "";
        private static Block[] Blocks;
        private static int ByteCorrectionIndex;
        private static int[][] ByteCorrectionBlocks;
        private static int CorrectionLevel { get; set; } 
        private static int Version { get; set; }  

        private static void ToBytes(string value, int correctionLevel)
        {
            ByteString = "";
            CorrectionLevel = correctionLevel;
            Console.WriteLine($"VALUE {value}");
            UTF8Encoding ascii = new UTF8Encoding();
            byte[] bytes = ascii.GetBytes(value);
            foreach (Byte b in bytes)
            {
                string code = Convert.ToString(b, 2);
                while (code.Length < 8)
                {
                    code = "0" + code;
                }
                ByteString += code;
            }
        }
        private static void SetVersion()
        {
            bool or = false;
            for (int i = 0; i < QRProperties.Size[CorrectionLevel].Length; i++)
            {
                if (ByteString.Length <= QRProperties.Size[CorrectionLevel][i])
                {
                    Version = i;
                    break;
                }
            }
            SetHeader();
            while (ByteString.Length % 8 != 0)
            {
                ByteString += "0";
            }
            while (ByteString.Length != QRProperties.Size[CorrectionLevel][Version])
            {
                ByteString += or ? "00010001" : "11101100";
                or = !or;
            }
/*            Console.WriteLine($"Битовая строка с служебной информацией: {ByteString}");*/
        }
        private static void SetHeader()
        {
            string header = Convert.ToString(ByteString.Length / 8, 2);
            string encodingMethod = "0100";
            if (Version <= 9)
            {
                while (header.Length < QRProperties.HeaderSize[0])
                {
                    header = "0" + header;
                }
            }
            else if (Version <= 40)
            {
                while (header.Length < QRProperties.HeaderSize[1])
                {
                    header = "0" + header;
                }
            }
/*            Console.WriteLine($"ByteString: {ByteString}");*/
            ByteString = encodingMethod + header + ByteString;
            if (ByteString.Length > QRProperties.Size[CorrectionLevel][Version])
            {
                Console.WriteLine("THIS SHIT WORKS");
                Version += 1;
               
            }
            Console.WriteLine($"encodingMethod: {encodingMethod}");
            Console.WriteLine($"header: {header}");
        }
        private static void SetBlocks()
        {
            Console.WriteLine($"Версия: {Version}");
            int blockValue, blockRemain;
            BlockNumber = QRProperties.Blocks[CorrectionLevel][Version];
            Blocks = new Block[BlockNumber];
            blockValue = ByteString.Length / 8 / BlockNumber;
            blockRemain = ByteString.Length / 8 % BlockNumber;
            for (int i = BlockNumber - 1; i >= 0; i--)
            {
                Blocks[i].size = blockValue;
                if (i > BlockNumber - blockRemain - 1)
                {
                    Blocks[i].size++;
                }
            }
            Console.WriteLine($"Блоки: {BlockNumber}");
        }
        private static void FillBlocks()
        {
            int j = 0;
            string str;
            for (int i = 0; i < Blocks.Length; i++)
            {
                Blocks[i].data = new List<int>();
                for (int k = 0; k < Blocks[i].size; k++, j += 8)
                {
                    str = "";
                    for (int m = j; m < j + 8; m++)
                    {
                        str += ByteString[m];
                    }
                    Blocks[i].data.Add(Convert.ToInt32(str, 2));
                }
            }
            for (int i = 0; i < Blocks.Length; i++)
            {
                Console.Write($"Размер: {Blocks[i].size}, блок: ");
                for (int k = 0; k < Blocks[i].data.Count; k++)
                {
                    Console.Write($"{Blocks[i].data[k]} ");
                }
                Console.WriteLine();
            }
        }
        private static void SetByteCorrection()
        {
/*            Console.WriteLine();
            Console.WriteLine("Байты коррекции:");*/
            int i;
 
            for (i = 0; i < QRProperties.ByteCorrection.Polynomial.Length; i++)
            {
        
                if (QRProperties.ByteCorrection.Polynomial[i][0] == QRProperties.ByteCorrection.Size[CorrectionLevel][Version])
                {

                    ByteCorrectionIndex = i;
                    break;
                }
            }
            ByteCorrectionBlocks = new int[BlockNumber][];
            for (int block = 0; block < BlockNumber; block++)
            {
                /*Перед выполнением цикла надо подготовить массив, длина которого равна максимуму 
                 * из количества байтов в текущем блоке и количества байтов коррекции, и 
                 * заполнить его начало байтами из текущего блока, а конец нулями.*/
                int[] tempArray;
                if (QRProperties.ByteCorrection.Size[CorrectionLevel][Version] > Blocks[block].data.Count)
                {
                    tempArray = new int[QRProperties.ByteCorrection.Size[CorrectionLevel][Version]];
                }
                else
                {
                    tempArray = new int[Blocks[block].data.Count];
                }


                List<int> numbers = Blocks[block].data;

                for (i = 0; i < numbers.Count; i++)
                {
                    tempArray[i] = numbers[i];
                }
                while (i < tempArray.Length)
                {
                    tempArray[i] = 0;
                    i++;
                }
                for (int m = 0; m < numbers.Count; m++)
                {
                    /* Берём первый элемент массива, сохраняем его значение в переменной А и 
                     * удаляем его из массива (все следующие значения сдвигаются на одну ячейку влево, 
                     * последний элемент заполняется нулём). */
                    int[] poly = new int[QRProperties.ByteCorrection.Size[CorrectionLevel][Version]];
                    for (int j = 0; j < poly.Length; j++)
                    {
                        poly[j] = QRProperties.ByteCorrection.Polynomial[ByteCorrectionIndex][j + 1];
                    }
                    int firstItem = tempArray[0];
                    if (firstItem != 0)
                    {
                        /* Находим соответствующее числу А число в таблице 8, заносим его в переменную Б.
                            Далее для N первых элементов, где N — количество байтов коррекции, i — счётчик цикла:

                            1.К i-му значению генерирующего многочлена надо прибавить значение Б и 
                              записать эту сумму в переменную В (сам многочлен не изменять).
                            2.Если В больше 254, надо использовать её остаток при делении на 255 (именно 255, а не 256).
                            3.Найти соответствующее В значение в таблице 7 и произвести опеацию побитового сложения по модулю 2 (XOR, во многих языках программирования оператор ^) 
                            с i-м значением подготовленного массива и записать полученное значение в i-ю ячейку подготовленного массива.
                        */
                        /*Console.Write("TEMPARRAY: ");*/
                        for (int j = 1; j < tempArray.Length; j++)
                        {
                            tempArray[j - 1] = tempArray[j];
                        }
                        tempArray[tempArray.Length - 1] = 0;
                        


                        int ratio = QRProperties.Galois[1][firstItem];
                        for (int j = 0; j < QRProperties.ByteCorrection.Size[CorrectionLevel][Version]; j++)
                        {
                            poly[j] += ratio;
                            poly[j] %= 255;
                            tempArray[j] = QRProperties.Galois[0][poly[j]] ^ tempArray[j];
                        }
                       /* for (int j = 1; j < tempArray.Length; j++)
                        {
                            Console.Write($"{tempArray[j - 1]} ");
                        }
                        Console.Write($"{tempArray[tempArray.Length - 1]}");
                        Console.WriteLine();*/
                    } else
                    {
                        for (int j = 1; j < tempArray.Length; j++)
                        {
                            tempArray[j - 1] = tempArray[j];
                        }
                        tempArray[tempArray.Length - 1] = 0;
                    }
                }
                ByteCorrectionBlocks[block] = new int[QRProperties.ByteCorrection.Size[CorrectionLevel][Version]];
                for (int f = 0; f < QRProperties.ByteCorrection.Size[CorrectionLevel][Version]; f++)
                {
                    ByteCorrectionBlocks[block][f] = tempArray[f];
                }
/*                foreach (int number in ByteCorrectionBlocks[block])
                {
                    Console.Write("{0} ", number);
                }
                Console.WriteLine();*/
            }
        }
        private static void MergeBlocks()
        {
            int maxBlockSize = Blocks[0].size;
            int BlocksSize = 0;
            int k = 0;
            for (int i = 0; i < BlockNumber; i++)
            {
                if (maxBlockSize < Blocks[i].size)
                {
                    maxBlockSize = Blocks[i].size;
                }
                BlocksSize += Blocks[i].size;
            }
            data = new string[BlocksSize + ByteCorrectionBlocks[0].Length * BlockNumber];
            Console.WriteLine();
            /*Console.WriteLine($"Размер строки {data.Length}");*/
            for (int i = 0; i < Blocks[0].size; i++)
            {
                for (int j = 0; j < BlockNumber; j++, k++)
                {
                    data[k] = Convert.ToString(Blocks[j].data[i], 2);
                    while (data[k].Length < 8)
                    {
                        data[k] = "0" + data[k];
                    }
                }
            }
            if (maxBlockSize != Blocks[0].size)
            {
                for (int i = 0; i < BlockNumber; i++)
                {
                    if (Blocks[i].size == maxBlockSize)
                    {
                        data[k] = Convert.ToString(Blocks[i].data[Blocks[i].data.Count - 1], 2);
                        while (data[k].Length < 8)
                        {
                            data[k] = "0" + data[k];
                        }
                        k++;
                    }
                }
            }
            for (int i = 0; i < ByteCorrectionBlocks[0].Length; i++)
            {
                for (int j = 0; j < ByteCorrectionBlocks.Length; j++, k++)
                {
                    data[k] = Convert.ToString(ByteCorrectionBlocks[j][i], 2);
                    while (data[k].Length < 8)
                    {
                        data[k] = "0" + data[k];
                    }
                }
            }
            int index = 0;
            Console.WriteLine();
/*            foreach (string number in data)
            {
                Console.Write($"{number} ");
                index++;
                if (index == BlocksSize)
                {
                    Console.WriteLine();
                    Console.WriteLine("-----------------------------------------------");
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
            foreach (string number in data)
            {
                Console.Write($"{number} ");
            }*/
        }

        public static QRCode Encode(string value, int correctionLevel)
        {
            ToBytes(value, correctionLevel);
            SetVersion();
            SetBlocks();
            FillBlocks();
            SetByteCorrection();
            MergeBlocks();
            string str = "";
            foreach (string number in data)
            {
                str += number;
            }
            return new QRCode(1, Version, str);
        } 
    }
}
