using BethesdaMobileXamarin.Registration.Model;
using BethesdaMobileXamarin.Registration.Services;
using BethesdaMobileXamarin.Utility;
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
    public partial class RegistrationNewPatientForm_2 : ContentPage
    {
        private PatientServices patientServices;
        private List<JenisKelamin> ListJenisKelamins = new List<JenisKelamin>();
        private NewPatient newPatient2;
       

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
            string currentDate = Preferences.Get("currentDate", "");

            DateTime current_date = DateUtil.ConvertStringToDate(currentDate, "dd/MM/yyyy");
            this.newPatient2 = newPatient;
            patientServices = new PatientServices();
            getJenisKel();
            txtJenisKel.Choices = ListJenisKelamins;
            txtNamaPasien.Text = newPatient2.NamaPasien;
            txtTempatLahir.Text = newPatient2.vc_tp_lhr;
            dtTglLahir.Date = current_date;
            setUmur();
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
            setStatusError(false);
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
                if (!(newPatient2.VC_jenis_k is null))
                {
                    JenisKelamin jenisKelaminSelected = ListJenisKelamins.Find(config => config.vc_kodeKelamin == (newPatient2.VC_jenis_k));
                    txtJenisKel.SelectedChoice =  jenisKelaminSelected;
                }
            }
           
            
        }

        private async void MaterialButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void MaterialButton_Clicked_1(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(txtNamaPasien.Text))
            {
                txtNamaPasien.HasError = true;
                return;
            }
            if (String.IsNullOrEmpty(txtJenisKel.Text))
            {
                txtJenisKel.HasError = true;
                return;
            }
            if (String.IsNullOrEmpty(txtTempatLahir.Text))
            {
                txtTempatLahir.HasError = true;
                return;
            }
            if (String.IsNullOrEmpty(dtTglLahir.Date.Value.ToShortDateString()))
            {
                dtTglLahir.HasError = true;
                return;
            }
          

            if (String.IsNullOrEmpty(txtAgama.Text))
            {
                txtAgama.HasError = true;
                return;
            }
         
            if (String.IsNullOrEmpty(txtPendidikan.Text))
            {
                txtPendidikan.HasError = true;
                return;
            }
            if (String.IsNullOrEmpty(txtPekerjaan.Text))
            {
                txtPekerjaan.HasError = true;
                return;
            }

            newPatient2.NamaPasien = txtNamaPasien.Text;
            newPatient2.vc_tp_lhr = txtTempatLahir.Text;
          
            await Navigation.PushAsync(new RegistrationNewPatientForm_3(newPatient2));

        }

        private void setStatusError(Boolean status)
        {
            txtPekerjaan.HasError = status;
            txtPendidikan.HasError = status;
            txtAgama.HasError = status;
            txtTempatLahir.HasError = status;
            txtJenisKel.HasError = status;
            txtNamaPasien.HasError = status;
            dtTglLahir.HasError = status;
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

        private void MaterialDateField_DateChanged(object sender, XF.Material.Forms.UI.Internals.NullableDateChangedEventArgs e)
        {
            string currentDate = Preferences.Get("currentDate", "");

            DateTime current_date = DateUtil.ConvertStringToDate(currentDate, "dd/MM/yyyy");
            if(dtTglLahir.Date > current_date)
            {
                dtTglLahir.Date = current_date;
            }
            setUmur();


        }
        private void setUmur()
        {
            Age age = CalculateAge(dtTglLahir.Date.Value);
            newPatient2.IN_umurth = age.year;
            newPatient2.IN_umurbl = age.month;
            newPatient2.IN_umurhr = age.day;
            newPatient2.dtgllahir = dtTglLahir.Date.Value.ToString();
        }

        private Age CalculateAge(DateTime Dob)
        {
            Age age = new Age();
            DateTime Now = DateTime.Now;
            int Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
            DateTime PastYearDate = Dob.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    Months = i - 1;
                    break;
                }
            }
            int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
            int Hours = Now.Subtract(PastYearDate).Hours;
            int Minutes = Now.Subtract(PastYearDate).Minutes;
            int Seconds = Now.Subtract(PastYearDate).Seconds;
            age.day = Days;
            age.month = Months;
            age.year = Years;
            return age;
           
        }

        private void txtJenisKel_ChoiceSelected(object sender, SelectedItemChangedEventArgs e)
        {

            var picker = (MaterialTextField)sender;
            JenisKelamin jenisKelamin = (JenisKelamin)picker.SelectedChoice;
            newPatient2.VC_jenis_k = jenisKelamin.vc_kodeKelamin;
        }
    }
}