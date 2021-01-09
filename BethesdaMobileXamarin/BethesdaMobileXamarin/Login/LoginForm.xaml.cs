using BethesdaMobileXamarin.Login.Model;
using BethesdaMobileXamarin.Login.Service;
using BethesdaMobileXamarin.Registration;
using BethesdaMobileXamarin.Utility;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    await Navigation.PushModalAsync(new RegistrationForm());
                }
                    else
                     {
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