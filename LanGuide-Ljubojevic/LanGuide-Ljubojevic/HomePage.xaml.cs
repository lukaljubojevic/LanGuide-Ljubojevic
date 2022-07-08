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

    public partial class HomePage : ContentPage
    {

        public HomePage()
        {   
            var LogoImage = new Image { Aspect = Aspect.AspectFit };
            var UserImage = new Image { Aspect = Aspect.AspectFit };
            var ResultImage = new Image { Aspect = Aspect.AspectFit };
            var LanguageImage = new Image { Aspect = Aspect.AspectFit };
            LogoImage.Source = ImageSource.FromFile("Logo.png");
            UserImage.Source = ImageSource.FromFile("Users.png");
            ResultImage.Source = ImageSource.FromFile("Result.png");
            LanguageImage.Source = ImageSource.FromFile("Language.png");
            InitializeComponent();
        }
        private void UserButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NavigationPage(new UsersPage()));
        }

        private void ResultButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ResultsPage());
        }

        private void LanguageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LanguagesPage());
        }
        private void OnProfile_Tap(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProfilePage());
        }
        private void OnLogout_Tap(object sender, EventArgs e) {
            Navigation.PushAsync(new MainPage());
        }
    }
}
