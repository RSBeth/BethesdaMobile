using BethesdaMobileXamarin.Registration.Model;
using BethesdaMobileXamarin.Registration.Services;
using BethesdaMobileXamarin.Utility;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BethesdaMobileXamarin.Registration
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KlinikPickerForm : ContentPage
    {

        private string _kodeKLinik;
        private string _namaKlinik;
        private string _kodeDokter;
        private string _namaDokter;
        private string _tglreg;

        private DataBaseUtil dataBaseUtil;
        private List<Klinik> kliniks;
        private KlinikServices klinikServices;
        public KlinikPickerForm()
        {
            InitializeComponent();
            klinikServices = new KlinikServices();
            dataBaseUtil = new DataBaseUtil();
            kliniks =  new List<Klinik>();
        }

        public KlinikPickerForm(string kodeKlinik, string namaKlinik, string kodeDokter, string namaDokter,String tglReg)
        {
            InitializeComponent();
            klinikServices = new KlinikServices();
            dataBaseUtil = new DataBaseUtil();
            kliniks = new List<Klinik>();
            _kodeKLinik = kodeKlinik;
            _namaKlinik = namaKlinik;
            _kodeDokter = kodeDokter;
            _namaDokter = namaDokter;
            _tglreg = tglReg;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if(_kodeDokter.Length > 0)
            {
                kliniks = await  klinikServices.GetKlinikByDokter(_kodeDokter, _tglreg);
            }
            else
            {
                kliniks = (List<Klinik>)dataBaseUtil.GetAllKlinik();
            }
            
            lvKlinik.ItemsSource = kliniks;
           
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            lvKlinik.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                lvKlinik.ItemsSource = kliniks;
            else
                lvKlinik.ItemsSource =
                    kliniks.Where(i => i.NamaKlinik.ToLower().Contains(e.NewTextValue.ToLower()));

            lvKlinik.EndRefresh();
        }

        private async void lvKlinik_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var itemClicked = (Klinik)e.Item;
            _kodeKLinik = itemClicked.KodeKlinik;
            _namaKlinik = itemClicked.NamaKlinik;
            if (itemClicked.praktek == "false")
            {
                await PopupNavigation.Instance.PushAsync(new DialogAlertCustom("Informasi", itemClicked.response));

            }
            else
            {
                await Navigation.PushModalAsync(new RegistrationForm(_kodeKLinik, _namaKlinik, _kodeDokter, _namaDokter));
            }
           
        }
    }
}