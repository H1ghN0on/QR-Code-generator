using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Linq;
using System.IO;
using System.Threading;
using System.Windows.Forms;
namespace Холст_для_QR
{
    public partial class QrForm : Form
    {
        List<SettingButton> buttonList = new List<SettingButton>();
        SettingButton activeButton;
        QRCode code;
        public string Text { get; set; }
        Color foreColor = Color.LightGray, backColor = Color.White;
        public char[][] qr;
        public Image logo;
        private int Size { get; set; }
        private int Version { get; set; }
        private string ErrorMessage { get; set; }

        public QrForm()
        {

            InitializeComponent();
            buttonList.Add(contentButton);
            buttonList.Add(colorButton);
            buttonList.Add(logoButton);
            this.Controls.Remove(uploadPanel);
            this.Controls.Remove(contentPanel);
            this.Controls.Remove(colorPanel);
            this.contentInput.Text = "SampleText";
            Draw();
            this.contentInput.Text = "";
            this.printButton.Visible = false;
            this.saveButton.Visible = false;
            this.foreColor = Color.Black;
            foreach (SettingButton btn in buttonList)
            {
                btn.BringToFront();
            }
            this.errorText.Text = "";
            this.dateUpdateTimer.Start();
            this.dateUpdateTimer.Tick += new EventHandler(UpdateDateEverySecond);
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
                int[] pattern = QRProperties.AlignmentPattern[Version - 1];
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
            }
        }
        public void FillVersion()
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
            return (x + y) % 2;
        }
        public int MaskArr(int x, int y)
        {
            return (x + y) % 2;
        }
        public void FillBackground(Pen pen, int x1, int y1, int width)
        {
            Graphics f = this.CreateGraphics();
            f.DrawLine(pen, x1 - pen.Width, y1 - pen.Width / 2, x1 + width + pen.Width, y1 - pen.Width / 2);
            f.DrawLine(pen, x1 - pen.Width / 2F, y1, x1 - pen.Width / 2F, y1 + width);
            f.DrawLine(pen, x1 + width + pen.Width / 2, y1 + width + pen.Width, x1 + width + pen.Width / 2, y1);
            f.DrawLine(pen, x1 + width + pen.Width / 2, y1 + width + pen.Width / 2, x1 - pen.Width, y1 + width + pen.Width / 2);
        }
        public void Draw()
        {
            this.qrPanel.Controls.Add(this.logoCanvas);
            string anecToCode = "Студент сдаёт экзамен по философии. Профессор спрашивает: - Что вы знаете по теме? - Я отвечу словами Сократа, я знаю что я ничего не знаю. - Интересная мысль. Но мне ближе философия Диогена, - парировал профессор. А потом залез в бочку и начал яростно дрочить";
            string songToCode = "If we can be completely simulated Do we need a real reality? Don't let words die, let love run dry Like what we did to the rivers we killed off in our near future Ah - ah - ah And mumble some stupid stuff Like I saw it coming Pretend it's not happening Us losers do nothing so winners keep winning [Verse 3] Sit Fetch your leash Dictated economy Show me Your belly Forgotten ecology Stay Okay, eat Human psychology G00dboi Here's a treat Hungry for energy[Verse 4] We are searching Following our human instincts Looking for ghosts of the non - existing kind Who make us whole from the very beginning We keep chasing Dreaming about the perfect being Perfect parents who are non - existing Our bodies grew, our minds stayed the same[Bridge] Now darling, where do we go from here? Now darling, where do we go from here? Now darling, where do we go from here? Darling, darling Hey honey, where do we go from here? Hey honey, where do we go from here? Now darling, where do we go from here? Now darling, where do we go from here? To where?";
            string stringToCode = "KAIDOKAIDOKAIDOKAIDOKAIDOKAIDOKAIDOKAIDOKAIDOKAIDOKAIDOKAIDOH";
            string theBiggest = "Артас пидорас Артас пидорас Артас пидорас Артас пидорас Артас пидорас Артас пидорас  Артас пидорас Артас пидорас Артас пидорас Артас пидорас Артас пидорас Артас пидорас Артас пидорас Артас пидорас Артас пидорас Артас пидорас Артас пидорас Артас пидорас Артас пидорас Артас пидорас Артас пидорас Артас пидорас Артас пидорас Артас пидорас Артас пидорас Артас пидорас Артас пидорас Артас пидорас Артас пидорас Артас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорасАртас пидорас";
            int correctionLevel = 1;
            string formName = chooseFormPanel.Controls.OfType<RadioButton>().Select((rb, i) => new { rb, i }).Single(z => z.rb.Checked).rb.Name;
            if (logo != null)
            {
                this.logoCanvas.Visible = true;
                correctionLevel = 3;
            } else
            {
                this.logoCanvas.Visible = false;
            }
            if (formName != "radioSquareForm") {
                correctionLevel = 3;
            }
            code = QREncode.Encode(contentInput.Text, correctionLevel);
            if (code == null)
            {
                this.errorText.Text = "Слишком много символов";
                return;
            }
            Size = QRProperties.FieldSize[code.Version];
            Text = contentInput.Text;
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
            float size = Convert.ToSingle(qrCanvas.Width) / Convert.ToSingle(QRProperties.FieldSize[code.Version]);
            Console.WriteLine($"Размер: {size}");
            FillSearchPatterns();
            FillAlignmentPatterns();
            FillSyncPatterns();
            FillVersion();
            FillMask(correctionLevel);
            FillData(code.Code);
            qrCanvas.Image = new Bitmap(qrCanvas.Width, qrCanvas.Height);
            Graphics g = Graphics.FromImage(qrCanvas.Image);
            Brush foreBrush = new SolidBrush(foreColor);
            Brush backBrush = new SolidBrush(backColor);
            this.qrPanel.BackColor = backColor;
            
            for (int i = 0; i < QRProperties.FieldSize[code.Version]; i++) {
                for (int j = 0; j < QRProperties.FieldSize[code.Version]; j++)
                {
                    switch (formName)
                    {
                        case "radioSquareForm":
                            {
                                if (qr[j][i] == '1' || qr[j][i] == '3' || qr[j][i] == '6' || qr[j][i] == '7')
                                {
                                    g.FillRectangle(foreBrush, i * size, j * size, size, size); 
                                }
                                else
                                {
                                    g.FillRectangle(backBrush, i * size, j * size, size, size);
                                }
                                break;
                            }
                        case "radioCircleForm":
                            {
                                if (qr[j][i] == '1' || qr[j][i] == '3' || qr[j][i] == '6' || qr[j][i] == '7')
                                {
                                    g.FillEllipse(foreBrush, i * size, j * size, size, size); 
                                }
                                else
                                {
                                    g.FillEllipse(backBrush, i * size, j * size, size, size);
                                }
                                break;
                            }
                    }
                    

                }
            }

            if (logo != null)
            {
                this.logoCanvas.Width = 0;
                this.logoCanvas.Height = 0;
                float width = code.Code.Length;
                width *= 0.1F;
                float mem = Convert.ToSingle(Math.Floor(Math.Sqrt(width)));
                for (int i = 0; i < mem; i++)
                {
                    this.logoCanvas.Width += Convert.ToInt32(size);
                    this.logoCanvas.Height += Convert.ToInt32(size);
                }
                if (this.checkBorderRound.Checked)
                {
                    System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                    path.AddEllipse(0, 0, this.logoCanvas.Width, this.logoCanvas.Height);
                    Region rgn = new Region(path);
                    this.logoCanvas.Region = rgn;
                } else
                {
                    this.logoCanvas.Region = null;
                }
                this.logoCanvas.Image = logo;
                this.logoCanvas.Left = this.qrCanvas.Location.X + this.qrCanvas.Width / 2 - this.logoCanvas.Width / 2;
                this.logoCanvas.Top = this.qrCanvas.Location.Y + this.qrCanvas.Height / 2 - this.logoCanvas.Height / 2;
                this.logoCanvas.BringToFront();
            }
            this.saveButton.Visible = true;
            this.printButton.Visible = true;
        }
        private string CheckExtension(string name, string[] exts)
        {
            string[] filename = name.Split('.');
            string extension = filename[filename.Length - 1];
            if (!exts.Contains(extension))
            {
                string extensionOutput = "";
                foreach (string ext in exts)
                {
                    extensionOutput += $"'{ext}', ";
                }
                extensionOutput = extensionOutput.Substring(0, extensionOutput.Length - 2);
                return $"Поддерживаются только файлы формата: {extensionOutput} !";
            } else
            {
                return "";
            }
        }
        private void DeactivateButton()
        {
            this.Controls.Remove(activeButton.Panel);
            for (int i = activeButton.Order + 1; i < buttonList.Count; i++)
            {
                buttonList[i].Location = new Point(buttonList[i].Location.X, buttonList[i].Location.Y - 150);
            }
            activeButton.Active = false;
            activeButton = null;
        }
        
        private void HandleSettingButtonClick(object sender, EventArgs e)
        {
            var clickedButton = sender as SettingButton;
            int order = clickedButton.Order;
            if (activeButton == clickedButton)
            {
                DeactivateButton();
                return;
            } 
            if (activeButton != null)
            {
                DeactivateButton();
            }
            clickedButton.Active = !clickedButton.Active;
            activeButton = clickedButton;
            this.Controls.Add(clickedButton.Panel);
            for (int i = order + 1; i < buttonList.Count; i++)
            {
                buttonList[i].Location = new Point(buttonList[i].Location.X, buttonList[i].Location.Y + 150);
            }
        }

        private void HandleCloseButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HandleContentButtonClick(object sender, EventArgs e)
        {


        }
        private void HandleColorButtonClick(object sender, EventArgs e)
        {

        }

        private void HandleLogoButtonClick(object sender, EventArgs e)
        {

        }

        private void HandleForecolorChooseButtonClick(object sender, EventArgs e)
        {
            if (this.colorChange.ShowDialog() != DialogResult.Cancel)
            {
                if (this.colorChange.Color == this.backColor)
                {
                    this.errorText.Text = "Нельзя выбрать одинаковые цвета для фона и переднего плана!";
                    return;
                }
                this.foregroundColorButton.BackColor = this.colorChange.Color;
                if (this.colorChange.Color == Color.White)
                {
                    this.foregroundColorButton.ForeColor = Color.Black;
                }
                else
                {
                    this.foregroundColorButton.ForeColor = this.colorChange.Color;
                }
                foreColor = this.colorChange.Color;
            }
        }

        private void HandleBackcolorChooseButtonClick(object sender, EventArgs e)
        {
            if (this.colorChange.ShowDialog() != DialogResult.Cancel)
            {
                if (this.colorChange.Color == this.foreColor)
                {
                    this.errorText.Text = "Нельзя выбрать одинаковые цвета для фона и переднего плана!";
                    return;
                }
                this.backgroundColorButton.BackColor = this.colorChange.Color;
                if (this.colorChange.Color == Color.White)
                {
                    this.backgroundColorButton.ForeColor = Color.Black;
                } else
                {
                    this.backgroundColorButton.ForeColor = this.colorChange.Color;
                }
                backColor = this.colorChange.Color;
            }
        }

        private void HandleFileUploadButtonClick(object sender, EventArgs e)
        {
            if (this.fileUpload.ShowDialog() != DialogResult.Cancel)
            {
                string[] extensions = { "png", "jpg", "bmp" };
                string possibleError = CheckExtension(this.fileUpload.FileName, extensions);
                if (possibleError == "")
                {
                    this.logoUploadButton.Text = this.fileUpload.FileName;
                    logo = Image.FromFile(this.fileUpload.FileName);
                    this.errorText.Text = "";
                }
                else
                {
                    this.errorText.Text = possibleError;
                    return;
                }
                
            }
        }

        private void HandleContentInputChange(object sender, EventArgs e)
        {
          
        }

        private void HandleDeleteLogoButtonClick(object sender, EventArgs e)
        {
            this.logoUploadButton.Text = "Файл не загружен";
            logo = null;
        }

        private void HandleSaveButtonClick(object sender, EventArgs e)
        {
            if (qrSave.ShowDialog() == DialogResult.OK) //если в диалоговом окне нажата кнопка "ОК"
            {
                try
                {
                    Console.WriteLine(logoCanvas.Image);
                    for (int i = 0; i < qrPanel.Controls.Count; i++)
                    {
                        Console.WriteLine(qrPanel.Controls[i].Name);
                    }
                    Bitmap bmp = new Bitmap(qrPanel.ClientSize.Width, qrPanel.ClientSize.Height);
                    using (Graphics G = Graphics.FromImage(bmp))
                    {
                        this.qrCanvas.BringToFront();
                        qrPanel.DrawToBitmap(bmp, qrPanel.ClientRectangle);
                        if (this.checkBorderRound.Checked)
                        {
                            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                            path.AddEllipse(0, 0, this.logoCanvas.Width, this.logoCanvas.Height);
                            Region rgn = new Region(path);
                            this.logoCanvas.Region = rgn;   
                        }
                        else
                        {
                            this.logoCanvas.Region = null;
              
                        }
                        this.logoCanvas.BringToFront();
                        
                    }

                    // now we can save it..
                    bmp.Save(qrSave.FileName, ImageFormat.Png);
                    // and let it go:
                    bmp.Dispose();
                }
                catch
                {
                    MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
           
        }
        private void PrintPage(object o, PrintPageEventArgs e)
        {
 
            Bitmap bmp = new Bitmap(qrPanel.ClientSize.Width, qrPanel.ClientSize.Height);
            Graphics gr = Graphics.FromImage(bmp);
            this.qrCanvas.BringToFront();
            
            qrPanel.DrawToBitmap(bmp, qrPanel.ClientRectangle);
            this.logoCanvas.BringToFront();
/*            string text = $"Исходный текст: {Text}";
            gr.DrawString(text, new Font("Comic Sans", 14), new SolidBrush(Color.Purple), 25f, Convert.ToSingle(375));*/
            e.Graphics.DrawImage(bmp, 0, 0);
            bmp.Dispose(); 
        }
        private void HandlePrintButtonClick(object sender, EventArgs e)
        {
            using (PrintDocument document = new PrintDocument())
            {
                document.PrintPage += PrintPage;

                qrPrint.Document = document;
                qrPrintPreview.Document = document;
                if (qrPrintPreview.ShowDialog() == DialogResult.OK)
                {
                    if (qrPrint.ShowDialog() == DialogResult.OK) document.Print();
                }       
            }
        }

        private void HandleTextFileUploadButtonClick(object sender, EventArgs e)
        {
            if (this.fileUpload.ShowDialog() != DialogResult.Cancel)
            {
                string[] extensions = { "txt" };
                string possibleError = CheckExtension(this.fileUpload.FileName, extensions);
                if (possibleError == "")
                {
                    this.textFileUploadButton.Text = this.fileUpload.FileName;
                    try
                    {
                        using (StreamReader sr = new StreamReader(fileUpload.FileName))
                        {
                            Text = contentInput.Text = sr.ReadToEnd();
                        }
                        this.errorText.Text = "";
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        this.errorText.Text = $"Непредвиденная ошибка: {ex}";
                    }
                } else
                {
                    this.errorText.Text = possibleError;
                    return;
                }
            }
        }

        private void HandleDeleteTextFileButtonClick(object sender, EventArgs e)
        {
            contentInput.Text = "";
        }

        private void HandleDateAppendButtonClick(object sender, EventArgs e)
        {
            contentInput.Text += DateTime.Now;
        }

        private void HandleSocialClick(object sender, EventArgs e)
        {
            string text = this.contentInput.Text;
            switch ((sender as PictureBox).Name)
            {
                case "vk":
                    {
                        this.contentInput.Text = $"https://vk.com/{text}";
                        logo = Image.FromFile(@"D:\Code\Проекты\QR-Code generator\Resources\formImages\vk-big.png");
                        break;
                    }
                case "inst":
                    {
                        this.contentInput.Text = $"https://www.instagram.com/{text}";
                        logo = Image.FromFile(@"D:\Code\Проекты\QR-Code generator\Resources\formImages\instagram-big.png");
                        break;
                    }
                case "fb":
                    {
                        this.contentInput.Text = $"https://www.facebook.com/{text}";
                        logo = Image.FromFile(@"D:\Code\Проекты\QR-Code generator\Resources\formImages\facebook-big.png");
                        break;
                    }
                case "ok":
                    {
                        this.contentInput.Text = $"https://ok.ru/profile/{text}";
                        logo = Image.FromFile(@"D:\Code\Проекты\QR-Code generator\Resources\formImages\ok-big.png");
                        break;
                    }
               
            }
            this.contentInput.SelectionStart = this.contentInput.Text.Length;
        }

        private void HandleSubmitClick(object sender, EventArgs e)
        {
            if (this.contentInput.Text == "")
            {
                this.errorText.Text = "Введите текст!";
            } else
            {
                this.errorText.Text = "";
                Draw();
            }
        }

        private void UpdateDateEverySecond(object sender, EventArgs e)
        {
            this.dateAppendButton.Text = $"Добавить текущую дату: {DateTime.Now}";
        }
        
    }
}
