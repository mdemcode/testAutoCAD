using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using AutoCAD;

namespace testAutoCADa {

    class Opisz_Rysunek {

        #region POLA KLASY
        private RysunekElementu dane_rysunku;
        private dynamic acadApp; //AcadApplication acadApp;
        private dynamic rysunek_ACAD; //AcadDocument
        private const string adres_blokow = "\\\\Plsiedfs01\\z1\\1ST\\DZIENNIKI SPAWANIA\\#program#\\_bloki\\";
        #endregion

        #region KONSTRUKTOR
        public Opisz_Rysunek(RysunekElementu rysunek) {
            if (Przechwyc_AutoCAD()) {
                dane_rysunku = rysunek;
                if (Otworz_Rysunek()) Opisz_Spoiny();
            }
        }
        #endregion

        #region METODY
        private bool Przechwyc_AutoCAD() {
            try {
                acadApp = Marshal.GetActiveObject("AutoCAD.Application"); //(AcadApplication)
                return true;
            }
            catch {
                try {
                    acadApp = Activator.CreateInstance(Type.GetTypeFromProgID("AutoCAD.Application"), true); //(AcadApplication)
                    acadApp.Visible = true;
                    MessageBox.Show("Otworzyłem AutoCad`a"); // <- wstrzymuje działanie programu do czasu pełnego otworzenia AutoCADa
                    return true;
                }
                catch (Exception e) {
                    MessageBox.Show("Mam problem z AutoCADem!\n" + e.Message);
                    return false;
                }
            }
        }

        private bool Otworz_Rysunek() {
            try {
                //AcadDocuments listaRys = acadApp.Documents; 
                rysunek_ACAD = acadApp.Documents.Open(dane_rysunku.Adres + dane_rysunku.Nazwa); //("C:\\Users\\demianczukm\\Desktop\\N-400100-C-UTH-IG04-00010-AB.dwg");
                return true;
            }
            catch {
                MessageBox.Show("Nie udało się otworzyć rysunku! \n Spróbuj jeszcze raz.");
                return false;
            }
        }

        private void Opisz_Spoiny() {
            Wstaw_Blok();
            acadApp.WindowState = 3; //AcWindowState.acMax;
            int ilosc_spoin = Petla_Glowna();
            bool zapis = Czy_zapisac(ilosc_spoin);
            if (zapis) dane_rysunku.Ile_spoin = ilosc_spoin;
            acadApp.WindowState = 2; // AcWindowState.acMin;
            rysunek_ACAD.Close(zapis);
        }

        private void Wstaw_Blok() {
                dynamic warstwa_spoin = rysunek_ACAD.Layers.Add("opisSpoin"); //AcadLayer
                rysunek_ACAD.ActiveLayer = warstwa_spoin;
                double[] pkt_bazowy = { 0, 0, 0 };
                dynamic blok_bazowy = rysunek_ACAD.ModelSpace.InsertBlock(pkt_bazowy, adres_blokow + dane_rysunku.Nazwa_bloku + ".dwg", 1, 1, 1, 0); //AcadBlockReference
                blok_bazowy.Delete();
                blok_bazowy = null;
        }

        private int Petla_Glowna() {                           // <- TO DO ZMIANY
            MessageBox.Show("Rysunek:\n" + dane_rysunku.Nazwa);
            rysunek_ACAD.Application.ZoomAll();
            double[] pktWst1;
            double[] pktWst2=new double[] { 0, 0, 0 };
            dynamic blok_spoiny; //AcadBlockReference
            int i = dane_rysunku.Ile_spoin;
            try {
                while (true) {
                    pktWst1 = rysunek_ACAD.Utility.GetPoint(pktWst2, "Wskaz punkt");
                    i++;
                    if (rysunek_ACAD.ActiveSpace == 1 ) //AcActiveSpace.acModelSpace
                        blok_spoiny = rysunek_ACAD.ModelSpace.InsertBlock(pktWst1, dane_rysunku.Nazwa_bloku, dane_rysunku.Skala_bloku, dane_rysunku.Skala_bloku, 1, 0);
                    else
                        blok_spoiny = rysunek_ACAD.PaperSpace.InsertBlock(pktWst1, dane_rysunku.Nazwa_bloku, dane_rysunku.Skala_bloku, dane_rysunku.Skala_bloku, 1, 0);
                    dynamic attr = blok_spoiny.GetAttributes();
                    attr[0].TextString = i.ToString(); //(AcadAttributeReference)
                }
            }
            catch  (Exception e) { // 
                if (e.Message!="Użytkownik wprowadził słowo kluczowe") MessageBox.Show(e.Message); // <- Tylko do testow
                return i;
            }
        }

        private bool Czy_zapisac(int j) {
            if (MessageBox.Show("Ilość spoin: " + j.ToString() + "\n " +
                                "Czy wszystkie spoiny zostały opisane?",
                                "Czy wszystkie spoiny opisane?",
                                MessageBoxButton.YesNo)
                                == MessageBoxResult.No) {
                if (MessageBox.Show("Zapisać niedokończony rysunek?", "Czy zapisać?", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return false;
                else {
                    dane_rysunku.Kto_opisal = "niedokonczony";
                    return true;
                }
            }
            else {
                dane_rysunku.Kto_opisal = Environment.UserName;
                return true;
            }
        }
        #endregion
    }
}
