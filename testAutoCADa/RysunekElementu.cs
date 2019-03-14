using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace testAutoCADa {

    class RysunekElementu {

        #region POLA KLASY
        public string Nazwa { get; set; }
        public string Adres { get; set; }
        public float Skala_bloku { get; set; }
        public string Nazwa_bloku { get; set; }
        public int Ile_spoin { get; set; }
        public string Kto_opisal { get; set; }
        #endregion

        #region KONSTRUKTOR
        public RysunekElementu(string nazwa_rys, string adres_rys, float skala_bloku, string nazwa_bloku, int ile_spoin, string kto_opisal) {
            Adres = adres_rys;
            //Nazwa = Nadaj_nazwe(nazwa_rys);
            Nadaj_nazwe(nazwa_rys);                 // <- to do usunięcia (nazwa rysunku ma być przekazywana z zewnątrz i uaktualniana w metodzie Nadaj_nazwę)
            Skala_bloku = skala_bloku;
            Nazwa_bloku = nazwa_bloku;
            Ile_spoin = ile_spoin;
            Kto_opisal = kto_opisal;
        }
        #endregion

        #region METODY
        private string Nadaj_nazwe(string nazwa_rys) {
            string nazwa_rysunku;

            // -> tu powstanie metoda nadająca rzeczywistą nazwę rysunku (na podstawie nazw plików w katalogu "Adres")

            OpenFileDialog oknoDialogowe = new OpenFileDialog {                                         // <- to do usunięcia
                Filter = "Pliki DWG (*.dwg)|*.dwg",                                                     // <- to do usunięcia
                InitialDirectory = "C:\\Users\\demianczukm\\Desktop\\PROTON\\"                          // <- to do usunięcia
            };                                                                                          // <- to do usunięcia
            if (oknoDialogowe.ShowDialog() == true) {                                                   // <- to do usunięcia
                this.Nazwa = oknoDialogowe.SafeFileName;
                //int poz = plik.LastIndexOf('\\');
                //this.Adres = plik.Substring(0, poz + 1);
                //this.Nazwa = plik.Substring(poz + 1, plik.Length - (poz + 1));
            }
            nazwa_rysunku = "cos";                                                                      // <- to do usunięcia
            return nazwa_rysunku;
        }
        #endregion
    }
}
