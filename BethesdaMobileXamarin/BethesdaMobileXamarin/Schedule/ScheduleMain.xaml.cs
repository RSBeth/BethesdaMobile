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
    {   string _klinikId  ;
        string _klinikName;
        string _dokterId;
        string _dokterName;
        private KlinikSchedule klinikSchedule;
        private DokterSchedule dokterSchedule;
        public ScheduleMain()
        {
            InitializeComponent();
            _klinikId = "";
            _dokterId = "";
            klinikSchedule = new KlinikSchedule();
            dokterSchedule = new DokterSchedule();
        }
        public ScheduleMain(string kode,string nama,string type)
        {
            InitializeComponent();
            klinikSchedule = new KlinikSchedule();
            dokterSchedule = new DokterSchedule();
            _klinikId = "";
            _dokterId = "";
            if (type == "klinik")
            {
                _klinikId = kode;
                _klinikName = nama;
                txtKlinik.Text = nama;
                CurrentPage = TabKlinik;
            }
            else
            {
                _dokterId = kode;
                _dokterName = nama;
                txtDokter.Text = nama;
                CurrentPage = TabDokter;
            }
         
        }
     

        private async void txtKlinik_Focused(object sender, FocusEventArgs e)
        {
              txtKlinik.Text = "";
               await Navigation.PushAsync(new KlinikSchedulePickerForm());

        }

        private async void btnCariJadwalKlinik_Clicked(object sender, EventArgs e)
        {
           
            if ((txtKlinik.Text== "") || (_klinikId == ""))
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
            listKlinikSchedule = await scheduleServices.GetKlinikSchedule(_klinikId);
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
            await Navigation.PushAsync(new DokterSchedulePickerForm());
        }

        private async void btnCariJadwalDokter_Clicked(object sender, EventArgs e)
        {
            if ((txtDokter.Text == "") || (_dokterId == ""))
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
            listDokterSchedule = await scheduleServices.GetDokterkSchedule(_dokterId);
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