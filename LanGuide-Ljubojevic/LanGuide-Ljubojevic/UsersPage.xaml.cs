using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LanGuide_Ljubojevic.Model;
using Newtonsoft.Json.Linq;
using System.Data;
using System.IO;
using CsvHelper;
using System.Xml;
using System.Net;
using System.Dynamic;
using Aspose.Cells;

namespace LanGuide_Ljubojevic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsersPage : ContentPage
    {
        List<UsersAPI> usersList = new List<UsersAPI>();
        public UsersPage()
        {
            GetJsonAsync();
            InitializeComponent();
        }
        public async Task GetJsonAsync()
        {
            var uri = new Uri("https://www.idt.mdh.se/personal/plt01/languide/?get=users");
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                string json = content.ToString();
                var jsonObject = JObject.Parse(json);
                var data = jsonObject["data"];
                var jsonArray = JArray.Parse(data.ToString());
                foreach (var token in jsonArray)
                {
                    UsersAPI u = new UsersAPI();
                    string id_user = token["id_user"].ToString();
                    string email = token["email"].ToString();
                    string create_time = token["create_time"].ToString();
                    u.id_user = id_user;
                    u.email = email;
                    u.create_time = create_time;
                    usersList.Add(u);
                }
            }
            UsersListView.ItemsSource = usersList;
        }
        private async void CSVButton_Clicked(object sender, EventArgs e)
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "users.csv");
            var uri = new Uri("https://www.idt.mdh.se/personal/plt01/languide/?get=users&order=id_user");
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(uri);
                var content = await response.Content.ReadAsStringAsync();
                string json = content.ToString();
                var jsonObject = JObject.Parse(json);
                var data = jsonObject["data"];
                var jsonArray = JArray.Parse(data.ToString());
                using (var writer = File.CreateText(path))
            {
              foreach (var token in jsonArray)
                    {
                        writer.Write(token);
                    }
                    await DisplayAlert("Download CSV", "File saved", "OK");
                    
            }        
        }
        private void UIDSort_Tapped(object sender, EventArgs e)
        {
            var sorted = usersList.OrderBy(x => x.id_user).ToList();
            UsersListView.ItemsSource = sorted;
        }
        private void EmailSort_Tapped(object sender, EventArgs e)
        {
            var sorted = usersList.OrderBy(x => x.email).ToList();
            UsersListView.ItemsSource = sorted;
        }
        private void DateSort_Tapped(object sender, EventArgs e)
        {
            var sorted = usersList.OrderBy(x => x.create_time).ToList();
            UsersListView.ItemsSource = sorted;
        }
        private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            var keyword = SearchMe.Text;
            UsersListView.ItemsSource = usersList.Where(x => x.id_user.ToString() == keyword);
        }
        private void ShowAll_Tapped(object sender, EventArgs e)
        {
            GetJsonAsync();
        }
        private void Show25_Tapped(object sender, EventArgs e)
        {
            var items = usersList.Select(x => x).Take(25);
            UsersListView.ItemsSource = items;
        }
        private void Show100_Tapped(object sender, EventArgs e)
        {
            var items = usersList.Select(x => x).Take(100);
            UsersListView.ItemsSource = items;
        }
    }
}
