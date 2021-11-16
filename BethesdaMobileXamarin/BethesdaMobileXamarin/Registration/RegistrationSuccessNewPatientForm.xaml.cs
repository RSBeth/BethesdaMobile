using BethesdaMobileXamarin.Main;
using BethesdaMobileXamarin.Registration.Model;
using BethesdaMobileXamarin.Utility;
using NativeMedia;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BethesdaMobileXamarin.Registration
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationSuccessNewPatientForm : ContentPage
    {
        private RegistrationResults registrationResults;
        private NewPatientResults newPatientResults;

        public RegistrationSuccessNewPatientForm()
        {

            InitializeComponent();
          
        }

        public RegistrationSuccessNewPatientForm(RegistrationResults registrationResults, NewPatientResults newPatientResults)
        {
            InitializeComponent();
            
            this.registrationResults = registrationResults;
            this.newPatientResults = newPatientResults;
            TxtNoKTP.Text = newPatientResults.vc_no_ktp;
            txtTgl.Text = registrationResults.tglreg;
            txtNama.Text = newPatientResults.NamaPasien;
            txtKlinik.Text = registrationResults.namaklinik;
            txtDokter.Text = registrationResults.namadokter;
            txtCatatan.Text = registrationResults.deskripsiresponse;
         
        }

        private async void btnScreenshoot_Clicked(object sender, EventArgs e)
        {
            await CaptureScreenshot();
        }

        async Task CaptureScreenshot()
        {
            var screenshot = await Screenshot.CaptureAsync();
          
            await MediaGallery.SaveAsync(MediaFileType.Image, await screenshot.OpenReadAsync(), "bethesdaMobile.png");

            await PopupNavigation.Instance.PushAsync(new DialogAlertCustom("Sukses", "Bukti Pendaftaran Berhasil Disimpan,Silahkan Cek Di Gallery"));    
        }

        private async void btnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainMenu());
            Navigation.RemovePage(this);
        }

      

    }
}