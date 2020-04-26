namespace ScShoAlpha
{
    partial class FormCaptureSheet
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
            this.pictureBoxCaptureSheet = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCaptureSheet)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxCaptureSheet
            // 
            this.pictureBoxCaptureSheet.BackColor = System.Drawing.Color.White;
            this.pictureBoxCaptureSheet.Location = new System.Drawing.Point(108, 57);
            this.pictureBoxCaptureSheet.Name = "pictureBoxCaptureSheet";
            this.pictureBoxCaptureSheet.Size = new System.Drawing.Size(141, 102);
            this.pictureBoxCaptureSheet.TabIndex = 1;
            this.pictureBoxCaptureSheet.TabStop = false;
            this.pictureBoxCaptureSheet.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxCaptureSheet_MouseMove);
            this.pictureBoxCaptureSheet.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxCaptureSheet_MouseDown);
            this.pictureBoxCaptureSheet.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxCaptureSheet_Paint);
            this.pictureBoxCaptureSheet.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxCaptureSheet_MouseUp);
            // 
            // FormCaptureSheet
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBoxCaptureSheet);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FormCaptureSheet";
            this.Opacity = 0.4;
            this.Text = "Form_CaputureSheet";
            this.Load += new System.EventHandler(this.FormCaputureSheet_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormCaputureSheet_KeyUp);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormCaputureSheet_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCaptureSheet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.PictureBox pictureBoxCaptureSheet;
    }
}