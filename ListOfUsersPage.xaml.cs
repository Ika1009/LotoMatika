using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace Loto_App
{
    /// <summary>
    /// Interaction logic for ListOfUsersPage.xaml
    /// </summary>
    public partial class ListOfUsersPage : Page
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private static readonly string ApiUrl = "https://tvojluksuz.rs/";
        private readonly MainWindow _mainWindow;
        public ListOfUsersPage(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();
            LoadUsers();
        }
        // Define a User model to match the API response
        public class User
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            // Add other user fields based on your API response
        }

        private async void LoadUsers()
        {
            string apiUrl = "https://yourapiurl.com/api/users"; // Replace with your actual API URL
            HttpClient client = new HttpClient();

            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode(); // Throw if not 2xx status code

                string responseBody = await response.Content.ReadAsStringAsync();
                List<User> users = JsonConvert.DeserializeObject<List<User>>(responseBody);

                // Bind the users list to the DataGrid
                UserDataGrid.ItemsSource = users;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
