using BethesdaMobileXamarin.Registration.Model;
using BethesdaMobileXamarin.Utility;
using Rg.Plugins.Popup.Services;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using File = System.IO.File;

namespace BethesdaMobileXamarin.Registration
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationNewPatientForm : ContentPage
    {
        private string PhotoPath;
        private NewPatient newPatient1;
        private DateUtil dateUtil;

        public RegistrationNewPatientForm()
        {
            InitializeComponent();
            setDatePickerValue();
            dateUtil = new DateUtil();
        }
            
        private void setDatePickerValue()
        {
            //dtTglPeriksa.NullableDate = null;

            string maxHari = Preferences.Get("maxHari", "");
            string currentDate = Preferences.Get("currentDate", "");

            DateTime current_date = DateUtil.ConvertStringToDate(currentDate, "dd/MM/yyyy");
            dtTglPeriksa.MinimumDate = current_date;
            dtTglPeriksa.MaximumDate = current_date.AddDays(Double.Parse(maxHari) - 1);

        }
        public RegistrationNewPatientForm(NewPatient newPatient)
        {

            InitializeComponent();
            this.newPatient1 = newPatient;
            dateUtil = new DateUtil();
        }

        protected override void OnAppearing()

        {
            base.OnAppearing();
            setDatePickerValue();
            txtKlinik.Text = App.KlinikNamaNewRegis;
            txtDokter.Text = App.DokterNamaNewRegis;

            lblNoKTP.Text = newPatient1.vc_no_ktp;



        }

        private async void txtKlinik_Focused(object sender, FocusEventArgs e)
        {
            App.KodeKlinikNewRegis = "";
            App.KlinikNamaNewRegis = "";
            txtKlinik.Text = "";
            await Navigation.PushModalAsync(new KlinikPickerForm(App.KodeKlinikNewRegis, App.KlinikNamaNewRegis, App.KodeDokterNewRegis, App.DokterNamaNewRegis, dtTglPeriksa.Date.ToString(), "New"));


        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Silahkan Memilih Foto"
            });

            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                using (MemoryStream memory = new MemoryStream())
                {
                    stream.CopyTo(memory);
                    byte[] bytes = memory.ToArray();
                    resultImage.Source = ImageSource.FromStream(() => new MemoryStream(bytes));
                    string base64 = System.Convert.ToBase64String(bytes);
                    newPatient1.filebtye = base64;
                }
                //resultImage.Source = ImageSource.FromStream(() => stream);
            }
        }
       
        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            var result = await MediaPicker.CapturePhotoAsync();

            if (result != null)
            {
                var stream = await result.OpenReadAsync();

                resultImage.Source = ImageSource.FromStream(() => stream);
            }
            //try
            //{
            //    var photo = await MediaPicker.CapturePhotoAsync();
            //    await LoadPhotoAsync(photo);
            //    Console.WriteLine($"CapturePhotoAsync COMPLETED: {PhotoPath}");
            //}
            //catch (FeatureNotSupportedException fnsEx)
            //{

            //    Console.WriteLine($"CapturePhotoAsync THREW: {fnsEx.Message}");
            //}
            //catch (PermissionException pEx)
            //{
            //    Console.WriteLine($"CapturePhotoAsync THREW: {pEx.Message}");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            //}
        }

        async Task LoadPhotoAsync(FileResult photo)
        {
            var result = await MediaPicker.CapturePhotoAsync();

            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                using (MemoryStream memory = new MemoryStream())
                {
                    stream.CopyTo(memory);
                    byte[] bytes = memory.ToArray();
                    resultImage.Source = ImageSource.FromStream(() => new MemoryStream(bytes));
                    string base64 = System.Convert.ToBase64String(bytes);
                    newPatient1.filebtye = base64;
                }
                //resultImage.Source = ImageSource.FromStream(() => stream);
                
            }

        }


        private void dtTglPeriksa_Focused(object sender, FocusEventArgs e)
        {
            App.KodeDokterNewRegis = "";
            App.DokterNamaNewRegis = "";
            txtDokter.Text = "";
        }

        private async void txtDokter_Focused(object sender, FocusEventArgs e)
        {
            App.KodeDokterNewRegis = "";
            App.DokterNamaNewRegis = "";
            txtDokter.Text = "";

            await Navigation.PushModalAsync(new DokterPickerForm(App.KodeKlinikNewRegis, App.KlinikNamaNewRegis, App.KodeDokterNewRegis, App.DokterNamaNewRegis, dtTglPeriksa.Date.ToString(),"New"));

        }

        private void BoxView_Focused(object sender, FocusEventArgs e)
        {

        }

        private async void MaterialButton_Clicked(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(txtKlinik.Text) || String.IsNullOrEmpty(App.KodeKlinikNewRegis))
            {
                await PopupNavigation.Instance.PushAsync(new DialogAlertCustom("Warning", "Klinik Belum Dipilih!!"));
                return;
            }
            if (String.IsNullOrEmpty(txtDokter.Text) || String.IsNullOrEmpty(App.KodeDokterNewRegis))
            {
                await PopupNavigation.Instance.PushAsync(new DialogAlertCustom("Warning", "Dokter Belum dipilih!!"));
                return;
            }
            if (String.IsNullOrEmpty(newPatient1.filebtye))
            {
                await PopupNavigation.Instance.PushAsync(new DialogAlertCustom("Warning", "Anda Belum Upload Foto KTP"));
                return;
            }

            App.noKTPNewRegis = lblNoKTP.Text;
            App.TglPeriksaNewRegis = dtTglPeriksa.Date.ToShortDateString();
            await Navigation.PushAsync(new RegistrationNewPatientForm_2(newPatient1));
        }
    }

   
}