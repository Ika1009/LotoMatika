using System;
using System.Windows;
using System.Windows.Controls;

namespace Loto_App
{
    public partial class SixthStepPage : Page
    {
        MainWindow _mainWindow;
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
        }

        private void CombinationCountTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // Implementirajte logiku za unos broja kombinacija ručno
        }

        private void NextStepButton_Click(object sender, RoutedEventArgs e)
        {
            // Implementirajte prelaz na sledeći korak
            _mainWindow.NavigateToSeventhStepPage();
        }
    }
}
