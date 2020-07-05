using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IgricaElvis
{
    public partial class Form2 : Form
    {
        SoundPlayer muzika = new SoundPlayer();
        Image img;
        delegate void delOsveziFormu();
        List<Krug> krugovi = new List<Krug>();
        Graphics gfx;
        Thread tGenerator, tAnimator;
        Rectangle rec;
        int poeni = 0;
        int broj = 0;
        int brojZivota = 3;
        int zivot;
        string username;
        bool izasao = false;
        int sledeciNivo = 0;

        public Form2(string name)
        {
            InitializeComponent();
            username = name;

            muzika.SoundLocation = @"C:\Users\djuri\source\repos\IgricaElvis\elvismuzika.wav";

            tGenerator = new Thread(new ThreadStart(generisiKrugove));
            tGenerator.IsBackground = true;

            tAnimator = new Thread(new ThreadStart(animacija));
            tAnimator.IsBackground = true;

            this.Paint += Form2_Paint;
            this.Shown += Form2_Shown;


        }

        private void Form2_Shown(object sender, EventArgs e)
        {
            if (!tGenerator.IsAlive)
            {
                tGenerator.Start();
            }

            if (!tAnimator.IsAlive)
                tAnimator.Start();

        }
        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            lock (krugovi)
            {
                foreach (Krug k in krugovi)
                {
                    k.crtaj(e.Graphics);
                }
            }

            e.Graphics.DrawString("Score: ", new Font(this.Font.FontFamily, 22.0f), Brushes.White, new Point(5, 35));
            e.Graphics.DrawString(poeni + "", new Font(this.Font.FontFamily, 22.0f), Brushes.White, new Point(5, 65));
            e.Graphics.DrawString("Life", new Font(this.Font.FontFamily, 22.0f), Brushes.White, new Point(ClientRectangle.Width - 100, 35));
            zivoti(sender, e); //isrctavanje zivota

            if (brojZivota == 0)
            {
                if(!izasao)     //Resen problem za muziku na pocetnoj
                    Form2_FormClosing(null, null);
            }

            Bitmap bitmap = new Bitmap(img.Width, img.Height);
            gfx = Graphics.FromImage(bitmap);

            gfx.DrawImage(img, -32, -35);
            e.Graphics.DrawImage(bitmap, rec);
        }

        private void generisiKrugove()
        {
            Random rnd = new Random();
            while (true)
            {
                lock (krugovi)
                {
                    int x = rnd.Next(0, this.ClientRectangle.Width - 50);
                    krugovi.Add(new Krug(new Point(x, -50)));
                }

                if (broj >= 450)  // Ogranicenje za thread
                    broj = 450;

                   Thread.Sleep(500 - broj);
            }
        }

        private void animacija()
        {
            while (true)
            {

                List<Krug> zaBrisanje = new List<Krug>();
                lock (krugovi)
                {
                    for (int i = 0; i < krugovi.Count; i++)
                    {

                        Krug k = krugovi[i];
                        k.Centar = new Point(k.Centar.X, k.Centar.Y + 1);
                        if (k.Rec.Top > this.ClientRectangle.Bottom)
                        {
                            zaBrisanje.Add(k);
                            poeni -= 10;
                            brojZivota--;
                        }
                        if (k.Rec.IntersectsWith(rec))
                        {
                            zaBrisanje.Add(k);
                            poeni += 10;

                            if (poeni == 350 + sledeciNivo) //povecava broj pojavljivanja lopti
                            {
                                broj += 50;
                                sledeciNivo += 100;
                            }
                                

                        }
                    }
                    foreach (Krug k in zaBrisanje)
                        krugovi.Remove(k);
                }
                this.Invoke(new delOsveziFormu(osveziFormu));

                Thread.Sleep(1);
            }
        }

        public void zivoti(object sender, PaintEventArgs e)
        {
            zivot = 0;
            for (int i = 0; i < brojZivota; i++)
            {
                e.Graphics.FillEllipse(Brushes.Red, ClientRectangle.Width - 50 - zivot, 65, 20, 20);
                zivot += 30;
            }
        }

        private void osveziFormu()
        {
            this.Invalidate();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            img = Image.FromFile(@"C:\Users\djuri\source\repos\IgricaElvis\el.png");
            muzika.Play();
        }

        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            rec = new Rectangle(e.X - 50, this.ClientRectangle.Bottom - 100, 100, 100);
            this.Invalidate();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            izasao = true;
            muzika.Stop();
            if(tGenerator.IsAlive)
                tGenerator.Abort();
            if (tAnimator.IsAlive)
            {
            tAnimator.Abort();
                if (MessageBox.Show("VAS SKOR: "+poeni+"\nPokusati ponovo?", "GAMEOVER", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Score s = new Score(username, poeni);   
                    Pocetna p = new Pocetna(s);
                    p.MdiParent = this.MdiParent;
                    
                    p.Show();
                    this.Hide();
                }
                    
                else
                {
                    tAnimator.Abort(); 
                    this.MdiParent.Close();
                    this.Close();
                }
            }

        }
    }
}
