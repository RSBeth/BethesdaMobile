using BethesdaMobileXamarin.Room.Model;
using BethesdaMobileXamarin.Room.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BethesdaMobileXamarin.Room
{   
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmptyRoomForm : ContentPage
    {

        private RoomServices roomServices;
        public EmptyRoomForm()
        {
            InitializeComponent();
          
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await GetEmptyRoom();
        }

        private async Task GetEmptyRoom()
        {
            roomServices = new RoomServices();
            var results = await roomServices.GetAvailableRoom();
            listKamarKosong.ItemsSource = results;
        }
    }
}