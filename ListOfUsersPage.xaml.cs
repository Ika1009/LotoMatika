using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Loto_App
{
    /// <summary>
    /// Interaction logic for ListOfUsersPage.xaml
    /// </summary>
    public partial class ListOfUsersPage : Page
    {
        private static readonly string ApiUrl = "https://tvojluksuz.rs/"; // Replace with actual API URL
        private readonly MainWindow _mainWindow;

        public ListOfUsersPage(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();
            _mainWindow.LenghtenWindowWidth(60);
            _mainWindow.LenghtenWindowHeight(100);
            LoadUsers();
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.ShortenWindowWidth(60);
            _mainWindow.ShortenWindowHeight(100);
            _mainWindow.NavigateToAdminPage();
        }

        private async void LoadUsers()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiUrl = $"{ApiUrl}/get_users.php"; // API endpoint

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        JsonElement parsed = JsonSerializer.Deserialize<JsonElement>(responseData);

                        if (parsed.TryGetProperty("success", out JsonElement successProp) && successProp.GetBoolean())
                        {
                            if (parsed.TryGetProperty("data", out JsonElement dataElement) && dataElement.ValueKind == JsonValueKind.Array)
                            {
                                List<User> users = new List<User>();

                                foreach (JsonElement userElement in dataElement.EnumerateArray())
                                {
                                    var user = new User
                                    {
                                        UID = userElement.GetProperty("UID").GetString()!,
                                        Password = userElement.GetProperty("Password").GetString()!,
                                        Email = userElement.GetProperty("Email").GetString()!,
                                        DeviceID = userElement.GetProperty("DeviceID").GetString()!,
                                        SecondDeviceAllowed = userElement.GetProperty("SecondDeviceAllowed").GetString() == "1" ? "odobren" : "neodobren",
                                        SecondDeviceID = userElement.GetProperty("SecondDeviceID").GetString()!,
                                        CreatedAt = userElement.GetProperty("CreatedAt").GetString()!
                                    };

                                    users.Add(user);
                                }

                                UserDataGrid.ItemsSource = users;
                            }
                            else
                            {
                                MessageBox.Show("Data not found or invalid format.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        else
                        {
                            string message = parsed.TryGetProperty("message", out JsonElement messageProp) ? messageProp.GetString()! : "No message provided.";
                            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error communicating with the server.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public class User
        {
            public string UID { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public string DeviceID { get; set; }
            public string SecondDeviceAllowed { get; set; }
            public string SecondDeviceID { get; set; }
            public string CreatedAt { get; set; }
        }
    }
}
