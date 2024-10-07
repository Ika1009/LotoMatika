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
            TotalCombinationsTextBlock.Text = $"{remainingCombinations}";

            // Ukupan broj mogućih kombinacija za loto 6/45, treba za svaki
            int totalPossibleCombinations = 8145060;

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

        private void BackButton_Click2(object sender, RoutedEventArgs e)
        {
            _mainWindow.BackToSecondStepPage();
        }
    }
}

