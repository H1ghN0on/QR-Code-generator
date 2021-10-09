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
        public char[][] qr = new char[21][];
        public Form1()
        {
            InitializeComponent();
            Draw();

        }

        public void FillSearchPatterns()
        {
            for (int i = 0; i < 7; i++)
            {
                qr[i][0] = '1';
                qr[0][i] = '1';
                qr[6][i] = '1';
                qr[i][6] = '1';
            }
            for (int i = 0; i < 7; i++)
            {
                qr[14][i] = '1';
                qr[20][i] = '1';
                qr[i][14] = '1';
                qr[i][20] = '1';
            }
            for (int i = 14; i < 21; i++)
            {
                qr[i][0] = '1';
                qr[i][6] = '1';
                qr[6][i] = '1';
                qr[0][i] = '1';
            }


            for (int i = 2; i < 5; i++)
            {
                for (int j = 2; j < 5; j++)
                {
                    qr[i][j] = '1';
                }
            }
            for (int i = 16; i < 19; i++)
            {
                for (int j = 2; j < 5; j++)
                {
                    qr[i][j] = '1';
                    qr[j][i] = '1';
                }
            }
        }

        public void fillData(string value)
        {
            int i = 0;
            int now = 20;
            bool down = false;
            for (int x = 20; x >= 14; x -= 2)
            {
                if (!down)
                {
                    for (int y = now; y >= 9; y--)
                    { 
                        qr[y][x] = value[i];
                        qr[y][x - 1] = value[i + 1];
                        if (MaskArr(x, y) == 0)
                        {
                            qr[y][x] = (value[i] == '1' ? '0' : '1');
                        }
                        if (MaskArr(x - 1, y) == 0)
                        {
                            qr[y][x - 1] = (value[i + 1] == '1' ? '0' : '1');
                        }
                        i += 2;
                        now = y;
                        down = true;
                    }
                }
                else
                {
                    for (int y = now; y <= 20; y++)
                    {
                        qr[y][x] = value[i];
                        qr[y][x - 1] = value[i + 1];
                        if (MaskArr(x, y) == 0)
                        {
                            qr[y][x] = (value[i] == '1' ? '0' : '1');
                        }
                        if (MaskArr(x - 1, y) == 0)
                        {
                            qr[y][x - 1] = (value[i + 1] == '1' ? '0' : '1');
                        }

                        i += 2;
                        now = y;
                        down = false;
                    }
                }
            }
            now = 20;
            for (int x = 12; x >= 9; x -= 2)
            {
                if (!down)
                {
                    for (int y = 20; y >= 0; y--)
                    {
                        if (y == 6)
                        {
                            continue;
                        }
                        qr[y][x] = value[i];
                        qr[y][x - 1] = value[i + 1];
                        if (MaskArr(x, y) == 0)
                        {
                            qr[y][x] = (value[i] == '1' ? '0' : '1');
                        }
                        if (MaskArr(x - 1, y) == 0)
                        {
                            qr[y][x - 1] = (value[i + 1] == '1' ? '0' : '1');
                        }

                        i += 2;
                        now = y;
                        down = true;
                    }

                }
                else
                {
                    for (int y = 0; y <= 20; y++)
                    {
                        if (y == 6)
                        {
                            continue;
                        }
                        qr[y][x] = value[i];
                        qr[y][x - 1] = value[i + 1];
                        if (MaskArr(x, y) == 0)
                        {
                            qr[y][x] = (value[i] == '1' ? '0' : '1');
                        }
                        if (MaskArr(x - 1, y) == 0)
                        {
                            qr[y][x - 1] = (value[i + 1] == '1' ? '0' : '1');
                        }

                        i += 2;
                        now = y;
                        down = false;
                    }
                }
            }
            for (int x = 8; x >= 7; x -= 2)
            {
                if (!down)
                {
                    for (int y = 12; y >= 9; y--)
                    {
                        if (x == 6)
                        {
                            x--;
                            continue;
                        }
                        if (i >= value.Length)
                        {
                            qr[y][x] = '0';
                        }
                        else
                        {
                            qr[y][x] = value[i];
                            if (MaskArr(x, y) == 0)
                            {
                                qr[y][x] = (value[i] == '1' ? '0' : '1');
                            }
                        }
                        if (i + 1 >= value.Length)
                        {
                            qr[y][x - 1] = '0';

                        }
                        else
                        {
                            qr[y][x - 1] = value[i + 1];
                            if (MaskArr(x - 1, y) == 0)
                            {
                                qr[y][x - 1] = (value[i + 1] == '1' ? '0' : '1');
                            }

                        }
                        i += 2;
                        now = y;
                        down = true;
                    }

                }
                else
                {
                    for (int y = 9; y <= 12; y++)
                    {
                        if (x == 6)
                        {
                            continue;
                        }
                        if (i >= value.Length)
                        {
                            qr[y][x] = '0';
                        }
                        else
                        {
                            qr[y][x] = value[i];
                            if (MaskArr(x, y) == 0)
                            {
                                qr[y][x] = (value[i] == '1' ? '0' : '1');
                            }
                        }
                        if (i + 1 >= value.Length)
                        {
                            qr[y][x - 1] = '0';
                        }
                        else
                        {
                            qr[y][x - 1] = value[i + 1];
                            if (MaskArr(x - 1, y) == 0)
                            {
                                qr[y][x - 1] = (value[i + 1] == '1' ? '0' : '1');
                            }
                        }
                        i += 2;
                        now = y;
                        down = false;
                    }

                }

            }
            for (int x = 5; x >= 1; x -= 2)
            {
                if (!down)
                {
                    for (int y = 12; y >= 9; y--)
                    {

                        if (i >= value.Length)
                        {
                            qr[y][x] = '0';
                        }
                        else
                        {
                            qr[y][x] = value[i];
                            if (MaskArr(x, y) == 0)
                            {
                                qr[y][x] = (value[i] == '1' ? '0' : '1');
                            }
                        }
                        if (i + 1 >= value.Length)
                        {
                            qr[y][x - 1] = '0';
                        }
                        else
                        {
                            qr[y][x - 1] = value[i + 1];
                            if (MaskArr(x - 1, y) == 0)
                            {
                                qr[y][x - 1] = (value[i + 1] == '1' ? '0' : '1');
                            }
                        }
                        i += 2;
                        now = y;
                        down = true;
                    }

                }
                else
                {
                    for (int y = 9; y <= 12; y++)
                    {

                        if (i >= value.Length)
                        {
                            qr[y][x] = '0';
                        }
                        else
                        {
                            qr[y][x] = value[i];
                            if (MaskArr(x, y) == 0)
                            {
                                qr[y][x] = (value[i] == '1' ? '0' : '1');
                            }

                        }
                        if (i + 1 >= value.Length)
                        {
                            qr[y][x - 1] = '0';
                        }
                        else
                        {
                            qr[y][x - 1] = value[i + 1];
                            if (MaskArr(x - 1, y) == 0)
                            {
                                qr[y][x - 1] = (value[i + 1] == '1' ? '0' : '1');
                            }
                        }
                        i += 2;
                        now = y;
                        down = false;
                    }

                }
            }
            qr[8][0] = '1';
            qr[8][2] = '1';
            qr[8][4] = '1';
            qr[4][8] = '1';
            qr[1][8] = '1';
            qr[20][8] = '1';
            qr[18][8] = '1';
            qr[16][8] = '1';
            qr[13][8] = '1';
            qr[8][16] = '1';
            qr[8][19] = '1';
        }
        public void FillAlignPatterns()
        {
            for (int x = 6, y = 6; x < 21 - 3; x += 2, y += 2)
            {
                qr[x][6] = '1';
                qr[6][y] = '1';
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
            QRCode code = QREncode.Encode("Undyne", 1);
            for (int i = 0; i < 21; i++)
            {
                qr[i] = new char[21];
            }
            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < 21; j++)
                {
                    qr[i][j] = '0';
                }
            }

            FillSearchPatterns();
            FillAlignPatterns();
            fillData(code.Code);
            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < 21; j++)
                {
                    Console.Write("{0}", qr[i][j]);
                }
                Console.WriteLine();
            }


            for (int i = 0; i < 21; i++)
            {

                dataGridView1.Columns.Add("", "");
                dataGridView1.Columns[i].Width = 10;
                dataGridView1.Rows.Add();
            }

            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < 21; j++)
                {
                    if (qr[i][j] == '1')
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Black;
                    } else
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                    }
                    
                }
            }


        }

        private void picture_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
