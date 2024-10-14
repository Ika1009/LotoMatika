using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Loto_App
{
    public partial class SecondStepPage : Page
    {
        private int maxNumbersToExclude = 5;
        private List<int> excludedNumbers = new();
        private readonly MainWindow _mainWindow;

        public SecondStepPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            AddNumberButtons(_mainWindow.GetMaxNumber());
        }

        private void AddNumberButtons(int totalNumbers)
        {
            for (int i = 1; i <= totalNumbers; i++)
            {
                Button numberButton = new()
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

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int number = (int)button.Tag;

            if (excludedNumbers.Contains(number))
            {
                excludedNumbers.Remove(number);
                button.Background = Brushes.White;
                button.Content = number.ToString();
            }
            else
            {
                if (excludedNumbers.Count < maxNumbersToExclude)
                {
                    excludedNumbers.Add(number);
                    button.Background = Brushes.Gray;
                    button.Content = "X";
                }
                else
                {
                    MessageBox.Show("Možete izabrati maksimalno 5 brojeva za isključivanje.");
                }
            }
        }

        private async void NextStepButton_Click(object sender, RoutedEventArgs e)
        {
            ShowLoadingIndicator();

            // Simulacija čekanja za dugotrajnu operaciju
            await Task.Delay(2000);

            _mainWindow.NavigateToThirdStepPage(excludedNumbers);

            HideLoadingIndicator();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.BackToFirstStepPage();
        }

        private void ShowLoadingIndicator()
        {
            LoadingIndicator.Visibility = Visibility.Visible;

            // Stop any previous animation
            LoadingRotateTransform.BeginAnimation(RotateTransform.AngleProperty, null);

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
