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
    public partial class RegistrationNewPatientForm_2 : ContentPage
    {
        private PatientServices patientServices;
        private List<JenisKelamin> ListJenisKelamins = new List<JenisKelamin>();
        private NewPatient newPatient2;
        public IList<string> JenisKelamin => new List<string>
        {
            "Laki-Laki",
            "Perempuan"
        };

        private List<Agama> ListAgamas;
        private List<Pendidikan> ListPendidikans;
        private List<Pekerjaan> ListPekerjaans;
       

        public RegistrationNewPatientForm_2()
        {
            InitializeComponent();
            patientServices = new PatientServices();
            getJenisKel();
            txtJenisKel.Choices = ListJenisKelamins;



        }

        public RegistrationNewPatientForm_2(NewPatient newPatient)
        {

            InitializeComponent();
            this.newPatient2 = newPatient;
            patientServices = new PatientServices();
            getJenisKel();
            txtJenisKel.Choices = ListJenisKelamins;
            txtNamaPasien.Text = newPatient2.NamaPasien;
            txtTempatLahir.Text = newPatient2.vc_tp_lhr;
            //  await DisplayAlert("Alert", "You have been alerted3", "OK");
        }


        private void getJenisKel()
        {
            ListJenisKelamins.Add(new JenisKelamin("L","Laki-Laki"));
            ListJenisKelamins.Add(new JenisKelamin("P", "Perempuan"));
        }
        protected override async void OnAppearing()

        {
           
            base.OnAppearing();
            using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Load Data"))
            {
                ListAgamas = await patientServices.GetAgamaServices();
                ListPekerjaans = await patientServices.GetPekerjaanServices();
                ListPendidikans = await patientServices.GetPendidikanServices();

                txtAgama.Choices = ListAgamas;
                txtPekerjaan.Choices = ListPekerjaans;
                txtPendidikan.Choices = ListPendidikans;
                if (!(newPatient2.vc_k_agm is null))
                {
                    Agama agamaSelected = ListAgamas.Find(config => config.vc_kode == (newPatient2.vc_k_agm));
                    txtAgama.SelectedChoice = agamaSelected;
                }
                if (!(newPatient2.vc_k_pend is null))
                {
                    Pendidikan pendidikanSelected = ListPendidikans.Find(config => config.vc_kode == (newPatient2.vc_k_pend));
                    txtPendidikan.SelectedChoice = pendidikanSelected;
                }
                if (!(newPatient2.vc_k_pek is null))
                {
                    Pekerjaan pekejeraanSelected = ListPekerjaans.Find(config => config.vc_kode == (newPatient2.vc_k_pek));
                    txtPekerjaan.SelectedChoice = pekejeraanSelected;
                }
            }
           
            
        }

        private async void MaterialButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void MaterialButton_Clicked_1(object sender, EventArgs e)
        {
            newPatient2.NamaPasien = txtNamaPasien.Text;
            newPatient2.vc_tp_lhr = txtTempatLahir.Text;
            await Navigation.PushAsync(new RegistrationNewPatientForm_3(newPatient2));

        }

        private void txtAgama_ChoiceSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var picker = (MaterialTextField) sender;
            Agama agama = (Agama) picker.SelectedChoice;

            newPatient2.vc_k_agm = agama.vc_kode;
        }

        private void txtPendidikan_ChoiceSelected(object sender, SelectedItemChangedEventArgs e)
        {

            var picker = (MaterialTextField)sender;
            Pendidikan pendidikan = (Pendidikan)picker.SelectedChoice;

            newPatient2.vc_k_pend = pendidikan.vc_kode;
        }

        private void txtPekerjaan_ChoiceSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var picker = (MaterialTextField)sender;
            Pekerjaan pekerjaan = (Pekerjaan)picker.SelectedChoice;

            newPatient2.vc_k_pek =pekerjaan.vc_kode;
        }
    }
}