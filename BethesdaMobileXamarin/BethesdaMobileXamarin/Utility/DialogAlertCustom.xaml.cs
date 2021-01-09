using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BethesdaMobileXamarin.Utility
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DialogAlertCustom
    { 
        public DialogAlertCustom()
        {
            InitializeComponent();
        }
        public DialogAlertCustom(string title,string desc)
        {
            InitializeComponent();
            lblTitle.Text = title;
            lblDesc.Text = desc;
        }

        private async void btnOK_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}