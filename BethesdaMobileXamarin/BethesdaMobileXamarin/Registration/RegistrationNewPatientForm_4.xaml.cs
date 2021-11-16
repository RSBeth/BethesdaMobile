using BethesdaMobileXamarin.Registration.Model;
using BethesdaMobileXamarin.Registration.Services;
using BethesdaMobileXamarin.Utility;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
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
        private NewPatientResults newPatientResults;
        private RegistrationResults registrationResults;
        private NewRegistration newRegistration;
        private RegistrationServices registrationServices;
        public RegistrationNewPatientForm_4()
        {
            InitializeComponent();
            patientServices = new PatientServices();
            registrationServices = new RegistrationServices();
        }

        public RegistrationNewPatientForm_4(NewPatient newPatient)
        {
            InitializeComponent();
            patientServices = new PatientServices();
            registrationServices = new RegistrationServices();
            this.newPatient4 = newPatient;
            newRegistration = new NewRegistration();

            newRegistration.NoKtp = App.noKTPNewRegis;
            newRegistration.tglreg = App.TglPeriksaNewRegis;
            newRegistration.kodedokter = App.KodeDokterNewRegis;
            newRegistration.kodeKLinik = App.KodeKlinikNewRegis;

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            setStatusError(false);
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
            if (!(newPatient4.vc_kode_camat is null))
            {
                Kecamatan kecamatanSelected = ListKecamatans.Find(config => config.vc_kode == newPatient4.vc_kode_camat);
                txtKecamatan.SelectedChoice= kecamatanSelected;

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
        {   if(String.IsNullOrEmpty(txtPropinsi.Text))
            {
                txtPropinsi.HasError = true;
                return;
            }
            if (String.IsNullOrEmpty(txtKabupaten.Text))
            {
                txtKabupaten.HasError = true;
                return;
            }
            if (String.IsNullOrEmpty(txtKecamatan.Text))
            {
                txtKecamatan.HasError = true;
                return;
            }
            if (String.IsNullOrEmpty(txtKelurahan.Text))
            {
                txtKelurahan.HasError = true;
                return;
            }
            if (String.IsNullOrEmpty(txtAlamat.Text))
            {
                txtAlamat.HasError = true;
                return;
            }
            newPatient4.Alamat = txtAlamat.Text;
            newPatient4.vc_kelurahan = txtKelurahan.Text;
            newPatient4.vc_nik = "";
            newPatient4.vc_turis = "T";
            newPatient4.vc_kd_cacat = "00";
            if(String.IsNullOrEmpty(txtNoBpjs.Text))
            {
                newPatient4.vc_no_peserta_bpjs = "";
            }
            else
            {
                newPatient4.vc_no_peserta_bpjs = txtNoBpjs.Text;
            }
            prosesSimpan();
        }
        private void setStatusError(Boolean status)
        {
            txtKabupaten.HasError = status;
            txtKelurahan.HasError = status;
            txtKelurahan.HasError = status;
            txtPropinsi.HasError = status;
            txtAlamat.HasError = status;
        }
        private async void prosesSimpan()
        {

            using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Proses Simpan Pasien"))
            {
                try
                {
                    newPatientResults = new NewPatientResults();
                    newPatientResults = await patientServices.postNewPatient(newPatient4);
                }
                catch (Exception ex)
                {
                    await PopupNavigation.Instance.PushAsync(new DialogAlertCustom("Error", ex.Message));
                }

                if (!string.IsNullOrEmpty(newPatientResults.response))
                {
                    if (newPatientResults.response.ToString() == "gagal")
                    {
                        await PopupNavigation.Instance.PushAsync(new DialogAlertCustom("Warning", newPatientResults.deskripsiresponse));
                    }
                    else
                    {
                        registrationResults = new RegistrationResults();
                        registrationResults = await registrationServices.postRegistrationNewPatient(newRegistration);
                        if (registrationResults.response.ToString() ==("ok"))
                        {
                            await Navigation.PushAsync(new RegistrationSuccessNewPatientForm(registrationResults,newPatientResults));
                            Navigation.RemovePage(this);
                        }
                        else
                        {
                            await PopupNavigation.Instance.PushAsync(new DialogAlertCustom("Peringatan", registrationResults.deskripsiresponse));
                        }

                    }
                }
                else
                {
                    await PopupNavigation.Instance.PushAsync(new DialogAlertCustom("Warning", "Maaf Terjadi Kegagalan saat simpan, Mohon Dicoba Kembali"));
                }
            }
           
            
        }
    }
}