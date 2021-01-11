using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BethesdaMobileXamarin.Droid;
using BethesdaMobileXamarin.Utility;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using DatePicker = Xamarin.Forms.DatePicker;

[assembly: ExportRenderer(typeof(NullableDatePicker), typeof(NullableDatePickerRenderer))]
namespace BethesdaMobileXamarin.Droid
{
    public class NullableDatePickerRenderer : DatePickerRenderer
    {
        [Obsolete]
        public NullableDatePickerRenderer()
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                TryShowEmptyState();
            }

               
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
