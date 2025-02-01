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
        private static readonly string ApiUrl = "https://tvojluksuz.rs/";
        private readonly MainWindow _mainWindow;

        public AdminPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _mainWindow.LenghtenWindowHeight();
        }

        private async void GenerateUser_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailInput.Text.Trim();

            // Email Validation
            if (string.IsNullOrEmpty(email))
            {
                EmailErrorMessage.Text = "Email je obavezan!";
                return;
            }

            if (!IsValidEmail(email))
            {
                EmailErrorMessage.Text = "Unesite validan email!";
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
                        GeneratedPassword.Text = $"Nova šifra: {newPassword}";
                    }
                    else
                    {
                        GeneratedPassword.Text = message ?? "Kreiranje korisnika nije uspelo.";
                    }
                }
                else
                {
                    GeneratedPassword.Text = "Greška u komunikaciji sa serverom.";
                }
            }
            catch (Exception ex)
            {
                GeneratedPassword.Text = $"Greška: {ex.Message}";
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
                ResetStatusMessage.Text = "Unesite šifru korisnika!";
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
                        ResetStatusMessage.Text = "Uređaj je uspešno resetovan.";
                    }
                    else
                    {
                        ResetStatusMessage.Text = message ?? "Korisnik sa unetom šifrom ne postoji.";
                    }
                }
                else
                {
                    ResetStatusMessage.Text = "Greška u komunikaciji sa serverom.";
                }
            }
            catch (Exception ex)
            {
                ResetStatusMessage.Text = $"Greška: {ex.Message}";
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
            _mainWindow.ShortenWindowHeight();
        }
        private void OpenUserListPage_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.NavigateToUserListPage();
            _mainWindow.ShortenWindowHeight();
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
                ResetStatusMessage.Text = "Unesite šifru korisnika!";
                return;
            }

            if (!ShowConfirmationDialog("Da li ste sigurni da želite da uklonite dva uređaja korisniku?"))
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
                        ResetStatusMessage.Text = "Korisniku su uklonjena dva uređaja.";
                    }
                    else
                    {
                        ResetStatusMessage.Text = message ?? "Greška u uklanjanju dva uređaja.";
                    }
                }
                else
                {
                    ResetStatusMessage.Text = "Greška u komunikaciji sa serverom.";
                }
            }
            catch (Exception ex)
            {
                ResetStatusMessage.Text = $"Greška: {ex.Message}";
            }
        }

        private async void ApproveTwoDevicesButton_Click(object sender, RoutedEventArgs e)
        {
            string userPassword = UserPasswordInput.Text;

            if (string.IsNullOrEmpty(userPassword))
            {
                ResetStatusMessage.Text = "Unesite šifru korisnika!";
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
                        ResetStatusMessage.Text = "Korisniku je dozvoljeno korišćenje dva uređaja.";
                    }
                    else
                    {
                        ResetStatusMessage.Text = message ?? "Greška u odobravanju dva uređaja.";
                    }
                }
                else
                {
                    ResetStatusMessage.Text = "Greška u komunikaciji sa serverom.";
                }
            }
            catch (Exception ex)
            {
                ResetStatusMessage.Text = $"Greška: {ex.Message}";
            }
        }

    }
}
