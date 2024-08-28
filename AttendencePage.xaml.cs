using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    
    public partial class AttendencePage : UserControl
    {
        supabaseservice service;
        sendemail mailservices;
        public List<sample> record { get; set; }
        public AttendencePage()
        {
            
            
            
            InitializeComponent();
            record = new List<sample>();
            InitializeAsync();

        }
        public async void InitializeAsync()
        {
            service = new supabaseservice();
            mailservices = new sendemail();
            
            Students = new ObservableCollection<City>(await service.fetchdata());
            foreach (var student in Students)
            {
                record.Add(new sample(student.Sno, student.name, student.email, ""));
                

            }
            DataContext =this;
        }
        public ObservableCollection<City> Students { get; set; }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null && comboBox.SelectedItem != null)
            {
                sample record = comboBox.DataContext as sample;
                record.status = comboBox.SelectedItem.ToString();
                
            }
        }
        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
            

        {
            string date = DateTime.Today.ToString();
            string slicedString = date.Substring(0, Math.Min(date.Length, 10));
            var m = new supabaseservice();
            int count = await m.getnoofdata();
          
            foreach (var r in record)
            {
                MessageBox.Show(r.status);
                var obj = new supabaseservice();
                string stat = "";
                if (r.status.Contains("Present"))
                {
                    stat = "Present";
                }
                else
                {
                    stat = "Absent";
                }
                count++;
                obj.sendattendence(r.sno, stat,count,r.name);
                mailservices.send(r.email, "Regarding todays attendence Date: "+ slicedString, "Your ward: " + r.name + " is " + stat + " Today");
            }

        }



        public class sample
        {
            public int sno { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string status { get; set; }
            public sample(int sno, string name, string email, string status)
            {
                this.sno = sno;
                this.name = name;
                this.email = email;
                this.status = status;
            }
        }
    }
    
}
