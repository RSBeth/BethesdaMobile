﻿using BethesdaMobileXamarin.Registration.Model;
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
    public partial class DokterPickerForm : ContentPage
    {

        DokterServices dokterServices;
        private DataBaseUtil dataBaseUtil;
        private List<Dokter> dokters;

        private string _kodeKLinik;
        private string _namaKlinik;
        private string _kodeDokter;
        private string _namaDokter;
        private string _tglReg;
        private string _source;
        public DokterPickerForm()
        {
            InitializeComponent();
            dataBaseUtil = new DataBaseUtil();
            dokters = new List<Dokter>();
     



        }

     

        public DokterPickerForm(string kodeKlinik, string namaKlinik, string kodeDokter, string namaDokter,String tglReg,string source)
        {
            InitializeComponent();
            dokterServices = new DokterServices();
            dataBaseUtil = new DataBaseUtil();
            dokters = new List<Dokter>();
            _kodeKLinik = kodeKlinik;
            _namaKlinik = namaKlinik;
            _kodeDokter = kodeDokter;
            _namaDokter = namaDokter;
            _tglReg = tglReg;
            _source = source;
        }
        protected override async void OnAppearing()

        {
            base.OnAppearing();

            if (_kodeKLinik.Length > 0)
            {
                dokters = await dokterServices.GetDokterByKlinik(_kodeKLinik, _tglReg);
            }
            else
            {
                dokters = (List<Dokter>)dataBaseUtil.GetAllDokter();
            }

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
            _kodeDokter = itemClicked.NID;
            _namaDokter = itemClicked.NamaDokter;
            if(_source=="New")
            {
                App.KodeDokterNewRegis = _kodeDokter;
                App.DokterNamaNewRegis = _namaDokter;
            }
            if (_source == "Old")
            {
                App.KodeDokterRegis = _kodeDokter;
                App.DokterNamaRegis = _namaDokter;
            }
            else
            {
                App.KodeDokterAntrian= _kodeDokter;
                App.DokterNamaAntrian = _namaDokter;
            }
       
            if (itemClicked.praktek == "false")
            {
                await PopupNavigation.Instance.PushAsync(new DialogAlertCustom("Informasi", itemClicked.response));

            }
            else
            {
                await Navigation.PopModalAsync();
                //    await Navigation.PushAsync(new RegistrationForm(_kodeKLinik, _namaKlinik, _kodeDokter, _namaDokter,_tglReg));
            }
        }

        private async void btnCLoseModal_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}