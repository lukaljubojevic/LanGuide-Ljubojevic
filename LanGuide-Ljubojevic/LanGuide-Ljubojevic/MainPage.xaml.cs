using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Firebase.Database;
using Firebase.Database.Query;
using LanGuide_Ljubojevic.Helpers;

namespace LanGuide_Ljubojevic
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            var LogoImage = new Image { Aspect = Aspect.AspectFit };
            LogoImage.Source = ImageSource.FromFile("Logo.png");
            InitializeComponent();
        }

        async void LoginButton_Clicked(object sender, EventArgs e)
        {
            bool isEmailEmpty = string.IsNullOrEmpty(Email.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(Password.Text);
            if (isEmailEmpty || isPasswordEmpty)
            {
              await App.Current.MainPage.DisplayAlert("Error", "Enter user data", "OK");
                  
            }
            else
            {
                var user = await FirebaseHelper.GetUser(Email.Text);
                if (user != null)
                    if (Email.Text == user.Email && Password.Text == user.Password)
                    {
                        await DisplayAlert("Login Success", "", "Ok");
                        Session.email = user.Email;
                        Session.name = user.Name;
                        Session.surname = user.Surname;
                        Session.address = user.Address;
                        await Navigation.PushAsync(new HomePage());
                    }
                    else
                        await DisplayAlert("Login Fail", "Please enter correct Email and Password", "OK");
                else
                    await DisplayAlert("Login Fail", "User not found", "OK");
            }

        }
        private void OnRegister_Tap(object sender, EventArgs e) { 
            Navigation.PushAsync(new RegisterPage());
        }
    }
}
