using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BethesdaMobileXamarin.iOS;
using Foundation;
using Rg.Plugins.Popup.IOS.Renderers;
using Rg.Plugins.Popup.Pages;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly: ExportRenderer(typeof(PopupPage), typeof(DialogAlertPageRenderer))]
namespace BethesdaMobileXamarin.iOS
{
    public class DialogAlertPageRenderer : PopupPageRenderer
    {

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            OverrideUserInterfaceStyle = UIUserInterfaceStyle.Light;
        }
    }
}