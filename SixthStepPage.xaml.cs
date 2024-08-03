using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Loto_App
{
    public partial class SixthStepPage : Page
    {
        MainWindow _mainWindow;
        private int? validNumber;

        public SixthStepPage(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();
            PopulateCombinationCountComboBox();
        }

        private void PopulateCombinationCountComboBox()
        {
            for (int i = 10; i <= 1000; i += 10)
            {
                CombinationCountComboBox.Items.Add(i.ToString());
            }
        }

        private void CombinationCountComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CombinationCountTextBox.Text = CombinationCountComboBox.SelectedItem.ToString();
            ValidateAndProcessInput();
        }

        private void CombinationCountTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ValidateAndProcessInput();
            }
        }

        private void CombinationCountTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Only allow numeric input
            e.Handled = !IsTextNumeric(e.Text);
        }

        private bool IsTextNumeric(string text)
        {
            foreach (char c in text)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        private void ValidateAndProcessInput()
        {
            if (int.TryParse(CombinationCountTextBox.Text, out int number))
            {
                if (number >= 10 && number % 10 == 0)
                {
                    // The number is valid, store it
                    validNumber = number;
                    MessageBox.Show($"Uneli ste validan broj: {number}");
                }
                else
                {
                    // The number is invalid
                    validNumber = null;
                    MessageBox.Show("Broj mora biti veći od 10 ili jednak 10, i deljiv sa 10.", "Neispravan unos", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                // The input is not a number
                validNumber = null;
                MessageBox.Show("Unesite ispravan broj.", "Neispravan unos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NextStepButton_Click(object sender, RoutedEventArgs e)
        {
            if (validNumber.HasValue)
            {
                // Navigate to the next step with the valid number
                _mainWindow.NavigateToSeventhStepPage(validNumber.Value);
            }
            else
            {
                MessageBox.Show("Molimo unesite validan broj pre nego što nastavite.", "Neispravan unos", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}