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
        private int maxNumbersToExclude = 5;
        private List<int> excludedNumbers = [];

        public SecondStepPage(int totalNumbers)
        {
            InitializeComponent();
            AddNumberButtons(totalNumbers);
        }

        private void AddNumberButtons(int totalNumbers)
        {
            for (int i = 1; i <= totalNumbers; i++)
            {
                Button numberButton = new ()
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

        private void NextStepButton_Click(object sender, RoutedEventArgs e)
        {
            // Implementirajte prelaz na sledeći korak, prosleđivanjem excludedNumbers liste
        }
    }

}
