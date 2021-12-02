
namespace Холст_для_QR
{
    partial class QRForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QRForm));
            this.CloseButton = new System.Windows.Forms.Label();
            this.contentInput = new System.Windows.Forms.TextBox();
            this.contentInputHint = new System.Windows.Forms.Label();
            this.colorChange = new System.Windows.Forms.ColorDialog();
            this.foregroundColorButton = new System.Windows.Forms.Button();
            this.foregroundButtonHint = new System.Windows.Forms.Label();
            this.backgroundButtonHint = new System.Windows.Forms.Label();
            this.backgroundColorButton = new System.Windows.Forms.Button();
            this.fileUpload = new System.Windows.Forms.OpenFileDialog();
            this.logoUploadButton = new System.Windows.Forms.Button();
            this.fileUploadHint = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.uploadPanel = new System.Windows.Forms.Panel();
            this.chooseFormPanel = new System.Windows.Forms.Panel();
            this.chooseFormHint = new System.Windows.Forms.Label();
            this.radioSquareForm = new System.Windows.Forms.RadioButton();
            this.radioCircleForm = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.deleteLogoButton = new System.Windows.Forms.PictureBox();
            this.checkBorderRound = new System.Windows.Forms.CheckBox();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ok = new System.Windows.Forms.PictureBox();
            this.fb = new System.Windows.Forms.PictureBox();
            this.inst = new System.Windows.Forms.PictureBox();
            this.vk = new System.Windows.Forms.PictureBox();
            this.dateAppendButton = new System.Windows.Forms.Button();
            this.deleteTextFileButton = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textFileUploadButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.colorPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.qrSave = new System.Windows.Forms.SaveFileDialog();
            this.printButton = new System.Windows.Forms.Button();
            this.qrPanel = new System.Windows.Forms.Panel();
            this.logoCanvas = new System.Windows.Forms.PictureBox();
            this.qrCanvas = new System.Windows.Forms.PictureBox();
            this.qrPrint = new System.Windows.Forms.PrintDialog();
            this.qrPrintPreview = new System.Windows.Forms.PrintPreviewDialog();
            this.errorText = new System.Windows.Forms.Label();
            this.dateUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.contentButton = new Холст_для_QR.SettingButton();
            this.logoButton = new Холст_для_QR.SettingButton();
            this.colorButton = new Холст_для_QR.SettingButton();
            this.uploadPanel.SuspendLayout();
            this.chooseFormPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deleteLogoButton)).BeginInit();
            this.contentPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ok)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deleteTextFileButton)).BeginInit();
            this.colorPanel.SuspendLayout();
            this.qrPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoCanvas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qrCanvas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
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
            this.foregroundColorButton.Click += new System.EventHandler(this.HandleColorChooseButtonClick);
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
            this.backgroundColorButton.Click += new System.EventHandler(this.HandleColorChooseButtonClick);
            // 
            // logoUploadButton
            // 
            resources.ApplyResources(this.logoUploadButton, "logoUploadButton");
            this.logoUploadButton.Name = "logoUploadButton";
            this.logoUploadButton.UseVisualStyleBackColor = true;
            this.logoUploadButton.Click += new System.EventHandler(this.HandleFileUploadButtonClick);
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
            this.uploadPanel.Controls.Add(this.chooseFormPanel);
            this.uploadPanel.Controls.Add(this.panel2);
            resources.ApplyResources(this.uploadPanel, "uploadPanel");
            this.uploadPanel.Name = "uploadPanel";
            // 
            // chooseFormPanel
            // 
            this.chooseFormPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chooseFormPanel.Controls.Add(this.chooseFormHint);
            this.chooseFormPanel.Controls.Add(this.radioSquareForm);
            this.chooseFormPanel.Controls.Add(this.radioCircleForm);
            resources.ApplyResources(this.chooseFormPanel, "chooseFormPanel");
            this.chooseFormPanel.Name = "chooseFormPanel";
            // 
            // chooseFormHint
            // 
            resources.ApplyResources(this.chooseFormHint, "chooseFormHint");
            this.chooseFormHint.Name = "chooseFormHint";
            // 
            // radioSquareForm
            // 
            this.radioSquareForm.Checked = true;
            this.radioSquareForm.Image = global::Холст_для_QR.Properties.Resources.square_form;
            resources.ApplyResources(this.radioSquareForm, "radioSquareForm");
            this.radioSquareForm.Name = "radioSquareForm";
            this.radioSquareForm.TabStop = true;
            this.radioSquareForm.UseVisualStyleBackColor = true;
            // 
            // radioCircleForm
            // 
            this.radioCircleForm.Image = global::Холст_для_QR.Properties.Resources.circle_form;
            resources.ApplyResources(this.radioCircleForm, "radioCircleForm");
            this.radioCircleForm.Name = "radioCircleForm";
            this.radioCircleForm.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.deleteLogoButton);
            this.panel2.Controls.Add(this.logoUploadButton);
            this.panel2.Controls.Add(this.checkBorderRound);
            this.panel2.Controls.Add(this.fileUploadHint);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
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
            this.contentPanel.Controls.Add(this.label4);
            this.contentPanel.Controls.Add(this.panel1);
            this.contentPanel.Controls.Add(this.dateAppendButton);
            this.contentPanel.Controls.Add(this.deleteTextFileButton);
            this.contentPanel.Controls.Add(this.label2);
            this.contentPanel.Controls.Add(this.textFileUploadButton);
            this.contentPanel.Controls.Add(this.label1);
            this.contentPanel.Controls.Add(this.contentInput);
            this.contentPanel.Controls.Add(this.contentInputHint);
            resources.ApplyResources(this.contentPanel, "contentPanel");
            this.contentPanel.Name = "contentPanel";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Name = "label4";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.ok);
            this.panel1.Controls.Add(this.fb);
            this.panel1.Controls.Add(this.inst);
            this.panel1.Controls.Add(this.vk);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // ok
            // 
            this.ok.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ok.Image = global::Холст_для_QR.Properties.Resources.ok;
            resources.ApplyResources(this.ok, "ok");
            this.ok.Name = "ok";
            this.ok.TabStop = false;
            this.ok.Click += new System.EventHandler(this.HandleSocialClick);
            // 
            // fb
            // 
            this.fb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.fb.Image = global::Холст_для_QR.Properties.Resources.facebook;
            resources.ApplyResources(this.fb, "fb");
            this.fb.Name = "fb";
            this.fb.TabStop = false;
            this.fb.Click += new System.EventHandler(this.HandleSocialClick);
            // 
            // inst
            // 
            this.inst.Cursor = System.Windows.Forms.Cursors.Hand;
            this.inst.Image = global::Холст_для_QR.Properties.Resources.instagram;
            resources.ApplyResources(this.inst, "inst");
            this.inst.Name = "inst";
            this.inst.TabStop = false;
            this.inst.Click += new System.EventHandler(this.HandleSocialClick);
            // 
            // vk
            // 
            this.vk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.vk.Image = global::Холст_для_QR.Properties.Resources.vk;
            resources.ApplyResources(this.vk, "vk");
            this.vk.Name = "vk";
            this.vk.TabStop = false;
            this.vk.Click += new System.EventHandler(this.HandleSocialClick);
            // 
            // dateAppendButton
            // 
            resources.ApplyResources(this.dateAppendButton, "dateAppendButton");
            this.dateAppendButton.Name = "dateAppendButton";
            this.dateAppendButton.UseVisualStyleBackColor = true;
            this.dateAppendButton.Click += new System.EventHandler(this.HandleDateAppendButtonClick);
            // 
            // deleteTextFileButton
            // 
            this.deleteTextFileButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.deleteTextFileButton.Image = global::Холст_для_QR.Properties.Resources.trash;
            resources.ApplyResources(this.deleteTextFileButton, "deleteTextFileButton");
            this.deleteTextFileButton.Name = "deleteTextFileButton";
            this.deleteTextFileButton.TabStop = false;
            this.deleteTextFileButton.Click += new System.EventHandler(this.HandleDeleteTextFileButtonClick);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // textFileUploadButton
            // 
            resources.ApplyResources(this.textFileUploadButton, "textFileUploadButton");
            this.textFileUploadButton.Name = "textFileUploadButton";
            this.textFileUploadButton.UseVisualStyleBackColor = true;
            this.textFileUploadButton.Click += new System.EventHandler(this.HandleTextFileUploadButtonClick);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // colorPanel
            // 
            this.colorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.colorPanel.Controls.Add(this.foregroundColorButton);
            this.colorPanel.Controls.Add(this.backgroundButtonHint);
            this.colorPanel.Controls.Add(this.backgroundColorButton);
            this.colorPanel.Controls.Add(this.foregroundButtonHint);
            this.colorPanel.Controls.Add(this.label3);
            resources.ApplyResources(this.colorPanel, "colorPanel");
            this.colorPanel.Name = "colorPanel";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label3.Name = "label3";
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
            // logoCanvas
            // 
            resources.ApplyResources(this.logoCanvas, "logoCanvas");
            this.logoCanvas.Name = "logoCanvas";
            this.logoCanvas.TabStop = false;
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
            // qrPrintPreview
            // 
            resources.ApplyResources(this.qrPrintPreview, "qrPrintPreview");
            this.qrPrintPreview.Name = "qrPrintPreview";
            // 
            // errorText
            // 
            resources.ApplyResources(this.errorText, "errorText");
            this.errorText.ForeColor = System.Drawing.Color.Red;
            this.errorText.Name = "errorText";
            // 
            // dateUpdateTimer
            // 
            this.dateUpdateTimer.Enabled = true;
            this.dateUpdateTimer.Interval = 1000;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::Холст_для_QR.Properties.Resources.logo;
            resources.ApplyResources(this.pictureBox5, "pictureBox5");
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.TabStop = false;
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
            // QRForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.errorText);
            this.Controls.Add(this.printButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.qrPanel);
            this.Controls.Add(this.uploadPanel);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.contentButton);
            this.Controls.Add(this.colorButton);
            this.Controls.Add(this.logoButton);
            this.Controls.Add(this.colorPanel);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QRForm";
            this.uploadPanel.ResumeLayout(false);
            this.chooseFormPanel.ResumeLayout(false);
            this.chooseFormPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deleteLogoButton)).EndInit();
            this.contentPanel.ResumeLayout(false);
            this.contentPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ok)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deleteTextFileButton)).EndInit();
            this.colorPanel.ResumeLayout(false);
            this.colorPanel.PerformLayout();
            this.qrPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logoCanvas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qrCanvas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
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
        private System.Windows.Forms.OpenFileDialog fileUpload;
        private System.Windows.Forms.Button logoUploadButton;
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox deleteTextFileButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button textFileUploadButton;
        private System.Windows.Forms.Label errorText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button dateAppendButton;
        private System.Windows.Forms.Timer dateUpdateTimer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox ok;
        private System.Windows.Forms.PictureBox fb;
        private System.Windows.Forms.PictureBox inst;
        private System.Windows.Forms.PictureBox vk;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel chooseFormPanel;
        private System.Windows.Forms.Label chooseFormHint;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radioSquareForm;
        private System.Windows.Forms.RadioButton radioCircleForm;
    }
}

