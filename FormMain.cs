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
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ScShoAlpha
{
    public partial class FormMain : Form
    {
        // カーソル情報取得用構造体
        [StructLayout(LayoutKind.Sequential)]
        private struct CURSORINFO
        {
            public uint cbSize;
            public uint flags;
            public IntPtr hCursor;
            public Point ptScreenPos;
        }

        // 現在のカーソル情報を取得するAPI
        [DllImport("user32.dll")]
        private static extern bool GetCursorInfo(ref CURSORINFO pci);

        // ホットキー
        private ClsHotkeys hotkeyPrtSc;
        private ClsHotkeys hotkeyAltPrtSc;

        public FormMain() {
            // この呼び出しは、Windows フォーム デザイナで必要です。
            InitializeComponent();

            // InitializeComponent() 呼び出しの後で初期化を追加します。
        }

        // 設定をコントロールに反映
        public void SetControlState()
        {
            // デフォルトをPNGに変更（いずれ削除）
            Settings.Instance.SaveModeType = DFN.MODE_PNG;

            // デスクトップに保存の設定状況
            if (Settings.Instance.SaveDesktopMode == true)
                CheckBoxSaveDesktop.Checked = true;
            else
                CheckBoxSaveDesktop.Checked = false;
            // デスクトップに保存のモード設定状況
            if (Settings.Instance.SaveModeType == DFN.MODE_BMP)
                RadioButtonBMP.Checked = true;
            else if (Settings.Instance.SaveModeType == DFN.MODE_JPEG)
                RadioButtonJPEG.Checked = true;
            else if (Settings.Instance.SaveModeType == DFN.MODE_PNG)
                RadioButtonPNG.Checked = true;
            // ファイル名の設定状況
            TextBoxSaveName.Text = Settings.Instance.SaveFileName;
            LabelSaveNamePreview.Text = Settings.Instance.SaveFileName;
            switch (Settings.Instance.SaveModeType)
            {
                case DFN.MODE_BMP:
                    {
                        LabelSaveNamePreview.Text = Settings.Instance.SaveFileName + "_"
                      + "20yyMMddHHmmss" + ".bmp";
                        break;
                    }

                case DFN.MODE_JPEG:
                    {
                        LabelSaveNamePreview.Text = Settings.Instance.SaveFileName + "_"
                      + "20yyMMddHHmmss" + ".jpg";
                        break;
                    }

                case DFN.MODE_PNG:
                    {
                        LabelSaveNamePreview.Text = Settings.Instance.SaveFileName + "_"
                      + "20yyMMddHHmmss" + ".png";
                        break;
                    }
            }

            // 撮影後に印刷の設定状況
            if (Settings.Instance.PrintCaptureMode == true)
                CheckBoxSetPrint.Checked = true;
            else
                CheckBoxSetPrint.Checked = false;
            // カーソルを撮影の設定状況
            if (Settings.Instance.CaptureCursorMode == true)
                CheckBoxWithCursor.Checked = true;
            else
                CheckBoxWithCursor.Checked = false;
            // スタートアップに登録の設定状況
            if (Settings.Instance.RegisterStartUp == true)
                CheckBoxRegStartUp.Checked = true;
            else
                CheckBoxRegStartUp.Checked = false;
            // 撮影後に編集の設定状況
            if (Settings.Instance.EditCapture == true)
                CheckBoxEditCapture.Checked = true;
            else
                CheckBoxEditCapture.Checked = false;
        }

        public void SetForm_Capture()
        {
            int width = 0;
            int height = 0;
            int minX = int.MaxValue;
            int minY = int.MaxValue;

            // 全ディスプレイの幅と高さをそれぞれ合計
            //System.Windows.Forms.Screen s = System.Windows.Forms.Screen.PrimaryScreen;
            System.Windows.Forms.Screen leftup_s = System.Windows.Forms.Screen.PrimaryScreen;
            foreach (System.Windows.Forms.Screen s in System.Windows.Forms.Screen.AllScreens)
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

            //左上座標を設定
            ClsCapture.minX = minX;
            ClsCapture.minY = minY;

            FormCaptureSheet fc = new FormCaptureSheet();

            // 'キャプチャウィンドウの位置と大きさの設定
            FormCaptureSheet fCaptureSheet = new FormCaptureSheet();
            fCaptureSheet.Opacity = 0.4;
            fCaptureSheet.StartPosition = FormStartPosition.Manual;
            fCaptureSheet.Location = leftup_s.Bounds.Location;
            fCaptureSheet.Width = width;
            fCaptureSheet.Height = height;
            fCaptureSheet.ShowDialog();
        }

        // 閉じるイベント
        private void ButtonClose_Click(System.Object sender, System.EventArgs e)
        {
            // 設定の保存
            Settings.SaveToXmlFile();
            this.Visible = false;
        }

        // ロード時の動作
        protected override void OnLoad(EventArgs e)
        {
            // ファイル名の文字数制限(64文字まで)
            TextBoxSaveName.MaxLength = DFN.LIMIT_FILENAME;
            TextBoxSaveName.Text = Settings.Instance.SaveFileName;
            // バージョンの設定
            ToolStripMenuItemVersion.Text = "Version:" + Application.ProductVersion;
            try
            {
                // PrtSrcをID 0のホットキーとして登録
                hotkeyPrtSc = new ClsHotkeys(this.Handle, 0, Keys.PrintScreen);
                // Alt+PrtSrcをID 1のホットキーとして登録
                hotkeyAltPrtSc = new ClsHotkeys(this.Handle, 1, Keys.Alt | Keys.PrintScreen);
            }
            catch (Exception ex)
            {
                // エラーメッセージ表示
                MessageBox.Show("キャプチャソフトの併用はできません。"
                                + "\n" + "他のキャプチャソフトを終了してからお試しください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // 終了（フォーム表示していないとうまく終了しない）
                this.Show();
                Application.Exit();
            }
            //メモウィンドウフォームの宣言
            Common.fPreview = new FormPreview();
        }

        // クローズ時の動作
        private void FormMain_FormClosing(System.Object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            // 設定の保存
            Settings.SaveToXmlFile();
        }

        // ホットキーによる動作
        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_CLOSE = 0xF060;
            const int WM_HOTKEY = 0x312;
            if (m.Msg == WM_HOTKEY && m.LParam == hotkeyPrtSc.LParam)
            {
                // カーソルを追加する場合
                if (Settings.Instance.CaptureCursorMode == true)
                {
                    // カーソルの情報を取得
                    CURSORINFO ci = new CURSORINFO();
                    ci.cbSize = (uint)Marshal.SizeOf(typeof(CURSORINFO));
                    GetCursorInfo(ref ci);
                    // カーソルの形状を取得
                    ClsCapture.shotCursor = new Cursor(ci.hCursor);
                    // カーソルのホットスポットを取得
                    ClsCapture.shotCursorHotspot = System.Windows.Forms.Cursor.Current.HotSpot;
                    // カーソルの位置を取得
                    ClsCapture.shotPoint = System.Windows.Forms.Cursor.Position;
                }
                // ホットキーの登録を解除(念のため)
                hotkeyPrtSc.Unregister();
                hotkeyAltPrtSc.Unregister();
                // トリミングモードを実行
                SetForm_Capture();
                try
                {
                    // PrtSrcをID 0のホットキーとして登録
                    hotkeyPrtSc = new ClsHotkeys(this.Handle, 0, Keys.PrintScreen);
                    // Alt+PrtSrcをID 1のホットキーとして登録
                    hotkeyAltPrtSc = new ClsHotkeys(this.Handle, 1, Keys.Alt | Keys.PrintScreen);
                }
                catch (Exception ex)
                {
                    // エラーメッセージ表示
                    MessageBox.Show("キャプチャソフトの併用はできません。"
                                    + "\n" + "他のキャプチャソフトを終了してからお試しください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // 終了（フォーム表示していないとうまく終了しない）
                    this.Show();
                    Application.Exit();
                }
            }
            else if (m.Msg == WM_HOTKEY && m.LParam == hotkeyAltPrtSc.LParam)
            {
                // キャプチャを取得
                Bitmap bmp = ClsCapture.ActiveWindowsCapture();
                // キャプチャ後の処理実行
                Common.ImageCaptured(bmp);
                // メモ画面が開いているとき
                if (Common.fPreview.Visible == true)
                    // キャプチャ終了後メモ画面に貼り付け
                    Common.fPreview.RichTextBox1.Paste();
                bmp.Dispose();
            }
            else if (m.Msg == WM_SYSCOMMAND && m.WParam.ToInt32() == SC_CLOSE)
            {
                Hide();
            }
            else
                base.WndProc(ref m);
        }

        // 常駐アイコンをダブルクリックでウィンドウを表示
        private void NotifyIconMain_MouseDoubleClick(System.Object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.Show();
        }

        // コンテキストメニューからバージョン情報を開く
        private void ToolStripMenuItemVersion_Click(System.Object sender, System.EventArgs e)
        {
            MessageBox.Show(Application.ProductName.Replace("Plus", "+") + "\n" + "Ver：" + Application.ProductVersion, "バージョン情報", MessageBoxButtons.OK);
        }

        // コンテキストメニューから設定を開く
        private void ToolStripMenuItemSetting_Click(System.Object sender, System.EventArgs e)
        {
            this.Show();
        }

        // コンテキストクリックメニューから終了を選択
        private void ToolStripMenuItemExit_Click(System.Object sender, System.EventArgs e)
        {
            // 通知アイコンを非表示にする（やっておかないと残る場合があるらしい？）
            NotifyIconMain.Visible = false;
            // ホットキーの登録を解除(念のため)
            hotkeyPrtSc.Unregister();
            hotkeyAltPrtSc.Unregister();
            // 設定を保存
            Settings.SaveToXmlFile();
            // プログラムを終了
            Application.Exit();
        }

        // 保存形式をBMPに設定
        private void RadioButtonBMP_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            if (RadioButtonBMP.Checked == true)
            {
                // 選択状態を更新
                RadioButtonJPEG.Checked = false;
                RadioButtonPNG.Checked = false;
                // 保存形式を設定
                Settings.Instance.SaveModeType = DFN.MODE_BMP;
            }
        }

        // 保存形式をJPEGに設定
        private void RadioButtonJPEG_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            if (RadioButtonJPEG.Checked == true)
            {
                // 選択状態を更新
                RadioButtonBMP.Checked = false;
                RadioButtonPNG.Checked = false;
                // 保存形式を設定
                Settings.Instance.SaveModeType = DFN.MODE_JPEG;
            }
        }

        // 保存形式をPNGに設定
        private void RadioButtonPNG_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            if (RadioButtonPNG.Checked == true)
            {
                // 選択状態を更新
                RadioButtonBMP.Checked = false;
                RadioButtonJPEG.Checked = false;
                // 保存形式を設定
                Settings.Instance.SaveModeType = DFN.MODE_PNG;
            }
        }

        // ﾃﾞｽｸﾄｯﾌﾟに保存するか切り替え
        private void CheckBoxSaveDesktop_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            if (CheckBoxSaveDesktop.Checked == true)
            {
                Settings.Instance.SaveDesktopMode = true;
                // 形式選択ボタンを使用可能にする
                RadioButtonBMP.Enabled = true;
                RadioButtonJPEG.Enabled = true;
                RadioButtonPNG.Enabled = true;
                // ファイル名入力ボックスを使用可能にする
                TextBoxSaveName.Enabled = true;
                ToolStripMenuItemSaveDesktop.Text = "✔撮影後にﾃﾞｽｸﾄｯﾌﾟに保存";
            }
            else
            {
                Settings.Instance.SaveDesktopMode = false;
                // 形式選択ボタンを使用不可能にする
                RadioButtonBMP.Enabled = false;
                RadioButtonJPEG.Enabled = false;
                RadioButtonPNG.Enabled = false;
                // ファイル名入力ボックスを使用不可能にする
                TextBoxSaveName.Enabled = false;
                ToolStripMenuItemSaveDesktop.Text = "　撮影後にﾃﾞｽｸﾄｯﾌﾟに保存";
            }
        }

        // 印刷するか切り替え
        private void CheckBoxSetPrint_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            if (CheckBoxSetPrint.Checked == true)
            {
                CheckBoxEditCapture.Checked = false;
                Settings.Instance.PrintCaptureMode = true;
                ToolStripMenuItemSetPrint.Text = "✔撮影後にプリンタに出力";
            }
            else
            {
                Settings.Instance.PrintCaptureMode = false;
                ToolStripMenuItemSetPrint.Text = "　撮影後にプリンタに出力";
            }
        }

        // 撮影後編集するか切り替え
        private void CheckBoxEditCapture_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            if (CheckBoxEditCapture.Checked == true)
            {
                CheckBoxSetPrint.Checked = false;
                Settings.Instance.EditCapture = true;
                ToolStripMenuItemEditCapture.Text = "✔撮影後に編集";
            }
            else
            {
                Settings.Instance.EditCapture = false;
                ToolStripMenuItemEditCapture.Text = "　撮影後に編集";
            }
        }

        // カーソルを撮影するか切り替え
        private void CheckBoxWithCursor_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            if (CheckBoxWithCursor.Checked == true)
            {
                Settings.Instance.CaptureCursorMode = true;
                ToolStripMenuItemWithCursor.Text = "✔カーソルも同時に撮影";
            }
            else
            {
                Settings.Instance.CaptureCursorMode = false;
                ToolStripMenuItemWithCursor.Text = "　カーソルも同時に撮影";
            }
        }

        // コンテキストメニューからのﾃﾞｽｸﾄｯﾌﾟに保存するか切り替え
        private void ToolStripMenuItemSaveDesktop_Click(System.Object sender, System.EventArgs e)
        {
            CheckBoxSaveDesktop.Checked = !(CheckBoxSaveDesktop.Checked);
        }

        // コンテキストメニューからの印刷するか切り替え
        private void ToolStripMenuItemSetPrint_Click(System.Object sender, System.EventArgs e)
        {
            CheckBoxSetPrint.Checked = !(CheckBoxSetPrint.Checked);
        }

        // コンテキストメニューから撮影後編集を切り替え
        private void ToolStripMenuItemEditCapture_Click(System.Object sender, System.EventArgs e)
        {
            CheckBoxEditCapture.Checked = !(CheckBoxEditCapture.Checked);
        }

        // コンテキストメニューからのカーソルを撮影するか切り替え
        private void ToolStripMenuItemWithCursor_Click(System.Object sender, System.EventArgs e)
        {
            CheckBoxWithCursor.Checked = !(CheckBoxWithCursor.Checked);
        }

        private void TextBoxSaveName_TextChanged(System.Object sender, System.EventArgs e)
        {
            if (TextBoxSaveName.Text.IndexOf(@"\") != -1 || TextBoxSaveName.Text.IndexOf("/") != -1 
                || TextBoxSaveName.Text.IndexOf(":") != -1 || TextBoxSaveName.Text.IndexOf("*") != -1 
                || TextBoxSaveName.Text.IndexOf("<") != -1 || TextBoxSaveName.Text.IndexOf(">") != -1 
                || TextBoxSaveName.Text.IndexOf("|") != -1 || TextBoxSaveName.Text.IndexOf("?") != -1 
                || TextBoxSaveName.Text.IndexOf((Char)34) != -1)
            {
                MessageBox.Show("ファイル名に次の文字は使えません。" + "\n" + "¥　/　:　*　?　\"　<　>　|", "注意", MessageBoxButtons.OK);
                TextBoxSaveName.Text = TextBoxSaveName.Text.Remove(TextBoxSaveName.Text.Length - 1, 1);
            }
            else
            {
                Settings.Instance.SaveFileName = TextBoxSaveName.Text;
                switch (Settings.Instance.SaveModeType)
                {
                    case DFN.MODE_BMP:
                        {
                            LabelSaveNamePreview.Text = Settings.Instance.SaveFileName + "_"
                          + "20YYMMDDHHMMSS" + ".bmp";
                            break;
                        }

                    case DFN.MODE_JPEG:
                        {
                            LabelSaveNamePreview.Text = Settings.Instance.SaveFileName + "_"
                          + "20YYMMDDHHMMSS" + ".jpg";
                            break;
                        }

                    case DFN.MODE_PNG:
                        {
                            LabelSaveNamePreview.Text = Settings.Instance.SaveFileName + "_"
                          + "20YYMMDDHHMMSS" + ".png";
                            break;
                        }
                }
            }
        }

        // デフォルトボタンの動作
        private void ButtonDefault_Click(System.Object sender, System.EventArgs e)
        {
            Settings.Instance = new Settings();
            SetControlState();
            Settings.SaveToXmlFile();
        }

        // メモ画面の起動
        private void ToolStripMenuItemMemo_Click(System.Object sender, System.EventArgs e)
        {
            Common.fPreview = new FormPreview();
            Common.fPreview.Visible = true;
            Common.fPreview.Show();
        }

        // スタートアップ登録
        private void CheckBoxRegStartUp_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            if (CheckBoxRegStartUp.Checked == true)
            {
                Settings.Instance.RegisterStartUp = true;
                ToolStripMenuItemRegStartUp.Text = "✔スタートアップに登録";
                RegisterCurrentUserRun();
            }
            else
            {
                Settings.Instance.RegisterStartUp = false;
                ToolStripMenuItemRegStartUp.Text = "　スタートアップに登録";
                ReleaseCurrentUserRun();
            }
        }

        // スタートアップレジストリ登録
        private void RegisterCurrentUserRun()
        {
            // Runキーを開く
            Microsoft.Win32.RegistryKey regkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            // 値の名前に製品名、値のデータに実行ファイルのパスを指定し、書き込む
            regkey.SetValue(Application.ProductName, Application.ExecutablePath);
            // 閉じる
            regkey.Close();
        }

        // スタートアップレジストリ登録解除
        private void ReleaseCurrentUserRun()
        {
            // Runキーを開く
            Microsoft.Win32.RegistryKey regkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            // 値の名前に製品名、値のデータに実行ファイルのパスを指定し、書き込む
            regkey.DeleteValue(Application.ProductName, false);
            // 閉じる
            regkey.Close();
        }

        // コンテキストメニューからスタートアップ登録切り替え
        private void ToolStripMenuItemRegStartUp_Click(System.Object sender, System.EventArgs e)
        {
            CheckBoxRegStartUp.Checked = !(CheckBoxRegStartUp.Checked);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //// ファイル名の文字数制限(64文字まで)
            //TextBoxSaveName.MaxLength = DFN.LIMIT_FILENAME;
            //TextBoxSaveName.Text = Settings.Instance.SaveFileName;
            //// バージョンの設定
            //ToolStripMenuItemVersion.Text = "Version:" + Application.ProductVersion;
            //// システム名の設定
            //this.Text = Application.ProductName.Replace("Plus", "+");
            //NotifyIconMain.Text = Application.ProductName.Replace("Plus", "+");
            //try
            //{
            //    // PrtSrcをID 0のホットキーとして登録
            //    hotkeyPrtSc = new ClsHotkeys(this.Handle, 0, Keys.PrintScreen);
            //    // Alt+PrtSrcをID 1のホットキーとして登録
            //    hotkeyAltPrtSc = new ClsHotkeys(this.Handle, 1, Keys.Alt | Keys.PrintScreen);
            //}
            //catch (Exception ex)
            //{
            //    // エラーメッセージ表示
            //    MessageBox.Show("キャプチャソフトの併用はできません。"
            //                    + "\n" + "他のキャプチャソフトを終了してからお試しください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    // 終了（フォーム表示していないとうまく終了しない）
            //    this.Show();
            //    Application.Exit();
            //}
            ////メモウィンドウフォームの宣言
            //Class_Shared.fPreview = new FormPreview();
            //SetControlState();
        }
    }

}