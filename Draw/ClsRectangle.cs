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

public class ClsRectangle : ClsFigure
{
    public int X;
    public int Y;
    public int Width;
    public int Height;

    public ClsRectangle()
    {
        figCat = "rectangle";
    }

    public ClsRectangle(Pen _linePen, Brush _fillBrush, int _X, int _Y, int _Width, int _Height)
    {
        figCat = "rectangle";
        this.setPen(_linePen);
        this.setBrush(_fillBrush);
        X = _X;
        Y = _Y;
        Width = _Width;
        Height = _Height;
    }

    public override void Draw(Graphics g)
    {
        g.FillRectangle(fillBrush, X, Y, Width, Height);
        g.DrawRectangle(linePen, X, Y, Width, Height);
    }
}
