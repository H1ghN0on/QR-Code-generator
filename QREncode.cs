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

        private static string ByteString = "";
        private static Block[] Blocks;
 
        private static int[][] ByteCorrectionBlocks;
        private static int CorrectionLevel { get; set; }
        private static int Version { get; set; }

        private static int Size { get; set; }
        private static char[][] qrArray { get; set; }
        /* Шифрование */
        private static void ToBytes(string value, int correctionLevel)
        {
            ByteString = "";
            CorrectionLevel = correctionLevel;
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
        private static int SetVersion()
        {
            
            Version = -1;
            bool or = false;
            for (int i = 0; i < QRProperties.Size[CorrectionLevel].Length; i++)
            {
                if (ByteString.Length <= QRProperties.Size[CorrectionLevel][i])
                {
                    Version = i;
                    break;
                }
            }
            if (Version == -1)
            {
                return -1;
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
            return 0;
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
            else if (Version <= 39)
            {
                while (header.Length < QRProperties.HeaderSize[1])
                {
                    header = "0" + header;
                }
            }
            ByteString = encodingMethod + header + ByteString;
            if (ByteString.Length > QRProperties.Size[CorrectionLevel][Version])
            {
                Version += 1; 
            }
        }
        private static void SetBlocks()
        {
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
        }
        private static void SetByteCorrection()
        {
            int ByteCorrectionIndex = -1;
            int i;
            //В Polynomial первым значением является размер, последующими являются значениями самого генерирующего многочлена
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

                for (i = 0; i < Blocks[block].data.Count; i++)
                {
                    tempArray[i] = Blocks[block].data[i];
                }
                while (i < tempArray.Length)
                {
                    tempArray[i] = 0;
                    i++;
                }
                for (int m = 0; m < Blocks[block].data.Count; m++)
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
            }
        }
        private static string MergeBlocks()
        {
            string[] data;
            string finalResult = "";
            int maxBlockSize = Blocks[0].size;
            int blocksSize = 0;
            int k = 0;
            for (int i = 0; i < BlockNumber; i++)
            {
                if (maxBlockSize < Blocks[i].size)
                {
                    maxBlockSize = Blocks[i].size;
                }
                blocksSize += Blocks[i].size;
            }
            data = new string[blocksSize + ByteCorrectionBlocks[0].Length * BlockNumber];
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
            foreach (string number in data)
            {
                finalResult += number;
            }
            return finalResult;
        }

        public static QRCode Encode(string value, int correctionLevel)
        {
            ToBytes(value, correctionLevel); //Превращение символьной строки в битовую строку
            if (SetVersion() == -1) //Установка версии QR кода, возвращает -1 в случае чрезмерно огромного текста
            {
                return null;
            }
            SetBlocks(); //Установка размеров блоков данных
            FillBlocks(); //Заполненое блоков информацией
            SetByteCorrection(); //Генерация блоков байтов коррекции на основе полученных блоков


            return new QRCode(1, Version, MergeBlocks()); 
            //Объединение блоков данных и блоков коррекции в одну битовую строку и возвращение итогового результата

        }


        /*   Преобразование зашифрованной информации в массив */
        public enum Direction
        {
            Up, Down
        }
        public static void InitializeArray()
        {
            Size = QRProperties.FieldSize[Version];
            qrArray = new char[Size][];
            for (int i = 0; i < Size; i++)
            {
                qrArray[i] = new char[Size];
            }
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    qrArray[i][j] = '0';
                }
            }
        }
        public static void FillSearchPatterns()
        {
            for (int i = 0; i < 7; i++)
            {
                qrArray[i][0] = '3';
                qrArray[0][i] = '3';
                qrArray[6][i] = '3';
                qrArray[i][6] = '3';
                qrArray[Size - 7][i] = '3';
                qrArray[Size - 1][i] = '3';
                qrArray[i][Size - 7] = '3';
                qrArray[i][Size - 1] = '3';
                qrArray[7][i] = '4';
                qrArray[i][7] = '4';
                qrArray[Size - 8][i] = '4';
                qrArray[i][Size - 8] = '4';
            }
            qrArray[7][7] = '4';
            qrArray[Size - 8][7] = '4';
            qrArray[7][Size - 8] = '4';
            for (int i = Size - 7; i < Size; i++)
            {
                qrArray[i][0] = '3';
                qrArray[i][6] = '3';
                qrArray[6][i] = '3';
                qrArray[0][i] = '3';
                qrArray[i - 1][7] = '4';
                qrArray[7][i - 1] = '4';
            }
            qrArray[Size - 1][7] = '4';
            qrArray[7][Size - 1] = '4';
            for (int i = 1; i < 6; i++)
            {
                qrArray[i][1] = '2';
                qrArray[1][i] = '2';
                qrArray[5][i] = '2';
                qrArray[i][5] = '2';
                qrArray[Size - 6][i] = '2';
                qrArray[Size - 2][i] = '2';
                qrArray[i][Size - 6] = '2';
                qrArray[i][Size - 2] = '2';
                qrArray[Size - i - 1][1] = '2';
                qrArray[Size - i - 1][5] = '2';
                qrArray[5][Size - i - 1] = '2';
                qrArray[1][Size - i - 1] = '2';
            }
            for (int i = 2; i < 5; i++)
            {
                for (int j = 2; j < 5; j++)
                {
                    qrArray[i][j] = '3';
                    qrArray[Size - i - 1][j] = '3';
                    qrArray[j][Size - i - 1] = '3';
                }
            }
        }
        public static void FillAlignmentPatterns()
        {
            if (Version > 0)
            {
                int xPoint, yPoint;
                int[] pattern = QRProperties.AlignmentPattern[Version - 1];
                for (int i = 0; i < pattern.Length; i++)
                {
                    for (int j = 0; j < pattern.Length; j++)
                    {
                        if (CheckExistanceOfAlignmentPattern(pattern[i], pattern[j]))
                        {
                            xPoint = pattern[i];
                            yPoint = pattern[j];
                            qrArray[xPoint][yPoint] = '6';
                            for (int x = xPoint - 1; x <= xPoint + 1; x++)
                            {
                                qrArray[x][yPoint + 1] = '5';
                                qrArray[x][yPoint - 1] = '5';
                            }
                            qrArray[xPoint + 1][yPoint] = '5';
                            qrArray[xPoint - 1][yPoint] = '5';
                            for (int x = xPoint - 2; x <= xPoint + 2; x++)
                            {
                                qrArray[x][yPoint + 2] = '6';
                                qrArray[x][yPoint - 2] = '6';
                            }
                            for (int y = yPoint - 2; y <= yPoint + 2; y++)
                            {
                                qrArray[xPoint + 2][y] = '6';
                                qrArray[xPoint - 2][y] = '6';
                            }
                        }
                    }
                }
            }
        }
        private static bool CheckExistanceOfAlignmentPattern(int xPoint, int yPoint)
        {
            for (int x = xPoint - 2; x < xPoint + 2; x++)
            {
                for (int y = yPoint - 2; y < yPoint + 2; y++)
                {
                    if (qrArray[x][y] == '3' || qrArray[x][y] == '2')
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private static void FillLocal(string value, int x, int y, ref int i)
        {
            try
            {
                if (qrArray[y][x] == '0')
                {
                    if (MaskArr(x, y) == 0)
                    {
                        qrArray[y][x] = (value[i] == '1' ? '0' : '1');
                    }
                    else
                    {
                        qrArray[y][x] = value[i];
                    }
                    i++;
                }
                if (qrArray[y][x - 1] == '0')
                {
                    qrArray[y][x - 1] = value[i + 1];

                    if (MaskArr(x - 1, y) == 0)
                    {
                        qrArray[y][x - 1] = (value[i] == '1' ? '0' : '1');
                    }
                    else
                    {
                        qrArray[y][x - 1] = value[i];
                    }
                    i++;
                }

            }
            catch (Exception error)
            {
                if (MaskArr(x, y) == 0)
                {
                    qrArray[y][x] = '1';
                }
                if (MaskArr(x - 1, y) == 0)
                {
                    qrArray[y][x - 1] = '1';
                }
            }
        }
        private static void FillLast(string value, int y, ref int i)
        {
            try
            {
                if (qrArray[y][0] == '0')
                {
                    qrArray[y][0] = value[i];
                    if (MaskArr(0, y) == 0)
                    {
                        qrArray[y][0] = (value[i] == '1' ? '0' : '1');
                    }
                    else
                    {
                        qrArray[y][0] = value[i];
                    }
                    i++;
                }
            }
            catch (Exception error)
            {
                if (MaskArr(0, y) == 0)
                {
                    qrArray[y][0] = '1';
                }
            }
        }
        private static void FillColumn(Direction dir, string value, int x, ref int i)
        {
            if (dir == Direction.Up)
            {
                for (int y = Size - 1; y >= 0; y--)
                {
                    FillLocal(value, x, y, ref i);
                }
            }
            else if (dir == Direction.Down)
            {
                for (int y = 0; y < Size; y++)
                {
                    FillLocal(value, x, y, ref i);
                }
            }
        }
        private static void FillData(string value)
        {
            int i = 0;
            for (int x = Size - 1; x > 0; x -= 4)
            {
                FillColumn(Direction.Up, value, x, ref i);
                FillColumn(Direction.Down, value, x - 2, ref i);

            }
            for (int y = Size - 1; y >= 0; y--)
            {
                FillLast(value, y, ref i);
            }
        }
        private static void FillSyncPatterns()
        {
            for (int x = 6, y = 6; x < Size - 6; x++, y++)
            {
                if (x % 2 == 0)
                {
                    qrArray[x][6] = '3';
                    qrArray[6][y] = '3';
                }
                else
                {
                    qrArray[x][6] = '2';
                    qrArray[6][y] = '2';
                }

            }
        }
        private static void FillMask(int correctionLevel)
        {
            string str = QRProperties.Mask[correctionLevel][0];
            Console.WriteLine(str);
            int m = 0;
            for (int x = 0; x < 8; x++, m++)
            {
                if (x == 6)
                {
                    m--;
                    continue;
                }
                if (str[m] == '1')
                {
                    qrArray[8][x] = '3';
                }
                else
                {
                    qrArray[8][x] = '2';
                }
            }
            for (int x = 8; x >= 0; x--, m++)
            {
                if (x == 6)
                {
                    m--;
                    continue;
                }
                if (str[m] == '1')
                {
                    qrArray[x][8] = '3';
                }
                else
                {
                    qrArray[x][8] = '2';
                }
            }
            m = 0;
            for (int x = Size - 1; x > Size - 8; x--, m++)
            {
                if (str[m] == '1')
                {
                    qrArray[x][8] = '3';
                }
                else
                {
                    qrArray[x][8] = '2';
                }
            }
            qrArray[Size - 8][8] = '3';
            for (int x = Size - 8; x < Size; x++, m++)
            {
                if (str[m] == '1')
                {
                    qrArray[8][x] = '3';
                }
                else
                {
                    qrArray[8][x] = '2';
                }
            }
        }
        private static void FillVersion()
        {
            if (Version >= 6)
            {
                string version = "";
                for (int i = 0; version != "" || i < QRProperties.VersionCode.Count(); i++)
                {
                    if (Convert.ToInt32(QRProperties.VersionCode[i][0]) == Version + 1)
                    {
                        version = QRProperties.VersionCode[i][1];
                        break;
                    }
                }
                for (int i = Size - 11, k = 0; i < Size - 8; i++)
                {
                    for (int j = 0; j < 6; j++, k++)
                    {
                        if (version[k] == '1')
                        {
                            qrArray[i][j] = '7';
                            qrArray[j][i] = '7';
                        }
                        else
                        {
                            qrArray[i][j] = '8';
                            qrArray[j][i] = '8';
                        }
                    }
                }
            }
        }
        private static int MaskArr(int x, int y)
        {
            return (x + y) % 2;
        }

        public static char[][] ToArray(int correctionLevel, string text)
        {
            InitializeArray();
            FillSearchPatterns(); //поисковые узоры
            FillAlignmentPatterns(); //выравнивающие узоры
            FillSyncPatterns(); //полосы синхронизации
            FillVersion(); //код версии
            FillMask(correctionLevel); //маска
            FillData(text); // данные
            return qrArray;
        }
    }
}
