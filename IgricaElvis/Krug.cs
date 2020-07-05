using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IgricaElvis
{
    public class Krug 
    {
        int r;
        Point centar;

        public int R
        {
            get { return r; }
            set { r = value; }
        }
        
        public Point Centar
        {
            get { return centar; }
            set { centar = value; }
        }

        Color boja;

        public Krug(Point centar)
        {
            this.centar = centar;
            Random rnd = new Random();
            r = rnd.Next(10, 30);
            boja = Color.FromArgb(
                rnd.Next(0, 256),
                rnd.Next(0, 256),
                rnd.Next(0, 256));
        }

        public Rectangle Rec
        {
            get
            {
                return new Rectangle(centar.X - r,
                centar.Y - r,
                2 * r,
                2 * r);
            }
        }

        public void crtaj(Graphics g)
        {
            g.FillEllipse(new SolidBrush(boja),
                this.Rec);
        }




    }
}

