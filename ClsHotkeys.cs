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
using System.ComponentModel;
using System.Media;
using System.Runtime.InteropServices;
using System.Windows.Forms;

/// <summary>ホットキーの登録・解除を行うためのクラス</summary>
public class ClsHotkeys
{
    [DllImport("user32.dll", SetLastError = true)]
    private static extern int RegisterHotKey(IntPtr hWnd, int id, int fsModifier, int vk);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern int UnregisterHotKey(IntPtr hWnd, int id);


    // ホットキーの修飾キー
    private const byte MOD_ALT = 0x1;
    private const byte MOD_CONTROL = 0x2;
    private const byte MOD_SHIFT = 0x4;

    // コンストラクタ（ホットキーの登録）
    public ClsHotkeys(IntPtr hWnd, int id, Keys key)
    {
        this.hWnd = hWnd;
        this.id = id;

        // Keys列挙体の値からWin32仮想キーコードを取り出す
        int keycode = System.Convert.ToInt32(key & Keys.KeyCode);

        // Keys列挙体の値から修飾キーコードを取り出す
        int modKey = System.Convert.ToInt32(key & Keys.Modifiers);
        // RegisterHotKeyに転送する修飾キーを設定する
        int modifiers = 0;
        switch (modKey)
        {
            case (int)Keys.Alt:
                {
                    modifiers = MOD_ALT;
                    break;
                }

            case (int)Keys.Control:
                {
                    modifiers = MOD_CONTROL;
                    break;
                }

            case (int)Keys.Shift:
                {
                    modifiers = MOD_SHIFT;
                    break;
                }
        }

        // Webのサンプルなどでは以下のようにすることが多いが、
        // キーボードの種類や設定によってKeys.AltとMOD_ALT、Keys.ShiftとMOD_SHIFTの値が対応しない場合がある？
        // Dim modifiers As Integer = CInt(key And Keys.Modifiers) >> 16

        this._lParam = new IntPtr(modifiers | keycode << 16);

        if (RegisterHotKey(hWnd, id, modifiers, keycode) == 0)
            // ホットキーの登録に失敗
            throw new Win32Exception(Marshal.GetLastWin32Error());
    }

    // ホットキーの登録解除
    public void Unregister()
    {
        if (hWnd == IntPtr.Zero)
            return;

        if (UnregisterHotKey(hWnd, id) == 0)
            // ホットキーの解除に失敗
            throw new Win32Exception(Marshal.GetLastWin32Error());

        hWnd = IntPtr.Zero;
    }

    public IntPtr LParam
    {
        get
        {
            return _lParam;
        }
    }

    private IntPtr hWnd; // ホットキーの入力メッセージを受信するウィンドウのhWnd
    private readonly int id; // ホットキーのID(0x0000〜0xBFFF)
    private readonly IntPtr _lParam; // WndProcメソッドで押下されたホットキーを識別するためのlParam値
}
