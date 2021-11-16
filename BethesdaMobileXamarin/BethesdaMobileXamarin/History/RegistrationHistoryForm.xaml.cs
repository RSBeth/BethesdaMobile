using BethesdaMobileXamarin.History.Models;
using BethesdaMobileXamarin.History.Services;
using BethesdaMobileXamarin.Utility;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace BethesdaMobileXamarin.History
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationHistoryForm : ContentPage
    {
        private RegistrationHistoryServices registrationHistoryServices;
        public RegistrationHistoryForm()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Load Data Pendaftaran"))

            {
                await GetKunjungan();
            }

        }

        private async Task GetKunjungan()
        {
            string noRM = Preferences.Get("noRM", "");
            registrationHistoryServices = new RegistrationHistoryServices();
            var results = await registrationHistoryServices.GetKunjunganServices(noRM);
            if(results.Count <=0)
            {
                await PopupNavigation.Instance.PushAsync(new DialogAlertCustom("Informasi", "Data Pendaftaran Tidak Ditemukan" ));
                return;
            }
            else
            {
                foreach (RegistrationHistory registrationHistory in results)
                {
                    registrationHistory.namadokter = "Dokter :  " + registrationHistory.namadokter;
                    registrationHistory.namaklinik = "Klinik :  " + registrationHistory.namaklinik;
                    registrationHistory.noUrutdokter = "No Urut :  " + registrationHistory.noUrutdokter;
                    registrationHistory.namapasien = "Nama :  " + registrationHistory.namapasien;
                    registrationHistory.tglreg = "Tgl Periksa :  " + registrationHistory.tglreg;
                }
                listKunjungan.ItemsSource = results;
            }
         
        }
    }
}