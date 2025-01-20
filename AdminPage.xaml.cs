﻿using System;
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
        private static readonly string ApiUrl = "https://weatheronthegogh.com/";

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
    }
}
