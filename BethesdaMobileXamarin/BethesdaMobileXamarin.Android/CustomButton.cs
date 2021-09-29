using BethesdaMobileXamarin.Droid;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Button), typeof(CustomButton))]

namespace BethesdaMobileXamarin.Droid
{
	public class CustomButton : ButtonRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{

			}
		}
	}
}
