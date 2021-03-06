﻿using BethesdaMobileXamarin.Login.Model;
using BethesdaMobileXamarin.Login.Service;
using BethesdaMobileXamarin.Registration;
using BethesdaMobileXamarin.Utility;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BethesdaMobileXamarin.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginForm : ContentPage
    {
  
        private LoginServices loginServices;
     
        public LoginForm()
        {
            InitializeComponent();
          
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            loginServices = new LoginServices();
   
            Pasien pasien = new Pasien();
            pasien = await loginServices.GetLoginByNoRmServices(txtNoRM.Text);
         
            if (pasien.response == "ok")
            {
              
              string  tglLahir = DateUtil.ConvertFormatDateTime(pasien.dtgllahir,"dd/MM/yyyy" ,"ddMMyyyy");

                if ((tglLahir == txtPassword.Text) &&  (pasien.NoRM == txtNoRM.Text))
                    {
                    App.IsUserLoggedIn = true;
                   
                    Preferences.Set("noRM",pasien.NoRM);
                    Preferences.Set("namaPasien", pasien.NamaPasien);
                    Preferences.Set("tglLahir", pasien.dtgllahir);
                    Preferences.Set("alamat", pasien.alamat);
                    Navigation.InsertPageBefore(new RegistrationForm(), this);
                    await Navigation.PopAsync();
                }
                    else
                     {
                    App.IsUserLoggedIn = false;
                    await PopupNavigation.Instance.PushAsync(new DialogAlertCustom("Gagal Login","Password Tidak Sesuai"));
                }
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(new DialogAlertCustom("Gagal Login", pasien.deskripsiresponse));
            }
        }
    }
}