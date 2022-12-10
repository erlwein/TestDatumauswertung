using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDatumauswertung
{
    internal class Testperson
    {
        int kdNummer;
        string anrede;
        string titel;
        string vorname;
        string nachname;
        DateTime geburtstag;
        string strasse;
        string hausnr;
        int plz;
        string stadt;
        string telefon;
        string mobil;
        string email;
        bool newsletter;

        public Testperson() { }

        public Testperson(int kdNummer, string anrede, string titel, string vorname, string nachname, DateTime geburtstag, string strasse, string hausnr, int plz, string stadt, string telefon, string mobil, string email, bool newsletter)
        {
            KdNummer = kdNummer;
            Anrede = anrede;
            Titel = titel;
            Vorname = vorname;
            Nachname = nachname;
            Geburtstag = geburtstag;
            Strasse = strasse;
            Hausnr = hausnr;
            Plz = plz;
            Stadt = stadt;
            Telefon = telefon;
            Mobil = mobil;
            Email = email;
            Newsletter = newsletter;
        }

        public int KdNummer { get => kdNummer; set => kdNummer = value; }
        public string Anrede { get => anrede; set => anrede = value; }
        public string Titel { get => titel; set => titel = value; }
        public string Vorname { get => vorname; set => vorname = value; }
        public string Nachname { get => nachname; set => nachname = value; }
        public DateTime Geburtstag { get => geburtstag; set => geburtstag = value; }
        public string Strasse { get => strasse; set => strasse = value; }
        public string Hausnr { get => hausnr; set => hausnr = value; }
        public int Plz { get => plz; set => plz = value; }
        public string Stadt { get => stadt; set => stadt = value; }
        public string Telefon { get => telefon; set => telefon = value; }
        public string Mobil { get => mobil; set => mobil = value; }
        public string Email { get => email; set => email = value; }
        public bool Newsletter { get => newsletter; set => newsletter = value; }

        public static List<Testperson> Laden()
        {
            if (File.Exists("TestDaten.csv"))
            {
                List<Testperson> testperonen = new List<Testperson>();
                using (StreamReader sr = new StreamReader("TestDaten.csv"))
                {
                    sr.ReadLine();
                    string ganzeDatei = sr.ReadToEnd();
                    string[] zeilen = ganzeDatei.Split('\n');

                    foreach (string zeile in zeilen)
                    {
                        if (!string.IsNullOrEmpty(zeile))
                        {
                            Testperson testperson = new Testperson();
                            string[] spalten = zeile.Split(";");
                            testperson.KdNummer = int.Parse(spalten[0]);
                            testperson.Anrede = spalten[1];
                            testperson.Titel = spalten[2];
                            testperson.Vorname = spalten[3];
                            testperson.Nachname = spalten[4];
                            DateTime.TryParse(spalten[5], out DateTime dateTime);
                            testperson.Geburtstag = dateTime;
                            testperson.Strasse = spalten[6];
                            testperson.Hausnr = spalten[7];
                            testperson.Plz = int.Parse(spalten[8]);
                            testperson.Stadt = spalten[9];
                            testperson.Telefon = spalten[10];
                            testperson.Mobil = spalten[11];
                            testperson.Email = spalten[12];
                            testperson.Newsletter = spalten[13] == "ja" ? true : false;

                            testperonen.Add(testperson);
                        }




                    }
                }
                return testperonen;
            }
            return null;
        }
        public override string ToString()
        {
            return $"{Convert.ToString(KdNummer)}, {Anrede}, {Vorname}, {Nachname}, {Convert.ToString(Geburtstag)}, {Strasse}, {Hausnr}, {Convert.ToString(Plz)}, {Stadt}, {Telefon}, {Mobil}, {Email}, {Newsletter}";
        }
    }
}
