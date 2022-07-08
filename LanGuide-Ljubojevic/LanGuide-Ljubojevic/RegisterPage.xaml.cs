using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LanGuide_Ljubojevic.Helpers;

namespace LanGuide_Ljubojevic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }
        async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            bool isNameEmpty = string.IsNullOrEmpty(NameEntry.Text);
            bool isSurnameEmpty = string.IsNullOrEmpty(SurnameEntry.Text);
            bool isEmailEmpty = string.IsNullOrEmpty(EmailEntry.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(PassEntry.Text);
            bool isRePasswEmpty = string.IsNullOrEmpty(RePassEntry.Text);
            bool isAddressEmpty = string.IsNullOrEmpty(AddressEntry.Text);
            bool isPassSame = PassEntry.Text == RePassEntry.Text;
            bool register = isNameEmpty && isSurnameEmpty && isEmailEmpty && isPasswordEmpty && isRePasswEmpty && isAddressEmpty;
            if (register && !isPassSame)
            {
                await DisplayAlert("Error", "Please reenter values", "OK");
            }
            else { 
                var user = await FirebaseHelper.AddUser(EmailEntry.Text, PassEntry.Text, NameEntry.Text, SurnameEntry.Text, AddressEntry.Text);
                if (user)
                {
                    await DisplayAlert("Registered", "", "OK");
                    await Navigation.PushAsync(new MainPage());
                }
                else await DisplayAlert("Error", "Registration failed", "OK");
            }
        }
    }
}
