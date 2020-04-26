using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ScShoAlpha
{
    class ClsGText : ClsFigure
    {
        //表示文字
        private string text;

        public int X;
        public int Y;
        public int Height;
        public int Width;

        public ClsGText() {
            figCat = "gtext";
        }

        public ClsGText(Pen _linePen, Brush _fillBrush, Font _textFont, Color _textColor,
            int _X, int _Y, int _Width, int _Height,string _text)
        {
            figCat = "gtext";
            this.setPen(_linePen);
            this.setBrush(_fillBrush);
            this.setText(_textColor, _textFont);
            text = _text;
            X = _X;
            Y = _Y;
            Width = _Width;
            Height = _Height;
        }

        public override void Draw(Graphics g)
        {
            g.FillRectangle(fillBrush, X, Y, Width, Height);
            g.DrawRectangle(linePen, X, Y, Width, Height);
            g.DrawString(text,textFont,new SolidBrush(textColor),new Point(X,Y));
        }

    }
}
