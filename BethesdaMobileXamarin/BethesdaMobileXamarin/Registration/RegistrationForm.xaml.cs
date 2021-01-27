using BethesdaMobileXamarin.Main;
using BethesdaMobileXamarin.Registration.Model;
using BethesdaMobileXamarin.Registration.Services;
using BethesdaMobileXamarin.Utility;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BethesdaMobileXamarin.Registration
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationForm : ContentPage
    {

        private Registrations registration;
        private HolidayDate holidayDate;
        private DateUtil dateUtil;
        private RegistrationServices registrationServices;
        private RegistrationResults registrationResults;
        public RegistrationForm()
        {
            InitializeComponent();
            dateUtil= new DateUtil();
            holidayDate = new HolidayDate();
            
            setDatePickerValue();
            setDataPasien();
          

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            setDatePickerValue();
            setDataPasien();
            txtKlinik.Text = App.KlinikNamaRegis;
            txtDokter.Text = App.DokterNamaRegis;
        }
        private void setDataPasien()
        {
            lblNoRM.Text =  Preferences.Get("noRM","");
            lblNama.Text =   Preferences.Get("namaPasien","");
            lblTglLahir.Text =  Preferences.Get("tglLahir", "");
            lblAlamat.Text  = Preferences.Get("alamat", "");
        }
        private void setDatePickerValue()
        {
            //dtTglPeriksa.NullableDate = null;
           
            string maxHari = Preferences.Get("maxHari", "");
            string currentDate = Preferences.Get("currentDate", "");
           
            DateTime current_date = DateUtil.ConvertStringToDate(currentDate, "dd/MM/yyyy");
            dtTglPeriksa.MinimumDate = current_date;
            dtTglPeriksa.MaximumDate = current_date.AddDays(Double.Parse(maxHari)-1);
            
        }
      

     

       
        private async void txtKlinik_Focused(object sender, FocusEventArgs e)
            {
            App.KodeKlinikRegis = "";
            App.KlinikNamaRegis = "";
            txtKlinik.Text = "";
            await Navigation.PushModalAsync(new KlinikPickerForm(App.KodeKlinikRegis, App.KlinikNamaRegis,App.KodeDokterRegis , App.DokterNamaRegis, dtTglPeriksa.Date.ToString()));
            // await Navigation.PushAsync(new KlinikPickerForm(_kodeKLinik, _namaKlinik, _kodeDokter, _namaDokter, dtTglPeriksa.Date.ToString()));


        }

        private async void txtDokter_Focused(object sender, FocusEventArgs e)
        {
            App.KodeDokterRegis = "";
            App.DokterNamaRegis = "";
            txtDokter.Text = "";
         
            await Navigation.PushModalAsync(new DokterPickerForm(App.KodeKlinikRegis, App.KlinikNamaRegis, App.KodeDokterRegis, App.DokterNamaRegis, dtTglPeriksa.Date.ToString()));
        }

        private async void btnDaftar_Clicked(object sender, EventArgs e)
        {
            holidayDate =  await GetHolidayDateTask(dtTglPeriksa.Date.ToString());
            if (holidayDate.deskripsiresponse.ToLower() != ("ok"))
            {

                 await PopupNavigation.Instance.PushAsync(new DialogAlertCustom("Informasi", holidayDate.response));
                return;

            }
           
            if ((txtKlinik.Text == "") || (App.KodeKlinikRegis == ""))
            {
                await PopupNavigation.Instance.PushAsync(new DialogAlertCustom("Warning", "Klinik Belum Dipilih!!"));
                return;
            }
            if ((txtDokter.Text == "") || (App.KodeDokterRegis == ""))
            {
                await PopupNavigation.Instance.PushAsync(new DialogAlertCustom("Warning", "Dokter Belum dipilih!!"));
                return;
            }
            string textKonfirmasi = "Apakah Anda Yakin Mendaftar Pada Tgl: " + dtTglPeriksa.Date.ToShortDateString().Trim() + " , Klinik : " + txtKlinik.Text.Trim() + ", Dokter : " + txtDokter.Text.Trim(); 
            bool answer = await DisplayAlert("Konfirmasi", textKonfirmasi, "Yes", "Cancel");
            if(answer)
            {
                doRegistration();
            }    
        }

        private async void doRegistration()
        {
            registration= new Registrations();
            registrationServices = new RegistrationServices();
            registration.norm = Preferences.Get("noRM", "");
            registration.tglreg = dtTglPeriksa.Date.ToShortDateString();
            registration.kodedokter = App.KodeDokterRegis;
            registration.kodeklinik = App.KodeKlinikRegis;
            try
            {
              registrationResults = new RegistrationResults();
                registrationResults = await registrationServices.postRegistration(registration);
            }
            catch(Exception ex)
            {
                await PopupNavigation.Instance.PushAsync(new DialogAlertCustom("Error", ex.Message));
            }
            if (registrationResults.response.ToString() =="gagal")
            {
                await PopupNavigation.Instance.PushAsync(new DialogAlertCustom("Warning", registrationResults.deskripsiresponse));
            }
            else{
                await PopupNavigation.Instance.PushAsync(new DialogAlertCustom("Sukses", registrationResults.deskripsiresponse));
            }
        }

        private async Task<HolidayDate> GetHolidayDateTask(string tglRegis)
        {
            RegistrationServices registrationServices = new RegistrationServices();
            HolidayDate holidayList = await registrationServices.GetHolidayDate(tglRegis);
          
            return holidayList;



        }

        private void dtTglPeriksa_Focused(object sender, FocusEventArgs e)
        {
            App.KodeDokterRegis = "";
            App.DokterNamaRegis = "";
            txtDokter.Text = "";
           
        }

        //private  void dtTglPeriksa_DateSelected(object sender, DateChangedEventArgs e)
        //{
        //    string tglRegis = dtTglPeriksa.Date.ToShortDateString();
            
        ////    await GetHolidayDateTask(tglRegis);
            
        //}
    }
}