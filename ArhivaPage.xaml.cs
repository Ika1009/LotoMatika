using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;

namespace Loto_App
{
    public partial class ArhivaPage : Page
    {
        private readonly MainWindow _mainWindow;
        private string FilePath;
        private int broj_loptica;
        private List<int> pogodjeni = new List<int> { 0, 0, 0, 0, 0, 0, 0 }; // Lista pogodjenih brojeva
        private int bonus; // Bonus broj
        private int[] greenCounts = new int[8]; // Broj kombinacija sa 0 do 7 zelenih brojeva
        private int[] greenRedCounts = new int[8]; // Broj kombinacija sa 0 do 7 zelenih i crvenih brojeva

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
                Array.Reverse(lines);

                // Reset brojača
                Array.Clear(greenCounts, 0, greenCounts.Length);
                Array.Clear(greenRedCounts, 0, greenRedCounts.Length);

                var document = new FlowDocument();
                foreach (var line in lines)
                {
                    var parts = line.Split(',', 2);
                    if (parts.Length == 2)
                    {
                        string date = parts[0].Trim();
                        string combination = parts[1].Trim();
                        var paragraph = new Paragraph();

                        var numbers = combination.Split(',').Select(int.Parse).ToList();

                        foreach (var number in numbers)
                        {
                            var run = new Run(number + " ");
                            if (pogodjeni.Contains(number))
                            {
                                run.Foreground = Brushes.Red;
                            }
                            paragraph.Inlines.Add(run);
                        }

                        paragraph.Inlines.Add(new Run(" " + date) { Foreground = Brushes.Black });
                        document.Blocks.Add(paragraph);
                    }
                }

                CombinationsRichTextBox.Document = document;
                ShowInputBoxes(DetermineMaxNumber(filePath));
                ButtonsPanel.Visibility = Visibility.Collapsed;
                InputBoxesPanel.Visibility = Visibility.Visible;
                SaveButton.Visibility = Visibility.Visible;
            }
            else
            {
                CombinationsRichTextBox.Document.Blocks.Clear();
                CombinationsRichTextBox.Document.Blocks.Add(new Paragraph(new Run("CSV fajl nije pronađen.")));
                ButtonsPanel.Visibility = Visibility.Visible;
                InputBoxesPanel.Visibility = Visibility.Collapsed;
            }
        }

        private int DetermineMaxNumber(string filePath)
        {
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
                broj_loptica = DetermineNumberOfBalls(FilePath);
                LoadCombinationsFromFile(FilePath);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateToStartPage();
        }

        private void SaveNumbers_Click(object sender, RoutedEventArgs e)
        {
            pogodjeni.Clear();
            bonus = 0;
            bool hasError = false;
            bool hasEmptyField = false;
            HashSet<int> seenNumbers = new HashSet<int>();
            HashSet<int> duplicateNumbers = new HashSet<int>();

            for (int i = 0; i < InputBoxesPanel.Children.Count; i++)
            {
                if (InputBoxesPanel.Children[i] is TextBox textBox)
                {
                    if (string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        hasEmptyField = true;
                    }
                    else if (int.TryParse(textBox.Text, out int number))
                    {
                        if (number > broj_loptica)
                        {
                            textBox.Text = "";
                            hasError = true;
                        }
                        else
                        {
                            if (i < InputBoxesPanel.Children.Count - 1)
                            {
                                if (seenNumbers.Contains(number))
                                {
                                    duplicateNumbers.Add(number);
                                }
                                else
                                {
                                    seenNumbers.Add(number);
                                    pogodjeni.Add(number);
                                }
                            }
                            else
                            {
                                bonus = number;
                            }
                        }
                    }
                    else
                    {
                        textBox.Text = "";
                        hasError = true;
                    }
                }
            }

            if (duplicateNumbers.Count > 0)
            {
                foreach (var number in duplicateNumbers)
                {
                    pogodjeni.Remove(number);
                    foreach (var child in InputBoxesPanel.Children)
                    {
                        if (child is TextBox textBox && textBox.Text == number.ToString())
                        {
                            textBox.Text = "";
                        }
                    }
                }
                hasError = true;
            }

            if (hasEmptyField)
            {
                if (!hasError)
                {
                    MessageBox.Show("Molimo popunite sva polja.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("Preveliki brojevi su izbrisani. Molimo popunite sva polja.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (hasError)
            {
                MessageBox.Show("Preveliki brojevi ili nevalidni unosi su izbrisani. Molimo proverite unos.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                // Sakrij kockice za unos
                InputBoxesPanel.Visibility = Visibility.Collapsed;

                // Sakrij dugme za čuvanje
                SaveButton.Visibility = Visibility.Collapsed;

                // Prikaži dugme za nove brojeve
                NewNumbersButton.Visibility = Visibility.Visible;

                if (!hasError && !hasEmptyField)
                {
                    // Sakrij kockice za unos
                    InputBoxesPanel.Visibility = Visibility.Collapsed;

                    // Sakrij dugme za čuvanje
                    SaveButton.Visibility = Visibility.Collapsed;

                    // Prikaži dugme za nove brojeve
                    NewNumbersButton.Visibility = Visibility.Visible;

                    // Ako nema grešaka i sva polja su popunjena, sačuvaj brojeve i oboji
                    SaveCombinations(greenCounts, greenRedCounts);

                    // Prikaži brojeve kombinacija
                    CountTextBlock.Text = GenerateCountText();
                    CountTextBlock.Visibility = Visibility.Visible;
                }
            }
        }

        private void SaveCombinations(int[] greenCounts, int[] greenRedCounts)
        {
            var document = CombinationsRichTextBox.Document as FlowDocument;
            var paragraphs = document?.Blocks.OfType<Paragraph>().ToList();

            if (paragraphs != null)
            {
                // Prvo oboji sve brojeve crno
                foreach (var paragraph in paragraphs)
                {
                    foreach (var inline in paragraph.Inlines.OfType<Run>())
                    {
                        if (int.TryParse(inline.Text.Trim(), out int number))
                        {
                            inline.Foreground = Brushes.Black; // Oboji sve brojeve crno
                        }
                        else
                        {
                            inline.Foreground = Brushes.Black; // Ako nije broj, postavi crnu boju
                        }
                    }
                }

                // Zatim oboji pogođene brojeve zeleno i bonus broj crveno
                foreach (var paragraph in paragraphs)
                {
                    int broj_zelenih = 0;
                    bool ima_crvenih = false;
                    foreach (var inline in paragraph.Inlines.OfType<Run>())
                    {
                        if (int.TryParse(inline.Text.Trim(), out int number))
                        {
                            if (pogodjeni.Contains(number))
                            {
                                inline.Foreground = Brushes.Green; // Zeleni tekst za pogođene brojeve
                                broj_zelenih++; // Broji zelene brojeve
                            }
                            else if (number == bonus) // Bonus broj
                            {
                                inline.Foreground = Brushes.Red; // Crveni tekst za bonus broj
                                ima_crvenih = true; // Označava da ima crvenih brojeva
                            }
                        }
                    }

                    if (broj_zelenih < greenCounts.Length)
                    {
                        if (ima_crvenih)
                        {
                            greenRedCounts[broj_zelenih]++;
                        }
                        else
                        {
                            greenCounts[broj_zelenih]++;
                        }
                    }
                }
            }
        }

        private int DetermineNumberOfBalls(string filePath)
        {
            if (filePath.Contains("35"))
            {
                return 35;
            }
            else if (filePath.Contains("45"))
            {
                return 45;
            }
            else if (filePath.Contains("39"))
            {
                return 39;
            }
            else if (filePath.Contains("44"))
            {
                return 44;
            }
            else if (filePath.Contains("37"))
            {
                return 37;
            }
            else
            {
                return 35;
            }
        }

        private void NewNumbersButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateToArhivaPage();
        }

        private string GenerateCountText()
        {
            string text = "";

            if (FilePath.Contains("7od35"))
            {
                text += $"7: {greenCounts[7]} \n";
                text += $"6 + 1: {greenRedCounts[6]} \n";
                text += $"6: {greenCounts[6]} \n";
                text += $"5 + 1: {greenRedCounts[5]} \n";
                text += $"5: {greenCounts[5]} \n";
                text += $"4 + 1: {greenRedCounts[4]} \n";
                text += $"4: {greenCounts[4]} \n";
                text += $"3 + 1: {greenRedCounts[3]} \n";
                text += $"0 + 1: {greenRedCounts[0]} \n";

            }
            else if (FilePath.Contains("6od45"))
            {
                text += $"6: {greenCounts[6]} \n";
                text += $"5 + 1: {greenRedCounts[5]} \n";
                text += $"5: {greenCounts[5]} \n";
                text += $"4 + 1: {greenRedCounts[4]} \n";
                text += $"4: {greenCounts[4]} \n";
                text += $"3 + 1: {greenRedCounts[3]} \n";
                text += $"3: {greenCounts[3]} \n";
            }
            else if (FilePath.Contains("7od39"))
            {
                text += $"7: {greenCounts[7]} \n";
                text += $"6: {greenCounts[6]} \n";
                text += $"5: {greenCounts[5]} \n";
                text += $"4: {greenCounts[4]} \n";
                text += $"3: {greenCounts[3]} \n";
            }
            else if (FilePath.Contains("6od44"))
            {
                text += $"6: {greenCounts[6]} \n";
                text += $"5 + 1: {greenRedCounts[5]} \n";
                text += $"5: {greenCounts[5]} \n";
                text += $"4 + 1: {greenRedCounts[4]} \n";
                text += $"4: {greenCounts[4]} \n";
                text += $"3 + 1: {greenRedCounts[3]} \n";
                text += $"3: {greenCounts[3]} \n";
                text += $"0 + 1: {greenRedCounts[0]} \n";
            }
            else if (FilePath.Contains("6od39"))
            {
                text += $"6: {greenCounts[6]} \n";
                text += $"5 + 1: {greenRedCounts[5]} \n";
                text += $"5: {greenCounts[5]} \n";
                text += $"4 + 1: {greenRedCounts[4]} \n";
                text += $"4: {greenCounts[4]} \n";
                text += $"3 + 1: {greenRedCounts[3]} \n";
                text += $"3: {greenCounts[3]} \n";
                text += $"2 + 1: {greenRedCounts[2]} \n";
            }
            else if (FilePath.Contains("7od37"))
            {
                text += $"7: {greenCounts[7]} \n";
                text += $"6: {greenCounts[6]} \n";
                text += $"5: {greenCounts[5]} \n";
                text += $"4: {greenCounts[4]} \n";
            }

            return text;
        }
    }
}
