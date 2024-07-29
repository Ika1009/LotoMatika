using System.Windows;

namespace Loto_App
{
    public partial class MainWindow : Window
    {
        private int maxNumber;
        private int combinationLength;
        List<int> excludedNumbers;
        List<int> favoriteNumbers;
        double? favoriteUsage;
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new StartPage(this));
        }

        public void NavigateToFirstStepPage()
        {
            MainFrame.Navigate(new FirstStepPage(this));
        }

        public void NavigateToSecondStepPage(int maxNumber, int combinationLength)
        {
            this.maxNumber = maxNumber;
            this.combinationLength = combinationLength;
            MainFrame.Navigate(new SecondStepPage(this, maxNumber));
        }

        public void NavigateToThirdStepPage(List<int> excludedNumbers)
        {
            this.excludedNumbers = excludedNumbers;
            MainFrame.Navigate(new ThirdStepPage(this, maxNumber, excludedNumbers));
        }

        public void NavigateToFourthStepPage(List<int> favoriteNumbers)
        {
            this.favoriteNumbers = favoriteNumbers;
            MainFrame.Navigate(new FourthStepPage(this));
        }

        public void NavigateToFifthStepPage(double? favoriteUsage)
        {
            this.favoriteUsage = favoriteUsage;
            MainFrame.Navigate(new FifthStepPage(this));
        }

        public void NavigateToSixthStepPage()
        {
            MainFrame.Navigate(new SixthStepPage(this));
        }

        public void NavigateToSeventhStepPage()
        {
            MainFrame.Navigate(new SeventhStepPage(this, 0, 0));
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
