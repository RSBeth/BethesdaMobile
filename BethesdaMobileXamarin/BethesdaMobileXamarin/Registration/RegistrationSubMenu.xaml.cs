using BethesdaMobileXamarin.Main;
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
    public partial class RegistrationSubMenu : ContentPage
    {
        public RegistrationSubMenu()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (App.IsUserLoggedIn == true)
            {

                await Navigation.PushAsync(new RegistrationForm());
            }
            else
            {
                       await Navigation.PushAsync(new LoginForm("Registration"));
            }
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CheckKTPForm());
        }
    }
}