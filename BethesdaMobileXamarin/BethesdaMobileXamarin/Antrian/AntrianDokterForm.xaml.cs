using BethesdaMobileXamarin.Antrian.Model;
using BethesdaMobileXamarin.Antrian.Models;
using BethesdaMobileXamarin.Antrian.Services;
using BethesdaMobileXamarin.Registration;
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

namespace BethesdaMobileXamarin.Antrian
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AntrianDokterForm : ContentPage
    {


        private Antrians antrian;
        private AntrianResults antrianResults;
        private string tgl_regis;
        private AntrianServices antrianServices;

        public AntrianDokterForm()
        {
            InitializeComponent();
            string currentDate = Preferences.Get("currentDate", "");
            tgl_regis = DateUtil.ConvertFormatDateTime(currentDate, "dd/MM/yyyy", "MM/dd/yyyy");
            antrianServices = new AntrianServices();
            App.KodeDokterAntrian = "";
            App.DokterNamaAntrian = "";
            txtDokter.Text = "";
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            txtKlinik.Text = App.KlinikNamaAntrian;
            txtDokter.Text = App.DokterNamaAntrian ;
        }
        private async void txtKlinik_Focused(object sender, FocusEventArgs e)
        {
            App.KodeKlinikAntrian = "";
            App.KlinikNamaAntrian = "";
            txtKlinik.Text = "";
            await Navigation.PushModalAsync(new KlinikPickerForm(App.KodeKlinikAntrian, App.KlinikNamaAntrian, App.KodeDokterAntrian, App.DokterNamaAntrian, tgl_regis, "Antrian"));


        }



        private async void btnAntrian_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtKlinik.Text) || String.IsNullOrEmpty(App.KodeKlinikAntrian))
            {
                await PopupNavigation.Instance.PushAsync(new DialogAlertCustom("Warning", "KLinik Harus Dipilih"));
                return;
            }
            if (string.IsNullOrEmpty(txtDokter.Text) || String.IsNullOrEmpty(App.KodeDokterAntrian))
            {
                await PopupNavigation.Instance.PushAsync(new DialogAlertCustom("Warning", "Dokter Harus Dipilih"));
                return;
            }
            antrian = new Antrians();
            antrian.tgl = tgl_regis;
            antrian.kodeDokter = App.KodeDokterAntrian;
            antrian.kodeKLinik = App.KodeKlinikAntrian;
            antrianResults = new AntrianResults();
            antrianServices = new AntrianServices();
            antrianResults = await antrianServices.GetAntrianServices(antrian);
            string infoAntrian = "Total Pasien dilayani adalah : " + antrianResults.dilayani + ", Hubungi petugas Klinik jika nomer antrian anda sudah terlewat";
            await PopupNavigation.Instance.PushAsync(new DialogAlertCustom("Info Antrian", infoAntrian));

        }

        private async void txtDokter_Focused(object sender, FocusEventArgs e)
        {
            App.KodeDokterAntrian = "";
            App.DokterNamaAntrian= "";
            txtDokter.Text = "";

            await Navigation.PushModalAsync(new DokterPickerForm(App.KodeKlinikAntrian, App.KlinikNamaAntrian, App.KodeDokterAntrian, App.DokterNamaAntrian, tgl_regis, "Antrian"));

        }
    }
}