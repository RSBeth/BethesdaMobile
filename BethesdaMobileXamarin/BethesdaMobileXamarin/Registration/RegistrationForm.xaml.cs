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

        private string _kodeKLinik;
        private string _namaKlinik;
        private string _kodeDokter;
        private string _namaDokter;
        private HolidayDate holidayDate;
        private DateUtil dateUtil;
        public RegistrationForm()
        {
            InitializeComponent();
            dateUtil= new DateUtil();
            holidayDate = new HolidayDate();
            _kodeKLinik = "";
            _namaKlinik = "";
            _kodeDokter = "";
            _namaDokter = "";
          // setDatePickerValue();
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

        public RegistrationForm(string kodeKlinik, string namaKlinik, string kodeDokter, string namaDokter,string dateSelected)
        {
            InitializeComponent();
          ;
            _kodeKLinik = kodeKlinik;
            _namaKlinik = namaKlinik;
            _kodeDokter = kodeDokter;
            _namaDokter = namaDokter;
            txtDokter.Text = _namaDokter;
            txtKlinik.Text = _namaKlinik;

            dtTglPeriksa.Date = DateTime.Parse(dateSelected);
          //  setDatePickerValue();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }
        private async void txtKlinik_Focused(object sender, FocusEventArgs e)
            {
            _kodeKLinik = "";
            _namaKlinik = "";
            txtKlinik.Text = "";
            await Navigation.PushAsync(new KlinikPickerForm(_kodeKLinik, _namaKlinik, _kodeDokter, _namaDokter, dtTglPeriksa.Date.ToString()));
            
          
        }

        private async void txtDokter_Focused(object sender, FocusEventArgs e)
        {
            _kodeDokter = "";
            _namaDokter = "";
            txtDokter.Text = "";
         
            await Navigation.PushAsync(new DokterPickerForm(_kodeKLinik, _namaKlinik, _kodeDokter, _namaDokter, dtTglPeriksa.Date.ToString()));
        }

        private async void btnDaftar_Clicked(object sender, EventArgs e)
        {
            holidayDate =  await GetHolidayDateTask(dtTglPeriksa.Date.ToString());
            if (holidayDate.deskripsiresponse.ToLower() != ("ok"))
            {

                 await PopupNavigation.Instance.PushAsync(new DialogAlertCustom("Informasi", holidayDate.response));
                return;

            }
           
            if ((txtKlinik.Text == "") || (_kodeKLinik == ""))
            {
                await PopupNavigation.Instance.PushAsync(new DialogAlertCustom("Warning", "Klinik Belum Dipilih!!"));
                return;
            }
            if ((txtDokter.Text == "") || (_kodeDokter == ""))
            {
                await PopupNavigation.Instance.PushAsync(new DialogAlertCustom("Warning", "Dokter Belum dipilih!!"));
                return;
            }
            await PopupNavigation.Instance.PushAsync(new DialogAlertCustom("Info", "Apakah Anda Yakin Mendaftar? "));
           
        }

        private async Task<HolidayDate> GetHolidayDateTask(string tglRegis)
        {
            RegistrationServices registrationServices = new RegistrationServices();
            HolidayDate holidayList = await registrationServices.GetHolidayDate(tglRegis);
          
            return holidayList;



        }

        private void dtTglPeriksa_Focused(object sender, FocusEventArgs e)
        {
            _kodeDokter = "";
            _namaDokter = "";
            txtDokter.Text = "";
           
        }

        //private  void dtTglPeriksa_DateSelected(object sender, DateChangedEventArgs e)
        //{
        //    string tglRegis = dtTglPeriksa.Date.ToShortDateString();
            
        ////    await GetHolidayDateTask(tglRegis);
            
        //}
    }
}