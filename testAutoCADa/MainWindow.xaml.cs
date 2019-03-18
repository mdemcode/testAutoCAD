using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
//using AutoCAD;


namespace testAutoCADa
{
    public partial class MainWindow : Window {

        #region POLA KLASY
        private string adresRysunku;
        private string nazwaRysunku;
        #endregion

        public MainWindow() {
            InitializeComponent();
        }

        private void ButtonOtworz_Click(object sender, RoutedEventArgs e) {
            Adres_i_nazwa_rys();
            RysunekElementu rys1 = new RysunekElementu(nazwaRysunku, adresRysunku, 0.8F, "tylkonr", (byte)0, "-");
            this.Hide();
                Opisz_Rysunek opis_rysunku = new Opisz_Rysunek(rys1);
                opis_rysunku = null;
                rys1 = null;
            this.Show();
        }

        private void Adres_i_nazwa_rys() {
            OpenFileDialog oknoDialogowe = new OpenFileDialog {                                         
                Filter = "Pliki DWG (*.dwg)|*.dwg",                                                     
                InitialDirectory = "\\\\Plsiedfs01\\z1\\1ST\\"
            };                                                                                          
            if (oknoDialogowe.ShowDialog() == true) {                                                  
                nazwaRysunku = oknoDialogowe.SafeFileName;
                int poz = oknoDialogowe.FileName.LastIndexOf('\\');
                adresRysunku = oknoDialogowe.FileName.Substring(0, poz + 1);
            }

        }
    }
}
