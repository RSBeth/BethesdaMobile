using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using BethesdaMobileXamarin.iOS;
using BethesdaMobileXamarin.Utility;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NullableDatePicker), typeof(NullableDatePickerRenderer))]
namespace BethesdaMobileXamarin.iOS
{
   public class NullableDatePickerRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            TryShowEmptyState();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == NullableDatePicker.NullableDateProperty.PropertyName ||
                e.PropertyName == NullableDatePicker.EmptyStateTextProperty.PropertyName)
            {
                TryShowEmptyState();
            }
        }

        private void TryShowEmptyState()
        {
            var el = Element as NullableDatePicker;
            if (el != null)
            {
                if (el.NullableDate == null)
                {
                    Control.Text = el.EmptyStateText;
                }
            }
        }
    }
}
