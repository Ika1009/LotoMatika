using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Xps.Packaging;

namespace Loto_App
{
    public partial class SeventhStepPage : Page
    {
        private MainWindow _mainWindow;

        // Variables to store calculated values
        private int totalCombinations;
        private int excludedCombinations;

        public SeventhStepPage(MainWindow mainWindow, int totalCombinations, int excludedCombinations)
        {
            _mainWindow = mainWindow;
            this.totalCombinations = totalCombinations;
            this.excludedCombinations = excludedCombinations;
            InitializeComponent();

            // Update text blocks with calculated values
            //TotalCombinationsText.Text = $"Softver je izračunao sveukupni broj mogućih kombinacija, a on iznosi: {totalCombinations}";
            //ExcludedCombinationsText.Text = $"Softver je iz igre izbacio ukupno {excludedCombinations} kombinacija, kako bi vam omogućio da lakše osvojite jedan ili više novčanih dobitaka.";
        }

        private void SaveCombinationsButton_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic to save combinations to computer
            MessageBox.Show("Kombinacije su spremljene na računalo.");
        }

        private void PrintCombinationsButton_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic to print combinations
            MessageBox.Show("Kombinacije su poslane na pisač.");
        }

        private void NextStepButton_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic to navigate to the next step
            //_mainWindow.NavigateToSixthStepPage();
        }

        private void PreviousStepButton_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic to navigate to the previous step
            _mainWindow.NavigateToSixthStepPage();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic to exit the program
            Application.Current.Shutdown();
        }

        private void NewCalculationButton_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic to start a new calculation
            _mainWindow.NavigateToFirstStepPage();
        }
    }
}
