using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Loto_App
{
    public partial class ArhivaPage : Page
    {
        private readonly MainWindow _mainWindow;

        public ArhivaPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow ?? throw new ArgumentNullException(nameof(mainWindow));
            LoadCombinationsFromFile();
        }

        private void LoadCombinationsFromFile()
        {
            string executablePath = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(executablePath, "SacuvaneKombinacije.csv");

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
            }
            else
            {
                CombinationsTextBlock.Text = "CSV fajl nije pronađen.";
            }

            OptionsPanel.Visibility = Visibility.Visible;
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateToStartPage();
        }
    }
}
