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
            CalculateTotalCombinations();
        }

        private void CalculateTotalCombinations()
        {
            // Implementirajte logiku za izračunavanje kombinacija
            int maxNumber = _mainWindow.GetMaxNumber();
            int combLenght = _mainWindow.GetCombinationLength();
            int totalCombinations = Combinations._sve_kombinacije(maxNumber, combLenght, _mainWindow.GetExcludedNumbers()).Count;

            TotalCombinationsTextBlock.Text = $"{totalCombinations}";
        }

        private void NextStepButton_Click(object sender, RoutedEventArgs e)
        {
            // Implementirajte prelaz na sledeći korak
            _mainWindow.NavigateToFourthStepPage();
        }
    }
}

