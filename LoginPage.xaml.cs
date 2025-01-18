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
using System.Management;

namespace Loto_App
{
/// <summary>
/// Interaction logic for LoginPage.xaml
/// </summary>
public partial class LoginPage : Page
{
        private readonly MainWindow _mainWindow;

        public LoginPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Placeholder for any initialization logic
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (ValidateCredentials(username, password))
            {
                string deviceId = GetDeviceSerialNumber();
                if (IsDeviceAllowed(deviceId))
                {
                    _mainWindow.NavigateToStartPage();
                }
                else
                {
                    MessageBox.Show("Ovaj uređaj nema dozvolu.", "Pristup Odbijen", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Pogrešno korisničko ime ili šifra.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private bool ValidateCredentials(string username, string password)
        {
            // Placeholder for actual credential validation logic
            return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password);
        }

        private string GetDeviceSerialNumber()
        {
            try
            {
                // Query for CPU information (ProcessorId)
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor");
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    return queryObj["ProcessorId"]?.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving CPU ID: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return string.Empty;
        }



        private bool IsDeviceAllowed(string deviceId)
        {
            // Placeholder for server-side check or local validation of device ID
            // This should ideally query a server or a local database
            return true; // Allow all devices for now
        }
    }
}
