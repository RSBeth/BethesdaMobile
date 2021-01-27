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

namespace BethesdaMobileXamarin.Schedule
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KlinikSchedulePickerForm : ContentPage
    {


        private DataBaseUtil dataBaseUtil;
        private List<Klinik> kliniks;
     
        public KlinikSchedulePickerForm()
        {
            InitializeComponent();
         
            dataBaseUtil = new DataBaseUtil();
            kliniks =  new List<Klinik>();
        }

       

        protected override void OnAppearing()
        {
            base.OnAppearing();
            kliniks = (List<Klinik>)dataBaseUtil.GetAllKlinik();

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
            string kodeKLinik = itemClicked.KodeKlinik;
            string namaKlinik = itemClicked.NamaKlinik;
            App.KodeKlinikSchedule = kodeKLinik;
            App.KlinikNamaSchedule = namaKlinik;
            await Navigation.PopModalAsync();
        }

        private async void btnCLoseModal_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}