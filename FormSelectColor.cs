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
    public partial class FormSelectColor : Form
    {
        public Color tmpColor;
        public Color nowColor;

        public FormSelectColor(Color _nowColor)
        {
            // この呼び出しは、Windows フォーム デザイナで必要です。
            InitializeComponent();

            // InitializeComponent() 呼び出しの後で初期化を追加します。
            nowColor = _nowColor;
            // キャンセル用
            tmpColor = _nowColor;
        }

        private void FormSelectColor_Load(System.Object sender, System.EventArgs e)
        {
            PanelNowColor.BackColor = nowColor;
            trackBarAlpha.Value = (255 - nowColor.A) * 100 / 255;
            labelAlpha.Text = "透明度:" + trackBarAlpha.Value + "%";
        }

        private void buttonColor_Click(System.Object sender, System.EventArgs e)
        {
            Button rdb = (Button)sender;
            nowColor = rdb.BackColor;
            trackBarAlpha.Value = 0;
            PanelNowColor.BackColor = nowColor;
        }

        private void buttonColorTransparent_Click(System.Object sender, System.EventArgs e)
        {
            trackBarAlpha.Value = 100;
            PanelNowColor.BackColor = Color.FromArgb(0, 0, 0, 0);
            nowColor = Color.FromArgb(0, 0, 0, 0);
        }

        /// <summary>
        ///     ''' キャンセルボタン
        ///     ''' </summary>
        ///     ''' <param name="sender"></param>
        ///     ''' <param name="e"></param>
        ///     ''' <remarks></remarks>
        private void ButtonCancel_Click(System.Object sender, System.EventArgs e)
        {
            nowColor = tmpColor;
            this.Close();
        }

        /// <summary>
        ///     ''' OKボタン
        ///     ''' </summary>
        ///     ''' <param name="sender"></param>
        ///     ''' <param name="e"></param>
        ///     ''' <remarks></remarks>
        private void ButtonOK_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void trackBarAlpha_ValueChanged(object sender, EventArgs e)
        {
            labelAlpha.Text = "透明度:" + trackBarAlpha.Value + "%";
            int Alpha = (int)((100 - trackBarAlpha.Value) * 2.55);
            nowColor = Color.FromArgb(Alpha, nowColor.R, nowColor.G, nowColor.B);
            PanelNowColor.BackColor = nowColor;
        }


    }
}