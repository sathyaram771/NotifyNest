using System;
using System.Collections.Generic;
using System.IO;
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
using System.Net.Http;
using System.Threading.Tasks;

namespace WpfApp1
{
    public partial class home : Page
    {
        private static readonly HttpClient httpClient = new HttpClient();
        public home()
        {
            InitializeComponent();
        }

        private async void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                string flaskServerUrl = "http://127.0.0.1:5000/start"; 

                HttpResponseMessage response = await httpClient.GetAsync(flaskServerUrl);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                MessageBox.Show("Facial recognition process started successfully!\n" + responseBody, "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show("Error starting facial recognition process:\n" + httpEx.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred:\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
