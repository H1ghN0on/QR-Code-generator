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
    public partial class QRForm : Form
    {
        Color foreColor = Color.LightGray, backColor = Color.White;
        List<SettingButton> buttonList = new List<SettingButton>();
        SettingButton activeButton;
        QRCode code;
        public bool SocialClicked { get; set; }
        public string InputText { get; set; }
        public char[][] qr;
        public Image logo;
        public enum Direction
        {
            Up, Down
        }
        public QRForm()
        {

            InitializeComponent();
            buttonList.Add(contentButton);
            buttonList.Add(colorButton);
            buttonList.Add(logoButton);
            SetDefaultSettings();

        }


        private void SetDefaultSettings()
        {
            DrawSampleQrCode();

            this.uploadPanel.Visible = false;
            this.contentPanel.Visible = false;
            this.colorPanel.Visible = false;
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
            logo = null;
        }
        private void DrawSampleQrCode()
        {
            this.contentInput.Text = "SampleText";
            Draw();
            this.contentInput.Text = "";
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
            int correctionLevel = 1;
            string qrElementFormName = chooseFormPanel.Controls.OfType<RadioButton>().Select((rb, i) => new { rb, i }).Single(z => z.rb.Checked).rb.Name;
            if (logo != null)
            {
                this.logoCanvas.Visible = true;
                correctionLevel = 3;
            } else
            {
                this.logoCanvas.Visible = false;
            }
            if (qrElementFormName == "radioCircleForm") {
                correctionLevel = 3;
            }
            code = QREncode.Encode(contentInput.Text, correctionLevel);
            if (code == null)
            {
                this.errorText.Text = "Слишком много символов";
                return;
            }

            Text = contentInput.Text;

            float size = Convert.ToSingle(qrCanvas.Width) / Convert.ToSingle(QRProperties.FieldSize[code.Version]);
            qr = QREncode.ToArray(correctionLevel, code.Code);

            qrCanvas.Image = new Bitmap(qrCanvas.Width, qrCanvas.Height);
            Graphics g = Graphics.FromImage(qrCanvas.Image);

            Brush foreBrush = new SolidBrush(foreColor);
            Brush backBrush = new SolidBrush(backColor);
            this.qrPanel.BackColor = backColor;
            
            for (int i = 0; i < QRProperties.FieldSize[code.Version]; i++) {
                for (int j = 0; j < QRProperties.FieldSize[code.Version]; j++)
                {
                    switch (qrElementFormName)
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
                SetImageOnQrCanvas(size);
            }
            this.saveButton.Visible = true;
            this.printButton.Visible = true;
        }

        private void SetImageOnQrCanvas(float size)
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
            }
            else
            {
                this.logoCanvas.Region = null;
            }
            this.logoCanvas.Image = logo;
            this.logoCanvas.Left = this.qrCanvas.Location.X + this.qrCanvas.Width / 2 - this.logoCanvas.Width / 2;
            this.logoCanvas.Top = this.qrCanvas.Location.Y + this.qrCanvas.Height / 2 - this.logoCanvas.Height / 2;
            this.logoCanvas.BringToFront();
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
                return null;
            }
        }
        private void DeactivateButton()
        {
            this.activeButton.Panel.Visible = false;
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
            clickedButton.Panel.Visible = true; 
            for (int i = order + 1; i < buttonList.Count; i++)
            {
                buttonList[i].Location = new Point(buttonList[i].Location.X, buttonList[i].Location.Y + 150);
            }
        }
        private void HandleCloseButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }
        private void HandleColorChooseButtonClick(object sender, EventArgs e)
        {
            var clickedButton = sender as Button;
            if (this.colorChange.ShowDialog() != DialogResult.Cancel)
                
            {
                if (this.colorChange.Color == foreColor && this.colorChange.Color == backColor)
                {
                    this.errorText.Text = "Нельзя выбрать одинаковые цвета для фона и переднего плана!";
                    return;
                }
                switch (clickedButton.Name)
                {
                    case "foregroundColorButton":
                        {
                            foreColor = this.colorChange.Color;
                            break;
                        }
                    case "backgroundColorButton":
                        {
                            backColor = this.colorChange.Color;
                            break;
                        }
                }
            }
            clickedButton.BackColor = this.colorChange.Color;
            if (this.colorChange.Color == Color.White)
            {
                clickedButton.ForeColor = Color.Black;
            }
            else
            {
                clickedButton.ForeColor = this.colorChange.Color;
            }

        }
        private void HandleFileUploadButtonClick(object sender, EventArgs e)
        {
            if (this.fileUpload.ShowDialog() != DialogResult.Cancel)
            {
                string[] extensions = { "png", "jpg", "bmp" };
                string possibleError = CheckExtension(this.fileUpload.FileName, extensions);
                if (possibleError == null)
                {
                    this.logoUploadButton.Text = this.fileUpload.FileName;
                    logo = Image.FromFile(this.fileUpload.FileName);
                    this.errorText.Text = "";
                }
                else
                {
                    this.errorText.Text = possibleError;
                }
                
            }
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
                    Bitmap bmp = new Bitmap(qrPanel.ClientSize.Width, qrPanel.ClientSize.Height);
                    using (Graphics G = Graphics.FromImage(bmp))
                    {
                        this.qrCanvas.BringToFront();
                        qrPanel.DrawToBitmap(bmp, qrPanel.ClientRectangle);
                        this.logoCanvas.BringToFront();
                        
                    }
                    bmp.Save(qrSave.FileName, ImageFormat.Png);
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
            SocialClicked = true;
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
                if (SocialClicked == true)
                {
                    logo = null;
                    SocialClicked = false;
                }
            }

        }
        private void UpdateDateEverySecond(object sender, EventArgs e)
        {
            this.dateAppendButton.Text = $"Добавить текущую дату: {DateTime.Now}";
        }  
    }
}
