using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScShoAlpha
{
    public partial class FormPreview : Form
    {
        public FormPreview() {
            // この呼び出しは、Windows フォーム デザイナで必要です。
            InitializeComponent();

            // InitializeComponent() 呼び出しの後で初期化を追加します。
        }

        // リッチテキストの中身を空にする
        private void ButtonClear_Click(System.Object sender, System.EventArgs e)
        {
            RichTextBox1.Clear();
        }

        // ボタンからキャプチャ
        private void ButtonCapture_Click(System.Object sender, System.EventArgs e)
        {
            bool tmpCaptureCursorMode = Settings.Instance.CaptureCursorMode;
            if (Settings.Instance.CaptureCursorMode == true)
                Settings.Instance.CaptureCursorMode = false;
            int width = 0;
            int height = 0;
            int minX = int.MaxValue;
            int minY = int.MaxValue;

            // 全ディスプレイの幅と高さをそれぞれ合計
            //System.Windows.Forms.Screen s = System.Windows.Forms.Screen.PrimaryScreen;
            System.Windows.Forms.Screen leftup_s = System.Windows.Forms.Screen.PrimaryScreen;
            foreach (var s in System.Windows.Forms.Screen.AllScreens)
            {
                height = height + s.Bounds.Height;
                width = width + s.Bounds.Width;
                if (s.Bounds.X < minX)
                {
                    minX = s.Bounds.X;
                    leftup_s = s;
                }
                if (s.Bounds.Y < minY)
                {
                    minY = s.Bounds.Y;
                    leftup_s = s;
                }
            }

            // 画面の高さと幅をグローバル変数に設定
            ClsCapture.dHeight = height;
            ClsCapture.dWidth = width;

            // この辺の調整が必要
            FormCaptureSheet fc = new FormCaptureSheet();

            // 'キャプチャウィンドウの位置と大きさの設定
            FormCaptureSheet fCaptureSheet = new FormCaptureSheet();
            fCaptureSheet.Opacity = 0.4;
            fCaptureSheet.StartPosition = FormStartPosition.Manual;
            fCaptureSheet.Location = leftup_s.Bounds.Location;
            fCaptureSheet.Width = width;
            fCaptureSheet.Height = height;
            fCaptureSheet.Show();
            // カーソル撮影設定を戻す
            Settings.Instance.CaptureCursorMode = tmpCaptureCursorMode;
        }

        // リッチテキストを保存
        private void ButtonSave_Click(System.Object sender, System.EventArgs e)
        {
            // SaveFileDialogクラスのインスタンスを作成
            SaveFileDialog sfd = new SaveFileDialog();
            // はじめのファイル名を指定する
            // はじめに「ファイル名」で表示される文字列を指定する
            sfd.FileName = "新しいファイル.rtf";
            // はじめに表示されるフォルダを指定する
            // 指定しない（空の文字列）の時は、現在のディレクトリが表示される
            // sfd.InitialDirectory = "C:\"
            // [ファイルの種類]に表示される選択肢を指定する
            sfd.Filter = "リッチテキストファイル(*.rtf)|*.rtf|すべてのファイル(*.*)|*.*";
            // [ファイルの種類]ではじめに選択されるものを指定する
            // 2番目の「すべてのファイル」が選択されているようにする
            sfd.FilterIndex = 1;
            // タイトルを設定する
            sfd.Title = "保存先のファイルを選択してください";
            // ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            sfd.RestoreDirectory = true;
            // 既に存在するファイル名を指定したとき警告する
            // デフォルトでTrueなので指定する必要はない
            sfd.OverwritePrompt = true;
            // 存在しないパスが指定されたとき警告を表示する
            // デフォルトでTrueなので指定する必要はない
            sfd.CheckPathExists = true;
            // ダイアログを表示する
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                // OKボタンがクリックされたとき、選択されたファイル名を表示する
                Console.WriteLine(sfd.FileName);
                RichTextBox1.SaveFile(sfd.FileName, RichTextBoxStreamType.RichText);
            }
        }

        private void FormPreview_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}