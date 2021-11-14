
namespace Холст_для_QR
{
    partial class QrForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QrForm));
            this.QrCanvas = new System.Windows.Forms.PictureBox();
            this.CloseButton = new System.Windows.Forms.Label();
            this.ContentButton = new System.Windows.Forms.Button();
            this.ColorButton = new System.Windows.Forms.Button();
            this.LogoButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.QrCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // QrCanvas
            // 
            resources.ApplyResources(this.QrCanvas, "QrCanvas");
            this.QrCanvas.Name = "QrCanvas";
            this.QrCanvas.TabStop = false;
            // 
            // CloseButton
            // 
            resources.ApplyResources(this.CloseButton, "CloseButton");
            this.CloseButton.BackColor = System.Drawing.Color.Red;
            this.CloseButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CloseButton.ForeColor = System.Drawing.Color.White;
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Click += new System.EventHandler(this.HandleCloseButtonClick);
            // 
            // ContentButton
            // 
            this.ContentButton.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.ContentButton, "ContentButton");
            this.ContentButton.ForeColor = System.Drawing.Color.Black;
            this.ContentButton.Name = "ContentButton";
            this.ContentButton.UseVisualStyleBackColor = false;
            this.ContentButton.Click += new System.EventHandler(this.HandleContentButtonClick);
            // 
            // ColorButton
            // 
            this.ColorButton.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.ColorButton, "ColorButton");
            this.ColorButton.ForeColor = System.Drawing.Color.Black;
            this.ColorButton.Name = "ColorButton";
            this.ColorButton.UseVisualStyleBackColor = false;
            this.ColorButton.Click += new System.EventHandler(this.HandleColorButtonClick);
            // 
            // LogoButton
            // 
            this.LogoButton.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.LogoButton, "LogoButton");
            this.LogoButton.ForeColor = System.Drawing.Color.Black;
            this.LogoButton.Name = "LogoButton";
            this.LogoButton.UseVisualStyleBackColor = false;
            this.LogoButton.Click += new System.EventHandler(this.HandleLogoButtonClick);
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // QrForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.LogoButton);
            this.Controls.Add(this.ColorButton);
            this.Controls.Add(this.ContentButton);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.QrCanvas);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QrForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.QrCanvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox QrCanvas;
        private System.Windows.Forms.Label CloseButton;
        private System.Windows.Forms.Button ContentButton;
        private System.Windows.Forms.Button ColorButton;
        private System.Windows.Forms.Button LogoButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
    }
}

