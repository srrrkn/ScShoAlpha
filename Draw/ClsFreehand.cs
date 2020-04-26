using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ScShoAlpha
{
    class ClsFreehand : ClsFigure
    {
        private List<int> X;
        private List<int> Y;

        public ClsFreehand() {
            figCat = "freehand";
        }

        public ClsFreehand(Pen _linePen, List<int> _X, List<int> _Y)
        {
            figCat = "freehand";
            this.setPen(_linePen);
            X = new List<int>(_X);
            Y = new List<int>(_Y);

        }

        public override void Draw(Graphics g)
        {
            for (int i = 0; i < X.Count - 1; i++) {
                g.DrawLine(linePen,X[i],Y[i],X[i+1],Y[i+1]);
            }
        }
    }
}
