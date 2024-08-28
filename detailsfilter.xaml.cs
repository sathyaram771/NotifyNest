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
    /// <summary>
    /// Interaction logic for detailsfilter.xaml
    /// </summary>
    public partial class detailsfilter : UserControl
    {
        supabaseservice service;
        List<City> data;
        List<final> finaldata;
        
        public detailsfilter()
        {
            initialize();
            InitializeComponent();
        }
        public async void initialize()
        {
            service = new supabaseservice();
            students = new ObservableCollection<City>(await service.fetchdata());

        }
        public ObservableCollection<City> students { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            applyfilter();
        }
        public void applyfilter()
        {
            int studentId = string.IsNullOrEmpty(txtStudentId.Text) ? 0 : int.Parse(txtStudentId.Text);
            string studentName = txtStudentName.Text;
            string email = txtStudentEmail.Text;
            string number = txtStudentNum.Text;
            data = new List<City>();
            data = students.Where(a => (studentId == 0 || a.Sno == studentId) && (studentName == "" || a.name.ToLower() == studentName.ToLower()) && 
        (email == "" || a.email == email) && (number==""||a.mobile_number==number)).ToList();
            int count = 1;
            finaldata = new List<final>();
            foreach(var d in data)
            {
                finaldata.Add(new final(count,d.Sno,d.name,d.email,d.mobile_number));
                count++;
            }
            dgFilteredResults.Items.Clear();
            foreach (var d in finaldata)
            {
                dgFilteredResults.Items.Add(d);
            }
        }
    }
    public class final
    {
        public int sno { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string number { get; set; }
        public final(int sno, int id, string name, string email, string number)
        {
            this.sno = sno;
            this.id = id;
            this.name = name;
            this.email = email;
            this.number = number;
        }
    }
}
