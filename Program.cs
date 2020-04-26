using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data;

namespace ScShoAlpha
{
    static class Program
    {
        /// <summary>
        ///     ''' アプリケーションのメイン エントリ ポイントです。
        ///     ''' </summary>
        [STAThread()]
        public static void Main()
        {
            // Mutex名を決める（必ずアプリケーション固有の文字列に変更すること！）
            string mutexName = Application.CompanyName + "_" + Application.ProductName;
            // Mutexオブジェクトを作成する
            System.Threading.Mutex mutex = new System.Threading.Mutex(false, mutexName);

            bool hasHandle = false;
            try
            {
                try
                {
                    // ミューテックスの所有権を要求する
                    hasHandle = mutex.WaitOne(0, false);
                }
                // .NET Framework 2.0以降の場合
                catch (System.Threading.AbandonedMutexException ex)
                {
                    // 別のアプリケーションがミューテックスを解放しないで終了した時
                    hasHandle = true;
                }
                // ミューテックスを得られたか調べる
                if (hasHandle == false)
                {
                    // 得られなかった場合は、すでに起動していると判断して終了
                    MessageBox.Show("多重起動はできません。");
                    return;
                }
                // はじめからMainメソッドにあったコードを実行
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // 保存ディレクトリがない場合、ディレクトリを作成
                Settings.CreateDirectory();
                // 設定データを読込
                Settings.LoadFromXmlFile();

                Settings.Instance.RunCount = Settings.Instance.RunCount + 1;
                Settings.SaveToXmlFile();
                // フォーム(Form_Main)のインスタンスを作成
                FormMain fMain = new FormMain();
                // プライマリスクリーンの中央に表示
                fMain.StartPosition = FormStartPosition.Manual;
                fMain.Top = (int)(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / (double)2 - fMain.Height / (double)2);
                fMain.Left = (int)(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / (double)2 - fMain.Width / (double)2);
                fMain.Show();
                fMain.SetControlState();
                fMain.Visible = false;
                // メッセージループを開始する
                Application.Run();
            }
            finally
            {
                if (hasHandle)
                    // ミューテックスを解放する
                    mutex.ReleaseMutex();
                mutex.Close();
            }
        }
    }
}
