
namespace ScShoAlpha
{
    partial class FormPrintPreview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrintPreview));
            this.ButtonSmall = new System.Windows.Forms.Button();
            this.ButtonPrint = new System.Windows.Forms.Button();
            this.PrintPreviewControl1 = new System.Windows.Forms.PrintPreviewControl();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ButtonLarge = new System.Windows.Forms.Button();
            this.ButtonPageSetting = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ButtonSmall
            // 
            this.ButtonSmall.Image = ((System.Drawing.Image)(resources.GetObject("ButtonSmall.Image")));
            this.ButtonSmall.Location = new System.Drawing.Point(143, 8);
            this.ButtonSmall.Name = "ButtonSmall";
            this.ButtonSmall.Size = new System.Drawing.Size(35, 35);
            this.ButtonSmall.TabIndex = 10;
            this.ToolTip1.SetToolTip(this.ButtonSmall, "余白を広くする");
            this.ButtonSmall.UseVisualStyleBackColor = true;
            this.ButtonSmall.Click += new System.EventHandler(this.ButtonSmall_Click);
            // 
            // ButtonPrint
            // 
            this.ButtonPrint.Image = ((System.Drawing.Image)(resources.GetObject("ButtonPrint.Image")));
            this.ButtonPrint.Location = new System.Drawing.Point(12, 8);
            this.ButtonPrint.Name = "ButtonPrint";
            this.ButtonPrint.Size = new System.Drawing.Size(35, 35);
            this.ButtonPrint.TabIndex = 7;
            this.ToolTip1.SetToolTip(this.ButtonPrint, "印刷");
            this.ButtonPrint.UseVisualStyleBackColor = true;
            this.ButtonPrint.Click += new System.EventHandler(this.ButtonPrint_Click);
            // 
            // PrintPreviewControl1
            // 
            this.PrintPreviewControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PrintPreviewControl1.Location = new System.Drawing.Point(12, 49);
            this.PrintPreviewControl1.Name = "PrintPreviewControl1";
            this.PrintPreviewControl1.Size = new System.Drawing.Size(457, 419);
            this.PrintPreviewControl1.TabIndex = 6;
            // 
            // ButtonLarge
            // 
            this.ButtonLarge.Image = ((System.Drawing.Image)(resources.GetObject("ButtonLarge.Image")));
            this.ButtonLarge.Location = new System.Drawing.Point(102, 8);
            this.ButtonLarge.Name = "ButtonLarge";
            this.ButtonLarge.Size = new System.Drawing.Size(35, 35);
            this.ButtonLarge.TabIndex = 9;
            this.ToolTip1.SetToolTip(this.ButtonLarge, "余白を狭くする");
            this.ButtonLarge.UseVisualStyleBackColor = true;
            this.ButtonLarge.Click += new System.EventHandler(this.ButtonLarge_Click);
            // 
            // ButtonPageSetting
            // 
            this.ButtonPageSetting.Image = ((System.Drawing.Image)(resources.GetObject("ButtonPageSetting.Image")));
            this.ButtonPageSetting.Location = new System.Drawing.Point(53, 8);
            this.ButtonPageSetting.Name = "ButtonPageSetting";
            this.ButtonPageSetting.Size = new System.Drawing.Size(35, 35);
            this.ButtonPageSetting.TabIndex = 8;
            this.ToolTip1.SetToolTip(this.ButtonPageSetting, "ページ設定");
            this.ButtonPageSetting.UseVisualStyleBackColor = true;
            this.ButtonPageSetting.Click += new System.EventHandler(this.ButtonPageSetting_Click);
            // 
            // Label1
            // 
            this.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label1.Location = new System.Drawing.Point(94, 8);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(2, 35);
            this.Label1.TabIndex = 11;
            // 
            // FormPrintPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 477);
            this.Controls.Add(this.ButtonSmall);
            this.Controls.Add(this.ButtonPrint);
            this.Controls.Add(this.PrintPreviewControl1);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.ButtonLarge);
            this.Controls.Add(this.ButtonPageSetting);
            this.Name = "FormPrintPreview";
            this.Text = "印刷プレビュー";
            this.Load += new System.EventHandler(this.FormPrintPreview_Load);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button ButtonSmall;
        internal System.Windows.Forms.ToolTip ToolTip1;
        internal System.Windows.Forms.Button ButtonPrint;
        internal System.Windows.Forms.PrintPreviewControl PrintPreviewControl1;
        internal System.Windows.Forms.Button ButtonLarge;
        internal System.Windows.Forms.Button ButtonPageSetting;
        internal System.Windows.Forms.Label Label1;
    }
}