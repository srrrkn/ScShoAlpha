namespace ScShoAlpha
{
    partial class FormPreview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPreview));
            this.Label1 = new System.Windows.Forms.Label();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.ButtonClear = new System.Windows.Forms.Button();
            this.ButtonCapture = new System.Windows.Forms.Button();
            this.RichTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label1.Location = new System.Drawing.Point(53, 12);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(2, 35);
            this.Label1.TabIndex = 11;
            // 
            // ButtonSave
            // 
            this.ButtonSave.Image = ((System.Drawing.Image)(resources.GetObject("ButtonSave.Image")));
            this.ButtonSave.Location = new System.Drawing.Point(61, 12);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(35, 35);
            this.ButtonSave.TabIndex = 10;
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // ButtonClear
            // 
            this.ButtonClear.Image = ((System.Drawing.Image)(resources.GetObject("ButtonClear.Image")));
            this.ButtonClear.Location = new System.Drawing.Point(102, 12);
            this.ButtonClear.Name = "ButtonClear";
            this.ButtonClear.Size = new System.Drawing.Size(35, 35);
            this.ButtonClear.TabIndex = 9;
            this.ButtonClear.UseVisualStyleBackColor = true;
            this.ButtonClear.Click += new System.EventHandler(this.ButtonClear_Click);
            // 
            // ButtonCapture
            // 
            this.ButtonCapture.Image = ((System.Drawing.Image)(resources.GetObject("ButtonCapture.Image")));
            this.ButtonCapture.Location = new System.Drawing.Point(12, 12);
            this.ButtonCapture.Name = "ButtonCapture";
            this.ButtonCapture.Size = new System.Drawing.Size(35, 35);
            this.ButtonCapture.TabIndex = 8;
            this.ButtonCapture.UseVisualStyleBackColor = true;
            this.ButtonCapture.Click += new System.EventHandler(this.ButtonCapture_Click);
            // 
            // RichTextBox1
            // 
            this.RichTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.RichTextBox1.Location = new System.Drawing.Point(12, 53);
            this.RichTextBox1.Name = "RichTextBox1";
            this.RichTextBox1.Size = new System.Drawing.Size(602, 588);
            this.RichTextBox1.TabIndex = 7;
            this.RichTextBox1.Text = "";
            // 
            // FormPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 653);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.ButtonSave);
            this.Controls.Add(this.ButtonClear);
            this.Controls.Add(this.ButtonCapture);
            this.Controls.Add(this.RichTextBox1);
            this.Name = "FormPreview";
            this.Text = "メモ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormPreview_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button ButtonSave;
        internal System.Windows.Forms.Button ButtonClear;
        internal System.Windows.Forms.Button ButtonCapture;
        internal System.Windows.Forms.RichTextBox RichTextBox1;
    }
}