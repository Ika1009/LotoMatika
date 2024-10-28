using System;
using System.Collections.Generic;
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

namespace Loto_App
{
    public partial class ThirdStepPage : Page
    {
        MainWindow _mainWindow;
        public ThirdStepPage(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();

            int remainingCombinations = CalculateTotalCombinations();
            TotalCombinationsTextBlock.Text += $" {remainingCombinations}";

            int duzina_kombinacije = _mainWindow.GetCombinationLength();
            int broj_loptica = _mainWindow.GetMaxNumber();
            int broj_zbranjenih = _mainWindow.GetExcludedNumbers().Count();
            int broj_brojeva = broj_loptica - broj_zbranjenih;

            // Ukupan broj mogućih kombinacija za loto 6/45, treba za svaki
            int totalPossibleCombinations = 8145060;

            /*if (duzina_kombinacije == 7 && broj_loptica == 35) // 7 od 35 (Hrvatska)
            {
                if (broj_brojeva == 35)
                {
                    totalPossibleCombinations = 6724520;
                }
                else if (broj_brojeva == 34)
                {
                    totalPossibleCombinations = 5379616;
                }
                else if (broj_brojeva == 33)
                {
                    totalPossibleCombinations = 4272048;
                }
                else if (broj_brojeva == 32)
                {
                    totalPossibleCombinations = 3365855;
                }
                else if (broj_brojeva == 31)
                {
                    totalPossibleCombinations = 2629575;
                }
                else if (broj_brojeva == 30)
                {
                    totalPossibleCombinations = 2035799;
                }
            }
            else if (duzina_kombinacije == 6 && broj_loptica == 45) // 6 od 45 (Hrvatska)
            {
                if (broj_brojeva == 45)
                {
                    totalPossibleCombinations = 8145060;
                }
                else if (broj_brojeva == 44)
                {
                    totalPossibleCombinations = 7059052;
                }
                else if (broj_brojeva == 43)
                {
                    totalPossibleCombinations = 6096454;
                }
                else if (broj_brojeva == 42)
                {
                    totalPossibleCombinations = 5245786;
                }
                else if (broj_brojeva == 41)
                {
                    totalPossibleCombinations = 4496387;
                }
                else if (broj_brojeva == 40)
                {
                    totalPossibleCombinations = 3838380;
                }
            }
            else if (duzina_kombinacije == 7 && broj_loptica == 39) // 7 od 39 (Srbija)
            {
                if (broj_brojeva == 39)
                {
                    totalPossibleCombinations = 15380936;
                }
                else if (broj_brojeva == 38)
                {
                    totalPossibleCombinations = 12620255;
                }
                else if (broj_brojeva == 37)
                {
                    totalPossibleCombinations = 10295472;
                }
                else if (broj_brojeva == 36)
                {
                    totalPossibleCombinations = 8347680;
                }
                else if (broj_brojeva == 35)
                {
                    totalPossibleCombinations = 6724520;
                }
                else if (broj_brojeva == 34)
                {
                    totalPossibleCombinations = 5379616;
                }
            }
            else if (duzina_kombinacije == 6 && broj_loptica == 44) // 6 od 44 (Slovenija)
            {
                if (broj_brojeva == 44)
                {
                    totalPossibleCombinations = 7059052;
                }
                else if (broj_brojeva == 43)
                {
                    totalPossibleCombinations = 6096454;
                }
                else if (broj_brojeva == 42)
                {
                    totalPossibleCombinations = 5245786;
                }
                else if (broj_brojeva == 41)
                {
                    totalPossibleCombinations = 4496387;
                }
                else if (broj_brojeva == 40)
                {
                    totalPossibleCombinations = 3838380;
                }
                else if (broj_brojeva == 39)
                {
                    totalPossibleCombinations = 3262622;
                }
            }
            else if (duzina_kombinacije == 6 && broj_loptica == 39) // 6 od 39 (BiH)
            {
                if (broj_brojeva == 39)
                {
                    totalPossibleCombinations = 3262622;
                }
                else if (broj_brojeva == 38)
                {
                    totalPossibleCombinations = 2760681;
                }
                else if (broj_brojeva == 37)
                {
                    totalPossibleCombinations = 2324784;
                }
                else if (broj_brojeva == 36)
                {
                    totalPossibleCombinations = 1947792;
                }
                else if (broj_brojeva == 35)
                {
                    totalPossibleCombinations = 1623160;
                }
                else if (broj_brojeva == 34)
                {
                    totalPossibleCombinations = 1344904;
                }
            }
            else if (duzina_kombinacije == 7 && broj_loptica == 37) // 7 od 37 (Makedonija)
            {
                if (broj_brojeva == 37)
                {
                    totalPossibleCombinations = 10295472;
                }
                else if (broj_brojeva == 36)
                {
                    totalPossibleCombinations = 8347680;
                }
                else if (broj_brojeva == 35)
                {
                    totalPossibleCombinations = 6724520;
                }
                else if (broj_brojeva == 34)
                {
                    totalPossibleCombinations = 5379616;
                }
                else if (broj_brojeva == 33)
                {
                    totalPossibleCombinations = 4272048;
                }
                else if (broj_brojeva == 32)
                {
                    totalPossibleCombinations = 3365855;
                }
            }*/

            if (duzina_kombinacije == 7 && broj_loptica == 35)
            {
                totalPossibleCombinations = 6724520;
            }
            else if (duzina_kombinacije == 6 && broj_loptica == 45)
            {
                totalPossibleCombinations = 8145060;
            }
            else if (duzina_kombinacije == 7 && broj_loptica == 39)
            {
                totalPossibleCombinations = 15380937;
            }
            else if (duzina_kombinacije == 6 && broj_loptica == 44)
            {
                totalPossibleCombinations = 7059052;
            }
            else if (duzina_kombinacije == 6 && broj_loptica == 39)
            {
                totalPossibleCombinations = 3262623;
            }
            else if (duzina_kombinacije == 7 && broj_loptica == 37)
            {
                totalPossibleCombinations = 10295472;
            }


            // Izračunaj broj eliminisanih kombinacija
            int excludedCombinations = totalPossibleCombinations - remainingCombinations;

            // Izračunaj procenat eliminisanih kombinacija
            double excludedPercentage = (double)excludedCombinations / totalPossibleCombinations * 100;


            // Prikazivanje broja eliminisanih kombinacija i procenta
            ExcludedCombinationsTextBlock.Text = $"Iz igre je izbačeno ukupno: {excludedCombinations:N0} kombinacija, što je {excludedPercentage:F2}% od svih mogućih kombinacija.";
        }

        private int CalculateTotalCombinations()
        {
            // Implementirajte logiku za izračunavanje kombinacija
            int maxNumber = _mainWindow.GetMaxNumber();
            int combLenght = _mainWindow.GetCombinationLength();
            int totalCombinations = Combinations._sve_kombinacije(maxNumber, combLenght, _mainWindow.GetExcludedNumbers());

            return totalCombinations;
        }

        private void NextStepButton_Click(object sender, RoutedEventArgs e)
        {
            // Implementirajte prelaz na sledeći korak
            _mainWindow.NavigateToFourthStepPage();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.BackToSecondStepPage();
        }
    }
}

