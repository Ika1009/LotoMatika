using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;
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
        private static readonly HttpClient httpClient = new HttpClient();
        private static readonly string ApiUrl = "https://tvojluksuz.rs/"; // Replace with actual API URL
        private readonly MainWindow _mainWindow;

        public ListOfUsersPage(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();
            LoadUsers();
        }

        private async void LoadUsers()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Make a GET request to retrieve the list of users
                    string apiUrl = $"{ApiUrl}/api/users"; // Replace with your actual API URL

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();

                        JsonElement parsed = JsonSerializer.Deserialize<JsonElement>(responseData);

                        if (parsed.TryGetProperty("success", out JsonElement successProp) && successProp.GetBoolean())
                        {
                            if (parsed.TryGetProperty("data", out JsonElement dataElement) && dataElement.ValueKind == JsonValueKind.Array)
                            {
                                List<Dictionary<string, object>> users = new List<Dictionary<string, object>>();
                                foreach (JsonElement userElement in dataElement.EnumerateArray())
                                {
                                    var user = new Dictionary<string, object>
                                    {
                                        { "UID", userElement.GetProperty("UID").GetInt32() },
                                        { "Password", userElement.GetProperty("Password").GetString()! },
                                        { "Email", userElement.GetProperty("Email").GetString()! },
                                        { "DeviceID", userElement.GetProperty("DeviceID").GetString()! },
                                        { "SecondDeviceAllowed", userElement.GetProperty("SecondDeviceAllowed").GetInt32() },
                                        { "SecondDeviceID", userElement.GetProperty("SecondDeviceID").GetString()! },
                                        { "CreatedAt", userElement.GetProperty("CreatedAt").GetString()! }
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
                            // Handle failure response
                            string message = parsed.TryGetProperty("message", out JsonElement messageProp) ? messageProp.GetString() : "No message provided.";
                            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error communicating with the server.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (JsonException jsonEx)
            {
                MessageBox.Show($"Error parsing the data: {jsonEx.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show($"Error with the server connection: {httpEx.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}
