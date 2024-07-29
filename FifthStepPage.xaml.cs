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
    public partial class FifthStepPage : Page
    {
        MainWindow _mainWindow;
        public FifthStepPage(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            // Implementirajte logiku za izračunavanje kombinacija
            int totalCombinations = CalculateTotalCombinations();

            TotalCombinationsTextBlock.Text = $"Softver je izračunao sveukupni broj mogućih kombinacija, a on iznosi: {totalCombinations}";
        }

        private int CalculateTotalCombinations()
        {
            //Combinations._sve_kombinacije()
            return 123456;
        }

        private void NextStepButton_Click(object sender, RoutedEventArgs e)
        {
            // Implementirajte prelaz na sledeći korak
            _mainWindow.NavigateToSixthStepPage();
        }
    }
}

