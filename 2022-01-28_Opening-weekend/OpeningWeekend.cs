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
        public DateTime bemutato { get; set; }
        public string forgalmazo { get; set; }
        public int bevetel { get; set; }
        public int latogatok { get; set; }

        public Film(string sor)
        {
            string[] t = sor.Split(';');
            eredetiCim = t[0];
            magyarCim = t[1];
            bemutato = DateTime.Parse(t[2]);
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

            Film legtobbLatogatosFilm = filmek.OrderBy(f => f.latogatok).Last();
            
            Console.WriteLine("5. feladat: Legtöbb látogató az első héten:");
            Console.WriteLine($"Eredeti cím: {legtobbLatogatosFilm.eredetiCim}");
            Console.WriteLine($"Magyar cím: {legtobbLatogatosFilm.magyarCim}");
            Console.WriteLine($"Forgalmazó: {legtobbLatogatosFilm.forgalmazo}");
            Console.WriteLine($"Bevétel az első héten: {legtobbLatogatosFilm.bevetel.ToString("C2")}");
            Console.WriteLine($"Látogatók száma: {legtobbLatogatosFilm.latogatok} fő");

            bool voltFilm = false;
            foreach (var f in filmek)
            {
                string[] dbMagyarCim = f.magyarCim.Split(' ');
                string[] dbEredetiCim = f.eredetiCim.Split(' ');
                if (dbMagyarCim[0].StartsWith("W") && dbMagyarCim[1].StartsWith("W") && dbEredetiCim[0].StartsWith("W") && dbEredetiCim[1].StartsWith("W"))
                {
                    voltFilm = true;
                }
            }

            if (voltFilm == true)
            {
                Console.WriteLine("6. feladat: Volt ilyen film!");
            }
            else
            {
                Console.WriteLine("6. feladat: Nem volt ilyen film!");
            }

            Dictionary<string, int> forgalmazoFilmek = new Dictionary<string, int>();

            foreach (var f in filmek)
            {

                if (!forgalmazoFilmek.ContainsKey(f.forgalmazo))
                {
                    forgalmazoFilmek.Add(f.forgalmazo, 1);
                }
                else
                {
                    forgalmazoFilmek[f.forgalmazo]++;
                }
            }

            List<string> statCsv = new List<string>();
            statCsv.Add("forgalmazo;filmekSzama");
            foreach (var f in forgalmazoFilmek)
            {
                if (f.Value > 1)
                {
                    statCsv.Add($"{f.Key};{f.Value}");
                }
            }
            File.WriteAllLines("stat.csv", statCsv);

            List<DateTime> iCBemutatok = new List<DateTime>();

            foreach (var f in filmek)
            {
                if (f.forgalmazo == "InterCom")
                {
                    iCBemutatok.Add(f.bemutato);
                }

            }

            int legnagyobbKulonbseg = 0;

            for (int i = 0; i < iCBemutatok.Count; i++)
            {
                if (i != 0)
                {
                    int kulonbsegSzamolas = (iCBemutatok[i] - iCBemutatok[i - 1]).Days;
                    if (kulonbsegSzamolas > legnagyobbKulonbseg)
                    {
                        legnagyobbKulonbseg = kulonbsegSzamolas;
                    }
                }
            }


            Console.WriteLine($"8. feladat: A leghosszabb időszak két InterCom-os bemutató között: {legnagyobbKulonbseg} nap");


            Console.ReadKey();
        }
    }
}
