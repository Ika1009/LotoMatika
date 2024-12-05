using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;

namespace Loto_App
{
    public partial class ArhivaPage : Page
    {
        private readonly MainWindow _mainWindow;
        string FilePath;
        int broj_loptica;
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
                string lastDateTime = string.Empty; // Zapamti poslednji datum i vreme

                int brojac = 1;
                foreach (var line in lines)
                {
                    var parts = line.Split(',', 2);
                    if (parts.Length == 2)
                    {
                        string dateTime = parts[0].Trim();
                        string combination = parts[1].Trim();

                        // Proveri da li se datum/vreme promenio
                        if (dateTime != lastDateTime)
                        {
                            // Dodaj novi red sa datumom/vremenom u plavoj boji
                            var dateParagraph = new Paragraph();
                            var dateRun = new Run(dateTime)
                            {
                                Foreground = Brushes.Blue,
                                FontWeight = FontWeights.Bold
                            };
                            var dateBlock = new TextBlock(dateRun)
                            {
                                IsHitTestVisible = false // Onemogući selektovanje
                            };
                            dateParagraph.Inlines.Add(dateBlock); // Dodaj TextBlock u paragraf
                            document.Blocks.Add(dateParagraph);

                            lastDateTime = dateTime; // Ažuriraj poslednji datum/vreme
                        }

                        // Dodaj kombinaciju
                        var paragraph = new Paragraph();
                        var numbers = combination.Split(' ').Select(int.Parse).ToList();

                        paragraph.Inlines.Add(brojac.ToString() + ".)     ");
                        foreach (var number in numbers)
                        {
                            var run = new Run(number + " ");
                            if (pogodjeni.Contains(number))
                            {
                                run.Foreground = Brushes.Red;
                            }
                            paragraph.Inlines.Add(run);
                        }

                        document.Blocks.Add(paragraph); // Dodaj kombinaciju

                        brojac++;
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

                // Dodavanje KeyDown događaja
                inputBox.KeyDown += InputBox_KeyDown;

                InputBoxesPanel.Children.Add(inputBox);
            }

            if (!((DetermineNumberOfBalls(FilePath) == 39 && DetermineMaxNumber(FilePath) == 7)
            || (DetermineNumberOfBalls(FilePath) == 37 && DetermineMaxNumber(FilePath) == 7)))
            {
                TextBox extraInputBox = new TextBox
                {
                    Width = 40,
                    Height = 40,
                    MaxLength = 2,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(20, 5, 0, 5) // Veća leva margina za razdvajanje
                };

                // Dodavanje KeyDown događaja
                extraInputBox.KeyDown += InputBox_KeyDown;

                InputBoxesPanel.Children.Add(extraInputBox);
            }
        }

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Pronađi trenutni TextBox
                TextBox currentTextBox = sender as TextBox;
                if (currentTextBox != null)
                {
                    // Pronađi indeks trenutnog TextBox-a
                    int currentIndex = InputBoxesPanel.Children.IndexOf(currentTextBox);

                    // Ako nije poslednji TextBox, pređi na sledeći
                    if (currentIndex < InputBoxesPanel.Children.Count - 1)
                    {
                        TextBox nextTextBox = InputBoxesPanel.Children[currentIndex + 1] as TextBox;
                        nextTextBox.Focus(); // Postavi fokus na sledeći TextBox
                    }

                    // Spreči zvučni signal
                    e.Handled = true;
                }
            }
        }

        private void LoadFile_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                // Extract the button's text and clean it up by removing spaces and everything after '('
                string buttonName = button.Content.ToString();
                int parenthesisIndex = buttonName.IndexOf('(');

                if (parenthesisIndex > -1)
                {
                    buttonName = buttonName.Substring(0, parenthesisIndex);
                }

                // Remove spaces from the cleaned name
                string cleanedButtonName = buttonName.Replace(" ", "");

                string executablePath = AppDomain.CurrentDomain.BaseDirectory;

                // Search for CSV files that start with the cleaned button name
                string[] files = Directory.GetFiles(executablePath, cleanedButtonName + "*.csv");

                if (files.Length > 0)
                {
                    // Clear previous text in RichTextBox
                    CombinationsRichTextBox.Document.Blocks.Clear();

                    foreach (var file in files)
                    {
                        // Get file name without path and extension
                        string fileName = Path.GetFileNameWithoutExtension(file);

                        // Create a new paragraph to hold the file name
                        Paragraph para = new Paragraph();

                        // Create a hyperlink-like Run for the file name
                        Run fileNameRun = new Run(fileName)
                        {
                            Foreground = Brushes.Green, // Set the text color to green
                            TextDecorations = TextDecorations.Underline // Make it look clickable
                        };

                        // Add a mouse event to simulate click behavior
                        fileNameRun.MouseDown += (s, args) =>
                        {
                            if (args.ChangedButton == MouseButton.Left)
                            {
                                FilePath = file;
                                LoadCombinationsFromFile(file); // Call the function with the file path
                            }
                        };

                        // Add the Run to the paragraph
                        para.Inlines.Add(fileNameRun);

                        // Add the paragraph to the RichTextBox
                        CombinationsRichTextBox.Document.Blocks.Add(para);
                    }
                }
                else
                {
                    // If no files found, display a message in green
                    TextRange range = new TextRange(CombinationsRichTextBox.Document.ContentStart, CombinationsRichTextBox.Document.ContentEnd);
                    range.Text = "Nema fajlova koji počinju sa: " + cleanedButtonName;
                    range.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Green); // Set the message color to green
                }
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

            for (int i = 0; ((i < (DetermineMaxNumber(FilePath) + 1)) && !((DetermineNumberOfBalls(FilePath) == 39 && DetermineMaxNumber(FilePath) == 7)
            || (DetermineNumberOfBalls(FilePath) == 37 && DetermineMaxNumber(FilePath) == 7))) || ((i < DetermineMaxNumber(FilePath)) && ((DetermineNumberOfBalls(FilePath) == 39 && DetermineMaxNumber(FilePath) == 7)
            || (DetermineNumberOfBalls(FilePath) == 37 && DetermineMaxNumber(FilePath) == 7))); i++)
            {
                if (InputBoxesPanel.Children[i] is TextBox textBox)
                {
                    if (string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        hasEmptyField = true;
                    }
                    else if (int.TryParse(textBox.Text, out int number))
                    {
                        if (number > DetermineNumberOfBalls(FilePath))
                        {
                            textBox.Text = "";
                            hasError = true;
                        }
                        else
                        {
                            if (((i < (DetermineMaxNumber(FilePath) + 1)) && !((DetermineNumberOfBalls(FilePath) == 39 && DetermineMaxNumber(FilePath) == 7)
                            || (DetermineNumberOfBalls(FilePath) == 37 && DetermineMaxNumber(FilePath) == 7))) || ((i < DetermineMaxNumber(FilePath)) && ((DetermineNumberOfBalls(FilePath) == 39 && DetermineMaxNumber(FilePath) == 7)
                            || (DetermineNumberOfBalls(FilePath) == 37 && DetermineMaxNumber(FilePath) == 7))))
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
                    foreach (var inline in paragraph.Inlines.OfType<Run>().ToList())
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

                    foreach (var inline in paragraph.Inlines.OfType<Run>().ToList())
                    {
                        if (int.TryParse(inline.Text.Trim(), out int number))
                        {
                            InlineUIContainer wrappedNumber = null;

                            if (pogodjeni.Contains(number))
                            {
                                // Kreiraj zaokružen zeleni broj
                                wrappedNumber = CreateRoundedNumber(number.ToString(), Brushes.Green, Brushes.White);
                                broj_zelenih++; // Broji zelene brojeve
                            }
                            else if (number == bonus) // Bonus broj
                            {
                                // Kreiraj zaokružen crveni broj
                                wrappedNumber = CreateRoundedNumber(number.ToString(), Brushes.Red, Brushes.White);
                                ima_crvenih = true; // Označava da ima crvenih brojeva
                            }

                            if (wrappedNumber != null)
                            {
                                // Zameni postojeći `Run` sa `InlineUIContainer`
                                paragraph.Inlines.InsertAfter(inline, wrappedNumber);
                                paragraph.Inlines.Remove(inline);
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

        // Funkcija koja kreira zaokružen broj
        private InlineUIContainer CreateRoundedNumber(string text, Brush background, Brush foreground)
        {
            var border = new Border
            {
                Background = background,
                BorderBrush = foreground,
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(8), // Slightly smaller corner radius for compact appearance
                Padding = new Thickness(3, 0, 3, 0), // Reduced vertical padding to minimize height
                Child = new TextBlock
                {
                    Text = text,
                    Foreground = foreground,
                    FontSize = 12, // Keep font size small and readable
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    TextAlignment = TextAlignment.Center
                }
            };

            return new InlineUIContainer(border);
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

        // Field to store the selected combination and previous selection
        private string selectedCombination = null;
        private TextRange previousSelection = null; // To store the previous selection for clearing

        // Mouse click event handler
        private void CombinationsRichTextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Dobavi trenutni red (liniju) na koju je kliknuto
            var richTextBox = sender as RichTextBox;
            var point = e.GetPosition(richTextBox);

            // Dobavi tekst na koji je kliknuto
            TextPointer textPointer = richTextBox.GetPositionFromPoint(point, true);
            if (textPointer != null)
            {
                // Pronađi celu liniju koja sadrži kliknutu tačku
                var lineStart = textPointer.GetLineStartPosition(0);
                var lineEnd = textPointer.GetLineStartPosition(1) ?? richTextBox.Document.ContentEnd;
                TextRange clickedLineRange = new TextRange(lineStart, lineEnd);

                // Clear previous selection highlight
                if (previousSelection != null)
                {
                    // Reset the background of the previously selected line
                    previousSelection.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Transparent);
                }

                // Set the selected combination to the text of the clicked line
                selectedCombination = clickedLineRange.Text.Trim();

                // Highlight the clicked line
                clickedLineRange.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.LightBlue); // Change the highlight color as needed
                previousSelection = clickedLineRange; // Store the current selection

                // Proveri da li linija sadrži znak '-'
                if (selectedCombination.Contains("-") || (selectedCombination == ""))
                {
                    // Sakrij Delete dugme ako linija sadrži '-'
                    DeleteButton.Visibility = Visibility.Collapsed;
                }
                else
                {
                    // Prikaži Delete dugme za ostale tekstove
                    DeleteButton.Visibility = Visibility.Visible;
                }
            }
        }

        private void CombinationsRichTextBox_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            var richTextBox = sender as RichTextBox;
            var point = e.GetPosition(richTextBox);

            // Get the TextPointer at the mouse position
            TextPointer textPointer = richTextBox.GetPositionFromPoint(point, true);
            if (textPointer != null)
            {
                // Get the line containing the mouse position
                TextPointer lineStart = textPointer.GetLineStartPosition(0);
                TextPointer lineEnd = textPointer.GetLineStartPosition(1) ?? richTextBox.Document.ContentEnd;

                if (lineStart != null && lineEnd != null)
                {
                    // Extract the text for the line
                    var textRange = new TextRange(lineStart, lineEnd);
                    string lineText = textRange.Text.Trim();

                    // Ensure the line is not empty
                    if (!string.IsNullOrEmpty(lineText))
                    {
                        // Check the color of the text in the line
                        var foreground = textRange.GetPropertyValue(TextElement.ForegroundProperty) as Brush;

                        if (foreground is SolidColorBrush solidColorBrush)
                        {
                            // Change the cursor to a hand if the color is green or black
                            if (solidColorBrush.Color == Colors.Green || solidColorBrush.Color == Colors.Black)
                            {
                                richTextBox.Cursor = Cursors.Hand;
                                return;
                            }
                        }
                    }
                    else
                    {
                        richTextBox.Cursor = Cursors.Arrow;
                    }
                }
            }

            // Reset the cursor to default if not over a valid line
            richTextBox.Cursor = Cursors.Arrow;
        }



        // Click event for the delete button
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCombination != null)
            {
                // Prvo, ukloni liniju iz RichTextBox-a
                var richTextBox = CombinationsRichTextBox; // Your RichTextBox instance

                // Očekivani format: "kombinacija - dd.MM.yyyy HH:mm"
                foreach (var block in richTextBox.Document.Blocks)
                {
                    var textRange = new TextRange(block.ContentStart, block.ContentEnd);
                    // Proveri da li tekst u RichTextBox-u sadrži selectedCombination
                    if (textRange.Text.Trim() == selectedCombination)
                    {
                        richTextBox.Document.Blocks.Remove(block);
                        break; // Ukloni samo prvu pronađenu kombinaciju
                    }
                }

                // Sada obriši kombinaciju iz CSV fajla
                DeleteCombination(selectedCombination.Contains(")     ") ? selectedCombination.Substring(selectedCombination.IndexOf(")     ") + 6) : selectedCombination, FilePath);

                // Reset selection
                selectedCombination = null;
                previousSelection = null; // Clear previous selection
                DeleteButton.Visibility = Visibility.Collapsed; // Hide delete button

                LoadCombinationsFromFile(FilePath);
            }
        }


        private void DeleteCombination(string combinationToDelete, string FilePath)
        {
            try
            {
                // Učitaj sve linije iz CSV fajla
                if (!File.Exists(FilePath))
                {
                    MessageBox.Show("Fajl ne postoji na putanji!");
                    return;
                }

                var lines = File.ReadAllLines(FilePath).ToList();

                // Proveri da li kombinacija počinje sa "Nema"
                if (combinationToDelete.StartsWith("Nema", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Kombinacija počinje sa 'Nema' i nije obrisana.");
                    return; // Prekini izvršavanje funkcije
                }

                // Pronađi liniju koju treba obrisati
                string lineToRemove = null;
                foreach (var line in lines)
                {
                    string[] parts = line.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

                    // Inicijalizuje niz za kombinaciju
                    string[] combination = new string[parts.Length - 2]; // Prva dva dela su datum i vreme

                    // Popunjava niz kombinacije
                    for (int i = 2; i < parts.Length; i++)
                    {
                        combination[i - 2] = parts[i];
                    }

                    // Spaja kombinaciju u jedan string sa razmacima
                    string result = string.Join(" ", combination);

                    if (result.Trim() == combinationToDelete)
                    {
                        lineToRemove = line;
                        break; // Izlaz iz petlje kada je linija pronađena
                    }
                }

                if (lineToRemove != null)
                {
                    // Ukloni liniju
                    lines.Remove(lineToRemove);

                    MessageBox.Show($"Linija za brisanje: {lineToRemove}");
                }
                else
                {
                    MessageBox.Show("Kombinacija nije pronađena.");
                }

                // Sačuvaj sve nazad u fajl (uvek, na kraju)
                File.WriteAllLines(FilePath, lines);

                MessageBox.Show("Promene su sačuvane.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Došlo je do greške: {ex.Message}");
            }
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
                text += $"0 + 1: {greenRedCounts[0]}";

            }
            else if (FilePath.Contains("6od45"))
            {
                text += $"6: {greenCounts[6]} \n";
                text += $"5 + 1: {greenRedCounts[5]} \n";
                text += $"5: {greenCounts[5]} \n";
                text += $"4: {greenCounts[4]} \n";
                text += $"3: {greenCounts[3]}";
            }
            else if (FilePath.Contains("7od39"))
            {
                text += $"7: {greenCounts[7]} \n";
                text += $"6: {greenCounts[6]} \n";
                text += $"5: {greenCounts[5]} \n";
                text += $"4: {greenCounts[4]} \n";
                text += $"3: {greenCounts[3]}";
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
                text += $"0 + 1: {greenRedCounts[0]}";
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
            }
            else if (FilePath.Contains("7od37"))
            {
                text += $"7: {greenCounts[7]} \n";
                text += $"6: {greenCounts[6]} \n";
                text += $"5: {greenCounts[5]} \n";
                text += $"4: {greenCounts[4]}";
            }

            return text;
        }
    }
}
