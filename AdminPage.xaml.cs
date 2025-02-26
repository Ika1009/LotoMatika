using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Loto_App
{
    public partial class AdminPage : Page
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private static readonly string ApiUrl = "http://lotomatika.com/app/";
        private readonly MainWindow _mainWindow;
        int heightAddOn = 90;

        public AdminPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _mainWindow.LenghtenWindowHeight(heightAddOn);
        }


        private async void SetEmailErrorMessage(string message)
        {
            EmailErrorMessage.Text = message;
            await Task.Delay(4000);
            EmailErrorMessage.Text = string.Empty;
        }
        private async void SetGeneratedPasswordMessage(string message)
        {
            GeneratedPassword.Text = message;
            await Task.Delay(10000);
            GeneratedPassword.Text = string.Empty;
        }

        private async void GenerateUser_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailInput.Text.Trim();

            // Email Validation
            if (string.IsNullOrEmpty(email))
            {
                SetEmailErrorMessage("Email je obavezan!");
                return;
            }

            if (!IsValidEmail(email))
            {
                SetEmailErrorMessage("Unesite validan email!");
                return;
            }

            if (!ShowConfirmationDialog("Da li ste sigurni da želite da kreirate novog korisnika?"))
                return;

            // Clear error message if valid email
            EmailErrorMessage.Text = string.Empty;

            string newPassword = GenerateRandomPassword();
            var payload = new { email, password = newPassword };
            string json = JsonSerializer.Serialize(payload);

            try
            {
                var response = await httpClient.PostAsync(
                    ApiUrl + "/add_user.php",
                    new StringContent(json, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    var parsed = JsonSerializer.Deserialize<JsonElement>(responseData);

                    bool success = parsed.TryGetProperty("success", out var successProp) && successProp.GetBoolean();
                    string message = parsed.TryGetProperty("message", out var messageProp) ? messageProp.GetString() : "Nema poruke.";

                    if (success)
                    {
                        SetGeneratedPasswordMessage($"Nova šifra: {newPassword}");
                    }
                    else
                    {
                        SetGeneratedPasswordMessage(message ?? "Kreiranje korisnika nije uspelo.");
                    }
                }
                else
                {
                    SetGeneratedPasswordMessage("Greška u komunikaciji sa serverom.");
                }
            }
            catch (Exception ex)
            {
                SetGeneratedPasswordMessage($"Greška: {ex.Message}");
            }
        }

        // Email validation function
        private bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private async void SetStatusMessage(string message)
        {
            ResetStatusMessage.Text = message;
            await Task.Delay(4000);
            ResetStatusMessage.Text = string.Empty;
        }

        private string GenerateRandomPassword()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            char[] password = new char[8];
            for (int i = 0; i < password.Length; i++)
            {
                password[i] = chars[random.Next(chars.Length)];
            }
            return new string(password);
        }

        private async void ResetDevice_Click(object sender, RoutedEventArgs e)
        {
            string userPassword = UserPasswordInput.Text;

            if (string.IsNullOrEmpty(userPassword))
            {
                SetStatusMessage("Unesite šifru korisnika!");
                return;
            }

            if (!ShowConfirmationDialog("Da li ste sigurni da želite da resetujete uređaj korisniku?"))
                return;

            var payload = new { password = userPassword };
            string json = JsonSerializer.Serialize(payload);

            try
            {
                var response = await httpClient.PostAsync(
                    ApiUrl + "reset_device.php",
                    new StringContent(json, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    var parsed = JsonSerializer.Deserialize<JsonElement>(responseData);

                    bool success = parsed.TryGetProperty("success", out var successProp) && successProp.GetBoolean();
                    string message = parsed.TryGetProperty("message", out var messageProp) ? messageProp.GetString()! : "Nema poruke.";

                    if (success)
                    {
                        SetStatusMessage("Uređaj je uspešno resetovan.");
                    }
                    else
                    {
                        SetStatusMessage(message ?? "Korisnik sa unetom šifrom ne postoji.");
                    }
                }
                else
                {
                    SetStatusMessage("Greška u komunikaciji sa serverom.");
                }
            }
            catch (Exception ex)
            {
                SetStatusMessage($"Greška: {ex.Message}");
            }
        }
        private void CopyPassword_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(GeneratedPassword.Text))
            {
                string password = GeneratedPassword.Text.Replace("Nova šifra: ", "").Trim();
                Clipboard.SetText(password);
            }
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateToLoginPage();
            _mainWindow.ShortenWindowHeight(heightAddOn);
        }
        private void OpenUserListPage_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateToUserListPage();
            _mainWindow.ShortenWindowHeight(heightAddOn);
        }
        private bool ShowConfirmationDialog(string message)
        {
            MessageBoxResult result = MessageBox.Show(message, "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result == MessageBoxResult.Yes;
        }

        private async void RemoveTwoDevicesButton_Click(object sender, RoutedEventArgs e)
        {
            string userPassword = UserPasswordInput.Text;

            if (string.IsNullOrEmpty(userPassword))
            {
                SetStatusMessage("Unesite šifru korisnika!");
                return;
            }

            if (!ShowConfirmationDialog("Da li ste sigurni da želite da uklonite drugi uređaj korisniku?"))
                return;

            var payload = new { password = userPassword };
            string json = JsonSerializer.Serialize(payload);

            try
            {
                var response = await httpClient.PostAsync(
                    ApiUrl + "/remove_two_devices.php",  // Adjusted API endpoint to remove devices.
                    new StringContent(json, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    var parsed = JsonSerializer.Deserialize<JsonElement>(responseData);

                    bool success = parsed.TryGetProperty("success", out var successProp) && successProp.GetBoolean();
                    string message = parsed.TryGetProperty("message", out var messageProp) ? messageProp.GetString() : "Nema poruke.";

                    if (success)
                    {
                        SetStatusMessage("Korisniku su uklonjena dva uređaja.");
                    }
                    else
                    {
                        SetStatusMessage(message ?? "Greška u uklanjanju dva uređaja.");
                    }
                }
                else
                {
                    SetStatusMessage("Greška u komunikaciji sa serverom.");
                }
            }
            catch (Exception ex)
            {
                SetStatusMessage($"Greška: {ex.Message}");
            }
        }

        private async void ApproveTwoDevicesButton_Click(object sender, RoutedEventArgs e)
        {
            string userPassword = UserPasswordInput.Text;

            if (string.IsNullOrEmpty(userPassword))
            {
                SetStatusMessage("Unesite šifru korisnika!");
                return;
            }

            if (!ShowConfirmationDialog("Da li ste sigurni da želite da odobrite drugi uređaj korisniku?"))
                return;

            var payload = new { password = userPassword };
            string json = JsonSerializer.Serialize(payload);

            try
            {
                var response = await httpClient.PostAsync(
                    ApiUrl + "/approve_two_devices.php",  // Adjust the API endpoint to handle this.
                    new StringContent(json, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    var parsed = JsonSerializer.Deserialize<JsonElement>(responseData);

                    bool success = parsed.TryGetProperty("success", out var successProp) && successProp.GetBoolean();
                    string message = parsed.TryGetProperty("message", out var messageProp) ? messageProp.GetString() : "Nema poruke.";

                    if (success)
                    {
                        SetStatusMessage("Korisniku je dozvoljeno korišćenje dva uređaja.");
                    }
                    else
                    {
                        SetStatusMessage(message ?? "Greška u odobravanju dva uređaja.");
                    }
                }
                else
                {
                    SetStatusMessage("Greška u komunikaciji sa serverom.");
                }
            }
            catch (Exception ex)
            {
                SetStatusMessage($"Greška: {ex.Message}");
            }
        }

    }
}
