using System.Windows;
using System.Windows.Media.Media3D;

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
            NavigateToLoginPage();
        }
        public void NavigateToLoginPage()
        {
            MainFrame.Navigate(new LoginPage(this));
        }
        public void NavigateToAdminPage()
        {
            MainFrame.Navigate(new AdminPage(this));
        }

        public void LenghtenWindowWidth(int width)
        {
            this.Width += width;
        }
        public void ShortenWindowWidth(int width)
        {
            this.Width -= width;
        }
        public void LenghtenWindowHeight(int height)
        {
            this.Height += height;
        }
        public void ShortenWindowHeight(int height)
        {
            this.Height -= height;
        }
        public void NavigateToUserListPage()
        {
            MainFrame.Navigate(new ListOfUsersPage(this));
        }
        public void NavigateToStartPage()
        {
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
            if (favoriteNumbers.Count > 0)
                MainFrame.Navigate(new FifthStepPage(this));
            else
            {
                MainFrame.Navigate(new SixthStepPage(this));
            }
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

        public void NavigateToArhivaPage()
        {
            MainFrame.Navigate(new ArhivaPage(this));
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void BackToFirstStepPage()
        {
            MainFrame.Navigate(new FirstStepPage(this));
        }

        public void BackToSecondStepPage()
        {
            MainFrame.Navigate(new SecondStepPage(this));
        }

        public void BackToThirdStepPage()
        {
            MainFrame.Navigate(new ThirdStepPage(this));
        }

        public void BackToFourthStepPage()
        {
            MainFrame.Navigate(new FourthStepPage(this));
        }

        public void BackToFifthStepPage()
        {
            MainFrame.Navigate(new FifthStepPage(this));
        }
    }
}
