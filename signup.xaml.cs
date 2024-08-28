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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for signup.xaml
    /// </summary>
    public partial class signup : Page
    {
        private supabaseservice _supabaseservice;
        public signup()
        {
            _supabaseservice = new supabaseservice();
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var supabaseservice = new supabaseservice();
        
                if(username.Text == "" || mobile_number.Text == "" || email.Text == "" || password.Password.ToString() == "")
                {
                    MessageBox.Show("Please enter all the Fields");
                }
                else
                {
                    
                    string res = await supabaseservice.signup(mobile_number.Text, username.Text, email.Text, password.Password.ToString());
                    if(res == "already")
                    {
                        MessageBox.Show("User already Registered Please Login");
                        mainFrame.Navigate(new login());
                    }
                    else if(res == "registered")
                    {
                        MessageBox.Show("User Registered Successfully Please Login");
                        mainFrame.Navigate(new login());
                    }
                    else
                    {
                        MessageBox.Show("error occured");
                    }
                }


                
            }
            catch (Exception ex)
            {
                
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new login());
        }
    }
}
