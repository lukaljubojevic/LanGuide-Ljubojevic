using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using LanGuide_Ljubojevic.Helpers;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace LanGuide_Ljubojevic
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ProfilePage : ContentPage
{
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email)); 
            }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private string surname;
        public string Surname
        {
            get { return surname; }
            set
            {
                surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }
        private string address;

        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged(nameof(Address));
            }
        }
        private double lat;

        public double Lat
        {
            get { return lat; }
            set
            {
                lat = value;
                OnPropertyChanged(nameof(Lat));
            }
        }
        private double lon;

        public double Lon
        {
            get { return lon; }
            set
            {
                lon = value;
                OnPropertyChanged(nameof(Lon));
            }
        }
   
        public ProfilePage()
    {
        BindingContext = this;
        Email = Session.email;
        Name = Session.name;
        Surname = Session.surname;
        Address = Session.address;
        InitializeComponent();

        }

        private async void Map_MapClicked(object sender, MapClickedEventArgs e)
        {
            Geocoder geoCoder = new Geocoder();
            IEnumerable<Position> approximateLocations = await geoCoder.GetPositionsForAddressAsync(Address);
            Position position = approximateLocations.FirstOrDefault();
            MapSpan mapSpan = new MapSpan(position, 0.01, 0.01);
            AddressMap.MoveToRegion(mapSpan);
            Pin pin = new Pin
            {
                Label = "You",
                Address = Address,
                Type = PinType.Place,
                Position = position
            };
            AddressMap.Pins.Add(pin);

        }
    }
}