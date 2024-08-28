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
    public partial class filterpage : UserControl
    {
        supabaseservice service;
        List<attendencer> data;
        
        
        public filterpage()

        {
            
            initialize();
            InitializeComponent();
        }
        public async void initialize()
        {
            service = new supabaseservice();

            attendencedata = new ObservableCollection<attendencer>(await service.fetchdata1());
            students = new ObservableCollection<City>(await service.fetchdata());
         
        }
        public ObservableCollection<attendencer> attendencedata { get; set; }
        public ObservableCollection<City>students { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ApplyFilter();
        }
        public void ApplyFilter()
        {
            int studentId = string.IsNullOrEmpty(txtStudentId.Text) ? 0 : int.Parse(txtStudentId.Text);
            
            string studentName = txtStudentName.Text;
            DateTime startDate = dpStartDate.SelectedDate ?? DateTime.MinValue;
            DateTime endDate = dpEndDate.SelectedDate ?? DateTime.MaxValue;
            string status;
            if(cmbStatus.SelectedItem == null)
            {
                status = "";
            }
            else if (cmbStatus.SelectedItem.ToString().Contains("Present"))
            {
                status = "Present";
            }
            else if (cmbStatus.SelectedItem.ToString().Contains("None"))
            {
                status = "";
            }
            else
            {
                status = "Absent";
            }
            
            data = attendencedata.Where(a => (studentId == 0 || a.id == studentId) && (studentName=="" || a.name.ToLower()==studentName.ToLower()) && (DateTime.TryParse(a.date, out DateTime attendanceDate) && (startDate == DateTime.MinValue || attendanceDate >= startDate)) &&
        (DateTime.TryParse(a.date, out attendanceDate) && (endDate == DateTime.MaxValue || attendanceDate <= endDate)) &&
        (status=="" || a.status == status)).ToList();
            int count = 1;
            foreach(var d in data)
            {
                d.sno = count;
                count++;
                MessageBox.Show(d.id.ToString() + d.status);
            }
            dgFilteredResults.Items.Clear();
            foreach (var d in data)
            {
                dgFilteredResults.Items.Add(d);
            }




        }
    }
    

    

}
