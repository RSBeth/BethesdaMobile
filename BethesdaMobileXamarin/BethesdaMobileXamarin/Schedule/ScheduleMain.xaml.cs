using BethesdaMobileXamarin.Schedule.Model;
using BethesdaMobileXamarin.Schedule.Services;
using BethesdaMobileXamarin.Utility;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BethesdaMobileXamarin.Schedule
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScheduleMain : TabbedPage
    {  
        private KlinikSchedule klinikSchedule;
        private DokterSchedule dokterSchedule;
        public ScheduleMain()
        {
            InitializeComponent();
            klinikSchedule = new KlinikSchedule();
            dokterSchedule = new DokterSchedule();
         
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            txtKlinik.Text = App.KlinikNamaSchedule;
            txtDokter.Text = App.DokterNamaSchedule;
        }

        private async void txtKlinik_Focused(object sender, FocusEventArgs e)
        {
              txtKlinik.Text = "";
            App.KlinikNamaSchedule = "";
            App.KodeKlinikSchedule = "";
            listJadwalKlnik.ItemsSource = null;
            await Navigation.PushModalAsync(new KlinikSchedulePickerForm());
                

        }

        private async void btnCariJadwalKlinik_Clicked(object sender, EventArgs e)
        {
           
            if ((txtKlinik.Text== "") || (App.KodeKlinikSchedule == ""))
            {
                await PopupNavigation.Instance.PushAsync(new DialogAlertCustom("Warning", "Klinik Belum Dipilih"));
                return;
            }
            getJadwalKlinik();
        }

        private async void getJadwalKlinik()
        {
            ScheduleServices scheduleServices = new ScheduleServices();
            List<KlinikSchedule> listKlinikSchedule = new List<KlinikSchedule>();
            listKlinikSchedule = await scheduleServices.GetKlinikSchedule(App.KodeKlinikSchedule);
            if (listKlinikSchedule.Count == 0)
            {
                await PopupNavigation.Instance.PushAsync(new DialogAlertCustom("Warning", "Jadwal Dokter Klinik Tidak Ditemukan"));
                return;
            }
            foreach (KlinikSchedule klinik in listKlinikSchedule)
            {
             //   klinik.NamaDokter = "Dokter :" + klinik.NamaDokter;
              //  klinik.hari = "Hari : " + klinik.hari;
                klinik.jam_full = klinik.jam_dari + " s/d " + klinik.jam_selesai;
            }
          listJadwalKlnik.ItemsSource = listKlinikSchedule;


        }

        private async void txtDokter_Focused(object sender, FocusEventArgs e)
        {
            txtDokter.Text = "";
           
            App.DokterNamaSchedule = "";
            App.KodeDokterSchedule = "";
            listJadwalDokter.ItemsSource = null;
            await Navigation.PushModalAsync(new DokterSchedulePickerForm());
        }

        private async void btnCariJadwalDokter_Clicked(object sender, EventArgs e)
        {
            if ((txtDokter.Text == "") || (App.KodeDokterSchedule == ""))
            {
                await PopupNavigation.Instance.PushAsync(new DialogAlertCustom("Warning", "Dokter Belum Dipilih"));
                return;
            }
            getJadwaDokter();
        }

        private async void getJadwaDokter()
        {
            ScheduleServices scheduleServices = new ScheduleServices();
            List<DokterSchedule> listDokterSchedule = new List<DokterSchedule>();
            listDokterSchedule = await scheduleServices.GetDokterkSchedule(App.KodeDokterSchedule);
            if (listDokterSchedule.Count == 0)
            {
                await PopupNavigation.Instance.PushAsync(new DialogAlertCustom("Warning", "Jadwal Dokter Tidak Ditemukan"));
                return;
            }
                foreach (DokterSchedule dokter in listDokterSchedule)
            {
                //   klinik.NamaDokter = "Dokter :" + klinik.NamaDokter;
                //  klinik.hari = "Hari : " + klinik.hari;
               dokter.jam_full = dokter.jam_dari + " s/d " + dokter.jam_selesai;
            }
          
            listJadwalDokter.ItemsSource = listDokterSchedule;


        }
    }
}