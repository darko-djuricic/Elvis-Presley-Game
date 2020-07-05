using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgricaElvis
{
    public class Score
    {
        string username;
        int rezultat;

        public Score(string username, int rezultat)
        {
            this.username = username;
            this.rezultat = rezultat;
        }

        public string Username { get => username; set => username = value; }
        public int Rezultat { get => rezultat; set => rezultat = value; }
    }
}
