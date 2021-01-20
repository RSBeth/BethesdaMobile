using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Rg.Plugins.Popup.IOS.Renderers;
using UIKit;
using Xamarin.Forms.Platform.iOS;

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