using BethesdaMobileXamarin.Registration.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BethesdaMobileXamarin.Registration
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationNewPatientForm : ContentPage
    {
        private string PhotoPath;
        private NewPatient newPatient1;
       

        public RegistrationNewPatientForm()
        {
            InitializeComponent();
       
        }

        public RegistrationNewPatientForm(NewPatient newPatient)
        {

            InitializeComponent();
            this.newPatient1 = newPatient;
        }

        protected override void OnAppearing()

        {
            base.OnAppearing();

            lblNoKTP.Text = newPatient1.vc_no_ktp;



        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Please pick a photo"
            });

            if (result != null)
            {
                var stream = await result.OpenReadAsync();

                resultImage.Source = ImageSource.FromStream(() => stream);
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                await LoadPhotoAsync(photo);
                Console.WriteLine($"CapturePhotoAsync COMPLETED: {PhotoPath}");
            }
            catch (FeatureNotSupportedException fnsEx)
            {
               
            }
            catch (PermissionException pEx)
            {
               
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }

        async Task LoadPhotoAsync(FileResult photo)
        {
            var result = await MediaPicker.CapturePhotoAsync();

            if (result != null)
            {
                var stream = await result.OpenReadAsync();

                resultImage.Source = ImageSource.FromStream(() => stream);
            }

        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            
            //   await Navigation.PushAsync(new RegistrationNewPatientForm_2(newPatient1));
            await Navigation.PushAsync(new RegistrationNewPatientForm_2(newPatient1));

        }
    }

   
}