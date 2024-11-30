using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Animation;
using System.Windows.Media;

namespace Loto_App
{
    public partial class SeventhStepPage : Page
    {
        private MainWindow _mainWindow;
        private List<List<int>> allCombinations;
        private int max_number;
        private int combination_length;

        public SeventhStepPage(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            this.max_number = mainWindow.GetMaxNumber();
            this.combination_length = mainWindow.GetCombinationLength();
            InitializeComponent();
        }

        private async void ShowCombinationsButton_Click(object sender, RoutedEventArgs e)
        {
            ShowLoadingIndicator();

            await Task.Delay(2000);

            // Generate and display combinations
            allCombinations = GenerateCombinations();
            StringBuilder combinationsText = new StringBuilder();
            foreach (var combination in allCombinations)
            {
                combinationsText.AppendLine(string.Join("    ", combination));
            }

            CombinationsTextBlock.Text = combinationsText.ToString();
            CombinationsTextBlock.Visibility = Visibility.Visible;

            // Show additional options after displaying combinations
            ShowCombinationsButton.Visibility = Visibility.Collapsed;
            OptionsPanel.Visibility = Visibility.Visible;

            HideLoadingIndicator();
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

                // Determine the base file name based on max_number and combination_length
                string baseFileName = (max_number, combination_length) switch
                {
                    (35, 7) => "7od35-Hrvatska",
                    (45, 6) => "6od45-Hrvatska",
                    (39, 7) => "7od39-Srbija",
                    (44, 6) => "6od44-Slovenija",
                    (39, 6) => "6od39-BiH",
                    (37, 7) => "7od37-Makedonija",
                    _ => throw new InvalidOperationException("Unknown game type")
                };

                // Add the current date and time to the file name in a readable format
                string dateTimeSuffix = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                string fileName = $"{baseFileName}_{dateTimeSuffix}.csv";

                string filePath = Path.Combine(executablePath, fileName);

                // Check if allCombinations contains data
                if (allCombinations == null || allCombinations.Count == 0)
                {
                    MessageBox.Show("No combinations available to save.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Prepare the content to save
                StringBuilder combinationsText = new StringBuilder();

                // Get the current date and time
                string currentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                // Add the combinations with the current date and time
                foreach (var combination in allCombinations)
                {
                    string combinationLine = string.Join(" ", combination);
                    combinationsText.AppendLine($"{currentDateTime}, {combinationLine}");
                }

                // Save the content to a new CSV file
                File.WriteAllText(filePath, combinationsText.ToString());

                MessageBox.Show($"Kombinacije su sačuvane u '{fileName}' u istom folderu kao aplikacija.", "Spremljeno", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Došlo je do greške prilikom spremanja kombinacija: {ex.Message}", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        /*private void PrintCombinationsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Check if there are combinations to print
                if (allCombinations == null || allCombinations.Count == 0)
                {
                    MessageBox.Show("No combinations available to print.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Create a StringBuilder to store the combinations as a single string
                StringBuilder combinationsText = new StringBuilder();

                // Get the current date and time
                string currentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                // Add the combinations with the current date and time
                foreach (var combination in allCombinations)
                {
                    string combinationLine = string.Join(", ", combination);
                    combinationsText.AppendLine($"{currentDateTime}, {combinationLine}");
                }

                // Convert the combinationsText to a FlowDocument for printing
                FlowDocument doc = new FlowDocument(new Paragraph(new Run(combinationsText.ToString())))
                {
                    FontSize = 14,
                    PagePadding = new Thickness(50)
                };

                // Create a PrintDialog
                PrintDialog printDialog = new PrintDialog();

                // Show the print dialog and print the document if the user clicks 'Print'
                if (printDialog.ShowDialog() == true)
                {
                    IDocumentPaginatorSource idpSource = doc;
                    printDialog.PrintDocument(idpSource.DocumentPaginator, "Combinations Printout");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during printing: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }<Button Content="ISPIŠI NA PISAČU" Click="PrintCombinationsButton_Click"/>*/

        private void PreviousStepButton_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic to navigate to the previous step
            _mainWindow.NavigateToSixthStepPage();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Archive_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateToArhivaPage();
        }

        private void NewCalculationButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateToStartPage();
        }

        private void ShowLoadingIndicator()
        {
            LoadingIndicator.Visibility = Visibility.Visible;

            DoubleAnimation rotateAnimation = new DoubleAnimation
            {
                From = 0,
                To = 360,
                Duration = TimeSpan.FromSeconds(1),
                RepeatBehavior = RepeatBehavior.Forever
            };

            LoadingRotateTransform.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
        }

        private void HideLoadingIndicator()
        {
            LoadingRotateTransform.BeginAnimation(RotateTransform.AngleProperty, null);
            LoadingIndicator.Visibility = Visibility.Hidden;
        }
    }
}
