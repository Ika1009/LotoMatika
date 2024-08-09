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
        private string FilePath;

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
                    ButtonsPanel.Visibility = Visibility.Visible;
                    InputBoxesPanel.Visibility = Visibility.Collapsed;
                }
                else
                {
                    StringBuilder combinations = new StringBuilder();

                    // Reverse the order of lines
                    Array.Reverse(lines);

                    foreach (var line in lines)
                    {
                        var parts = line.Split(',', 2);
                        if (parts.Length == 2)
                        {
                            string date = parts[0].Trim();
                            string combination = parts[1].Trim();
                            string formattedLine = $"{combination.PadRight(40)} {date}";
                            combinations.AppendLine(formattedLine);
                        }
                    }

                    CombinationsTextBlock.Text = combinations.ToString();

                    // Prikazivanje unosa kutijica na osnovu maksimalnog broja
                    int maxNumber = DetermineMaxNumber(filePath); // Pretpostavimo da imate metodu za određivanje maksimalnog broja
                    ShowInputBoxes(maxNumber);
                }

                ButtonsPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                CombinationsTextBlock.Text = "CSV fajl nije pronađen.";
                ButtonsPanel.Visibility = Visibility.Visible;
                InputBoxesPanel.Visibility = Visibility.Collapsed;
            }
        }

        private int DetermineMaxNumber(string filePath)
        {
            // Pretpostavite da možete odrediti maksimalan broj na osnovu imena datoteke
            // Na primer, 7 za "7 od 35" itd.
            if (filePath.Contains("7od35") || filePath.Contains("7od39") || filePath.Contains("7od37"))
            {
                return 7;
            }
            else if (filePath.Contains("6od45") || filePath.Contains("6od44") || filePath.Contains("6od39"))
            {
                return 6;
            }
            else
            {
                return 6; // Podrazumevano
            }
        }

        private void ShowInputBoxes(int numberOfBoxes)
        {
            InputBoxesPanel.Children.Clear();

            // Dodavanje glavnih textbox-ova
            for (int i = 0; i < numberOfBoxes; i++)
            {
                TextBox inputBox = new TextBox
                {
                    Width = 40, // Kvadratni oblik
                    Height = 40,
                    MaxLength = 2, // Omogućava unošenje jednog ili dva broja
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(5)
                };

                InputBoxesPanel.Children.Add(inputBox);
            }

            // Dodavanje dodatnog textbox-a izdvojenog desno
            TextBox extraInputBox = new TextBox
            {
                Width = 40,
                Height = 40,
                MaxLength = 2,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                Margin = new Thickness(20, 5, 0, 5) // Veća leva margina za razdvajanje
            };

            InputBoxesPanel.Children.Add(extraInputBox);

            InputBoxesPanel.Visibility = Visibility.Visible;
        }

        private void LoadFile_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string fileName = button.Tag.ToString();
                string executablePath = AppDomain.CurrentDomain.BaseDirectory;
                FilePath = Path.Combine(executablePath, fileName);

                // Učitaj izabrani fajl
                LoadCombinationsFromFile(FilePath);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateToStartPage();
        }
    }
}
