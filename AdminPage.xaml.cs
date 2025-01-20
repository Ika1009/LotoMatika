using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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

namespace Loto_App
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public AdminPage()
        {
            InitializeComponent();
        }

        private async void GenerateUser_Click(object sender, RoutedEventArgs e)
        {
            string newPassword = GenerateRandomPassword();
            var payload = new { password = newPassword };
            string json = JsonSerializer.Serialize(payload);

            try
            {
                var response = await httpClient.PostAsync(
                    "http://your-api-url/add_user.php",
                    new StringContent(json, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<ApiResponse>(await response.Content.ReadAsStringAsync());
                    if (result.Success)
                    {
                        GeneratedPassword.Text = $"Nova šifra: {newPassword}";
                    }
                    else
                    {
                        GeneratedPassword.Text = "Kreiranje korisnika nije uspelo.";
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

            var payload = new { password = userPassword };
            string json = JsonSerializer.Serialize(payload);

            try
            {
                var response = await httpClient.PostAsync(
                    "http://your-api-url/reset_device.php",
                    new StringContent(json, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<ApiResponse>(await response.Content.ReadAsStringAsync());
                    if (result.Success)
                    {
                        ResetStatusMessage.Text = "Uređaj je uspešno resetovan.";
                    }
                    else
                    {
                        ResetStatusMessage.Text = "Korisnik sa unetom šifrom ne postoji.";
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

    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
