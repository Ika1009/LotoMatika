using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Loto_App
{
    public partial class FourthStepPage : Page
    {
        private int maxFavoriteNumbers = 2;
        private int maxNumber;
        private List<int> favoriteNumbers = new List<int>();
        private List<int> excludedNumbers; // Updated to store excluded numbers
        MainWindow _mainWindow;

        public FourthStepPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            this.maxNumber = _mainWindow.GetMaxNumber();
            this.excludedNumbers = _mainWindow.GetExcludedNumbers();
            AddNumberButtons();
        }

        private void AddNumberButtons()
        {
            // Generate buttons for numbers not in excluded list
            for (int i = 1; i <= maxNumber; i++)
            {
                if (!excludedNumbers.Contains(i))
                {
                    Button numberButton = new Button
                    {
                        Content = i.ToString(),
                        Width = 35,
                        Height = 35,
                        Margin = new Thickness(5),
                        Background = Brushes.White,
                        Foreground = Brushes.Black,
                        FontWeight = FontWeights.Bold,
                        Tag = i
                    };

                    numberButton.Click += NumberButton_Click;
                    NumberGrid.Children.Add(numberButton);
                }
            }
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int number = (int)button.Tag;

            if (favoriteNumbers.Contains(number))
            {
                favoriteNumbers.Remove(number);
                button.Background = Brushes.Gray;
            }
            else
            {
                if (favoriteNumbers.Count < maxFavoriteNumbers)
                {
                    favoriteNumbers.Add(number);
                    button.Background = Brushes.Green;
                }
                else
                {
                    MessageBox.Show("Možete izabrati maksimalno 2 favorit broja.");
                }
            }
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            YesButton.Visibility = Visibility.Hidden;
            NoButton.Visibility = Visibility.Hidden;
            QuestionText.Text = "Možete izabrati najviše do 2 favorit broja.";
            NumberGrid.Visibility = Visibility.Visible;
            NextStepButton.Visibility = Visibility.Visible;
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            // Implement navigation to the fifth step, skipping the fourth step
            _mainWindow.NavigateToSixthStepPage(-1);
        }

        private void NextStepButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the next step, passing the favoriteNumbers list
            _mainWindow.NavigateToFifthStepPage(favoriteNumbers);
        }
    }
}
