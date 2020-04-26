using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

namespace ScShoAlpha {

    public partial class FormPrintPreview : Form
    {
        public FormPrintPreview()
        {
            // この呼び出しは、Windows フォーム デザイナで必要です。
            InitializeComponent();

            // InitializeComponent() 呼び出しの後で初期化を追加します。

            PrintDocument1 = new System.Drawing.Printing.PrintDocument();
        }

        // 画像の保管
        public Bitmap memoryImage;

        // 'ページセットアップダイアログ表示
        private PageSetupDialog PSDlg = new PageSetupDialog();
        // プリンタ選択ダイアログの表示
        private PrintDialog PDlg = new PrintDialog();

        // PrintDocumentオブジェクトの作成
        private System.Drawing.Printing.PrintDocument _PrintDocument1;

        private System.Drawing.Printing.PrintDocument PrintDocument1
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _PrintDocument1;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_PrintDocument1 != null)
                {
                }

                _PrintDocument1 = value;
                if (_PrintDocument1 != null)
                {
                }
            }
        }

        private void FormPrintPreview_Load(System.Object sender, System.EventArgs e)
        {
            // PrintPageイベントハンドラの追加
            PrintDocument1.PrintPage += pd_PrintPage;

            // 前回使用した印刷向きを設定
            PrintDocument1.DefaultPageSettings.Landscape = Settings.Instance.PageSettingsLandscape;

            // プレビューするPrintDocumentを設定
            PrintPreviewControl1.Document = PrintDocument1;

            // 画面を更新する（.NET Framework 1.1以前では必要なし）
            PrintPreviewControl1.InvalidatePreview();
        }

        // ﾌﾟﾘﾝﾄイベント処理
        private void pd_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int left = e.MarginBounds.Left; // 印字領域
            int top = e.MarginBounds.Top;
            int right = e.MarginBounds.Right;
            int bottom = e.MarginBounds.Bottom;

            int width = right - left;
            int height = bottom - top;

            double zoomRatio = 1.0;
            // 画像サイズが用紙サイズを超えるときは縮小
            if (memoryImage.Width >= width && memoryImage.Height <= height)
                zoomRatio = width / (double)memoryImage.Width;
            else if (memoryImage.Width <= width && memoryImage.Height >= height)
                zoomRatio = height / (double)memoryImage.Height;
            else if (memoryImage.Width >= width && memoryImage.Height >= height)
            {
                double zoomRatio_X = width / (double)memoryImage.Width;
                double zoomRatio_Y = height / (double)memoryImage.Height;
                if (zoomRatio_X >= zoomRatio_Y)
                    zoomRatio = zoomRatio_Y;
                else
                    zoomRatio = zoomRatio_X;
            }
            else
                zoomRatio = 1.0;

            int zoomWidth = (int)(memoryImage.Width * zoomRatio);
            int zoomHeight = (int)(memoryImage.Height * zoomRatio);
            int centerX = (int)(left + width / (double)2);
            int centerY = (int)(top + height / (double)2);
            int imgStartX = (int)(centerX - zoomWidth / (double)2);
            int imgStartY = (int)(centerY - zoomHeight / (double)2);
            Rectangle rect = new Rectangle(imgStartX, imgStartY, zoomWidth, zoomHeight);
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            e.Graphics.DrawImage(memoryImage, rect);
            // 次のページがないことを通知する 
            e.HasMorePages = false;
        }

        // 印刷ダイアログ表示
        private void ButtonPrint_Click(System.Object sender, System.EventArgs e)
        {
            PDlg.Document = PrintDocument1;
            PDlg.UseEXDialog = true;
            if ((PDlg.ShowDialog() == DialogResult.OK))
            {
                PDlg.Document.Print(); // ﾌﾟﾘﾝﾄ
                this.Close();
            }
        }

        // ページ設定の表示
        private void ButtonPageSetting_Click(System.Object sender, System.EventArgs e)
        {
            PSDlg.Document = PrintDocument1;
            PSDlg.EnableMetric = true;
            if ((PSDlg.ShowDialog() == DialogResult.OK))
            {
                // 画面を更新する（.NET Framework 1.1以前では必要なし）
                PrintPreviewControl1.InvalidatePreview();
                // 印刷向きを保持
                Settings.Instance.PageSettingsLandscape = PrintDocument1.DefaultPageSettings.Landscape;
            }
        }

        // 余白を狭くする
        private void ButtonLarge_Click(System.Object sender, System.EventArgs e)
        {
            // 余白を狭くしておく
            PrintDocument1.DefaultPageSettings.Margins.Left = 5;
            PrintDocument1.DefaultPageSettings.Margins.Top = 5;
            PrintDocument1.DefaultPageSettings.Margins.Right = 5;
            PrintDocument1.DefaultPageSettings.Margins.Bottom = 5;
            // 画面を更新する（.NET Framework 1.1以前では必要なし）
            PrintPreviewControl1.InvalidatePreview();
        }

        // 余白を広くする
        private void ButtonSmall_Click(System.Object sender, System.EventArgs e)
        {
            // 余白を広くしておく
            PrintDocument1.DefaultPageSettings.Margins.Left = (int)101.6;
            PrintDocument1.DefaultPageSettings.Margins.Top = (int)101.6;
            PrintDocument1.DefaultPageSettings.Margins.Bottom = (int)101.6;
            PrintDocument1.DefaultPageSettings.Margins.Right = (int)101.6;
            // 画面を更新する（.NET Framework 1.1以前では必要なし）
            PrintPreviewControl1.InvalidatePreview();
        }
    }

}