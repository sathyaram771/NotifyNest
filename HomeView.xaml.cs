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
    public partial class HomeView : UserControl
    {
        public string date { get; set; }
        public string noofstu { get; set; }
        public string noofpres { get; set; }
        public string noofabs { get; set; }
        public string dept { get; set; }
        supabaseservice service;
        public ObservableCollection<City> a { get; set; }
        public ObservableCollection<attendencer> b { get; set; }
        public HomeView()
        {
            initialize();
            InitializeComponent();
            
            
        }
        public async void initialize()
        {
            int c = 0;
            int c1 = 0;
            service = new supabaseservice();
            a = new ObservableCollection<City>(await service.fetchdata());
            b = new ObservableCollection<attendencer>(await service.fetchdata1());
            foreach (var item in a )
            {
                c++;

            }
            noofstu = "Total no of \nStudents: "+ c.ToString();
            int stindex = Math.Max(0, b.Count - c);
            List<attendencer>sliced = b.Skip(stindex).ToList();
            foreach(var item in sliced)
            {
                if(item.status == "Present")
                {
                    c1++;
                }
                date = item.date;
            }
            noofpres = "No of Students \nPresent: "+c1.ToString();
            noofabs = "No of Students \nAbsent: "+(c - c1).ToString();
            dept = "ComputerScience \n          and  \nBusiness Systems";
            date = "Date:\n" + date;
            DataContext = this;

        }
    }
}
