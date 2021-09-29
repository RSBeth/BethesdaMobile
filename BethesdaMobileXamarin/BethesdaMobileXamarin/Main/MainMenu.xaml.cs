using BethesdaMobileXamarin.Main.model;
using BethesdaMobileXamarin.Registration;
using BethesdaMobileXamarin.Registration.Model;
using BethesdaMobileXamarin.Room;
using BethesdaMobileXamarin.Schedule;
using BethesdaMobileXamarin.Utility;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace BethesdaMobileXamarin.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenu : ContentPage
    {
        public ObservableCollection<MenuModel> MenuModels { get; set; }
        public MainMenu()
        {
            InitializeComponent();
            loadMenu();
            NavigationPage.SetHasBackButton(this, false);

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if(App.IsUserLoggedIn)
            {
                lblLogout.IsVisible = true;
            }
        }
        private void loadMenu()
        {

            MenuModels = new ObservableCollection<MenuModel>() {

            new MenuModel(){ Menuid=1 , Title ="Pendaftaran Online" ,ImageSource="registration.jpg"},
            new MenuModel(){Menuid=2 ,Title ="Riwayat Pendaftaran Online" ,ImageSource="stetoskop.jpg"},
            new MenuModel(){Menuid=3 ,Title ="Jadwal Dokter" ,ImageSource="doctor.png"},
            new MenuModel(){Menuid=4 ,Title ="Informasi Kamar" ,ImageSource="bed.jpg"},
         //   new MenuModel(){Menuid=5 ,Title ="Info Antrian Dokter" ,ImageSource="antri.png"},
          //  new MenuModel(){Menuid=6 ,Title ="Tracking Resep Farmasi" ,ImageSource="farmasi.png"},
           // new MenuModel(){Menuid=7 ,Title ="Telemedicine" ,ImageSource="telemedecine.png"}



       };

            KoleksiMenu.ItemsSource = MenuModels;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Frame frameClicked = (Frame)sender;
            var item = (TapGestureRecognizer)frameClicked.GestureRecognizers[0];
            int id = (int)item.CommandParameter;
            if (id == 1)  //menu login
            {
                if (App.IsUserLoggedIn == true)
                {

                    await Navigation.PushAsync(new RegistrationForm());
                }
                else
                {
                    NewPatient newPatient = new NewPatient();
                    newPatient.vc_no_ktp = "123";
                    await Navigation.PushAsync(new RegistrationNewPatientForm(newPatient));

                    //   await Navigation.PushAsync(new RegistrationNewPatientForm(newPatient));
                }




            }
            if (id == 3)
            {
                await Navigation.PushAsync(new ScheduleMain());

                //}

                //            MaterialCardmaterialCard = (MaterialCard)sender;

            }

            if (id == 4)
            {
                await Navigation.PushAsync(new EmptyRoomForm());

            }

        }
    }
}