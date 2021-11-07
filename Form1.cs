using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Холст_для_QR
{
    public partial class Form1 : Form
    {
        public char[][] qr;
        private int Size { get; set; } 
        private int Version { get; set; }
        public Form1()
        {
            InitializeComponent();
            Draw();

        }

        public void FillSearchPatterns()
        {
            for (int i = 0; i < 7; i++)
            {
                qr[i][0] = '3';
                qr[0][i] = '3';
                qr[6][i] = '3';
                qr[i][6] = '3';
            }
            for (int i = 0; i < 7; i++)
            {
                qr[Size - 7][i] = '3';
                qr[Size - 1][i] = '3';
                qr[i][Size - 7] = '3';
                qr[i][Size - 1] = '3';
            }
            for (int i = 0; i < 7; i++)
            {
                qr[7][i] = '4';
                qr[i][7] = '4';
            }
            for (int i = 0; i < 7; i++)
            {
                qr[Size - 8][i] = '4'; 
                qr[i][Size - 8] = '4';
            }
            qr[7][7] = '4';
            qr[Size - 8][7] = '4';
            qr[7][Size - 8] = '4';
            for (int i = Size - 7; i < Size; i++)
            {
                qr[i][0] = '3';
                qr[i][6] = '3';
                qr[6][i] = '3';
                qr[0][i] = '3';
            }
            for (int i = Size - 8; i < Size; i++)
            {
                qr[i][7] = '4';
                qr[7][i] = '4';
            }

            for (int i = 1; i < 6; i++)
            {
                qr[i][1] = '2';
                qr[1][i] = '2';
                qr[5][i] = '2';
                qr[i][5] = '2';
            }
            for (int i = 1; i < 6; i++)
            {
                qr[Size - 6][i] = '2';
                qr[Size - 2][i] = '2';
                qr[i][Size - 6] = '2';
                qr[i][Size - 2] = '2';
            }
            for (int i = Size - 6; i < Size - 1; i++)
            {
                qr[i][1] = '2';
                qr[i][5] = '2';
                qr[5][i] = '2';
                qr[1][i] = '2';
            }




            for (int i = 2; i < 5; i++)
            {
                for (int j = 2; j < 5; j++)
                {
                    qr[i][j] = '3';
                }
            }
            for (int i = Size - 5; i < Size - 2; i++)
            {
                for (int j = 2; j < 5; j++)
                {
                    qr[i][j] = '3';
                    qr[j][i] = '3';
                }
            }
        }
        public void FillAlignmentPatterns()
        {
            if (Version > 0)
            {
                int xPoint, yPoint;
                int []pattern = QRProperties.AlignmentPattern[Version - 1];
                for (int i = 0; i < pattern.Length; i++)
                {
                    for (int j = 0; j < pattern.Length; j++)
                    {
                        if (CheckExistanceOfAlignmentPattern(pattern[i], pattern[j]))
                        {
                            xPoint = pattern[i];
                            yPoint = pattern[j];
                            qr[xPoint][yPoint] = '6';
                            for (int x = xPoint - 1; x <= xPoint + 1; x++)
                            {
                                qr[x][yPoint + 1] = '5';
                                qr[x][yPoint - 1] = '5';
                            }
                            qr[xPoint + 1][yPoint] = '5';
                            qr[xPoint - 1][yPoint] = '5';
                            for (int x = xPoint - 2; x <= xPoint + 2; x++)
                            {
                                qr[x][yPoint + 2] = '6';
                                qr[x][yPoint - 2] = '6';
                            }
                            for (int y = yPoint - 2; y <= yPoint + 2; y++)
                            {
                                qr[xPoint + 2][y] = '6';
                                qr[xPoint - 2][y] = '6';
                            }
                        }
                    }
                }
            }
        }
        private bool CheckExistanceOfAlignmentPattern(int xPoint, int yPoint)
        {
            for (int x = xPoint - 2; x < xPoint + 2; x++)
            {
                for (int y = yPoint - 2; y < yPoint + 2; y++)
                {
                    if (qr[x][y] == '3' || qr[x][y] == '2')
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public void FillData(string value)
        {
            Console.WriteLine($"Version: {Version} ");
            int i = 0;
            bool down = false;
            for (int x = Size - 1; x > 0; x -= 2)
            {
                if (!down)
                {
                    for (int y = Size - 1; y >= 0; y--)
                    {
                        try
                        {
                            if (qr[y][x] == '0')
                            {
                                
                                if (MaskArr(x, y) == 0)
                                {
                                    qr[y][x] = (value[i] == '1' ? '0' : '1');
                                } else 
                                {
                                    qr[y][x] = value[i];
                                }
                                i++;
                            }
                            if (qr[y][x - 1] == '0')
                            {
                                qr[y][x - 1] = value[i + 1];

                                if (MaskArr(x - 1, y) == 0)
                                {
                                    qr[y][x - 1] = (value[i] == '1' ? '0' : '1');
                                }
                                else
                                {
                                    qr[y][x - 1] = value[i];
                                }
                                i++;
                            }

                        }
                        catch (Exception error)
                        {
                            if (MaskArr(x, y) == 0)
                            {
                                qr[y][x] = '1';
                            }
                            if (MaskArr(x - 1, y) == 0)
                            {
                                qr[y][x - 1] = '1';
                            }
                        }
                        down = true;
                    }
                }
                else
                {
                    for (int y = 0; y < Size; y++)
                    {
                        try
                        {
                            if (qr[y][x] == '0')
                            {
                                qr[y][x] = value[i];
                                if (MaskArr(x, y) == 0)
                                {
                                    qr[y][x] = (value[i] == '1' ? '0' : '1');
                                }
                                i++;
                            }
                            if (qr[y][x - 1] == '0')
                            {
                                qr[y][x - 1] = value[i];

                                if (MaskArr(x - 1, y) == 0)
                                {
                                    qr[y][x - 1] = (value[i] == '1' ? '0' : '1');
                                }
                                i++;
                            }
                        }
                        catch (Exception error)
                        {
                            if (MaskArr(x, y) == 0)
                            {
                                qr[y][x] = '1';
                            }
                            if (MaskArr(x - 1, y) == 0)
                            {
                                qr[y][x - 1] = '1';
                            }
                        }
                        down = false;
                    }
                }
            }
            if (!down)
            {
                for (int y = Size - 1; y >= 0; y--)
                {
                    try
                    {
                        if (qr[y][0] == '0')
                        {

                            if (MaskArr(0, y) == 0)
                            {
                                qr[y][0] = (value[i] == '1' ? '0' : '1');
                            }
                            else
                            {
                                qr[y][0] = value[i];
                            }
                            i++;
                        }
                    }
                    catch (Exception error)
                    {
                        if (MaskArr(0, y) == 0)
                        {
                            qr[y][0] = '1';
                        }
                    }
                    down = true;
                }
            }
            else
            {
                for (int y = 0; y < Size; y++)
                {
                    try
                    {
                        if (qr[y][0] == '0')
                        {
                            qr[y][0] = value[i];
                            if (MaskArr(0, y) == 0)
                            {
                                qr[y][0] = (value[i] == '1' ? '0' : '1');
                            }
                            else
                            {
                                qr[y][0] = value[i];
                            }
                            i++;
                        }
                    }
                    catch (Exception error)
                    {
                        if (MaskArr(0, y) == 0)
                        {
                            qr[y][0] = '1';
                        }
                    }
                    down = false;
                }
            }
        }
        public void FillSyncPatterns()
        {
            for (int x = 6, y = 6; x < Size - 6; x++, y++)
            {
                if (x % 2 == 0)
                {
                    qr[x][6] = '3';
                    qr[6][y] = '3';
                } else
                {
                    qr[x][6] = '2';
                    qr[6][y] = '2';
                }

            }
        }
        public void FillMask(int correctionLevel)
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
                    qr[8][x] = '3';
                }
                else
                {
                    qr[8][x] = '2';
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
                    qr[x][8] = '3';
                }
                else
                {
                    qr[x][8] = '2';
                }
                Console.WriteLine(m);
            }
            m = 0;
            for (int x = Size - 1; x > Size - 8; x--, m++)
            {
                if (str[m] == '1')
                {
                    qr[x][8] = '3';
                }
                else
                {
                    qr[x][8] = '2';
                }
            }
            qr[Size - 8][8] = '3';
            for (int x = Size - 8; x < Size; x++, m++)
            {
                if (str[m] == '1')
                {
                    qr[8][x] = '3';
                }
                else
                {
                    qr[8][x] = '2';
                }
                Console.WriteLine(m);
            }
        }

        public void FillVersion()
        {

            if (Version >= 7 - 1)
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
                            qr[i][j] = '7';
                            qr[j][i] = '7';
                        } else
                        {
                            qr[i][j] = '8';
                            qr[j][i] = '8';
                        }
                    }
                }
            }
        }

        public int Mask(int x, int y)
        {
            return (x / 10+ y/10) % 2;
        }
        public int MaskArr(int x, int y)
        {
            return (x + y) % 2;
        }
        public void Draw()
        {
            string stringToCode = "If we can be completely simulated Do we need a real reality? Don't let words die, let love run dry Like what we did to the rivers we killed off in our near future Ah - ah - ah And mumble some stupid stuff Like I saw it coming Pretend it's not happening Us losers do nothing so winners keep winning [Verse 3] Sit Fetch your leash Dictated economy Show me Your belly Forgotten ecology Stay Okay, eat Human psychology G00dboi Here's a treat Hungry for energy[Verse 4] We are searching Following our human instincts Looking for ghosts of the non - existing kind Who make us whole from the very beginning We keep chasing Dreaming about the perfect being Perfect parents who are non - existing Our bodies grew, our minds stayed the same[Bridge] Now darling, where do we go from here? Now darling, where do we go from here? Now darling, where do we go from here? Darling, darling Hey honey, where do we go from here? Hey honey, where do we go from here? Now darling, where do we go from here? Now darling, where do we go from here? To where? ";
            int correctionLevel = 3; 
            QRCode code = QREncode.Encode(stringToCode, correctionLevel);
            Size = QRProperties.FieldSize[code.Version];
            Version = code.Version;
            Console.WriteLine($"\n{code.Code.Length}");
            qr = new char[Size][];
            for (int i = 0; i < Size; i++)
            {
                qr[i] = new char[Size];
            }
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    qr[i][j] = '0';
                }
            }

            FillSearchPatterns();
            FillAlignmentPatterns();
            FillSyncPatterns();
            FillVersion();
            FillMask(correctionLevel);
            FillData(code.Code);

            Console.WriteLine(QRProperties.FieldSize[code.Version]);
            for (int i = 0; i < QRProperties.FieldSize[code.Version]; i++)
            {
                for (int j = 0; j < QRProperties.FieldSize[code.Version]; j++)
                {
                    Console.Write("{0}", qr[i][j]);
                }
                Console.WriteLine();
            }
            for (int i = 0; i < Size; i++)
            {
                
                dataGridView1.Columns.Add("", "");
                dataGridView1.Columns[i].Width = 4;
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Height = 4;
            }

            for (int i = 0; i < QRProperties.FieldSize[code.Version]; i++)
            {
                for (int j = 0; j < QRProperties.FieldSize[code.Version]; j++)
                {
                    if (qr[i][j] == '1')
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Black;
                    }
                    else if (qr[i][j] == '2')
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                    }
                    else if (qr[i][j] == '3')
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Black;
                    }
                    else if (qr[i][j] == '4')
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                    }
                    else if (qr[i][j] == '5')
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                    }
                    else if (qr[i][j] == '6')
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Black;
                    }
                    else if (qr[i][j] == '7')
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Black;
                    }
                    else if (qr[i][j] == '8')
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                    }

                    /*                    if (qr[i][j] == '1')
                                        {
                                            dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Black;
                                        }
                                        else if (qr[i][j] == '2')
                                        {
                                            dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Green;
                                        }
                                        else if (qr[i][j] == '3')
                                        {
                                            dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Purple;
                                        }
                                        else if (qr[i][j] == '4')
                                        {
                                            dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Red;
                                        }
                                        else if (qr[i][j] == '5')
                                        {
                                            dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                                        }
                                        else if (qr[i][j] == '6')
                                        {
                                            dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Yellow;
                                        }
                                        else if (qr[i][j] == '7')
                                        {
                                            dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Blue;
                                        }
                                        else if (qr[i][j] == '8')
                                        {
                                            dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Pink;
                                        }
                                        else
                                        {
                                            dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                                        }*/

                }
            }
        }

        private void picture_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
