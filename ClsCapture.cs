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

public static class ClsCapture
{
    // 画面の高さと幅
    public static int dWidth = 1280;
    public static int dHeight = 1024;

    //左上座標
    public static int minX = 0;
    public static int minY = 0;

    // スクリーンショット実行時のカーソル
    public static System.Windows.Forms.Cursor shotCursor = Cursors.Arrow;
    // カーソルのホットスポット
    public static Point shotCursorHotspot = new Point(0, 0);
    // スクリーンショット実行時のカーソル位置
    public static Point shotPoint = new Point(0, 0);

    // アクティブウィンドウのキャプチャ
    public static Bitmap ActiveWindowsCapture()
    {
        // 'クリップボードのデータを削除
        // Clipboard.SetDataObject(new DataObject());
        // Bitmapの作成(暫定的に画面サイズとする)
        Bitmap bmp = new Bitmap(dWidth, dHeight);

        // ★★クリップボードによる取得★★
        SendKeys.SendWait("%{PRTSC}");
        // DoEventsを呼び出したほうがよい場合があるらしい
        Application.DoEvents();
        // クリップボードにあるデータの取得
        IDataObject d = Clipboard.GetDataObject();
        // クリップボードにデータがあったか確認
        if (d != null)
        {
            // ビットマップデータ形式に関連付けられているデータを取得
            Image img = (Image)d.GetData(DataFormats.Bitmap);
            if (img != null)
            {
                // データが取得できたときはbmpに表示する
                bmp = (Bitmap)img.Clone();
                // クリップボードのデータを一度削除
                Clipboard.SetDataObject(new DataObject());
            }
            img.Dispose();
        }
        return bmp;
    }

    // フルスクリーンキャプチャ
    public static Bitmap FullScreenCapture()
    {
        // 'クリップボードのデータを削除
        // Clipboard.SetDataObject(new DataObject());
        // Bitmapの作成(暫定的に画面サイズとする)
        Bitmap bmp = new Bitmap(dWidth, dHeight);

        // ★★クリップボードによる取得★★
        SendKeys.SendWait("^{PRTSC}");
        // DoEventsを呼び出したほうがよい場合があるらしい
        Application.DoEvents();

        // [Alt+PrtSc]を実行した直後の実行の際、うまく動作しない（PrtScの仕様？）ため、[PrtSc]を二重で行う
        SendKeys.SendWait("^{PRTSC}");
        // DoEventsを呼び出したほうがよい場合があるらしい
        Application.DoEvents();

        // クリップボードにあるデータの取得
        IDataObject d = Clipboard.GetDataObject();
        // クリップボードにデータがあったか確認
        if (d != null)
        {
            // ビットマップデータ形式に関連付けられているデータを取得
            Image img = (Image)d.GetData(DataFormats.Bitmap);
            if (img != null)
            {
                // データが取得できたときはbmpに表示する
                bmp = (Bitmap)img.Clone();
                // クリップボードのデータを一度削除
                Clipboard.SetDataObject(new DataObject());
                img.Dispose();
            }
        }

        return bmp;
    }

    // カーソルの追加描画
    public static Bitmap AddCursor(Image img)
    {
        // 描画位置
        Point position = new Point(shotPoint.X - shotCursorHotspot.X - minX, shotPoint.Y - shotCursorHotspot.Y - minY);
        // 描画先とするImageオブジェクトを作成する
        Bitmap bmp = (Bitmap)img.Clone();
        // ImageオブジェクトのGraphicsオブジェクトを作成する
        Graphics g = Graphics.FromImage(bmp);
        // 'Cursorsクラスの静的プロパティを取得する 
        System.Windows.Forms.Cursor cur = shotCursor;
        // カーソルを描画する 
        cur.Draw(g, new Rectangle(position, cur.Size));
        return bmp;
    }

    // トリミング
    public static Bitmap CaptureTrim(Image img, Point startP, Point endP)
    {
        Point refPoint = new Point(startP.X, startP.Y);
        if (refPoint.X > endP.X)
            refPoint.X = endP.X;
        if (refPoint.Y > endP.Y)
            refPoint.Y = endP.Y;
        // 描画先とするImageオブジェクトを作成する
        Bitmap bmp = new Bitmap(Math.Abs(startP.X - endP.X), Math.Abs(startP.Y - endP.Y));
        // ImageオブジェクトのGraphicsオブジェクトを作成する
        Graphics g = Graphics.FromImage(bmp);
        // 切り取る部分の範囲を決定する。
        Rectangle srcRect = new Rectangle(refPoint.X, refPoint.Y, bmp.Width, bmp.Height);
        // 描画する部分の範囲を決定する。ここでは、
        Rectangle desRect = new Rectangle(0, 0, srcRect.Width, srcRect.Height);
        // 画像の一部を描画する
        g.DrawImage(img, desRect, srcRect, GraphicsUnit.Pixel);
        g.Dispose();
        return bmp;
    }
}
