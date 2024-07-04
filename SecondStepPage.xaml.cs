using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Loto_App
{
    public partial class SecondStepPage : Page
    {
        private int maxDeselectCount = 5;
        private int deselectCount = 0;

        public SecondStepPage(int maxNumber)
        {
            InitializeComponent();
            InitializeNumbers(maxNumber);
        }

        private void InitializeNumbers(int maxNumber)
        {
            NumberGrid.Columns = (maxNumber > 35) ? 6 : 5; // Adjust columns for larger sets
            for (int i = 1; i <= maxNumber; i++)
            {
                Button numberButton = new Button
                {
                    Content = i.ToString(),
                    Width = 50,
                    Height = 50,
                    Margin = new Thickness(5),
                    Background = Brushes.LightGray,
                    Foreground = Brushes.Black,
                    FontWeight = FontWeights.Bold
                };
                numberButton.Click += NumberButton_Click;
                NumberGrid.Children.Add(numberButton);
            }
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button != null)
            {
                if (button.Background != Brushes.Gray)
                {
                    if (deselectCount < maxDeselectCount)
                    {
                        button.Background = Brushes.Gray;
                        button.Content = "X";
                        deselectCount++;
                    }
                    else
                    {
                        MessageBox.Show("Možete odabrati najviše 5 brojeva.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    button.Background = Brushes.LightGray;
                    button.Content = button.Content.ToString().Replace("X", button.Content.ToString());
                    deselectCount--;
                }
            }
        }

        private void NextStepButton_Click(object sender, RoutedEventArgs e)
        {
            // Ovdje možete dodati kod za prelazak na sljedeći korak
        }
    }
}
