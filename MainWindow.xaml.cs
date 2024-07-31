using System.Windows;

namespace Loto_App
{
    public partial class MainWindow : Window
    {
        private int maxNumber;
        private int combinationLength;
        int combinationsRequested;
        List<int> excludedNumbers;
        List<int> favoriteNumbers;
        int favoriteUsage = -1; // in percentage
        // Functions to retrieve the values for better encapsulation
        public int GetMaxNumber() => maxNumber;
        public int GetCombinationLength() => combinationLength;
        public int GetCombinationsRequested() => combinationsRequested;
        public List<int> GetExcludedNumbers() => excludedNumbers;
        public List<int> GetFavoriteNumbers() => favoriteNumbers;
        public int GetFavoriteUsage() => favoriteUsage;

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
            MainFrame.Navigate(new SecondStepPage(this));
        }

        public void NavigateToThirdStepPage(List<int> excludedNumbers)
        {
            this.excludedNumbers = excludedNumbers;
            MainFrame.Navigate(new ThirdStepPage(this));
        }

        public void NavigateToFourthStepPage()
        {
            MainFrame.Navigate(new FourthStepPage(this));
        }

        public void NavigateToFifthStepPage(List<int> favoriteNumbers)
        {
            this.favoriteNumbers = favoriteNumbers;
            MainFrame.Navigate(new FifthStepPage(this));
        }

        public void NavigateToSixthStepPage(int favoriteUsage)
        {
            this.favoriteUsage = favoriteUsage;
            MainFrame.Navigate(new SixthStepPage(this));
        }
        public void NavigateToSixthStepPage()
        {
            MainFrame.Navigate(new SixthStepPage(this));
        }

        public void NavigateToSeventhStepPage(int combinationsRequested)
        {
            this.combinationsRequested = combinationsRequested;
            MainFrame.Navigate(new SeventhStepPage(this));
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
