using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Loto_App
{
    public partial class ArhivaPage : Page
    {
        private readonly MainWindow _mainWindow;
        private string FilePath; // Local variable to store the file path

        public ArhivaPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow ?? throw new ArgumentNullException(nameof(mainWindow));
        }

        private void LoadCombinationsFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);

                if (lines.Length == 0)
                {
                    CombinationsTextBlock.Text = "Trenutno nema kombinacija.";
                }
                else
                {
                    StringBuilder combinations = new StringBuilder();

                    // Reverse the order of lines
                    Array.Reverse(lines);

                    foreach (var line in lines)
                    {
                        // Assuming the format is "date, combination"
                        var parts = line.Split(',', 2);
                        if (parts.Length == 2)
                        {
                            // Remove any leading or trailing spaces
                            string date = parts[0].Trim();
                            string combination = parts[1].Trim();

                            // Format the combination with the date aligned to the right
                            string formattedLine = $"{combination.PadRight(40)} {date}";
                            combinations.AppendLine(formattedLine);
                        }
                    }

                    CombinationsTextBlock.Text = combinations.ToString();
                }

                // Hide buttons after file load
                OptionsPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                CombinationsTextBlock.Text = "CSV fajl nije pronađen.";
            }
        }

        private void LoadFile_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string fileName = button.Tag.ToString();
                string executablePath = AppDomain.CurrentDomain.BaseDirectory;
                FilePath = Path.Combine(executablePath, fileName);

                // Load the selected file
                LoadCombinationsFromFile(FilePath);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateToStartPage();
        }
    }
}
