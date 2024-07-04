using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for FirstStepPage.xaml
    /// </summary>
    public partial class FirstStepPage : Page
    {
        public FirstStepPage()
        {
            InitializeComponent();
        }
        private void NextStepButton_Click(object sender, RoutedEventArgs e)
        {
            // Here, you would handle the selection from the ComboBox
            // and navigate to the next step based on the selected game.
            // For this example, we'll just navigate to a placeholder SecondStepPage.

            //this.NavigationService.Navigate();
        }
    }
}
