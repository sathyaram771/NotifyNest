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
    public partial class login : Page
    {
        public login()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string value1 = email.Text;
            string value2 = password.Password.ToString();
            MessageBox.Show(value2);
            MessageBox.Show(value1);
            if (value1 == "" || value2 == "")
            {
                MessageBox.Show("Please Enter all the Fields");
            }
            else
            {
                var service = new supabaseservice();
                bool ans = await service.Login(value1, value2);
                if(ans == false)
                {
                    MessageBox.Show("Login unsuccessfull, Please Retry");

                }
                else
                {
                    MessageBox.Show("Login successful");
                    mainFrame.Navigate(new home());
                }
            }

        }
        
    }
}
