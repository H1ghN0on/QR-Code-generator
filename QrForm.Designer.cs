
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
            this.CloseButton = new System.Windows.Forms.Label();
            this.contentInput = new System.Windows.Forms.TextBox();
            this.contentInputHint = new System.Windows.Forms.Label();
            this.colorChange = new System.Windows.Forms.ColorDialog();
            this.foregroundColorButton = new System.Windows.Forms.Button();
            this.foregroundButtonHint = new System.Windows.Forms.Label();
            this.backgroundButtonHint = new System.Windows.Forms.Label();
            this.backgroundColorButton = new System.Windows.Forms.Button();
            this.logoUpload = new System.Windows.Forms.OpenFileDialog();
            this.fileUploadButton = new System.Windows.Forms.Button();
            this.fileUploadHint = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.uploadPanel = new System.Windows.Forms.Panel();
            this.deleteLogoButton = new System.Windows.Forms.PictureBox();
            this.checkBorderRound = new System.Windows.Forms.CheckBox();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.colorPanel = new System.Windows.Forms.Panel();
            this.saveButton = new System.Windows.Forms.Button();
            this.qrSave = new System.Windows.Forms.SaveFileDialog();
            this.printButton = new System.Windows.Forms.Button();
            this.logoCanvas = new System.Windows.Forms.PictureBox();
            this.qrPanel = new System.Windows.Forms.Panel();
            this.qrCanvas = new System.Windows.Forms.PictureBox();
            this.qrPrint = new System.Windows.Forms.PrintDialog();
            this.colorButton = new Холст_для_QR.SettingButton();
            this.logoButton = new Холст_для_QR.SettingButton();
            this.contentButton = new Холст_для_QR.SettingButton();
            this.qrPrintPreview = new System.Windows.Forms.PrintPreviewDialog();
            this.uploadPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deleteLogoButton)).BeginInit();
            this.contentPanel.SuspendLayout();
            this.colorPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoCanvas)).BeginInit();
            this.qrPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qrCanvas)).BeginInit();
            this.SuspendLayout();
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
            // contentInput
            // 
            resources.ApplyResources(this.contentInput, "contentInput");
            this.contentInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.contentInput.Name = "contentInput";
            this.contentInput.TextChanged += new System.EventHandler(this.HandleContentInputChange);
            // 
            // contentInputHint
            // 
            resources.ApplyResources(this.contentInputHint, "contentInputHint");
            this.contentInputHint.Name = "contentInputHint";
            // 
            // colorChange
            // 
            this.colorChange.FullOpen = true;
            // 
            // foregroundColorButton
            // 
            this.foregroundColorButton.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.foregroundColorButton, "foregroundColorButton");
            this.foregroundColorButton.Name = "foregroundColorButton";
            this.foregroundColorButton.UseVisualStyleBackColor = false;
            this.foregroundColorButton.Click += new System.EventHandler(this.HandleForecolorChooseButtonClick);
            // 
            // foregroundButtonHint
            // 
            resources.ApplyResources(this.foregroundButtonHint, "foregroundButtonHint");
            this.foregroundButtonHint.Name = "foregroundButtonHint";
            // 
            // backgroundButtonHint
            // 
            resources.ApplyResources(this.backgroundButtonHint, "backgroundButtonHint");
            this.backgroundButtonHint.Name = "backgroundButtonHint";
            // 
            // backgroundColorButton
            // 
            this.backgroundColorButton.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.backgroundColorButton, "backgroundColorButton");
            this.backgroundColorButton.ForeColor = System.Drawing.Color.Black;
            this.backgroundColorButton.Name = "backgroundColorButton";
            this.backgroundColorButton.UseVisualStyleBackColor = false;
            this.backgroundColorButton.Click += new System.EventHandler(this.HandleBackcolorChooseButtonClick);
            // 
            // fileUploadButton
            // 
            resources.ApplyResources(this.fileUploadButton, "fileUploadButton");
            this.fileUploadButton.Name = "fileUploadButton";
            this.fileUploadButton.UseVisualStyleBackColor = true;
            this.fileUploadButton.Click += new System.EventHandler(this.HandleFileUploadButtonClick);
            // 
            // fileUploadHint
            // 
            resources.ApplyResources(this.fileUploadHint, "fileUploadHint");
            this.fileUploadHint.Name = "fileUploadHint";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.HandleSubmitClick);
            // 
            // uploadPanel
            // 
            this.uploadPanel.Controls.Add(this.deleteLogoButton);
            this.uploadPanel.Controls.Add(this.checkBorderRound);
            this.uploadPanel.Controls.Add(this.fileUploadButton);
            this.uploadPanel.Controls.Add(this.fileUploadHint);
            resources.ApplyResources(this.uploadPanel, "uploadPanel");
            this.uploadPanel.Name = "uploadPanel";
            // 
            // deleteLogoButton
            // 
            this.deleteLogoButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.deleteLogoButton.Image = global::Холст_для_QR.Properties.Resources.trash;
            resources.ApplyResources(this.deleteLogoButton, "deleteLogoButton");
            this.deleteLogoButton.Name = "deleteLogoButton";
            this.deleteLogoButton.TabStop = false;
            this.deleteLogoButton.Click += new System.EventHandler(this.HandleDeleteLogoButtonClick);
            // 
            // checkBorderRound
            // 
            resources.ApplyResources(this.checkBorderRound, "checkBorderRound");
            this.checkBorderRound.Name = "checkBorderRound";
            this.checkBorderRound.UseVisualStyleBackColor = true;
            // 
            // contentPanel
            // 
            this.contentPanel.Controls.Add(this.contentInput);
            this.contentPanel.Controls.Add(this.contentInputHint);
            resources.ApplyResources(this.contentPanel, "contentPanel");
            this.contentPanel.Name = "contentPanel";
            // 
            // colorPanel
            // 
            this.colorPanel.Controls.Add(this.foregroundColorButton);
            this.colorPanel.Controls.Add(this.backgroundButtonHint);
            this.colorPanel.Controls.Add(this.backgroundColorButton);
            this.colorPanel.Controls.Add(this.foregroundButtonHint);
            resources.ApplyResources(this.colorPanel, "colorPanel");
            this.colorPanel.Name = "colorPanel";
            // 
            // saveButton
            // 
            resources.ApplyResources(this.saveButton, "saveButton");
            this.saveButton.Name = "saveButton";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.HandleSaveButtonClick);
            // 
            // qrSave
            // 
            resources.ApplyResources(this.qrSave, "qrSave");
            // 
            // printButton
            // 
            resources.ApplyResources(this.printButton, "printButton");
            this.printButton.Name = "printButton";
            this.printButton.UseVisualStyleBackColor = true;
            this.printButton.Click += new System.EventHandler(this.HandlePrintButtonClick);
            // 
            // logoCanvas
            // 
            resources.ApplyResources(this.logoCanvas, "logoCanvas");
            this.logoCanvas.Name = "logoCanvas";
            this.logoCanvas.TabStop = false;
            // 
            // qrPanel
            // 
            this.qrPanel.AllowDrop = true;
            this.qrPanel.BackColor = System.Drawing.Color.White;
            this.qrPanel.Controls.Add(this.logoCanvas);
            this.qrPanel.Controls.Add(this.qrCanvas);
            this.qrPanel.ForeColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.qrPanel, "qrPanel");
            this.qrPanel.Name = "qrPanel";
            // 
            // qrCanvas
            // 
            this.qrCanvas.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.qrCanvas, "qrCanvas");
            this.qrCanvas.Name = "qrCanvas";
            this.qrCanvas.TabStop = false;
            // 
            // qrPrint
            // 
            this.qrPrint.UseEXDialog = true;
            // 
            // colorButton
            // 
            this.colorButton.Active = false;
            resources.ApplyResources(this.colorButton, "colorButton");
            this.colorButton.Name = "colorButton";
            this.colorButton.Order = 1;
            this.colorButton.Panel = this.colorPanel;
            this.colorButton.UseVisualStyleBackColor = true;
            this.colorButton.Click += new System.EventHandler(this.HandleSettingButtonClick);
            // 
            // logoButton
            // 
            this.logoButton.Active = false;
            resources.ApplyResources(this.logoButton, "logoButton");
            this.logoButton.Name = "logoButton";
            this.logoButton.Order = 2;
            this.logoButton.Panel = this.uploadPanel;
            this.logoButton.UseVisualStyleBackColor = true;
            this.logoButton.Click += new System.EventHandler(this.HandleSettingButtonClick);
            // 
            // contentButton
            // 
            this.contentButton.Active = false;
            resources.ApplyResources(this.contentButton, "contentButton");
            this.contentButton.Name = "contentButton";
            this.contentButton.Order = 0;
            this.contentButton.Panel = this.contentPanel;
            this.contentButton.UseVisualStyleBackColor = true;
            this.contentButton.Click += new System.EventHandler(this.HandleSettingButtonClick);
            // 
            // qrPrintPreview
            // 
            resources.ApplyResources(this.qrPrintPreview, "qrPrintPreview");
            this.qrPrintPreview.Name = "qrPrintPreview";
            // 
            // QrForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.printButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.colorButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.logoButton);
            this.Controls.Add(this.colorPanel);
            this.Controls.Add(this.uploadPanel);
            this.Controls.Add(this.contentButton);
            this.Controls.Add(this.qrPanel);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QrForm";
            this.uploadPanel.ResumeLayout(false);
            this.uploadPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deleteLogoButton)).EndInit();
            this.contentPanel.ResumeLayout(false);
            this.contentPanel.PerformLayout();
            this.colorPanel.ResumeLayout(false);
            this.colorPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoCanvas)).EndInit();
            this.qrPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.qrCanvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label CloseButton;
        private System.Windows.Forms.TextBox contentInput;
        private System.Windows.Forms.Label contentInputHint;
        private System.Windows.Forms.ColorDialog colorChange;
        private System.Windows.Forms.Button foregroundColorButton;
        private System.Windows.Forms.Label foregroundButtonHint;
        private System.Windows.Forms.Label backgroundButtonHint;
        private System.Windows.Forms.Button backgroundColorButton;
        private System.Windows.Forms.OpenFileDialog logoUpload;
        private System.Windows.Forms.Button fileUploadButton;
        private System.Windows.Forms.Label fileUploadHint;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel uploadPanel;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.Panel colorPanel;
        private SettingButton contentButton;
        private SettingButton colorButton;
        private SettingButton logoButton;
        private System.Windows.Forms.CheckBox checkBorderRound;
        private System.Windows.Forms.PictureBox deleteLogoButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.SaveFileDialog qrSave;
        private System.Windows.Forms.Button printButton;
        private System.Windows.Forms.PictureBox logoCanvas;
        private System.Windows.Forms.Panel qrPanel;
        private System.Windows.Forms.PictureBox qrCanvas;
        private System.Windows.Forms.PrintDialog qrPrint;
        private System.Windows.Forms.PrintPreviewDialog qrPrintPreview;
    }
}

