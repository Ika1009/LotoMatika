using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Loto_App
{
    public partial class FifthStepPage : Page
    {
        private int favoriteUsage;
        MainWindow _mainWindow;
        public FifthStepPage(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();
        }

        private void Favorite100Button_Click(object sender, RoutedEventArgs e)
        {
            favoriteUsage = 100;
            NextStepButton.Visibility = Visibility.Visible;
        }

        private void Favorite50Button_Click(object sender, RoutedEventArgs e)
        {
            favoriteUsage = 50;
            NextStepButton.Visibility = Visibility.Visible;
        }

        private void NextStepButton_Click(object sender, RoutedEventArgs e)
        {
            // Implementirajte prelaz na sledeći korak, prosleđivanjem favoriteUsage
            _mainWindow.NavigateToSixthStepPage(favoriteUsage);
        }

        private void BackButton_Click4(object sender, RoutedEventArgs e)
        {
            _mainWindow.BackToFourthStepPage();
        }
    }
}
