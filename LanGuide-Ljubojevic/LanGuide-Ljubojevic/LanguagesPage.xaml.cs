using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LanGuide_Ljubojevic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LanguagesPage : ContentPage
    {
        public static string[] Languages = new[]{
            "Croatian",
            "Slovenian",
            "English",
            };
        public LanguagesPage()
        {
            InitializeComponent();
        }

       
    }
}
