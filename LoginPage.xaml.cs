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
        private static readonly string ApiUrl = "https://tvojluksuz.rs/";
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
                    string deviceId = GetDeviceSerialNumber(); // Get the current device's CPU ID

                    var payload = new { password, deviceId };
                    string jsonPayload = JsonSerializer.Serialize(payload);
                    StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(ApiUrl + "/login.php", content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        var parsed = JsonSerializer.Deserialize<JsonElement>(responseData);

                        bool success = parsed.TryGetProperty("success", out var successProp) && successProp.GetBoolean();
                        bool isAdmin = parsed.TryGetProperty("isAdmin", out var isAdminProp) && isAdminProp.GetBoolean();
                        string message = parsed.TryGetProperty("message", out var messageProp) ? messageProp.GetString()! : "Nema poruke.";
                        string? deviceIdFromServer = parsed.TryGetProperty("deviceId", out var deviceIdProp) ? deviceIdProp.GetString() : null;
                        bool secondDeviceAllowed = parsed.TryGetProperty("secondDeviceAllowed", out var secondDeviceAllowedProp) && secondDeviceAllowedProp.GetBoolean();
                        string? secondDeviceId = parsed.TryGetProperty("secondDeviceId", out var secondDeviceIdProp) ? secondDeviceIdProp.GetString() : null;

                        if (success)
                        {
                            if (secondDeviceAllowed && !string.IsNullOrEmpty(secondDeviceId) && secondDeviceId != deviceId)
                            {
                                MessageBox.Show("Ovaj uređaj nije dozvoljen za prijavu.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }

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
                            MessageBox.Show(message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
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


        private static string GetDeviceSerialNumber()
        {
            try
            {
                // Query for CPU information (ProcessorId)
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor");
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    return queryObj["ProcessorId"]?.ToString()!;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri dobijanju CPU ID: {ex.Message}", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return string.Empty;
        }

    }
}
