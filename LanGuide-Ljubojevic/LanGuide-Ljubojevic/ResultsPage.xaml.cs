using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LanGuide_Ljubojevic.Model;
using System.IO;

namespace LanGuide_Ljubojevic
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ResultsPage : ContentPage
{
        List<ResultsAPI> resultsList = new List<ResultsAPI>();
        public ResultsPage()
        {
            GetJsonAsync();
            InitializeComponent();
        }
        public async Task GetJsonAsync()
        {
            var uri = new Uri("https://www.idt.mdh.se/personal/plt01/languide/?get=results");
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
                    ResultsAPI u = new ResultsAPI();
                    string id_user = token["id_user"].ToString();
                    string email = token["email"].ToString();
                    string create_time = token["create_time"].ToString();
                    string id_exercise = token["id_exercise"].ToString();
                    string result_percent = token["result_percent"].ToString();
                    string result_score = token["result_score"].ToString();
                    string result_max = token["result_max"].ToString();
                    string skill = token["skill"].ToString();
                    string language = token["language"].ToString();
                    string result_date = token["result_date"].ToString();
                    u.id_user = id_user;
                    u.email = email;
                    u.create_time = create_time;
                    u.id_exercise = id_exercise;
                    u.result_percent = Convert.ToInt32(result_percent);
                    u.result_score = Convert.ToInt32(result_score);
                    u.result_max = result_max;
                    u.skill = skill;
                    u.language = language;
                    u.result_date = result_date;
                    resultsList.Add(u);
                }
            }
            ResultsListView.ItemsSource = resultsList;
        }
        private async void CSVButton_Clicked(object sender, EventArgs e)
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "results.csv");
            var uri = new Uri("https://www.idt.mdh.se/personal/plt01/languide/?get=results");
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
            var sorted = resultsList.OrderBy(x => x.id_user).ToList();
            ResultsListView.ItemsSource = sorted;
        }
        private void EmailSort_Tapped(object sender, EventArgs e)
        {
            var sorted = resultsList.OrderBy(x => x.email).ToList();
            ResultsListView.ItemsSource = sorted;
        }
        private void ExerSort_Tapped(object sender, EventArgs e)
        {
            var sorted = resultsList.OrderBy(x => x.id_exercise).ToList();
            ResultsListView.ItemsSource = sorted;
        }
        private void ResPSort_Tapped(object sender, EventArgs e)
        {
            var sorted = resultsList.OrderBy(x => x.result_percent).ToList();
            ResultsListView.ItemsSource = sorted;
        }
        private void ResSort_Tapped(object sender, EventArgs e)
        {
            var sorted = resultsList.OrderBy(x => x.result_score).ToList();
            ResultsListView.ItemsSource = sorted;
        }
        private void ResMaxSort_Tapped(object sender, EventArgs e)
        {
            var sorted = resultsList.OrderBy(x => x.result_max).ToList();
            ResultsListView.ItemsSource = sorted;
        }
        private void LangSort_Tapped(object sender, EventArgs e)
        {
            var sorted = resultsList.OrderBy(x => x.language).ToList();
            ResultsListView.ItemsSource = sorted;
        }
        private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            var keyword = SearchMe.Text;
            ResultsListView.ItemsSource = resultsList.Where(x => x.id_user.ToString() == keyword);
        }
        private void ShowAll_Tapped(object sender, EventArgs e)
        {
            GetJsonAsync();
        }
        private void Show25_Tapped(object sender, EventArgs e)
        {
            var items = resultsList.Select(x => x).Take(25);
            ResultsListView.ItemsSource = items;
        }
        private void Show100_Tapped(object sender, EventArgs e)
        {
            var items = resultsList.Select(x => x).Take(100);
            ResultsListView.ItemsSource = items;
        }

        private async void FilterButton_Clicked(object sender, EventArgs e)
        {
            bool isUserIDEmpty = string.IsNullOrEmpty(UserIDEntry.Text);
            bool isExerEmpty = string.IsNullOrEmpty(ExerEntry.Text);
            bool isUpperPEmpty = string.IsNullOrEmpty(UpperLimit.Text);
            bool isLowerPEmpty = string.IsNullOrEmpty(LowerLimit.Text);
            bool isMinSEmpty = string.IsNullOrEmpty(MinSEntry.Text);
            bool isMaxSEmpty = string.IsNullOrEmpty(MaxSEntry.Text);

            if (isUserIDEmpty && isExerEmpty && isUpperPEmpty && isLowerPEmpty && isMinSEmpty && isMaxSEmpty) {
                await App.Current.MainPage.DisplayAlert("Error", "Enter filter data", "OK");
            }
            if (!isUserIDEmpty && isExerEmpty && isUpperPEmpty && isLowerPEmpty && isMinSEmpty && isMaxSEmpty) {
                ResultsListView.ItemsSource = resultsList.Where(x => x.id_user.ToString() == UserIDEntry.Text);
            }
            if (isUserIDEmpty && !isExerEmpty && isUpperPEmpty && isLowerPEmpty && isMinSEmpty && isMaxSEmpty) {
                ResultsListView.ItemsSource = resultsList.Where(x => x.id_exercise.ToString() == ExerEntry.Text);
            }
            if (!isUserIDEmpty && !isExerEmpty && isUpperPEmpty && isLowerPEmpty && isMinSEmpty && isMaxSEmpty) {
                ResultsListView.ItemsSource = resultsList.Where(x => x.id_exercise.ToString() == ExerEntry.Text && x.id_user.ToString() == ExerEntry.Text);
            }

            if (isUserIDEmpty && isExerEmpty && !isUpperPEmpty && !isLowerPEmpty && isMinSEmpty && isMaxSEmpty) {
                ResultsListView.ItemsSource = resultsList.Where(x => x.result_percent <=  Convert.ToInt32(UpperLimit.Text) && x.result_percent >= Convert.ToInt32(LowerLimit.Text));
            }
            if (isUserIDEmpty && isExerEmpty && !isUpperPEmpty && isLowerPEmpty && isMinSEmpty && isMaxSEmpty) {
                ResultsListView.ItemsSource = resultsList.Where(x => x.result_percent <= Convert.ToInt32(UpperLimit.Text));
            }
            if (isUserIDEmpty && isExerEmpty && isUpperPEmpty && !isLowerPEmpty && isMinSEmpty && isMaxSEmpty) {
                ResultsListView.ItemsSource = resultsList.Where(x => x.result_percent >= Convert.ToInt32(LowerLimit.Text));
            }

            if (isUserIDEmpty && isExerEmpty && !isUpperPEmpty && isLowerPEmpty && !isMinSEmpty && isMaxSEmpty) {
                ResultsListView.ItemsSource = resultsList.Where(x => x.result_percent <= Convert.ToInt32(UpperLimit.Text) && x.result_score >= Convert.ToInt32(MinSEntry.Text));
            }
            if (isUserIDEmpty && isExerEmpty && !isUpperPEmpty && isLowerPEmpty && isMinSEmpty && !isMaxSEmpty) {
                ResultsListView.ItemsSource = resultsList.Where(x => x.result_percent <= Convert.ToInt32(UpperLimit.Text) && x.result_score <= Convert.ToInt32(MaxSEntry.Text));
            }
            if (isUserIDEmpty && isExerEmpty && !isUpperPEmpty && isLowerPEmpty && !isMinSEmpty && !isMaxSEmpty) {
                ResultsListView.ItemsSource = resultsList.Where(x => x.result_percent <= Convert.ToInt32(UpperLimit.Text) && x.result_score <= Convert.ToInt32(MaxSEntry.Text) && x.result_score >= Convert.ToInt32(MinSEntry.Text));
            }

            if (isUserIDEmpty && isExerEmpty && isUpperPEmpty && !isLowerPEmpty && !isMinSEmpty && isMaxSEmpty) {
                ResultsListView.ItemsSource = resultsList.Where(x => x.result_percent >= Convert.ToInt32(LowerLimit.Text) && x.result_score >= Convert.ToInt32(MinSEntry.Text));
            }
            if (isUserIDEmpty && isExerEmpty && isUpperPEmpty && !isLowerPEmpty && isMinSEmpty && !isMaxSEmpty) {
                ResultsListView.ItemsSource = resultsList.Where(x => x.result_percent >= Convert.ToInt32(LowerLimit.Text) && x.result_score <= Convert.ToInt32(MaxSEntry.Text));
            }
            if (isUserIDEmpty && isExerEmpty && isUpperPEmpty && !isLowerPEmpty && !isMinSEmpty && !isMaxSEmpty) {
                ResultsListView.ItemsSource = resultsList.Where(x => x.result_percent >= Convert.ToInt32(LowerLimit.Text) && x.result_score >= Convert.ToInt32(MinSEntry.Text) && x.result_score <= Convert.ToInt32(MaxSEntry.Text));
            }

            if (isUserIDEmpty && isExerEmpty && !isUpperPEmpty && !isLowerPEmpty && !isMinSEmpty && isMaxSEmpty) {
                ResultsListView.ItemsSource = resultsList.Where(x => x.result_percent >= Convert.ToInt32(LowerLimit.Text) && x.result_percent <= Convert.ToInt32(UpperLimit.Text) && x.result_score >= Convert.ToInt32(MinSEntry.Text));
            }
            if (isUserIDEmpty && isExerEmpty && !isUpperPEmpty && !isLowerPEmpty && isMinSEmpty && !isMaxSEmpty) {
                ResultsListView.ItemsSource = resultsList.Where(x => x.result_percent >= Convert.ToInt32(LowerLimit.Text) && x.result_percent <= Convert.ToInt32(UpperLimit.Text) && x.result_score <= Convert.ToInt32(MaxSEntry.Text));
            }
            if (isUserIDEmpty && isExerEmpty && !isUpperPEmpty && !isLowerPEmpty && !isMinSEmpty && !isMaxSEmpty) {
                ResultsListView.ItemsSource = resultsList.Where(x => x.result_percent >= Convert.ToInt32(LowerLimit.Text) && x.result_percent <= Convert.ToInt32(UpperLimit.Text) && x.result_score >= Convert.ToInt32(MinSEntry.Text) && x.result_score <= Convert.ToInt32(MaxSEntry.Text));
            }

            if (isUserIDEmpty && isExerEmpty && isUpperPEmpty && isLowerPEmpty && !isMinSEmpty && isMaxSEmpty)
            {
                ResultsListView.ItemsSource = resultsList.Where(x => x.result_score >= Convert.ToInt32(MinSEntry.Text));
            }
            if (isUserIDEmpty && isExerEmpty && isUpperPEmpty && isLowerPEmpty && isMinSEmpty && !isMaxSEmpty)
            {
                ResultsListView.ItemsSource = resultsList.Where(x => x.result_score <= Convert.ToInt32(MaxSEntry.Text));
            }
            if (isUserIDEmpty && isExerEmpty && isUpperPEmpty && isLowerPEmpty && !isMinSEmpty && !isMaxSEmpty)
            {
                ResultsListView.ItemsSource = resultsList.Where(x => x.result_score <= Convert.ToInt32(MaxSEntry.Text) && x.result_score >= Convert.ToInt32(MinSEntry));
            }
           

        }
    }
}