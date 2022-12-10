using TestDatumauswertung;

//Laden der Testdaten

List<Testperson> testliste = Testperson.Laden();
//foreach (var list in testliste)
//{
//    Console.WriteLine(list);
//}

var newsletterAnzahl = testliste.Count(i => i.Newsletter == true); // nao presica do true
Console.WriteLine($"Wir haben {newsletterAnzahl} Newsletterrabos.");

var newsletterAnzak = (from t in testliste
                       where t.Newsletter
                       select t).Count();
Console.WriteLine($"Wir haben {newsletterAnzahl} Newsletterabos.");




// Mann-Frau-Verteilung in Porzent

int gesamtZahl = testliste.Count;
var maennerAnzahl = testliste.Count(i => i.Anrede == "Herr");
var frauenAnzahl = testliste.Count(i => i.Anrede == "Frau");

double maennerPro = (double)maennerAnzahl * 100 / gesamtZahl;
double frauenPro = (double)frauenAnzahl * 100 / gesamtZahl;

Console.WriteLine($"Der Anteil an Mäannern beträgt {maennerPro}%\nDer Anteil an fraus beträgt {frauenPro}%. ");


//Email-Anbieter

var emailAnbieter = from e in testliste
                    where e.Email != ""
                    group e by e.Email.Split("@")[1]
                    into gruppeAnbieter
                    orderby gruppeAnbieter.Count() descending
                    select gruppeAnbieter;
Console.WriteLine($"Der beliebteste Email-Anbieter ist {emailAnbieter.FirstOrDefault().Key}");

//Altergruppierung

var alterGruppierung = from t in testliste
                       where (DateTime.Now - t.Geburtstag).TotalDays < 150 * 365
                       orderby t.Geburtstag descending
                       group t by Math.Floor((DateTime.Now - t.Geburtstag).TotalDays / 365);
foreach(var item in alterGruppierung)
{
    Console.WriteLine($"\nAltersgruppe: {item.Key}- jährige");

    foreach (Testperson t in item)
        Console.WriteLine($"Name: {t.Anrede} {t.Nachname}");
}

//Personen mit Alter

//var altersAngabe = from t in testliste
//                   where t.Geburtstag != DateTime.MinValue
//                   orderby t.Geburtstag descending
//                   select new { Anrede = t.Anrede, Name = t.Nachname, Alter = (Math.Floor((DateTime.Now - t.Geburtstag).TotalDays / 365)) };
//foreach (var item in altersAngabe)
//{
//    Console.WriteLine($"{item.Anrede} {item.Name} {item.Alter} Jahre");
//}

//AnzeigeBlöcke
int seitenGröße = 50;
int seitenZähler = 0;


var blocksatz =testliste.Take(seitenGröße);
while (blocksatz.Count() > 0)
{
    foreach (var item in blocksatz)
        Console.WriteLine(item);
    Console.WriteLine("Für die nächste Seite bitte Enter drücken");
    Console.ReadLine();
    seitenZähler++;
    blocksatz = testliste.Skip(seitenGröße + seitenZähler).Take(seitenGröße);
}
Console.WriteLine("Ende das DatenSatzes.");

//Console.WriteLine("Willkommen User !\nFür welche stadt sollen die Testpersonendaten ausgegeben werden?");
//string suchanfrage = Console.ReadLine();
//var suchQuery = from t in testliste
//                where t.Stadt == suchanfrage
//                select new { Anrede = t.Anrede, Name = t.Nachname, Mobil = t.Mobil, Mail = t.Email };
//foreach (var item in suchQuery)
//    Console.WriteLine($"{item.Anrede} {item.Name} Mobilnummer: {item.Mobil}, E-Mail: {item.Mail}");
