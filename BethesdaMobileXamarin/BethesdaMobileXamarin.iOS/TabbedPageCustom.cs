using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BethesdaMobileXamarin.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(TabbedPageCustom))]
namespace BethesdaMobileXamarin.iOS
{
    class TabbedPageCustom : TabbedRenderer
    {

        public TabbedPageCustom()
        {
            TabBar.TintColor = UIKit.UIColor.White;
            TabBar.BarTintColor = UIKit.UIColor.White;
            TabBar.BackgroundColor = UIKit.UIColor.Red;
        }
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            // Set Text Font for unselected tab states
            UITextAttributes normalTextAttributes = new UITextAttributes();
            normalTextAttributes.Font = UIFont.FromName("ChalkboardSE-Light", 20.0F); // unselected
         //   normalTextAttributes.TextColor = UIKit.UIColor.Blue;

            UITabBarItem.Appearance.SetTitleTextAttributes(normalTextAttributes, UIControlState.Normal);
        }
        public override UIViewController SelectedViewController
        {
            get
            {
                UITextAttributes selectedTextAttributes = new UITextAttributes();
                selectedTextAttributes.Font = UIFont.FromName("ChalkboardSE-Bold", 20.0F); // SELECTED
                if (base.SelectedViewController != null)
                {
                    base.SelectedViewController.TabBarItem.SetTitleTextAttributes(selectedTextAttributes, UIControlState.Normal);
                }
                return base.SelectedViewController;
            }
            set
            {
                base.SelectedViewController = value;

                foreach (UIViewController viewController in base.ViewControllers)
                {
                    UITextAttributes normalTextAttributes = new UITextAttributes();
                    normalTextAttributes.Font = UIFont.FromName("ChalkboardSE-Light",20.0F); // unselected
               //d     normalTextAttributes.TextColor = UIKit.UIColor.Red;
                    viewController.TabBarItem.SetTitleTextAttributes(normalTextAttributes, UIControlState.Normal);
                }
            }
        }

    }
}
