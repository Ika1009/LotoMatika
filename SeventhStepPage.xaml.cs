using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Loto_App
{
    public partial class SeventhStepPage : Page
    {
        private MainWindow _mainWindow;
        private List<List<int>> allCombinations;

        public SeventhStepPage(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();
        }

        private void ShowCombinationsButton_Click(object sender, RoutedEventArgs e)
        {
            // Generate and display combinations
            allCombinations = GenerateCombinations();
            StringBuilder combinationsText = new StringBuilder();
            foreach (var combination in allCombinations)
            {
                combinationsText.AppendLine(string.Join(", ", combination));
            }

            CombinationsTextBlock.Text = combinationsText.ToString();
            CombinationsTextBlock.Visibility = Visibility.Visible;

            // Show additional options after displaying combinations
            ShowCombinationsButton.Visibility = Visibility.Collapsed;
            OptionsPanel.Visibility = Visibility.Visible;
        }

        private List<List<int>> GenerateCombinations()
        {
            // Placeholder logic for generating combinations
            // Replace this with your actual combination generation logic
            int maxNumber = _mainWindow.GetMaxNumber();
            int combinationLength = _mainWindow.GetCombinationLength();
            int combinationsRequested = _mainWindow.GetCombinationsRequested();
            List<int> excludedNumbers = _mainWindow.GetExcludedNumbers();
            List<int> favoritedNumbers = _mainWindow.GetFavoriteNumbers();
            int favoriteUsage = _mainWindow.GetFavoriteUsage();

            return Combinations._neke_kombinacije(maxNumber, combinationLength, combinationsRequested, excludedNumbers, favoritedNumbers, favoriteUsage);
        }

        private void SaveCombinationsButton_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic to save combinations to computer
            MessageBox.Show("Kombinacije su spremljene na računalo.");
        }

        private void PrintCombinationsButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder combinationsText = new StringBuilder();
            foreach (var combination in allCombinations)
            {
                combinationsText.AppendLine(string.Join(", ", combination));
            }

            // Implement logic to print combinations
            // Example: Print the combinationsText.ToString()
            MessageBox.Show("Kombinacije su poslane na pisač.");
        }

        private void NextStepButton_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic to navigate to the next step
            // _mainWindow.NavigateToSixthStepPage();
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
