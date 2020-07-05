using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IgricaElvis
{
    public partial class Rezultati : Form
    {
        static List<Score> sortirana;
        public Rezultati(List<Score> skorovi)
        {
            InitializeComponent();
            sortirana = sortirajListuSkorova(skorovi);
        }

        private List<Score> sortirajListuSkorova(List<Score> skorovi)
        {
            List<Score> lista = skorovi;
            Score temp;

            for (int i = 0; i < lista.Count-1; i++)
                for (int j = 1; j < lista.Count; j++)
                {
                    if (lista[i].Rezultat < lista[j].Rezultat)
                    {
                        temp = lista[i];
                        lista[i] = lista[j];
                        lista[j] = temp;
                    }
                }
                
            return lista;
        }

        private void Rezultati_Load(object sender, EventArgs e)
        {
            int broj = 1;
            int y = 100;

            button1.Location = new Point((this.MdiParent.Width/2)-button1.Width/2,button1.Location.Y);

            foreach (Score s in sortirana)
            {
                Label l = new Label();
                Label rez = new Label();

                l.Text = (broj++) + ". " + s.Username;
                l.ForeColor = Color.White;
                l.BackColor = Color.Coral;
                l.Font = new Font(this.Font.FontFamily,22);
                l.Width = 471;
                l.Height = 35;

                rez.Text = ""+s.Rezultat;
                rez.ForeColor = Color.White;
                rez.BackColor = Color.Coral;
                rez.Font = new Font(this.Font.FontFamily, 22);
                rez.Height = 35;
                rez.Width = 45;

                Controls.Add(l);
                l.Location = new Point(this.MdiParent.Width / 2 - l.Width/2, y);

                
                Controls.Add(rez);
                rez.BringToFront();
                rez.Location = new Point(l.Location.X+l.Width-50, l.Location.Y);

                y += l.Height + 10;
                
            }
        }

        SoundPlayer klik = new SoundPlayer();
        private void button1_Click(object sender, EventArgs e)
        {
            klik.SoundLocation = @"C:\Users\djuri\source\repos\IgricaElvis\button_click.wav";
            klik.Play();
            Pocetna p = new Pocetna(null);
            p.MdiParent = this.MdiParent;
            p.Show();
            this.Close();
        }
    }
}
