using System;
using System.Collections.Generic;
using System.IO;
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
            try
            {
                // Get the base directory where the executable is located
                string executablePath = AppDomain.CurrentDomain.BaseDirectory;

                // Navigate up to the project root directory
                string projectRootPath = Path.GetFullPath(Path.Combine(executablePath, @"..\..\..\"));

                // Define the relative path for the CSV file in the project root directory
                string filePath = Path.Combine(projectRootPath, "SacuvaneKombinacije.csv");

                // Check if allCombinations contains data
                if (allCombinations == null || allCombinations.Count == 0)
                {
                    MessageBox.Show("No combinations available to save.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Prepare the content to append
                StringBuilder combinationsText = new StringBuilder();

                // Add the combinations
                foreach (var combination in allCombinations)
                {
                    combinationsText.AppendLine(string.Join(",", combination));
                }

                // Append the content to the CSV file
                File.AppendAllText(filePath, combinationsText.ToString());

                MessageBox.Show("Kombinacije su dodane u 'SacuvaneKombinacije.csv' u istom folderu kao aplikacija.", "Spremljeno", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Došlo je do greške prilikom spremanja kombinacija: {ex.Message}", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void PrintCombinationsButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void PreviousStepButton_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic to navigate to the previous step
            _mainWindow.NavigateToSixthStepPage();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void NewCalculationButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateToStartPage();
        }
    }
}
