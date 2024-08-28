using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Newtonsoft.Json.Serialization;
using Supabase;
using Supabase.Interfaces;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace WpfApp1
{
    public class supabaseservice
    {
        string url;
        string key;
        public List<City>Students { get; set; }
        public List<attendencer>attendencedata { get; set; }
        public supabaseservice()
        {
            url = "https://yljpymuabyyngwxvbbgm.supabase.co";
            key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InlsanB5bXVhYnl5bmd3eHZiYmdtIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MTYwMTY0MzgsImV4cCI6MjAzMTU5MjQzOH0.H6dmMUp6b__bP5yuTwsKq-uL_XItEdt7mNjsU5TR_WA";
        }

        public async Task<int> getnoofdata()
        {
            try
            {
                var options = new SupabaseOptions
                {
                    AutoConnectRealtime = true
                };

                var supabase = new Supabase.Client(url, key, options);
                await supabase.InitializeAsync();
                var re = await supabase.From<attendencer>().Get();
                var cities = re.Models;
                int count = 0;
                foreach (var city in cities)
                {
                    count++;
                }
                return count;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
        }

        public async Task<string> signup(string mobile_number,string name,string email,string password)
        {
            try
            {
                var options = new SupabaseOptions
                {
                    AutoConnectRealtime = true
                };

                var supabase = new Supabase.Client(url, key, options);
                await supabase.InitializeAsync();
                var result = await supabase.From<City>()
                    .Select(x => new object[] { x.mobile_number, x.name })
                    .Where(x => x.mobile_number == mobile_number && x.email == email)
                    .Single();
                if (result != null)
                {
                    
                    return "already";
                }
                else
                {
                    var re = await supabase.From<City>().Get();
                    var cities = re.Models;
                    int count = 0;
                    foreach(var city in cities)
                    {
                        count++;
                    }
                    var data = new City
                    {
                        Sno = count + 1,
                        mobile_number = mobile_number,
                        name = name,
                        email = email,
                        password = password
                    };
                    await supabase.From<City>().Insert(data);
                    
                    return "registered";
                }
            }
            catch(Exception e)
            {
                
                return e.Message;

            }
        }
        public async Task sendattendence(int id,string status,int count,string name)
        {
            try
            {
                var options = new SupabaseOptions
                {
                    AutoConnectRealtime = true
                };

                var supabase = new Supabase.Client(url, key, options);
                await supabase.InitializeAsync();
                var data = new attendencer { id=id,sno = count, date=DateTime.Today.ToString("yyyy-MM-dd"), status=status,name=name };
                await supabase.From<attendencer>().Insert(data);
                MessageBox.Show("data Inserted");
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public async Task<bool> Login(string email, string password)
        {
            try
            {
                var options = new SupabaseOptions
                {
                    AutoConnectRealtime = true
                };

                var supabase = new Supabase.Client(url, key, options);
                await supabase.InitializeAsync();
                var result = await supabase.From<City>()
                    .Select(x => new object[] { x.mobile_number, x.name })
                    .Where(x => x.email == email && x.password == password)
                    .Single();
                var result1 = await supabase.From<City>()
                    .Select(x => new object[] { x.mobile_number, x.name })
                    .Where(x => x.name == email && x.password == password)
                    .Single();
                if (result == null && result1 == null)

                {
                    
                    MessageBox.Show("data not found");
                    return false;
                }
                else
                { 
                    return true;
                }

            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        public async Task<List<City>> fetchdata()
        {
            try
            {
                var options = new SupabaseOptions
                {
                    AutoConnectRealtime = true
                };

                var supabase = new Supabase.Client(url, key, options);
                await supabase.InitializeAsync();
                var result = await supabase.From<City>().Get();
                Students = result.Models;
                
                return Students;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }
        public async Task<List<attendencer>> fetchdata1()
        {
            try
            {
                var options = new SupabaseOptions
                {
                    AutoConnectRealtime = true
                };

                var supabase = new Supabase.Client(url, key, options);
                await supabase.InitializeAsync();
                var result = await supabase.From<attendencer>().Get();
                attendencedata = result.Models;
                MessageBox.Show("attendence data fetched successfully");
                return attendencedata;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

    }
    [Table("registration")]
    public class City : BaseModel
    {
        [Column("Sno")]
        public int Sno { get; set; }

        [Column("name")]
        public string name { get; set; }

        [Column("email")]
        public string email { get; set; }

        [Column("password")]
        public string password { get; set; }

        [Column("mobile_number")]
        public string mobile_number { get; set; }


    }
    [Table("Attendence")]
    public class attendencer : BaseModel
    {
        [Column("id")]
        public int id { get; set; }

        [Column("sno")]
        public int sno { get; set; }

        [Column("date")]
        public string date { get; set; }

        [Column("status")]
        public string status { get; set; }

        [Column("name")]
        public string name { get; set; }
        
    }

}
