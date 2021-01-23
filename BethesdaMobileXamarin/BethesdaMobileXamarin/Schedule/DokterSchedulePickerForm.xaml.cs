using BethesdaMobileXamarin.Registration.Model;
using BethesdaMobileXamarin.Utility;
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
    public partial class DokterSchedulePickerForm : ContentPage
    {
        private DataBaseUtil dataBaseUtil;
        private List<Dokter> dokters;
     
        public DokterSchedulePickerForm()
        {
            InitializeComponent();
            dataBaseUtil = new DataBaseUtil();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            dokters = (List<Dokter>)dataBaseUtil.GetAllDokter();

            lvDokter.ItemsSource = dokters;

        }
        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            lvDokter.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                lvDokter.ItemsSource = dokters;
            else
                lvDokter.ItemsSource =
                    dokters.Where(i => i.NamaDokter.ToLower().Contains(e.NewTextValue.ToLower()));

            lvDokter.EndRefresh();
        }

        private async void lvDokter_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var itemClicked = (Dokter)e.Item;
            string kodeDokter = itemClicked.NID;
            string namaDokter = itemClicked.NamaDokter;
            await Navigation.PushAsync(new ScheduleMain(kodeDokter, namaDokter,"dokter"));
        }
    }
}