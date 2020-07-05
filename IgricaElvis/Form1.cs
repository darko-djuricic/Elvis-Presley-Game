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
    public partial class Form1 : Form
    {



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {            
            Pocetna p = new Pocetna(null);
            p.MdiParent = this;
            p.Height = this.ClientSize.Height;
            p.Width = this.ClientSize.Width;
            p.Show();
            p.FormBorderStyle = FormBorderStyle.None;
            
            
        }
    }
}
