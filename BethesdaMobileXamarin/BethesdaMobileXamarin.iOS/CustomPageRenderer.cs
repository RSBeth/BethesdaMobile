using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BethesdaMobileXamarin.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly: ExportRenderer(typeof(ContentPage), typeof(CustomPageRenderer))]
namespace BethesdaMobileXamarin.iOS
{
    public class CustomPageRenderer : PageRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            OverrideUserInterfaceStyle = UIUserInterfaceStyle.Light;
        }
    }
}