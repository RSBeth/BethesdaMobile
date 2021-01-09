using BethesdaMobileXamarin.Registration.Services;
using BethesdaMobileXamarin.Utility;
using BethesdaMobileXamarin.Utility.Model;
using BethesdaMobileXamarin.Utility.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BethesdaMobileXamarin.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IntroPage : ContentPage
    {
        private DataBaseUtil dataBaseUtil;
        private DokterServices dokterServices;
        private KlinikServices klinikServices;
        private UtilServices utilServices;
        private Hari hari;
        public IntroPage()
        {
            InitializeComponent();
            dataBaseUtil = new DataBaseUtil();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await GetInsertDokterAll();
            await pbIntro.ProgressTo(0.2, 750, Easing.Linear);
            await GetInsertKlinikAll();
            await pbIntro.ProgressTo(0.4, 750, Easing.Linear);
            await GetMaxDay();
            await pbIntro.ProgressTo(0.6, 750, Easing.Linear);
            await GetCurrentDate();
            await pbIntro.ProgressTo(1, 750, Easing.Linear);
            await Navigation.PushModalAsync(new MainMenu());
        }



        private async Task GetInsertDokterAll()
        {
            dokterServices = new DokterServices();
            var results = await dokterServices.GetAllDokterServices();
            dataBaseUtil.DeleteAllDokter();
            dataBaseUtil.InsertDokterAll(results);
          
        }



        private async Task GetInsertKlinikAll()
        {
            klinikServices = new KlinikServices();
            var results = await klinikServices.GetAllKlinikServices();
            dataBaseUtil.DeleteAllKlinik();
            dataBaseUtil.InsertKlinikAll(results);
          
        }


        private async Task GetMaxDay()
        {
            utilServices= new UtilServices();

            hari = await utilServices.GetMaxDayServices();
            Preferences.Set("maxHari", hari.hari);

        }

        private async Task GetCurrentDate()
        {
            utilServices = new UtilServices();

             string currentDate = await utilServices.GetCurrentDateServer();
             Preferences.Set("currentDate", currentDate);

        }
    }
}