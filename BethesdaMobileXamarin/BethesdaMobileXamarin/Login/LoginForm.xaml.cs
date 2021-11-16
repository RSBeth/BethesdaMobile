using BethesdaMobileXamarin.History;
using BethesdaMobileXamarin.Login.Model;
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
        private String _source = "";
        public LoginForm(string source)
        {
            InitializeComponent();
            _source = source;

        }
        public LoginForm(string noRM,string source)
        {
            InitializeComponent();
            txtNoRM.Text = noRM;
            _source = source;

        }
        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtNoRM.Text))
            {
                txtNoRM.HasError = true;
                return;
            }
            if (String.IsNullOrEmpty(txtPassword.Text))
            {
                txtPassword.HasError = true;
                return;
            }
            loginServices = new LoginServices();

            Pasien pasien = new Pasien();
            pasien = await loginServices.GetLoginByNoRmServices(txtNoRM.Text);

            if (pasien.response == "ok")
            {

                string tglLahir = DateUtil.ConvertFormatDateTime(pasien.dtgllahir, "dd/MM/yyyy", "ddMMyyyy");

                if ((tglLahir == txtPassword.Text) && (pasien.NoRM == txtNoRM.Text))
                {
                    App.IsUserLoggedIn = true;

                    Preferences.Set("noRM", pasien.NoRM);
                    Preferences.Set("namaPasien", pasien.NamaPasien);
                    Preferences.Set("tglLahir", pasien.dtgllahir);
                    Preferences.Set("alamat", pasien.alamat);
                    if (_source == "Registration")
                    {
                        Navigation.InsertPageBefore(new RegistrationForm(), this);
                    }
                    else
                    {
                        Navigation.InsertPageBefore(new RegistrationHistoryForm(), this);
                    }
                    await Navigation.PopAsync();
                }
                else
                {
                    App.IsUserLoggedIn = false;
                    await PopupNavigation.Instance.PushAsync(new DialogAlertCustom("Gagal Login", "Password Tidak Sesuai"));
                }
            }
            else
            {
                await PopupNavigation.Instance.PushAsync(new DialogAlertCustom("Gagal Login", pasien.deskripsiresponse));
            }
        }
    }
}