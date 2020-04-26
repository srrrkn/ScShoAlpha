using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

public  class ClsArrow : ClsFigure
{
    public int X1;
    public int Y1;
    public int X2;
    public int Y2;

    public ClsArrow()
    {
        figCat = "arrow";
    }

    public ClsArrow(Pen _linePen, int _X1, int _Y1, int _X2, int _Y2)
    {
        figCat = "arrow";
        this.setPen(_linePen);
        X1 = _X1;
        Y1 = _Y1;
        X2 = _X2;
        Y2 = _Y2;
    }

    public override void Draw(Graphics g)
    {
        g.DrawLine(linePen, X1, Y1, X2, Y2);

        int penSize = (int)linePen.Width;

        float vx = X2 - X1;
        float vy = Y2 - Y1;
        float v = (float)Math.Sqrt(vx * vx + vy * vy);
        float ux = vx / v;
        float uy = vy / v;
        if (X2 - X1 != 0)
        {
            g.DrawLine(linePen, 
                X2, 
                Y2, 
                X2 - uy * (penSize * 4) - ux * (penSize * 6), 
                Y2 + ux * (penSize * 4) - uy * (penSize * 6));
            g.DrawLine(linePen, 
                X2, 
                Y2, 
                X2 + uy * (penSize * 4) - ux * (penSize * 6),
                Y2 - ux * (penSize * 4) - uy * (penSize * 6));
        }
    }
}
