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
    public partial class FirstStepPage : Page
    {
        private readonly MainWindow _mainWindow;

        public FirstStepPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void NextStepButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedGame = (ComboBoxItem)LotoGamesComboBox.SelectedItem;
            if (selectedGame != null)
            {
                string selectedGameText = selectedGame.Content.ToString();
                int maxNumber = 35;
                int combinationLength = 7;

                switch (selectedGameText)
                {
                    case "7 od 35 (Hrvatska)":
                        combinationLength = 7;
                        maxNumber = 35;
                        break;
                    case "6 od 45 (Hrvatska)":
                        combinationLength = 6;
                        maxNumber = 45;
                        break;
                    case "7 od 39 (Srbija)":
                        combinationLength = 7;
                        maxNumber = 39;
                        break;
                    case "6 od 44 (Slovenija)":
                        combinationLength = 6;
                        maxNumber = 44;
                        break;
                    case "6 od 39 (BiH)":
                        combinationLength = 6;
                        maxNumber = 39;
                        break;
                    case "7 od 37 (Makedonija)":
                        combinationLength = 7;
                        maxNumber = 37;
                        break;
                }

                _mainWindow.NavigateToSecondStepPage(maxNumber, combinationLength);
            }
            else
            {
                MessageBox.Show("Molimo vas da odaberete loto igru.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateToStartPage();
        }
    }
}
