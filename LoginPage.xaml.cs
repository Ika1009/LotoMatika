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
                        var parsed = JsonSerializer.Deserialize<JsonElement>(responseData);

                        // Safely extracting properties
                        bool success = parsed.TryGetProperty("success", out var successProp) && successProp.GetBoolean();
                        bool isAdmin = parsed.TryGetProperty("isAdmin", out var isAdminProp) && isAdminProp.GetBoolean();
                        string message = parsed.TryGetProperty("message", out var messageProp) ? messageProp.GetString()! : "Nema poruke.";

                        if (success)
                        {
                            if (isAdmin)
                            {
                                _mainWindow.NavigateToAdminPage();
                            }
                            else
                            {
                                _mainWindow.NavigateToStartPage();
                            }
                        }
                        else
                        {
                            MessageBox.Show(message ?? "Pogrešno uneseni podaci.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Greška u komunikaciji sa serverom.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (JsonException jsonEx)
            {
                MessageBox.Show($"Greška pri parsiranju podataka: {jsonEx.Message}", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show($"Greška u vezi sa serverom: {httpEx.Message}", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška: {ex.Message}", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
