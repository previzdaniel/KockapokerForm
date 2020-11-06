using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KockapokerForm
{
    class Dobas
    {
        int[] kockak = new int[5];

        public int[] Kockak
        {
            get { return kockak; }
        }
        private string eredmeny;
        public string Eredmeny
        {
            get
            {
                return eredmeny;
            }
        }
        private int pont;
        public int Pont { get { return pont; } }

        public int nyert { get; set; }


        public Dobas()
        {
            nyert = 0;
        }

        public Dobas(int k1, int k2, int k3, int k4, int k5)
        {
            kockak[0] = k1;
            kockak[1] = k2;
            kockak[2] = k3;
            kockak[3] = k4;
            kockak[4] = k5;

            eredmeny = Erteke();
        }

        public void EgyDobas()
        {
            Random vel = new Random(Guid.NewGuid().GetHashCode());
            for (int i = 0; i < kockak.Length; i++)
            {
                kockak[i] = vel.Next(1, 7);
            }

            eredmeny = Erteke();
        }

        public void Kiiras()
        {
            foreach (var k in kockak)
            {
                Console.Write($"{k};");
            }
            Console.WriteLine($" -> {eredmeny}");
        }

        private string Erteke()
        {
            Dictionary<int, int> eredemeny = new Dictionary<int, int>();

            for (int i = 1; i <= 6; i++)
            {
                eredemeny.Add(i, 0);
            }

            foreach (var k in kockak)
            {
                eredemeny[k]++;
            }

            // A dic-ből lekérdezzük az 1 Value-nál nagyobb elemeket
            // Első eset ha egy elem marad (Value értéket nézzük):
            //   - 5 -> nagypóker
            //   - 4 -> póker
            //   - 3 -> drill
            //   - 2 -> pár
            //  Key érték mondja meg, hogy hányas -> 4 póker 
            // Második eset két elem marad:
            //   - Value: 3 és 2 -> Full
            //   - Value: 2 és 2 -> 2 pár
            // Harmadik eset nem marad egy sem, akkor
            //   - Ha Key:6 == 0 -> Kissor
            //   - Ha Key:1 == 0 -> Nagysor
            // Minden más esetben -> Szemét (moslék)

            var result = (from e in eredemeny
                          orderby e.Value descending
                          where e.Value > 1
                          select new { Szam = e.Key, Db = e.Value }).ToList();

            Console.WriteLine();
            int darab = result.Count;
            if (darab == 1)
            {
                string[] egyes = new string[] { "", "", "Pár", "Drill", "Póker", "Nagypóker" };

                int[] pontok = new int[] { 0, 0, 10, 300, 600, 900 };
                pont = pontok[result[0].Db] + result[0].Szam;

                return $"{result[0].Szam} {egyes[result[0].Db]}";
            }
            else if (darab == 2)
            {
                if (result[0].Db == 3 && result[1].Db == 2)
                {
                    pont = 500 + (result[0].Szam * 10 + result[1].Szam);
                    return $"{result[0].Szam}-{result[1].Szam} Full";
                }
                else
                {
                    pont = 100 + (result[0].Szam * 10 + result[1].Szam);
                    return $"{result[1].Szam}-{result[0].Szam} Pár";
                }
            }
            else
            {
                if (eredemeny[6] == 0)
                {
                    pont = 700;
                    return "Kissor";
                }
                else if (eredemeny[1] == 0)
                {
                    pont = 800;
                    return "Nagysor";
                }

            }

            pont = 1;
            return "Szemét";
        }
    }
}
