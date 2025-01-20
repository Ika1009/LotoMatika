using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Management;
using System.Security.Policy;

namespace Loto_App
{
    public partial class LoginPage : Page
    {
        private readonly MainWindow _mainWindow;
        private static readonly string ApiUrl = "https://weatheronthegogh.com/";
        public LoginPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Šifra ne sme biti prazna.", "Greška", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var payload = new { password = password };
                    string jsonPayload = JsonSerializer.Serialize(payload);
                    StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(ApiUrl + "/login.php", content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        var result = JsonSerializer.Deserialize<LoginResponse>(responseData);

                        if (result != null && result.Success)
                        {
                            if (result.IsAdmin)
                            {
                                MessageBox.Show("Dobrodošli na Admin Panel.", "Uspešno", MessageBoxButton.OK, MessageBoxImage.Information);
                                _mainWindow.NavigateToAdminPage();
                            }
                            else
                            {
                                _mainWindow.NavigateToStartPage();
                            }
                        }
                        else
                        {
                            MessageBox.Show(result?.Message ?? "Pogrešno uneseni podaci.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Greška u komunikaciji sa serverom.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška: {ex.Message}", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public class LoginResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public bool IsAdmin { get; set; }
        }
    }
}
