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
    public partial class Pocetna : Form
    {
        SoundPlayer klik=new SoundPlayer();
        SoundPlayer pocetna = new SoundPlayer();
        static List<Score> rezultati = new List<Score>();
        public Pocetna(Score s)
        {
            InitializeComponent();
            

            if (s != null)
                rezultati.Add(s);

            klik.SoundLocation = @"C:\Users\djuri\source\repos\IgricaElvis\button_click.wav";

        }
        Form aktivnaForma;
        private void button1_Click(object sender, EventArgs e)
        {
            if (rezultati.Count < 6)
            {
                pocetna.Stop();
                klik.Play();
                upisImena();
            }
            else
            {
                MessageBox.Show("Iskoriceno maximalno igraca, obrisite neki od skorova da bi ste nastavili", "Iskoriceno maximalno igraca");
            }

        }

        private void upisImena()
        {
            foreach (Control x in Controls)
            {
                x.Hide();

            }

            pnl.Show();
            pnl.Height = 200;
            pnl.Width = 500;
            pnl.Location = new Point((this.MdiParent.Width / 2) - pnl.Width / 2, this.MdiParent.Height / 2 - pnl.Height / 2);
            pnl.BackColor = Color.Navy;
            pnl.BorderStyle = BorderStyle.FixedSingle;

            txt.Show();
            txt.Width = pnl.Width - 50;
            txt.Location = new Point(pnl.Location.X-pnl.Width/2, pnl.Height/ 2);

            sacuvajIme.Location = new Point(txt.Location.X, txt.Location.Y + txt.Height+5);
            sacuvajIme.Width = txt.Width;
            sacuvajIme.Height = txt.Height;
            sacuvajIme.Text = "Sacuvaj nickname";

            nickname.Location = new Point(txt.Location.X, txt.Location.Y -50);
            nickname.Text = "NICKNAME";

        }


        private void Pocetna_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            pocetna.SoundLocation = @"C:\Users\djuri\source\repos\IgricaElvis\pocetna.wav";
            pocetna.Play();
            pnl.Hide();
            txt.Hide();
            
            
            foreach(Control x in Controls)
            {
                x.Left= this.MdiParent.Width / 2 - x.Width / 2;
            }
            label1.Left = this.MdiParent.Width / 2 - label1.Width;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            klik.Play();
            Rezultati r = new Rezultati(rezultati);
            r.MdiParent = this.MdiParent;
            r.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            if(MessageBox.Show("Da li ste sigurni da zelite da izadjete iz igrice?", "Izlazak?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.MdiParent.Close();
            }

        }

        private void sacuvajIme_Click(object sender, EventArgs e)
        {
            if (txt.Text.Trim() == string.Empty)
                MessageBox.Show("Niste uneli nista, pokusajte ponovo.");
            else
            {
                if (rezultati.Count == 0)
                {
                    Score score = new Score(txt.Text.Trim(), 0);
                    aktivnaForma = new Form2(score.Username);
                    aktivnaForma.MdiParent = this.MdiParent;
                    aktivnaForma.Height = ClientSize.Height;
                    aktivnaForma.Width = ClientSize.Width;
                    aktivnaForma.Show();
                    this.Close();
                    
                }
                else
                {
                    foreach (Score s in rezultati)
                    {
                        if (s.Username.Equals(txt.Text.Trim()))
                        {
                            MessageBox.Show("Postoji username!");
                            return;
                        }
                    }
                        Score score = new Score(txt.Text.Trim(), 0);
                        aktivnaForma = new Form2(score.Username);
                        aktivnaForma.MdiParent = this.MdiParent;
                        aktivnaForma.Height = ClientSize.Height;
                        aktivnaForma.Width = ClientSize.Width;
                        aktivnaForma.Show();
                    this.Close();
                    }
                }

            }
        }
    }

