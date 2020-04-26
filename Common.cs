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
    public static class DFN
    {
        // ファイル名文字数制限
        public const int LIMIT_FILENAME = 64;
        public const string DEFAULT_FILENAME = "CaptureImage";

        // 保存形式の識別文字定数
        public const string MODE_BMP = "BMP";
        public const string MODE_JPEG = "JPEG";
        public const string MODE_PNG = "PNG";
    }
    public static class Common
    {
        //メモフォーム
        public static FormPreview fPreview;

        // キャプチャ完了後処理
        public static void ImageCaptured(Bitmap bmp)
        {
            // クリップボードに貼り付け
            Clipboard.SetImage(bmp);
            // デスクトップに保存
            if (Settings.Instance.SaveDesktopMode == true)
                ClsImageSave.ImageDesktopSaveFile(bmp);
            // 編集
            if (Settings.Instance.EditCapture == true)
            {
                // プレビューフォームをプライマリスクリーンの中央に表示
                FormPaint fPaint = new FormPaint();
                fPaint.StartPosition = FormStartPosition.Manual;
                fPaint.Top = (int)(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / (double)2 - fPaint.Height / (double)2);
                fPaint.Left = (int)(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / (double)2 - fPaint.Width / (double)2);
                // 画像をディープコピー
                fPaint.PictureBoxCapture.Image = (Bitmap)bmp.Clone();
                // プレビューフォームを表示
                fPaint.Show();
            }
            // 印刷
            if (Settings.Instance.PrintCaptureMode == true)
            {
                // プレビューフォームをプライマリスクリーンの中央に表示
                FormPrintPreview fPrintPreview = new FormPrintPreview();
                fPrintPreview.StartPosition = FormStartPosition.Manual;
                fPrintPreview.Top = (int)(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / (double)2 - fPrintPreview.Height / (double)2);
                fPrintPreview.Left = (int)(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / (double)2 - fPrintPreview.Width / (double)2);
                // 画像をディープコピー
                fPrintPreview.memoryImage = (Bitmap)bmp.Clone();
                // プレビューフォームを表示
                fPrintPreview.Show();
            }
        }
    }
}