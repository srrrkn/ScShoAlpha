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

public class ClsLine : ClsFigure
{
    public int X1;
    public int Y1;
    public int X2;
    public int Y2;

    public ClsLine()
    {
        figCat = "line";
    }

    public ClsLine(Pen _linePen, int _X1, int _Y1, int _X2, int _Y2)
    {
        figCat = "line";
        this.setPen(_linePen);
        X1 = _X1;
        Y1 = _Y1;
        X2 = _X2;
        Y2 = _Y2;
    }

    public override void Draw(Graphics g)
    {
        g.DrawLine(linePen, X1, Y1, X2, Y2);
    }
}
