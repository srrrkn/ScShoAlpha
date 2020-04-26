using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace ScShoAlpha
{
    public partial class FormPaint : Form
    {

        AlphaControls.AlphaRichTextBox inputTextBox = new AlphaControls.AlphaRichTextBox();

        public FormPaint() {
            // この呼び出しは、Windows フォーム デザイナで必要です。
            InitializeComponent();

            // InitializeComponent() 呼び出しの後で初期化を追加します。
        }

        //透明カラー
        private Color TransparentColor = Color.FromArgb(0, 0, 0, 0);
        //枠カラー
        private Color lineColor = Color.FromArgb(255, 255, 0, 0);
        //塗りつぶしカラー
        private Color brushColor = Color.FromArgb(0, 255, 255, 255);
        // テキストカラー
        private Color textColor = Color.FromArgb(255, 255, 0, 0);
        //テキストフォント
        private Font textFont = new System.Drawing.Font("Comic Sans MS", 
            15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 
            ((System.Byte)(0)));

        //テキスト用ブラシ
        private Brush textBrush = new SolidBrush(Color.FromArgb(0, 0, 0, 0));
        //テキスト用ペン
        private Pen textPen = new Pen(Color.FromArgb(0, 0, 0, 0), 1);
        // テキストカラー
        private Color paintTextColor = Color.FromArgb(255, 255, 0, 0);
        //テキストフォント
        private Font paintTextFont = new System.Drawing.Font("Comic Sans MS",
            15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point,
            ((System.Byte)(0)));
        // 範囲描画用ペン
        private Pen blacDotPen = new Pen(Color.Black, 1);
        // 描画用ペン
        private Pen paintPen = new Pen(Color.Red, 3);
        // 塗りつぶし用ブラシ
        private SolidBrush paintBrush = new SolidBrush(Color.White);

        // 描画フラグ
        private bool isPainting = false;
        // 四角形描画
        private bool isPaintingRectangle = false;
        // 楕円描画
        private bool isPaintingEllipse = false;
        //テキスト描画
        private bool isPaintingText = false;
        // テキスト入力
        private bool isInputtingText = false;
        // ライン描画
        private bool isPaintingLine = false;
        //矢印描画
        private bool isPaintingArrow = false;
        // ﾌﾘｰﾊﾝﾄﾞ描画
        private bool isPaintingFreehand = false;

        // ドラッグフラグ
        private bool isDragging = false;
        private Point startPoint = new Point();
        private Point endPoint = new Point();
        private Point nowPoint = new Point();
        private List<int> freeHandX = new List<int>();
        private List<int> freeHandY = new List<int>();

        //最後に使った図形設定
        private ClsFigure lastFigure = new ClsFigure();

        // パーツリスト
        private List<ClsFigure> figList = new List<ClsFigure>();
        //REDO用パーツリスト
        private List<ClsFigure> reFigList = new List<ClsFigure>();

        // コントロールの透明化
        private void ControlsTransparent(Control obj1, Control obj2)
        {
            obj2.Controls.Add(obj1);
            obj1.Top = obj1.Top - obj2.Top;
            obj1.Left = obj1.Left - obj2.Left;
        }

        // コントロールのDoubleBufferedプロパティをTrueに
        public static void EnableDoubleBuffering(Control control)
        {
            control.GetType().InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null/* TODO Change to default(_) if this is not a reference type */, control, new object[] { true });
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormPaint_Load(System.Object sender, System.EventArgs e)
        {
            // PanelのダブルバッファをTrueに
            EnableDoubleBuffering(PanelPaint);
            // PictureBoxの上に透明のパネルを配置
            ControlsTransparent(PanelPaint, PictureBoxCapture);
            // 範囲描画用のペンの設定
            blacDotPen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
            // ペンの太さ初期値設定
            ComboBoxLineSize.SelectedIndex = 2;
            // ペンの太さ初期値設定
            ComboBoxFontSize.SelectedIndex = 7;

            // 描画用ペン
            paintPen = new Pen(lineColor, 3);
            // 塗りつぶし用ブラシ
            paintBrush = new SolidBrush(brushColor);

            //初期選択色の設定
            panelLineColor.BackColor = lineColor;
            panelBrushColor.BackColor = brushColor;
            panelTextColor.BackColor = textColor;
            //カスタム図形のロード
            setCustomFigure();
        }

        #region "描画処理"

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PanelPaint_Paint(System.Object sender, System.Windows.Forms.PaintEventArgs e)
        {
            // パーツ描画
            for (int i = 0; i <= figList.Count - 1; i++)
                figList[i].Draw(e.Graphics);
            // ドラッグ中は長方形描画
            if (isDragging)
            {
                if (!isPaintingFreehand)
                {
                    // 範囲の描画
                    // |
                    e.Graphics.DrawLine(blacDotPen, startPoint.X, startPoint.Y, startPoint.X, nowPoint.Y);
                    // |
                    e.Graphics.DrawLine(blacDotPen, nowPoint.X, startPoint.Y, nowPoint.X, nowPoint.Y);
                    // ￣
                    e.Graphics.DrawLine(blacDotPen, startPoint.X, startPoint.Y, nowPoint.X, startPoint.Y);
                    // ＿
                    e.Graphics.DrawLine(blacDotPen, startPoint.X, nowPoint.Y, nowPoint.X, nowPoint.Y);
                }
                // 仮描画
                if (isPaintingRectangle)
                {
                    int leftTopX = startPoint.X > nowPoint.X ? nowPoint.X : startPoint.X;
                    int leftTopY = startPoint.Y > nowPoint.Y ? nowPoint.Y : startPoint.Y;
                    ClsRectangle rect = new ClsRectangle(paintPen, paintBrush, 
                        leftTopX, leftTopY, 
                        Math.Abs(nowPoint.X - startPoint.X), Math.Abs(nowPoint.Y - startPoint.Y));
                    rect.Draw(e.Graphics);
                }
                else if (isPaintingEllipse)
                {
                    int leftTopX = startPoint.X > nowPoint.X ? nowPoint.X : startPoint.X;
                    int leftTopY = startPoint.Y > nowPoint.Y ? nowPoint.Y : startPoint.Y;
                    ClsEllipse elps = new ClsEllipse(paintPen, paintBrush, 
                        leftTopX, leftTopY, 
                        Math.Abs(nowPoint.X - startPoint.X), Math.Abs(nowPoint.Y - startPoint.Y));
                    elps.Draw(e.Graphics);
                }
                else if (isPaintingLine)
                {
                    ClsFigure line = new ClsLine(paintPen, 
                        startPoint.X, startPoint.Y, 
                        nowPoint.X, nowPoint.Y);
                    line.Draw(e.Graphics);
                }
                else if (isPaintingArrow)
                {
                    ClsFigure arrow = new ClsArrow(paintPen,
                        startPoint.X, startPoint.Y,
                        nowPoint.X, nowPoint.Y);
                    arrow.Draw(e.Graphics);
                }
                else if(isPaintingFreehand){
                    ClsFigure freehand = new ClsFreehand(paintPen, freeHandX, freeHandY);
                    freehand.Draw(e.Graphics);
                }
            }
        }

        /// <summary>
        /// パネル上の図形をImageに転写
        /// </summary>
        /// <returns></returns>
        private Bitmap CreateImage()
        {
            //描画先とするImageオブジェクトを作成する
            Bitmap canvas = new Bitmap(PictureBoxCapture.Image);
            //ImageオブジェクトのGraphicsオブジェクトを作成する
            Graphics g = Graphics.FromImage(canvas);

            // パーツ描画
            for (int i = 0; i <= figList.Count - 1; i++)
                figList[i].Draw(g);

            //Graphicsオブジェクトのリソースを解放する
            g.Dispose();

            return canvas;
        }

        /// <summary>
        ///     ''' マウスエンター
        ///     ''' </summary>
        ///     ''' <param name="sender"></param>
        ///     ''' <param name="e"></param>
        ///     ''' <remarks></remarks>
        private void PanelPaint_MouseEnter(System.Object sender, System.EventArgs e)
        {
            if (isPainting)
                PanelPaint.Cursor = Cursors.Cross;
            else
                PanelPaint.Cursor = Cursors.Arrow;
        }

        /// <summary>
        ///     ''' マウス移動
        ///     ''' </summary>
        ///     ''' <param name="sender"></param>
        ///     ''' <param name="e"></param>
        ///     ''' <remarks></remarks>
        private void PanelPaint_MouseMove(System.Object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // 現在の位置の保持と表示
            LabelXY.Text = e.X + "," + e.Y;
            nowPoint.X = e.X;
            nowPoint.Y = e.Y;

            //ﾌﾘｰﾊﾝﾄﾞ描画でドラッグ中は座標保存
            if (isDragging && isPaintingFreehand) {
                freeHandX.Add(e.X);
                freeHandY.Add(e.Y);
            }

            PanelPaint.Refresh();
        }

        /// <summary>
        ///     ''' マウスボタンダウン
        ///     ''' </summary>
        ///     ''' <param name="sender"></param>
        ///     ''' <param name="e"></param>
        ///     ''' <remarks></remarks>
        private void PanelPaint_MouseDown(System.Object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // 左ボタン
            if (e.Button == MouseButtons.Left)
            {
                // ドラッグ開始
                if (isPainting)
                {
                    isDragging = true;
                    startPoint.X = e.X;
                    startPoint.Y = e.Y;
                    //ﾌﾘｰﾊﾝﾄﾞ描画の場合、座標記録開始
                    if (isPaintingFreehand) { 
                        freeHandX.Clear();
                        freeHandY.Clear();
                    }
                }
            }
        }

        /// <summary>
        ///     ''' マウスボタンアップ
        ///     ''' </summary>
        ///     ''' <param name="sender"></param>
        ///     ''' <param name="e"></param>
        ///     ''' <remarks></remarks>
        private void PanelPaint_MouseUp(System.Object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // 
            if (e.Button == MouseButtons.Left)
            {
                // ドラッグ終了
                if (isPainting)
                {
                    isDragging = false;
                    endPoint.X = e.X;
                    endPoint.Y = e.Y;
                }
                //文字入力終了
                if (isInputtingText)
                {
                    if (checkBoxBrush.Checked) {
                        textBrush = paintBrush;
                    }else {
                        textBrush = new SolidBrush(TransparentColor);
                    }
                    if (checkBoxLine.Checked)
                    {
                        textPen = paintPen;
                    }
                    else {
                        textPen = new Pen(TransparentColor, 1);
                    }
                    ClsFigure gText = new ClsGText(textPen, textBrush, paintTextFont, paintTextColor,
                        inputTextBox.Left, inputTextBox.Top,
                        inputTextBox.Width, inputTextBox.Height, inputTextBox.Text);
                    inputTextBox.Dispose();
                    isPainting = false;
                    isInputtingText = false;
                    figList.Add(gText);
                    reFigList.Clear();
                    PanelPaint.Refresh();
                    lastFigure = gText.castClassFigure();
                }
                // パーツ確定
                if (isPaintingRectangle)
                {
                    int leftTopX = startPoint.X > endPoint.X ? endPoint.X : startPoint.X;
                    int leftTopY = startPoint.Y > endPoint.Y ? endPoint.Y : startPoint.Y;
                    ClsFigure rect = new ClsRectangle(paintPen, paintBrush, 
                        leftTopX, leftTopY, 
                        Math.Abs(endPoint.X - startPoint.X), Math.Abs(endPoint.Y - startPoint.Y));
                    figList.Add(rect);
                    reFigList.Clear();
                    PanelPaint.Refresh();
                    lastFigure = rect.castClassFigure();
                }
                else if (isPaintingEllipse)
                {
                    int leftTopX = startPoint.X > endPoint.X ? endPoint.X : startPoint.X;
                    int leftTopY = startPoint.Y > endPoint.Y ? endPoint.Y : startPoint.Y;
                    ClsFigure elps = new ClsEllipse(paintPen, paintBrush, 
                        leftTopX, leftTopY, 
                        Math.Abs(endPoint.X - startPoint.X), Math.Abs(endPoint.Y - startPoint.Y));
                    figList.Add(elps);
                    reFigList.Clear();
                    PanelPaint.Refresh();
                    lastFigure = elps.castClassFigure();
                }
                else if (isPaintingLine)
                {
                    ClsFigure line = new ClsLine(paintPen, 
                        startPoint.X, startPoint.Y, 
                        endPoint.X, endPoint.Y);
                    figList.Add(line);
                    reFigList.Clear();
                    PanelPaint.Refresh();
                    lastFigure = line.castClassFigure();
                }
                else if (isPaintingArrow)
                {
                    ClsFigure arrow = new ClsArrow(paintPen,
                        startPoint.X, startPoint.Y,
                        endPoint.X, endPoint.Y);
                    figList.Add(arrow);
                    reFigList.Clear();
                    PanelPaint.Refresh();
                    lastFigure = arrow.castClassFigure();
                }
                else if (isPaintingText)
                {
                    //入力用リッチテキストボックスの設定
                    inputTextBox = new AlphaControls.AlphaRichTextBox();
                    if (checkBoxBrush.Checked && brushColor.A > 0)
                    {
                        inputTextBox.BackAlpha = brushColor.A;
                        inputTextBox.BackColor = Color.FromArgb(brushColor.R, brushColor.G, brushColor.B);
                    }
                    else
                    {
                        inputTextBox.BackAlpha = 0;
                        inputTextBox.BackColor = Color.White;
                    }
                    inputTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
                    inputTextBox.Font = paintTextFont;
                    inputTextBox.ForeColor = paintTextColor;
                    inputTextBox.Location = new System.Drawing.Point(startPoint.X, startPoint.Y);
                    inputTextBox.Multiline = true;
                    inputTextBox.Name = "inputTextBox";
                    int width = (endPoint.X != startPoint.X) ? (int)Math.Abs(endPoint.X - startPoint.X) : 320;
                    inputTextBox.Size = new System.Drawing.Size(
                        width,
                        (int)(TextRenderer.MeasureText("あ", paintTextFont).Height + inputTextBox.Margin.Vertical));
                    inputTextBox.TabIndex = 0;
                    inputTextBox.ImeMode = ImeMode.Off;
                    inputTextBox.TextChanged += new EventHandler(inputTextBox_TextChanged);
                    ControlsTransparent(inputTextBox,PanelPaint);
                    //強制的にフォーカス
                    inputTextBox.Focus();
                    isPaintingText = false;
                    isInputtingText = true;
                }
                else if (isPaintingFreehand) {
                    ClsFigure freehand = new ClsFreehand(paintPen,freeHandX,freeHandY);
                    figList.Add(freehand);
                    reFigList.Clear();
                    PanelPaint.Refresh();
                    lastFigure = freehand.castClassFigure();
                }
            }
        }

        // 文字の出現回数をカウント
        public static int CountChar(string s, char c)
        {
            return s.Length - s.Replace(c.ToString(), "").Length;
        }

        /// <summary>
        /// 行数によってリッチテキストボックスの高さを調整する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inputTextBox_TextChanged(object sender, EventArgs e)
        {
            int cntCRLF = CountChar(inputTextBox.Text, '\n');
            int height = (int)((TextRenderer.MeasureText("あ", paintTextFont).Height + inputTextBox.Margin.Vertical / 2) * (cntCRLF + 1));

            if (height > (int)(TextRenderer.MeasureText("あ", paintTextFont).Height + inputTextBox.Margin.Vertical))
            {
                inputTextBox.Height = height;
            }
            else {
                inputTextBox.Height = (int)(TextRenderer.MeasureText("あ", paintTextFont).Height + inputTextBox.Margin.Vertical);
            }
        }
        #endregion

        #region "描画設定"

        /// <summary>
        /// 線色変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelLineColor_Click(System.Object sender, System.EventArgs e)
        {
            FormSelectColor fSelClr = new FormSelectColor(lineColor);
            fSelClr.ShowDialog();
            panelLineColor.BackColor = fSelClr.nowColor;
            lineColor = fSelClr.nowColor;
            paintPen.Color = lineColor;
            fSelClr.Dispose();
        }

        /// <summary>
        /// 塗りつぶし色変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelBrushColor_Click(System.Object sender, System.EventArgs e)
        {
            FormSelectColor fSelClr = new FormSelectColor(brushColor);
            fSelClr.ShowDialog();
            panelBrushColor.BackColor = fSelClr.nowColor;
            brushColor = fSelClr.nowColor;
            paintBrush.Color = brushColor;
            fSelClr.Dispose();
        }

        /// <summary>
        /// テキスト色変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelTextColor_Click(object sender, EventArgs e)
        {
            FormSelectColor fSelClr = new FormSelectColor(textColor);
            fSelClr.ShowDialog();
            panelTextColor.BackColor = fSelClr.nowColor;
            textColor = fSelClr.nowColor;
            paintTextColor = textColor;
            fSelClr.Dispose();
        }

        /// <summary>
        /// 線の太さ変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxLineSize_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            paintPen.Width = float.Parse(ComboBoxLineSize.SelectedItem.ToString());
        }

        /// <summary>
        /// フォントサイズ変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {

            textFont = new System.Drawing.Font(textFont.FontFamily, 
                float.Parse(ComboBoxFontSize.SelectedItem.ToString()));
            paintTextFont = textFont;
        }

        #endregion

        #region "便利機能"
        /// <summary>
        /// UNDO処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUndo_Click(object sender, EventArgs e)
        {
            if (figList.Count == 0) return;
            reFigList.Add(figList[figList.Count - 1]);
            figList.RemoveAt(figList.Count - 1);
            PanelPaint.Refresh();
        }

        /// <summary>
        /// REDO処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRedo_Click(object sender, EventArgs e)
        {
            if (reFigList.Count == 0) return;
            figList.Add(reFigList[reFigList.Count - 1]);
            reFigList.RemoveAt(reFigList.Count - 1);
            PanelPaint.Refresh();
        }

        /// <summary>
        /// クリップボードに保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSaveCB_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(new DataObject());
            Clipboard.SetImage(CreateImage());
        }

        /// <summary>
        /// プリンタに出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonPrint_Click(object sender, EventArgs e)
        {
            Image img = CreateImage();
            // プレビューフォームをプライマリスクリーンの中央に表示
            FormPrintPreview fPrintPreview = new FormPrintPreview();
            fPrintPreview.StartPosition = FormStartPosition.Manual;
            fPrintPreview.Top = (int)(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / (double)2 - fPrintPreview.Height / (double)2);
            fPrintPreview.Left = (int)(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / (double)2 - fPrintPreview.Width / (double)2);
            // 画像をディープコピー
            fPrintPreview.memoryImage = (Bitmap)img.Clone();
            // プレビューフォームを表示
            fPrintPreview.ShowDialog();
        }

        /// <summary>
        /// 画像の保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            Image img = CreateImage();
            // SaveFileDialogクラスのインスタンスを作成
            SaveFileDialog sfd = new SaveFileDialog();
            // はじめのファイル名を指定する
            // はじめに「ファイル名」で表示される文字列を指定する
            sfd.FileName = Settings.Instance.SaveFileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
            // はじめに表示されるフォルダを指定する
            // 指定しない（空の文字列）の時は、現在のディレクトリが表示される
            // sfd.InitialDirectory = "C:\"
            // [ファイルの種類]に表示される選択肢を指定する
            sfd.Filter = "(*.bmp)|*.bmp|(*.jpeg)|*.jpeg|(*.png)|*.png";
            // [ファイルの種類]ではじめに選択されるものを指定する
            // 2番目の「すべてのファイル」が選択されているようにする
            sfd.FilterIndex = 3;
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
                if (sfd.FileName.Substring(sfd.FileName.Length - 4, 4) == ".bmp") {
                    ClsImageSave.saveImage(0, sfd.FileName, img);
                }
                else if (sfd.FileName.Substring(sfd.FileName.Length - 5, 5) == ".jpeg")
                {
                    ClsImageSave.saveImage(1, sfd.FileName, img);
                }
                else if (sfd.FileName.Substring(sfd.FileName.Length - 4, 4) == ".png")
                {
                    ClsImageSave.saveImage(2, sfd.FileName, img);
                }
                
            }
        }

        #endregion

        #region "描画フラグ制御"

        /// <summary>
        /// 全描画ﾓｰﾄﾞをオフ
        /// </summary>
        private void setPaintOff()
        {
            isPainting = false;
            isPaintingFreehand = false;
            isPaintingRectangle = false;
            isPaintingEllipse = false;
            isPaintingLine = false;
            isPaintingText = false;
            isPaintingArrow = false;
        }

        /// <summary>
        /// 色設定
        /// </summary>
        private void setSettingColor()
        {
            paintBrush.Color = brushColor;
            paintPen.Color = lineColor;
            paintPen.Width = int.Parse(ComboBoxLineSize.SelectedItem.ToString());
            paintTextColor = textColor;
            paintTextFont = textFont;
        }

        /// <summary>
        /// フリーハンド描画のフラグ制御
        /// </summary>
        private void setPaintFreeHand() {
            setPaintOff();
            if (isPainting == false)
            {
                isPainting = true;
                isPaintingFreehand = true;
            }
        }

        /// <summary>
        /// 長方形描画のフラグ制御
        /// </summary>
        private void setPaintRectangle()
        {
            setPaintOff();
            isPainting = true;
            isPaintingRectangle = true;
        }

        /// <summary>
        /// 楕円描画のフラグ制御
        /// </summary>
        private void setPaintEllipse()
        {
            setPaintOff();
            isPainting = true;
            isPaintingEllipse = true;
        }

        /// <summary>
        /// 直線描画のフラグ制御
        /// </summary>
        private void setPaintLine()
        {
            setPaintOff();
            isPainting = true;
            isPaintingLine = true;
        }

        /// <summary>
        /// 矢印描画のフラグ制御
        /// </summary>
        private void setPaintArrow()
        {
            setPaintOff();
            isPainting = true;
            isPaintingArrow = true;
        }

        /// <summary>
        /// テキスト描画のフラグ制御
        /// </summary>
        private void setPaintText()
        {
            setPaintOff();
            isPainting = true;
            isPaintingText = true;
        }

        #endregion

        #region "描画ボタン"

        /// <summary>
        /// ﾌﾘｰﾊﾝﾄﾞ描画ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFreehand_Click(object sender, EventArgs e)
        {
            setSettingColor();
            setPaintFreeHand();
        }

        /// <summary>
        ///     ''' 長方形描画ボタン
        ///     ''' </summary>
        ///     ''' <param name="sender"></param>
        ///     ''' <param name="e"></param>
        ///     ''' <remarks></remarks>
        private void ButtonRectangle_Click(System.Object sender, System.EventArgs e)
        {
            setSettingColor();
            setPaintRectangle();
        }

        /// <summary>
        ///     ''' 楕円描画ボタン
        ///     ''' </summary>
        ///     ''' <param name="sender"></param>
        ///     ''' <param name="e"></param>
        ///     ''' <remarks></remarks>
        private void ButtonEllipse_Click(System.Object sender, System.EventArgs e)
        {
            setSettingColor();
            setPaintEllipse();
        }

        /// <summary>
        ///     ''' 直線描画
        ///     ''' </summary>
        ///     ''' <param name="sender"></param>
        ///     ''' <param name="e"></param>
        ///     ''' <remarks></remarks>
        private void ButtonLine_Click(System.Object sender, System.EventArgs e)
        {
            setSettingColor();
            setPaintLine();
        }

        /// <summary>
        /// 矢印描画
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonArrow_Click(object sender, EventArgs e)
        {
            setSettingColor();
            setPaintArrow();
        }

        /// <summary>
        ///     ''' テキスト入力ボタン
        ///     ''' </summary>
        ///     ''' <param name="sender"></param>
        ///     ''' <param name="e"></param>
        ///     ''' <remarks></remarks>
        private void ButtonText_Click(System.Object sender, System.EventArgs e)
        {
            setSettingColor();
            setPaintText();
        }

        #endregion

        #region "カスタム図形関連"
        /// <summary>
        /// カスタム図形の場合のペイントフラグ制御
        /// </summary>
        /// <param name="figCat"></param>
        private void setPaintFlag(string figCat)
        {
            switch (figCat)
            {
                case "gtext":
                    setPaintText();
                    break;
                case "line":
                    setPaintLine();
                    break;
                case "ellipse":
                    setPaintEllipse();
                    break;
                case "rectangle":
                    setPaintRectangle();
                    break;
                case "arrow":
                    setPaintArrow();
                    break;
                case "freehand":
                    setPaintFreeHand();
                    break;
            }
        }

        /// <summary>
        /// カスタム図形の色設定
        /// </summary>
        /// <param name="figure"></param>
        private void setCustomColor(ClsFigure figure)
        {
            paintBrush.Color = figure.fillColor;
            paintPen.Color = figure.lineColor;
            paintPen.Width = figure.lineWidth;
            paintTextColor = figure.textColor;
            paintTextFont = figure.textFont;
        }

        /// <summary>
        /// カスタム図形設定時のボタンアイコン変更
        /// </summary>
        /// <param name="figCat"></param>
        /// <param name="bt"></param>
        private void setButtonImage(string figCat, Button bt)
        {
            System.ComponentModel.ComponentResourceManager resources 
                = new System.ComponentModel.ComponentResourceManager(typeof(FormPaint));
            switch (figCat)
            {
                case "gtext":
                    bt.Image = ((System.Drawing.Image)(resources.GetObject("ButtonText.Image")));
                    break;
                case "line":
                    bt.Image = ((System.Drawing.Image)(resources.GetObject("ButtonLine.Image")));
                    break;
                case "ellipse":
                    bt.Image = ((System.Drawing.Image)(resources.GetObject("ButtonEllipse.Image")));
                    break;
                case "rectangle":
                    bt.Image = ((System.Drawing.Image)(resources.GetObject("ButtonRectangle.Image")));
                    break;
                case "arrow":
                    bt.Image = ((System.Drawing.Image)(resources.GetObject("buttonArrow.Image")));
                    break;
                case "freehand":
                    bt.Image = ((System.Drawing.Image)(resources.GetObject("buttonFreehand.Image")));
                    break;
            }
        }

        /// <summary>
        /// カスタム図形の色をパネルに設定
        /// </summary>
        private void setCustomFigure() {
            if (Settings.Instance.CustomFigure01.figCat != "")
            {
                panelLineCustom01.BackColor = Settings.Instance.CustomFigure01.lineColor;
                panelBrushCustom01.BackColor = Settings.Instance.CustomFigure01.fillColor;
                setButtonImage(Settings.Instance.CustomFigure01.figCat, buttonCustom01);
            }
            if (Settings.Instance.CustomFigure02.figCat != "")
            {
                panelLineCustom02.BackColor = Settings.Instance.CustomFigure02.lineColor;
                panelBrushCustom02.BackColor = Settings.Instance.CustomFigure02.fillColor;
                setButtonImage(Settings.Instance.CustomFigure02.figCat, buttonCustom02);
            }
            if (Settings.Instance.CustomFigure03.figCat != "")
            {
                panelLineCustom03.BackColor = Settings.Instance.CustomFigure03.lineColor;
                panelBrushCustom03.BackColor = Settings.Instance.CustomFigure03.fillColor;
                setButtonImage(Settings.Instance.CustomFigure03.figCat, buttonCustom03);
            }
            if (Settings.Instance.CustomFigure04.figCat != "")
            {
                panelLineCustom04.BackColor = Settings.Instance.CustomFigure04.lineColor;
                panelBrushCustom04.BackColor = Settings.Instance.CustomFigure04.fillColor;
                setButtonImage(Settings.Instance.CustomFigure04.figCat, buttonCustom04);
            }
        }

        /// <summary>
        /// カスタム図形の登録操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItemRegFigure_Click(object sender, EventArgs e)
        {
            SolidBrush brush;
            Button bt = (Button)contextMenuStripCustom.SourceControl;
            switch (bt.Name)
            {
                case "buttonCustom01":
                    Settings.Instance.CustomFigure01 = (ClsFigure)lastFigure.Clone();
                    panelLineCustom01.BackColor = Settings.Instance.CustomFigure01.linePen.Color;
                    if (Settings.Instance.CustomFigure01.fillBrush != null)
                    {
                        brush = (SolidBrush)(Settings.Instance.CustomFigure01.fillBrush);
                        panelBrushCustom01.BackColor = brush.Color;
                    }
                    break;
                case "buttonCustom02":
                    Settings.Instance.CustomFigure02 = (ClsFigure)lastFigure.Clone();
                    panelLineCustom02.BackColor = Settings.Instance.CustomFigure02.linePen.Color;
                    if (Settings.Instance.CustomFigure02.fillBrush != null)
                    {
                        brush = (SolidBrush)(Settings.Instance.CustomFigure02.fillBrush);
                        panelBrushCustom02.BackColor = brush.Color;
                    }
                    break;
                case "buttonCustom03":
                    Settings.Instance.CustomFigure03 = (ClsFigure)lastFigure.Clone();
                    panelLineCustom03.BackColor = Settings.Instance.CustomFigure03.linePen.Color;
                    if (Settings.Instance.CustomFigure03.fillBrush != null)
                    {
                        brush = (SolidBrush)(Settings.Instance.CustomFigure03.fillBrush);
                        panelBrushCustom03.BackColor = brush.Color;
                    }
                    break;
                case "buttonCustom04":
                    Settings.Instance.CustomFigure04 = (ClsFigure)lastFigure.Clone();
                    panelLineCustom04.BackColor = Settings.Instance.CustomFigure04.linePen.Color;
                    if (Settings.Instance.CustomFigure04.fillBrush != null)
                    {
                        brush = (SolidBrush)(Settings.Instance.CustomFigure04.fillBrush);
                        panelBrushCustom04.BackColor = brush.Color;
                    }
                    break;
            }
            setButtonImage(lastFigure.figCat, bt);
            Settings.SaveToXmlFile();
        }

        /// <summary>
        /// カスタム図形01ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCustom01_Click(object sender, EventArgs e)
        {
            if (Settings.Instance.CustomFigure01.figCat != "")
            {
                setCustomColor(Settings.Instance.CustomFigure01);
                setPaintFlag(Settings.Instance.CustomFigure01.figCat);
            }
        }

        /// <summary>
        /// カスタム図形02ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCustom02_Click(object sender, EventArgs e)
        {
            if (Settings.Instance.CustomFigure02.figCat != "")
            {
                setCustomColor(Settings.Instance.CustomFigure02);
                setPaintFlag(Settings.Instance.CustomFigure02.figCat);
            }
        }

        /// <summary>
        /// カスタム図形03ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCustom03_Click(object sender, EventArgs e)
        {
            if (Settings.Instance.CustomFigure03.figCat != "")
            {
                setCustomColor(Settings.Instance.CustomFigure03);
                setPaintFlag(Settings.Instance.CustomFigure03.figCat);
            }
        }

        /// <summary>
        /// カスタム図形04ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCustom04_Click(object sender, EventArgs e)
        {
            if (Settings.Instance.CustomFigure04.figCat != "")
            {
                setCustomColor(Settings.Instance.CustomFigure04);
                setPaintFlag(Settings.Instance.CustomFigure04.figCat);
            }
        }

        #endregion

    }

}