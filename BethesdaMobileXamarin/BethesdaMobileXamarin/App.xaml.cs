using BethesdaMobileXamarin.Main;

using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BethesdaMobileXamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            XF.Material.Forms.Material.Init(this);
            MainPage = new NavigationPage(new IntroPage());


        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
        public static string AppTheme
        {
            get; set;
        }
        public static bool IsUserLoggedIn { get; internal set; }
        public static string KodeKlinikRegis { get; internal set; }
        public static string KlinikNamaRegis { get; internal set; }

        public static string KodeDokterRegis { get; internal set; }
        public static string DokterNamaRegis { get; internal set; }

        public static string KodeKlinikSchedule{ get; internal set; }
        public static string KlinikNamaSchedule { get; internal set; }

        public static string KodeDokterSchedule { get; internal set; }
        public static string DokterNamaSchedule { get; internal set; }

    }
}
