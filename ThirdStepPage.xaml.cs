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
        }

        private void CalculateTotalCombinations()
        {
            // Implementirajte logiku za izračunavanje kombinacija
            //int totalCombinations = Combinations._sve_kombinacije();

            //TotalCombinationsTextBlock.Text = $"Softver je izračunao sveukupni broj mogućih kombinacija, a on iznosi: {totalCombinations}";
        }

        private void NextStepButton_Click(object sender, RoutedEventArgs e)
        {
            // Implementirajte prelaz na sledeći korak
            _mainWindow.NavigateToFourthStepPage();
        }
    }
}

