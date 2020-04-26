namespace ScShoAlpha
{
    partial class FormMain
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.Panel1 = new System.Windows.Forms.Panel();
            this.LabelSaveNamePreview = new System.Windows.Forms.Label();
            this.CheckBoxRegStartUp = new System.Windows.Forms.CheckBox();
            this.ButtonDefault = new System.Windows.Forms.Button();
            this.TextBoxSaveName = new System.Windows.Forms.TextBox();
            this.LabelSaveName = new System.Windows.Forms.Label();
            this.LabelSaveFormat = new System.Windows.Forms.Label();
            this.LabelAltPrtSc = new System.Windows.Forms.Label();
            this.CheckBoxWithCursor = new System.Windows.Forms.CheckBox();
            this.RadioButtonPNG = new System.Windows.Forms.RadioButton();
            this.RadioButtonJPEG = new System.Windows.Forms.RadioButton();
            this.RadioButtonBMP = new System.Windows.Forms.RadioButton();
            this.ToolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.CheckBoxSetPrint = new System.Windows.Forms.CheckBox();
            this.ButtonClose = new System.Windows.Forms.Button();
            this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemMemo = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextMenuMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemSaveDesktop = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSetPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemEditCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemWithCursor = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemRegStartUp = new System.Windows.Forms.ToolStripMenuItem();
            this.NotifyIconMain = new System.Windows.Forms.NotifyIcon(this.components);
            this.CheckBoxSaveDesktop = new System.Windows.Forms.CheckBox();
            this.CheckBoxEditCapture = new System.Windows.Forms.CheckBox();
            this.ContextMenuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Location = new System.Drawing.Point(180, -25);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(195, 260);
            this.Panel1.TabIndex = 30;
            // 
            // LabelSaveNamePreview
            // 
            this.LabelSaveNamePreview.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LabelSaveNamePreview.Location = new System.Drawing.Point(28, 112);
            this.LabelSaveNamePreview.Name = "LabelSaveNamePreview";
            this.LabelSaveNamePreview.Size = new System.Drawing.Size(400, 11);
            this.LabelSaveNamePreview.TabIndex = 28;
            this.LabelSaveNamePreview.Text = "CurrentImage_20YYMMDDHHMMSS";
            // 
            // CheckBoxRegStartUp
            // 
            this.CheckBoxRegStartUp.AutoSize = true;
            this.CheckBoxRegStartUp.Location = new System.Drawing.Point(11, 203);
            this.CheckBoxRegStartUp.Name = "CheckBoxRegStartUp";
            this.CheckBoxRegStartUp.Size = new System.Drawing.Size(117, 16);
            this.CheckBoxRegStartUp.TabIndex = 31;
            this.CheckBoxRegStartUp.Text = "スタートアップに登録";
            this.CheckBoxRegStartUp.UseVisualStyleBackColor = true;
            this.CheckBoxRegStartUp.CheckedChanged += new System.EventHandler(this.CheckBoxRegStartUp_CheckedChanged);
            // 
            // ButtonDefault
            // 
            this.ButtonDefault.Location = new System.Drawing.Point(59, 236);
            this.ButtonDefault.Name = "ButtonDefault";
            this.ButtonDefault.Size = new System.Drawing.Size(57, 23);
            this.ButtonDefault.TabIndex = 29;
            this.ButtonDefault.Text = "ﾃﾞﾌｫﾙﾄ";
            this.ButtonDefault.UseVisualStyleBackColor = true;
            this.ButtonDefault.Click += new System.EventHandler(this.ButtonDefault_Click);
            // 
            // TextBoxSaveName
            // 
            this.TextBoxSaveName.Enabled = false;
            this.TextBoxSaveName.Location = new System.Drawing.Point(30, 90);
            this.TextBoxSaveName.Name = "TextBoxSaveName";
            this.TextBoxSaveName.Size = new System.Drawing.Size(150, 19);
            this.TextBoxSaveName.TabIndex = 27;
            this.TextBoxSaveName.TextChanged += new System.EventHandler(this.TextBoxSaveName_TextChanged);
            // 
            // LabelSaveName
            // 
            this.LabelSaveName.AutoSize = true;
            this.LabelSaveName.Location = new System.Drawing.Point(27, 75);
            this.LabelSaveName.Name = "LabelSaveName";
            this.LabelSaveName.Size = new System.Drawing.Size(47, 12);
            this.LabelSaveName.TabIndex = 26;
            this.LabelSaveName.Text = "保存名：";
            // 
            // LabelSaveFormat
            // 
            this.LabelSaveFormat.AutoSize = true;
            this.LabelSaveFormat.Location = new System.Drawing.Point(27, 33);
            this.LabelSaveFormat.Name = "LabelSaveFormat";
            this.LabelSaveFormat.Size = new System.Drawing.Size(59, 12);
            this.LabelSaveFormat.TabIndex = 23;
            this.LabelSaveFormat.Text = "保存形式：";
            // 
            // LabelAltPrtSc
            // 
            this.LabelAltPrtSc.AutoSize = true;
            this.LabelAltPrtSc.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LabelAltPrtSc.Location = new System.Drawing.Point(28, 189);
            this.LabelAltPrtSc.Name = "LabelAltPrtSc";
            this.LabelAltPrtSc.Size = new System.Drawing.Size(137, 11);
            this.LabelAltPrtSc.TabIndex = 25;
            this.LabelAltPrtSc.Text = "※Alt+PrtScでは動作しません";
            // 
            // CheckBoxWithCursor
            // 
            this.CheckBoxWithCursor.AutoSize = true;
            this.CheckBoxWithCursor.Location = new System.Drawing.Point(11, 170);
            this.CheckBoxWithCursor.Name = "CheckBoxWithCursor";
            this.CheckBoxWithCursor.Size = new System.Drawing.Size(128, 16);
            this.CheckBoxWithCursor.TabIndex = 24;
            this.CheckBoxWithCursor.Text = "カーソルも同時に撮影";
            this.CheckBoxWithCursor.UseVisualStyleBackColor = true;
            this.CheckBoxWithCursor.CheckedChanged += new System.EventHandler(this.CheckBoxWithCursor_CheckedChanged);
            // 
            // RadioButtonPNG
            // 
            this.RadioButtonPNG.Appearance = System.Windows.Forms.Appearance.Button;
            this.RadioButtonPNG.Enabled = false;
            this.RadioButtonPNG.Location = new System.Drawing.Point(130, 52);
            this.RadioButtonPNG.Name = "RadioButtonPNG";
            this.RadioButtonPNG.Size = new System.Drawing.Size(50, 20);
            this.RadioButtonPNG.TabIndex = 22;
            this.RadioButtonPNG.Text = "PNG";
            this.RadioButtonPNG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioButtonPNG.CheckedChanged += new System.EventHandler(this.RadioButtonPNG_CheckedChanged);
            // 
            // RadioButtonJPEG
            // 
            this.RadioButtonJPEG.Appearance = System.Windows.Forms.Appearance.Button;
            this.RadioButtonJPEG.Enabled = false;
            this.RadioButtonJPEG.Location = new System.Drawing.Point(80, 52);
            this.RadioButtonJPEG.Name = "RadioButtonJPEG";
            this.RadioButtonJPEG.Size = new System.Drawing.Size(50, 20);
            this.RadioButtonJPEG.TabIndex = 21;
            this.RadioButtonJPEG.Text = "JPEG";
            this.RadioButtonJPEG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioButtonJPEG.CheckedChanged += new System.EventHandler(this.RadioButtonJPEG_CheckedChanged);
            // 
            // RadioButtonBMP
            // 
            this.RadioButtonBMP.Appearance = System.Windows.Forms.Appearance.Button;
            this.RadioButtonBMP.Checked = true;
            this.RadioButtonBMP.Enabled = false;
            this.RadioButtonBMP.Location = new System.Drawing.Point(30, 52);
            this.RadioButtonBMP.Name = "RadioButtonBMP";
            this.RadioButtonBMP.Size = new System.Drawing.Size(50, 20);
            this.RadioButtonBMP.TabIndex = 20;
            this.RadioButtonBMP.TabStop = true;
            this.RadioButtonBMP.Text = "BMP";
            this.RadioButtonBMP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RadioButtonBMP.CheckedChanged += new System.EventHandler(this.RadioButtonBMP_CheckedChanged);
            // 
            // ToolStripMenuItemExit
            // 
            this.ToolStripMenuItemExit.Name = "ToolStripMenuItemExit";
            this.ToolStripMenuItemExit.Size = new System.Drawing.Size(220, 22);
            this.ToolStripMenuItemExit.Text = "終了";
            this.ToolStripMenuItemExit.Click += new System.EventHandler(this.ToolStripMenuItemExit_Click);
            // 
            // CheckBoxSetPrint
            // 
            this.CheckBoxSetPrint.AutoSize = true;
            this.CheckBoxSetPrint.Location = new System.Drawing.Point(11, 126);
            this.CheckBoxSetPrint.Name = "CheckBoxSetPrint";
            this.CheckBoxSetPrint.Size = new System.Drawing.Size(135, 16);
            this.CheckBoxSetPrint.TabIndex = 19;
            this.CheckBoxSetPrint.Text = "撮影後にプリンタに出力";
            this.CheckBoxSetPrint.UseVisualStyleBackColor = true;
            this.CheckBoxSetPrint.CheckedChanged += new System.EventHandler(this.CheckBoxSetPrint_CheckedChanged);
            // 
            // ButtonClose
            // 
            this.ButtonClose.Location = new System.Drawing.Point(122, 236);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(57, 23);
            this.ButtonClose.TabIndex = 17;
            this.ButtonClose.Text = "閉じる";
            this.ButtonClose.UseVisualStyleBackColor = true;
            this.ButtonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // ToolStripSeparator3
            // 
            this.ToolStripSeparator3.Name = "ToolStripSeparator3";
            this.ToolStripSeparator3.Size = new System.Drawing.Size(217, 6);
            // 
            // ToolStripMenuItemMemo
            // 
            this.ToolStripMenuItemMemo.Name = "ToolStripMenuItemMemo";
            this.ToolStripMenuItemMemo.Size = new System.Drawing.Size(220, 22);
            this.ToolStripMenuItemMemo.Text = "メモ画面を開く";
            this.ToolStripMenuItemMemo.Click += new System.EventHandler(this.ToolStripMenuItemMemo_Click);
            // 
            // ToolStripMenuItemVersion
            // 
            this.ToolStripMenuItemVersion.Name = "ToolStripMenuItemVersion";
            this.ToolStripMenuItemVersion.Size = new System.Drawing.Size(220, 22);
            this.ToolStripMenuItemVersion.Text = "バージョン";
            this.ToolStripMenuItemVersion.Click += new System.EventHandler(this.ToolStripMenuItemVersion_Click);
            // 
            // ContextMenuMain
            // 
            this.ContextMenuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemVersion,
            this.ToolStripMenuItemMemo,
            this.ToolStripMenuItemSetting,
            this.ToolStripSeparator1,
            this.ToolStripMenuItemSaveDesktop,
            this.ToolStripMenuItemSetPrint,
            this.ToolStripMenuItemEditCapture,
            this.ToolStripMenuItemWithCursor,
            this.ToolStripSeparator2,
            this.ToolStripMenuItemRegStartUp,
            this.ToolStripSeparator3,
            this.ToolStripMenuItemExit});
            this.ContextMenuMain.Name = "ContextMenuStrip1";
            this.ContextMenuMain.Size = new System.Drawing.Size(221, 220);
            // 
            // ToolStripMenuItemSetting
            // 
            this.ToolStripMenuItemSetting.Name = "ToolStripMenuItemSetting";
            this.ToolStripMenuItemSetting.Size = new System.Drawing.Size(220, 22);
            this.ToolStripMenuItemSetting.Text = "設定";
            this.ToolStripMenuItemSetting.Click += new System.EventHandler(this.ToolStripMenuItemSetting_Click);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(217, 6);
            // 
            // ToolStripMenuItemSaveDesktop
            // 
            this.ToolStripMenuItemSaveDesktop.Name = "ToolStripMenuItemSaveDesktop";
            this.ToolStripMenuItemSaveDesktop.Size = new System.Drawing.Size(220, 22);
            this.ToolStripMenuItemSaveDesktop.Text = "　撮影後にﾃﾞｽｸﾄｯﾌﾟに保存";
            this.ToolStripMenuItemSaveDesktop.Click += new System.EventHandler(this.ToolStripMenuItemSaveDesktop_Click);
            // 
            // ToolStripMenuItemSetPrint
            // 
            this.ToolStripMenuItemSetPrint.Name = "ToolStripMenuItemSetPrint";
            this.ToolStripMenuItemSetPrint.Size = new System.Drawing.Size(220, 22);
            this.ToolStripMenuItemSetPrint.Text = "　撮影後にプリンタに出力";
            this.ToolStripMenuItemSetPrint.Click += new System.EventHandler(this.ToolStripMenuItemSetPrint_Click);
            // 
            // ToolStripMenuItemEditCapture
            // 
            this.ToolStripMenuItemEditCapture.Name = "ToolStripMenuItemEditCapture";
            this.ToolStripMenuItemEditCapture.Size = new System.Drawing.Size(220, 22);
            this.ToolStripMenuItemEditCapture.Text = "　撮影後に編集";
            this.ToolStripMenuItemEditCapture.Click += new System.EventHandler(this.ToolStripMenuItemEditCapture_Click);
            // 
            // ToolStripMenuItemWithCursor
            // 
            this.ToolStripMenuItemWithCursor.Name = "ToolStripMenuItemWithCursor";
            this.ToolStripMenuItemWithCursor.Size = new System.Drawing.Size(220, 22);
            this.ToolStripMenuItemWithCursor.Text = "　カーソルも同時に撮影";
            this.ToolStripMenuItemWithCursor.Click += new System.EventHandler(this.ToolStripMenuItemWithCursor_Click);
            // 
            // ToolStripSeparator2
            // 
            this.ToolStripSeparator2.Name = "ToolStripSeparator2";
            this.ToolStripSeparator2.Size = new System.Drawing.Size(217, 6);
            // 
            // ToolStripMenuItemRegStartUp
            // 
            this.ToolStripMenuItemRegStartUp.Name = "ToolStripMenuItemRegStartUp";
            this.ToolStripMenuItemRegStartUp.Size = new System.Drawing.Size(220, 22);
            this.ToolStripMenuItemRegStartUp.Text = "　スタートアップに登録";
            this.ToolStripMenuItemRegStartUp.Click += new System.EventHandler(this.ToolStripMenuItemRegStartUp_Click);
            // 
            // NotifyIconMain
            // 
            this.NotifyIconMain.ContextMenuStrip = this.ContextMenuMain;
            this.NotifyIconMain.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIconMain.Icon")));
            this.NotifyIconMain.Text = "ScShoAlpha";
            this.NotifyIconMain.Visible = true;
            this.NotifyIconMain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIconMain_MouseDoubleClick);
            // 
            // CheckBoxSaveDesktop
            // 
            this.CheckBoxSaveDesktop.AutoSize = true;
            this.CheckBoxSaveDesktop.Location = new System.Drawing.Point(11, 10);
            this.CheckBoxSaveDesktop.Name = "CheckBoxSaveDesktop";
            this.CheckBoxSaveDesktop.Size = new System.Drawing.Size(148, 16);
            this.CheckBoxSaveDesktop.TabIndex = 18;
            this.CheckBoxSaveDesktop.Text = "撮影後にﾃﾞｽｸﾄｯﾌﾟに保存";
            this.CheckBoxSaveDesktop.UseVisualStyleBackColor = true;
            this.CheckBoxSaveDesktop.CheckedChanged += new System.EventHandler(this.CheckBoxSaveDesktop_CheckedChanged);
            // 
            // CheckBoxEditCapture
            // 
            this.CheckBoxEditCapture.AutoSize = true;
            this.CheckBoxEditCapture.Location = new System.Drawing.Point(11, 148);
            this.CheckBoxEditCapture.Name = "CheckBoxEditCapture";
            this.CheckBoxEditCapture.Size = new System.Drawing.Size(93, 16);
            this.CheckBoxEditCapture.TabIndex = 32;
            this.CheckBoxEditCapture.Text = "撮影後に編集";
            this.CheckBoxEditCapture.UseVisualStyleBackColor = true;
            this.CheckBoxEditCapture.CheckedChanged += new System.EventHandler(this.CheckBoxEditCapture_CheckedChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(204, 272);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.LabelSaveNamePreview);
            this.Controls.Add(this.CheckBoxRegStartUp);
            this.Controls.Add(this.ButtonDefault);
            this.Controls.Add(this.TextBoxSaveName);
            this.Controls.Add(this.LabelSaveName);
            this.Controls.Add(this.LabelSaveFormat);
            this.Controls.Add(this.LabelAltPrtSc);
            this.Controls.Add(this.CheckBoxWithCursor);
            this.Controls.Add(this.RadioButtonPNG);
            this.Controls.Add(this.RadioButtonJPEG);
            this.Controls.Add(this.RadioButtonBMP);
            this.Controls.Add(this.CheckBoxSetPrint);
            this.Controls.Add(this.ButtonClose);
            this.Controls.Add(this.CheckBoxSaveDesktop);
            this.Controls.Add(this.CheckBoxEditCapture);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(220, 310);
            this.MinimumSize = new System.Drawing.Size(220, 310);
            this.Name = "FormMain";
            this.Text = "ScShoAlpha";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ContextMenuMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Label LabelSaveNamePreview;
        internal System.Windows.Forms.CheckBox CheckBoxRegStartUp;
        internal System.Windows.Forms.Button ButtonDefault;
        internal System.Windows.Forms.TextBox TextBoxSaveName;
        internal System.Windows.Forms.Label LabelSaveName;
        internal System.Windows.Forms.Label LabelSaveFormat;
        internal System.Windows.Forms.Label LabelAltPrtSc;
        internal System.Windows.Forms.CheckBox CheckBoxWithCursor;
        internal System.Windows.Forms.RadioButton RadioButtonPNG;
        internal System.Windows.Forms.RadioButton RadioButtonJPEG;
        internal System.Windows.Forms.RadioButton RadioButtonBMP;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemExit;
        internal System.Windows.Forms.CheckBox CheckBoxSetPrint;
        internal System.Windows.Forms.Button ButtonClose;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator3;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemMemo;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemVersion;
        internal System.Windows.Forms.ContextMenuStrip ContextMenuMain;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSetting;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSaveDesktop;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSetPrint;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemEditCapture;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemWithCursor;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemRegStartUp;
        internal System.Windows.Forms.NotifyIcon NotifyIconMain;
        internal System.Windows.Forms.CheckBox CheckBoxSaveDesktop;
        internal System.Windows.Forms.CheckBox CheckBoxEditCapture;


    }
}