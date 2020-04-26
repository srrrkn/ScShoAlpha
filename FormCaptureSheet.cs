using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using System.Drawing;

namespace ScShoAlpha
{
    public partial class FormCaptureSheet : Form
    {
        // ドラッグ中フラグ
        private bool _isDraging = false;
        // ドラッグ開始点
        private Point dragStartPoint = new Point(0, 0);
        // ドラッグ終了点
        private Point dragEndPoint = new Point(0, 0);
        // スクリーン全体の画像
        private Image FullScreenImg;
        // 擬似的に半透明化した画像
        private Image DispScreenImg;

        private bool _isKeyDown = false;

        public FormCaptureSheet() {
            // この呼び出しは、Windows フォーム デザイナで必要です。
            InitializeComponent();

            // InitializeComponent() 呼び出しの後で初期化を追加します。
        }

        // ロード時の処理
        private void FormCaputureSheet_Load(System.Object sender, System.EventArgs e)
        {
            // PictureBox1の解放
            if (!(this.pictureBoxCaptureSheet.Image == null))
                this.pictureBoxCaptureSheet.Image.Dispose();

            // FullScreenImgの解放
            if (!(FullScreenImg == null))
                FullScreenImg.Dispose();

            // DispScreenImgの解放
            if (!(DispScreenImg == null))
                DispScreenImg.Dispose();

            // フルスクリーンのキャプチャを取得
            FullScreenImg = new Bitmap(ClsCapture.FullScreenCapture());
            // カーソルを追加する場合実行
            if (Settings.Instance.CaptureCursorMode == true)
                FullScreenImg = ClsCapture.AddCursor((Image)FullScreenImg.Clone());
            DispScreenImg = new Bitmap(FullScreenImg);
            // スタートメニューを閉じる
            this.Cursor = Cursors.Cross;
            this.pictureBoxCaptureSheet.Left = 0;
            this.pictureBoxCaptureSheet.Top = 0;
            this.pictureBoxCaptureSheet.Width = this.Width;
            this.pictureBoxCaptureSheet.Height = this.Height;
            // 白塗り画像の格納先
            Bitmap bmp_white = new Bitmap(pictureBoxCaptureSheet.Width, pictureBoxCaptureSheet.Height);
            // ImageオブジェクトのGraphicsオブジェクトを作成する
            Graphics g = Graphics.FromImage(bmp_white);
            Rectangle rect = new Rectangle(0, 0, FullScreenImg.Width, FullScreenImg.Height);
            // 白塗り画像を生成
            g.FillRectangle(Brushes.White, rect);
            // 白塗り画像を重ねて擬似的に透過
            Add_currentImage(bmp_white);
            // PictureBox1に反映
            this.pictureBoxCaptureSheet.Image = DispScreenImg;
            this.Opacity = 1.0;
            // リソースの解放
            g.Dispose();
            bmp_white.Dispose();
            // フォームを最前面に設定し表示
            this.TopMost = true;
            this.Visible = true;
        }

        // 半透明にした画像を重ねる
        public void Add_currentImage(Image img)
        {
            // ImageオブジェクトのGraphicsオブジェクトを作成する
            Graphics g = Graphics.FromImage(DispScreenImg);
            System.Drawing.Imaging.ImageAttributes ia = new System.Drawing.Imaging.ImageAttributes();
            Rectangle rect = new Rectangle();
            rect.X = 0;
            rect.Y = 0;
            rect.Width = DispScreenImg.Width;
            rect.Height = DispScreenImg.Height;
            // ColorMatrixオブジェクトの作成
            System.Drawing.Imaging.ColorMatrix cm = new System.Drawing.Imaging.ColorMatrix();
            // ColorMatrixの行列の値を変更して、アルファ値が0.5に変更されるようにする
            cm.Matrix00 = 1;
            cm.Matrix11 = 1;
            cm.Matrix22 = 1;
            cm.Matrix33 = 0.3F;
            cm.Matrix44 = 1;
            // ColorMatrixを設定する
            ia.SetColorMatrix(cm);

            // 画像を指定された位置、サイズで描画する
            g.DrawImage(img, rect, 0, 0, DispScreenImg.Width, DispScreenImg.Height, GraphicsUnit.Pixel, ia);
            // Imageオブジェクトのリソースを解放する
            img.Dispose();
            // Graphicsオブジェクトのリソースを解放する
            g.Dispose();
        }

        // 終了時の解放処理
        public void CloseProc()
        {
            // PictureBox1の解放
            if (!(this.pictureBoxCaptureSheet.Image == null))
                this.pictureBoxCaptureSheet.Image.Dispose();

            // FullScreenImgの解放
            if (!(FullScreenImg == null))
                FullScreenImg.Dispose();

            // DispScreenImgの解放
            if (!(DispScreenImg == null))
                DispScreenImg.Dispose();
        }

        // ドラッグ開始
        private void pictureBoxCaptureSheet_MouseDown(System.Object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            _isDraging = true;
            System.Drawing.Point sp = System.Windows.Forms.Cursor.Position;
            dragStartPoint = this.PointToClient(sp);
        }


        // ドラッグ終了
        private void pictureBoxCaptureSheet_MouseUp(System.Object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (_isDraging == false)
                return;
            this.Visible = false;
            _isDraging = false;
            // カーソル位置を取得し、計算用に変換
            System.Drawing.Point ep = System.Windows.Forms.Cursor.Position;
            dragEndPoint = this.PointToClient(ep);
            if (dragStartPoint.X - dragEndPoint.X != 0 && dragStartPoint.Y - dragEndPoint.Y != 0)
            {
                Bitmap bmp = ClsCapture.CaptureTrim(FullScreenImg, dragStartPoint, dragEndPoint);
                // キャプチャ後の処理実行
                Common.ImageCaptured(bmp);
                // メモ画面が開いているとき
                if (Common.fPreview.Visible == true)
                    // キャプチャ終了後メモ画面に貼り付け
                    Common.fPreview.RichTextBox1.Paste();
                // 開放
                bmp.Dispose();
            }
            // フォームを閉じる
            this.Close();
        }

        // 移動中の描画
        private void pictureBoxCaptureSheet_MouseMove(System.Object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (_isDraging == false)
                return;
            pictureBoxCaptureSheet.Refresh();
        }

        // pictureBoxCaptureSheetの描画
        private void pictureBoxCaptureSheet_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (_isDraging == false)
                return;
            System.Drawing.Graphics g = e.Graphics;
            System.Drawing.Point cp = System.Windows.Forms.Cursor.Position;
            g.DrawLine(Pens.Red, dragStartPoint.X, dragStartPoint.Y, this.PointToClient(cp).X, dragStartPoint.Y);
            g.DrawLine(Pens.Red, dragStartPoint.X, dragStartPoint.Y, dragStartPoint.X, this.PointToClient(cp).Y);
            g.DrawLine(Pens.Red, this.PointToClient(cp).X, dragStartPoint.Y, this.PointToClient(cp).X, this.PointToClient(cp).Y);
            g.DrawLine(Pens.Red, dragStartPoint.X, this.PointToClient(cp).Y, this.PointToClient(cp).X, this.PointToClient(cp).Y);
        }

        // 何かしらキーが押された場合Formを閉じる
        private void FormCaputureSheet_KeyDown(System.Object sender, System.Windows.Forms.KeyEventArgs e)
        {
            _isKeyDown = true;
        }

        // 何かしらキーが押された場合Formを閉じる
        private void FormCaputureSheet_KeyUp(System.Object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (_isKeyDown == true)
            {
                CloseProc();
                this.Close();
            }
        }
    }
}