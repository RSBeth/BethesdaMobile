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
        private string _source;

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

        public KlinikPickerForm(string kodeKlinik, string namaKlinik, string kodeDokter, string namaDokter,String tglReg,String source)
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
            _source = source;
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

            if (_source == "New") // jika pendaftaran pasien baru balikannya ini
            {
                App.KodeKlinikNewRegis = _kodeKLinik;
                App.KlinikNamaNewRegis = _namaKlinik;
            }
            if (_source == "Old") 
            {
                App.KodeKlinikRegis = _kodeKLinik;
                App.KlinikNamaRegis = _namaKlinik;
            }
            else 
            {
                App.KodeKlinikAntrian = _kodeKLinik;
                App.KlinikNamaAntrian = _namaKlinik;
            }
           
            if (itemClicked.praktek == "false")
            {
                await PopupNavigation.Instance.PushAsync(new DialogAlertCustom("Informasi", itemClicked.response));

            }
            else
            {
                // await Navigation.PopAsync();
                
                
                await Navigation.PopModalAsync();
                //  await Navigation.PushAsync(new RegistrationForm(_kodeKLinik, _namaKlinik, _kodeDokter, _namaDokter,_tglreg));
            }
           
        }

        private async void btnCLoseModal_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void btnCLoseModal_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}