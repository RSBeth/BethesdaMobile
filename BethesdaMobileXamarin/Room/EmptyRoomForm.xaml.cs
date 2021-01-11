using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BethesdaMobileXamarin.Room
{   
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmptyRoomForm : ContentPage
    {
        public EmptyRoomForm()
        {
            InitializeComponent();
          //   FlView.ItemsSource = 
        }
    }
}