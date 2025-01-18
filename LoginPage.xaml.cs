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
        public LoginPage()
        {
            InitializeComponent();
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
                    MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    // Navigate to the next page or perform further actions
                }
                else
                {
                    MessageBox.Show("This device is not authorized.", "Access Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BIOS");
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    return queryObj["SerialNumber"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving device serial number: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
