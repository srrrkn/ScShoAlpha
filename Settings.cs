using System;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Win32;
using System.Windows.Forms;

namespace ScShoAlpha
{
    [Serializable()]
    public class Settings
    {
        // 撮影後デスクトップ保存フラグ
        private bool _SaveDesktopMode = false;
        // 保存形式(デフォルトはBMP)
        private string _SaveModeType = DFN.MODE_PNG;
        // 保存ファイル名
        private string _SaveFileName = DFN.DEFAULT_FILENAME;
        // 撮影後デスクトップ保存フラグ
        private bool _CaptureCursorMode = false;
        // 撮影後印刷フラグ
        private bool _PrintCaptureMode = false;
        // 撮影後簡易編集フラグ
        private bool _EditCapture = false;
        // エラーメッセージ
        private string _ErrMessage = "";
        // これまでの起動回数
        private int _RunCount = 0;
        // 前回の印刷向き(false:縦、true:横)
        private bool _PageSettingsLandscape = false;
        // スタートアップ登録
        private bool _RegisterStartUp = false;
        //カスタム図形
        private ClsFigure _customFigure01 = new ClsFigure();
        private ClsFigure _customFigure02 = new ClsFigure();
        private ClsFigure _customFigure03 = new ClsFigure();
        private ClsFigure _customFigure04 = new ClsFigure();

        // 設定のプロパティ
        public bool SaveDesktopMode
        {
            get
            {
                return _SaveDesktopMode;
            }
            set
            {
                _SaveDesktopMode = value;
            }
        }

        public string SaveModeType
        {
            get
            {
                return _SaveModeType;
            }
            set
            {
                _SaveModeType = value;
            }
        }

        public string SaveFileName
        {
            get
            {
                return _SaveFileName;
            }
            set
            {
                _SaveFileName = value;
            }
        }

        public bool CaptureCursorMode
        {
            get
            {
                return _CaptureCursorMode;
            }
            set
            {
                _CaptureCursorMode = value;
            }
        }

        public bool PrintCaptureMode
        {
            get
            {
                return _PrintCaptureMode;
            }
            set
            {
                _PrintCaptureMode = value;
            }
        }

        public string ErrMessage
        {
            get
            {
                return _ErrMessage;
            }
            set
            {
                _ErrMessage = value;
            }
        }

        public int RunCount
        {
            get
            {
                return _RunCount;
            }
            set
            {
                _RunCount = value;
            }
        }

        public bool PageSettingsLandscape
        {
            get
            {
                return _PageSettingsLandscape;
            }
            set
            {
                _PageSettingsLandscape = value;
            }
        }

        public bool RegisterStartUp
        {
            get
            {
                return _RegisterStartUp;
            }
            set
            {
                _RegisterStartUp = value;
            }
        }

        public bool EditCapture
        {
            get
            {
                return _EditCapture;
            }
            set
            {
                _EditCapture = value;
            }
        }

        public ClsFigure CustomFigure01
        {
            get
            {
                return _customFigure01;
            }
            set
            {
                _customFigure01 = value;
            }
        }

        public ClsFigure CustomFigure02
        {
            get
            {
                return _customFigure02;
            }
            set
            {
                _customFigure02 = value;
            }
        }

        public ClsFigure CustomFigure03
        {
            get
            {
                return _customFigure03;
            }
            set
            {
                _customFigure03 = value;
            }
        }

        public ClsFigure CustomFigure04
        {
            get
            {
                return _customFigure04;
            }
            set
            {
                _customFigure04 = value;
            }
        }

        // UTF-8でXMLを書き込むためのStringWriter
        private class StringWriterUTF8 : StringWriter
        {
            public override System.Text.Encoding Encoding
            {
                get
                {
                    return System.Text.Encoding.UTF8;
                }
            }
        }


        // コンストラクタ
        public Settings()
        {
            // 撮影後デスクトップ保存フラグ
            _SaveDesktopMode = false;
            // 保存形式(デフォルトはBMP)
            _SaveModeType = DFN.MODE_BMP;
            // 保存ファイル名
            _SaveFileName = DFN.DEFAULT_FILENAME;
            // 撮影後デスクトップ保存フラグ
            _CaptureCursorMode = false;
            // 撮影後印刷フラグ
            _PrintCaptureMode = false;
        }

        // Settingsクラスのただ一つのインスタンス
        [NonSerialized()]
        private static Settings _instance;
        [System.Xml.Serialization.XmlIgnore()]
        public static Settings Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Settings();
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        /// <summary>
        ///     ''' 設定をXMLファイルから読み込み復元する
        ///     ''' </summary>
        public static void LoadFromXmlFile()
        {
            StreamReader sr = null;
            try
            {
                string p = GetSettingPath();

                sr = new StreamReader(p, new UTF8Encoding(false));
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(Settings));
                // 読み込んで逆シリアル化する
                object obj = xs.Deserialize(sr);
                sr.Close();
                Instance = (Settings)obj;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (sr != null)
                {
                    sr.Dispose();
                }
            }
        }


        /// <summary>
        ///     ''' 現在の設定をXMLファイルに保存する
        ///     ''' </summary>
        public static void SaveToXmlFile()
        {
            StreamWriter sw = null;
            try
            {
                string p = GetSettingPath();

                sw = new StreamWriter(p, false, new UTF8Encoding(false));
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(Settings));
                // シリアル化して書き込む
                xs.Serialize(sw, Instance);
                sw.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (sw != null)
                {
                    sw.Dispose();
                }
            }
        }

        /// <summary>
        ///     ''' 設定のXMLファイルを取得
        ///     ''' </summary>
        ///     ''' <returns></returns>
        ///     ''' <remarks></remarks>
        public static string GetXmlCode()
        {
            System.IO.StringWriter SWtr = new StringWriterUTF8();
            try
            {
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(Settings));
                // シリアル化して書き込む
                xs.Serialize(SWtr, Instance);
                SWtr.Close();
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
            return SWtr.ToString();
        }

        /// <summary>
        ///     ''' 設定をバイナリファイルから読み込み復元する
        ///     ''' </summary>
        public static void LoadFromBinaryFile()
        {
            FileStream fs = null;
            try
            {
                string p = GetSettingPath();

                fs = new FileStream(p, FileMode.Open, FileAccess.Read);
                BinaryFormatter bf = new BinaryFormatter();
                // 読み込んで逆シリアル化する
                object obj = bf.Deserialize(fs);
                fs.Close();

                Instance = (Settings)obj;
            }
            catch (Exception ex)
            {
                Instance.ErrMessage = ex.ToString();
            }
            finally
            {
                if (fs != null)
                {
                    fs.Dispose();
                }
            }
        }

        /// <summary>
        ///     ''' 現在の設定をバイナリファイルに保存する
        ///     ''' </summary>
        public static void SaveToBinaryFile()
        {
            FileStream fs = null;
            try
            {
                string p = GetSettingPath();

                fs = new FileStream(p, FileMode.Create, FileAccess.Write);
                BinaryFormatter bf = new BinaryFormatter();
                // シリアル化して書き込む
                bf.Serialize(fs, Instance);
                fs.Close();
            }
            catch (Exception ex)
            {
                Instance.ErrMessage = ex.ToString();
            }
            finally
            {
                if (fs != null)
                {
                    fs.Dispose();
                }
            }
        }

        /// <summary>
        ///     ''' 設定をレジストリから読み込み復元する
        ///     ''' </summary>
        public static void LoadFromRegistry()
        {
            MemoryStream ms = null;
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                // レジストリから読み込む
                RegistryKey reg = GetSettingRegistry();
                byte[] bs = (byte[])reg.GetValue("");
                // 逆シリアル化して復元
                ms = new MemoryStream(bs, false);
                Instance = (Settings)bf.Deserialize(ms);
                // 閉じる
                ms.Close();
                reg.Close();
            }
            catch (Exception ex)
            {
                Instance.ErrMessage = ex.ToString();
            }
            finally
            {
                if (ms != null)
                {
                    ms.Dispose();
                }
            }
        }

        /// <summary>
        ///     ''' 現在の設定をレジストリに保存する
        ///     ''' </summary>
        public static void SaveToRegistry()
        {
            MemoryStream ms = null;
            try
            {
                ms = new MemoryStream();
                BinaryFormatter bf = new BinaryFormatter();
                // シリアル化してMemoryStreamに書き込む
                bf.Serialize(ms, Instance);
                // レジストリへ保存する
                RegistryKey reg = GetSettingRegistry();
                reg.SetValue("", ms.ToArray());
                // 閉じる
                ms.Close();
                reg.Close();
            }
            catch (Exception ex)
            {
                Instance.ErrMessage = ex.ToString();
            }
            finally
            {
                if (ms != null)
                {
                    ms.Dispose();
                }
            }
        }

        /// <summary>
        ///     ''' ディレクトリの作成
        ///     ''' </summary>
        public static void CreateDirectory()
        {
            try
            {
                System.IO.Directory.CreateDirectory(GetDirectoryPath());
            }
            catch (Exception ex)
            {
                Instance.ErrMessage = ex.ToString();
                MessageBox.Show("ディレクトリを作成できませんでした。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///     ''' データ保存ディレクトリの取得
        ///     ''' </summary>
        ///     ''' <returns></returns>
        ///     ''' <remarks></remarks>
        public static string GetDirectoryPath()
        {
            string p = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.CompanyName + @"\" + Application.ProductName);
            return p;
        }

        /// <summary>
        ///     ''' 設定ファイルのパスの取得
        ///     ''' </summary>
        ///     ''' <returns></returns>
        ///     ''' <remarks></remarks>
        public static string GetSettingPath()
        {
            string p = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.CompanyName + @"\" + Application.ProductName
               + @"\" + Application.ProductName + ".config");
            return p;
        }

        /// <summary>
        ///     ''' 設定保存先レジストリの取得
        ///     ''' </summary>
        ///     ''' <returns></returns>
        ///     ''' <remarks></remarks>
        private static RegistryKey GetSettingRegistry()
        {
            RegistryKey reg = Registry.CurrentUser.CreateSubKey((@"Software\" + Application.CompanyName + @"\" + Application.ProductName));
            return reg;
        }
    }


}
