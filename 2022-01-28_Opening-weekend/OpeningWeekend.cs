using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _2022_01_28_Opening_weekend
{
    class Film
    {
        public string eredetiCim { get; set; }
        public string magyarCim { get; set; }
        public string bemutato { get; set; }
        public string forgalmazo { get; set; }
        public int bevetel { get; set; }
        public int latogatok { get; set; }

        public Film(string sor)
        {
            string[] t = sor.Split(';');
            eredetiCim = t[0];
            magyarCim = t[1];
            bemutato = t[2];
            forgalmazo = t[3];
            bevetel = int.Parse(t[4]);
            latogatok = int.Parse(t[4]);

        }
    }

    class OpeningWeekend
    {
        static void Main(string[] args)
        {
            //2
            List<Film> filmek = new List<Film>();
            foreach (var sor in File.ReadAllLines("nyitohetvege.txt").Skip(1))
            {
                filmek.Add(new Film(sor));
            }

            Console.WriteLine($"3. feladat: Filmek száma az állományban: {filmek.Count} db");

            long uipKereset = 0;
            foreach (var f in filmek)
            {
                if (f.forgalmazo=="UIP")
                {
                    uipKereset+=f.bevetel;
                }
            }

            Console.WriteLine($"4. feladat: Az UIP Duna Film forgalmazó első heti bevételeinek összege: {uipKereset.ToString("C2")}");

            Console.ReadKey();
        }
    }
}
