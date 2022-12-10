using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestDatumWPF
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        List<Testperson> testepersonen = Testperson.Laden();

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Nr.", typeof(int));
            dt.Columns.Add("Anrede", typeof(string));
            dt.Columns.Add("Titel", typeof(string));
            dt.Columns.Add("Vorname", typeof(string));
            dt.Columns.Add("Nachname", typeof(string));
            dt.Columns.Add("Geburtsdatum", typeof(DateTime));
            dt.Columns.Add("Straße", typeof(string));
            dt.Columns.Add("Hausnummer", typeof(string));
            dt.Columns.Add("Postleitzahl", typeof(string));
            dt.Columns.Add("Stadt", typeof(string));
            dt.Columns.Add("Telefon", typeof(string));
            dt.Columns.Add("Mobil", typeof(string));
            dt.Columns.Add("EMail", typeof(string));
            dt.Columns.Add("Newsletter", typeof(bool));

            dtGridView.ItemsSource = testepersonen;
        }

        private void btnMan_Click(object sender, RoutedEventArgs e)
        {
            int gesamtZahl = testepersonen.Count;
            var maennerAnzahl = testepersonen.Count(i => i.Anrede == "Herr");
            double maennerPro = (double)maennerAnzahl * 100 / gesamtZahl;
            tbl_1.Text= maennerPro.ToString();
        }

        private void BtnFrau_Click(object sender, RoutedEventArgs e)
        {
            int gesamtZahl = testepersonen.Count;
            var frauenAnzahl = testepersonen.Count(i => i.Anrede == "Frau");
            double frauenPro = (double)frauenAnzahl * 100 / gesamtZahl;
            tbl_1.Text = frauenPro.ToString();
        }

        private void btn_Email_Click(object sender, RoutedEventArgs e)
        {
            var email = from x in testepersonen
                                where x.Email != ""
                                group x by x.Email.Split('@')[1]
                    into gruppeAnbieter
                                orderby gruppeAnbieter.Count() descending
                                select gruppeAnbieter;
            tbl_1.Text =  email.FirstOrDefault().Key.ToString();
        }

        private void btn_News_Click(object sender, RoutedEventArgs e)
        {
            var newsletterAnzahl = testepersonen.Count(i => i.Newsletter == true);
            tbl_1.Text = newsletterAnzahl.ToString();
        }

        private void btn_Alter_Click(object sender, RoutedEventArgs e)
        {
            var altersAngabe = from t in testepersonen
                               where t.Geburtstag != DateTime.MinValue
                               orderby t.Geburtstag descending
                               select new { Anrede = t.Anrede, Name = t.Nachname, Alter = (Math.Floor((DateTime.Now - t.Geburtstag).TotalDays / 365)) };
            dtGridView.ItemsSource = altersAngabe;

        }

        private void btn_zeiten_Click(object sender, RoutedEventArgs e)
        {
            int seitenGröße = 50;
            int seitenZähler = 0;


            var blocksatz = testepersonen.Take(seitenGröße);
            while (blocksatz.Count() > 0)
            {
                foreach (var item in blocksatz)
                    Console.WriteLine(item);
                //Console.WriteLine("Für die nächste Seite bitte Enter drücken");
                //Console.ReadLine();
                seitenZähler++;
                blocksatz = testepersonen.Skip(seitenGröße + seitenZähler).Take(seitenGröße);
            }
           dtGridView.ItemsSource = blocksatz;
        }

        private void btn_off_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Border_DragEnter(object sender, DragEventArgs e)
        {
            string suchanfrage = tbl_1.Text;
            var suchQuery = from t in testepersonen
                            where t.Stadt == suchanfrage
                            select new { Anrede = t.Anrede, Name = t.Nachname, Mobil = t.Mobil, Mail = t.Email };
            dtGridView.ItemsSource = suchQuery;
            //foreach (var item in suchQuery)
            //    Console.WriteLine($"{item.Anrede} {item.Name} Mobilnummer: {item.Mobil}, E-Mail: {item.Mail}");

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string suchanfrage = tb_1.Text;
            var suchQuery = from t in testepersonen
                            where t.Stadt == suchanfrage
                            select new { Anrede = t.Anrede, Name = t.Nachname, Mobil = t.Mobil, Mail = t.Email };
            dtGridView.ItemsSource = suchQuery;
        }

        private void dragMe(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();

            }
            catch (Exception)
            {
                //throw;
            }

        }

        private void btnCloseGreen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.WindowState = WindowState.Minimized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnCloseRed_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
