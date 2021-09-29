using BethesdaMobileXamarin.Registration.Model;
using BethesdaMobileXamarin.Registration.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI;
using XF.Material.Forms.UI.Dialogs;

namespace BethesdaMobileXamarin.Registration
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationNewPatientForm_4 : ContentPage
    {
        private PatientServices patientServices;
        private List<Propinsi> ListPropinsis;
        private List<Kabupaten> ListKabupatens;
        private List<Kecamatan> ListKecamatans;
        private NewPatient newPatient4;
        public RegistrationNewPatientForm_4()
        {
            InitializeComponent();
            patientServices = new PatientServices();
        }

        public RegistrationNewPatientForm_4(NewPatient newPatient)
        {
            InitializeComponent();
            patientServices = new PatientServices();
            this.newPatient4 = newPatient;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Load Data Provinsi"))
            {
                ListPropinsis = await patientServices.GetPropinsiServices();
                txtPropinsi.Choices = ListPropinsis;
            }

            if (!(newPatient4.vc_k_prop is null))
            {
                Propinsi propinsiSelected = ListPropinsis.Find(config => config.vc_kode == newPatient4.vc_k_prop);
                txtPropinsi.SelectedChoice = propinsiSelected;

            }
            if (!(newPatient4.vc_k_kota is null))
            {
                Kabupaten kabupatenSelected = ListKabupatens.Find(config => config.vc_kode == newPatient4.vc_k_kota);
                txtKabupaten.SelectedChoice = kabupatenSelected;

            }
            //if (!(newPatient4.vc_kode_camat is null))
            //{
            //    Kecamatan kecamatanSelected = ListKecamatans.Find(config => config.vc_kode == newPatient4.vc_kode_camat);
            //    txtKecamatan.SelectedChoice = kecamatanSelected;
            //}
        }
        private async void txtPropinsi_ChoiceSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var picker = (MaterialTextField)sender;
            Propinsi propinsi = (Propinsi)picker.SelectedChoice;

            newPatient4.vc_k_prop = propinsi.vc_kode;
            newPatient4.VC_propinsi = propinsi.vc_propinsi;
            using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Load Data Kabupaten"))
            {
                ListKabupatens = await patientServices.GetKabupantenByIdServices(propinsi.vc_kode);
                txtKabupaten.Choices = ListKabupatens;
            }
            
        }

        private async void txtKabupaten_ChoiceSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var picker = (MaterialTextField)sender;
            Kabupaten kabupaten = (Kabupaten)picker.SelectedChoice;

            newPatient4.vc_k_kota = kabupaten.vc_kode;
            newPatient4.vc_kota = kabupaten.vc_kota;
            using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Load Data Kecamatan"))
            {
                ListKecamatans = await patientServices.GetKecamatanByIdServices(kabupaten.vc_kode);
                txtKecamatan.Choices = ListKecamatans;
            }
          
        }

        private void txtKecamatan_ChoiceSelected(object sender, SelectedItemChangedEventArgs e)
        {

            var picker = (MaterialTextField)sender;
            Kecamatan  kecamatan = (Kecamatan)picker.SelectedChoice;
            newPatient4.vc_kode_camat = kecamatan.vc_kode;
            newPatient4.vc_kecamatan = kecamatan.vc_kecamatan;
        }

        private async void MaterialButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void MaterialButton_Clicked_1(object sender, EventArgs e)
        {
           
        }
    }
}