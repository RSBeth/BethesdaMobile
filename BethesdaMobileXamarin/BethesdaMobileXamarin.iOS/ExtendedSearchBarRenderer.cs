using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using UIKit;
using System.Diagnostics;
using BethesdaMobileXamarin.iOS;

[assembly: ExportRenderer(typeof(SearchBar), typeof(ExtendedSearchBarRenderer))]
namespace BethesdaMobileXamarin.iOS
{
    class ExtendedSearchBarRenderer : SearchBarRenderer
    {

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == "Text")
            {
                Control.ShowsCancelButton = false;
              
            }
        }
    }
}