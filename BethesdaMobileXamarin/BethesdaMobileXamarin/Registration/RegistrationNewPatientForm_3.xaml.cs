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
    public partial class RegistrationNewPatientForm_3 : ContentPage
    {
        private PatientServices patientServices;
        private List<Negara> ListNegaras;
        private NewPatient newPatient3;
        private List<Kawin> ListKawins;
        private List<String> listGolonganDarah = new List<String>();
        public RegistrationNewPatientForm_3(NewPatient newPatient)
        {
            InitializeComponent();
            patientServices = new PatientServices();
            this.newPatient3 = newPatient;
            setGolonganDarah();
            
        }

        private void setGolonganDarah()
        {
            listGolonganDarah.Add("A");
            listGolonganDarah.Add("B");
            listGolonganDarah.Add("AB");
            listGolonganDarah.Add("O");
            txtGolDarah.Choices = listGolonganDarah;
        }

       


        public RegistrationNewPatientForm_3()
        {
            InitializeComponent();
            patientServices = new PatientServices();

        }
        protected override async void OnAppearing()

        {
            base.OnAppearing();
            setStatusError(false);
            using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Load Data Negara"))
            {
                ListNegaras = await patientServices.GetNegaraServices();
                txtNegara.Choices = ListNegaras;
                if (!(newPatient3.vc_k_negara is null))
                {
                    Negara negaraSelected = ListNegaras.Find(config => config.vc_k_negara == newPatient3.vc_k_negara);
                    txtNegara.SelectedChoice = negaraSelected;

                }
                else
                {
                    Negara negaraSelected = ListNegaras.Find(config => config.vc_k_negara == "062");
                    txtNegara.SelectedChoice = negaraSelected;
                }
            }

            using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Load Data Kawin"))
            {
                ListKawins = await patientServices.GetKawinServices();
                txtStatusKawin.Choices = ListKawins;
                if (!(newPatient3.vc_k_status is null))
                {
                    Kawin kawinSelected = ListKawins.Find(config => config.vc_stkawin == newPatient3.vc_k_status);
                    txtStatusKawin.SelectedChoice = kawinSelected;

                }

            }
            if (!(newPatient3.VC_gol_d is null))
            {
                txtGolDarah.Text = newPatient3.VC_gol_d;
            }  
            if (!(newPatient3.vc_email is null))
            {
                txtEmail.Text = newPatient3.vc_email;
            }
               
            if (!(newPatient3.vc_telpon is null))
            {
                txtNoTelp.Text = newPatient3.vc_telpon;
            }
            if (!(newPatient3.vc_BB is null))
            {
                txtBerat.Text = newPatient3.vc_BB;
            }
            if (!(newPatient3.vc_TB is null))
            {
                txtTinggi.Text = newPatient3.vc_TB;
            }
        }

        private async void MaterialButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void MaterialButton_Clicked_1(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtNegara.Text))
            {
                txtNegara.HasError = true;
                return;
            }
            if (String.IsNullOrEmpty(txtStatusKawin.Text))
            {
                txtStatusKawin.HasError = true;
                return;
            }
            if (String.IsNullOrEmpty(txtNoTelp.Text))
            {
                txtNoTelp.HasError = true;
                return;
            }
            if (String.IsNullOrEmpty(txtGolDarah.Text))
            {
                txtGolDarah.HasError = true;
                return;
            }
            if (String.IsNullOrEmpty(txtBerat.Text))
            {
                txtBerat.HasError = true;
                return;
            }
            newPatient3.vc_email = txtEmail.Text;
            newPatient3.vc_BB = txtBerat.Text;
            newPatient3.vc_TB = txtTinggi.Text;
            newPatient3.VC_gol_d = txtGolDarah.Text;
            newPatient3.vc_telpon = txtNoTelp.Text;

            await Navigation.PushAsync(new RegistrationNewPatientForm_4(newPatient3));
        }
        private void setStatusError(Boolean status)
        {
            txtBerat.HasError = status;
            txtGolDarah.HasError = status;
            txtNoTelp.HasError = status;
            txtStatusKawin.HasError = status;
            txtNegara.HasError = status;
            
        }

        private void txtNegara_ChoiceSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var picker = (MaterialTextField)sender;
            Negara negara = (Negara)picker.SelectedChoice;
            newPatient3.vc_k_negara = negara.vc_k_negara;
        }

        private void txtStatusKawin_ChoiceSelected(object sender, SelectedItemChangedEventArgs e)
        {
             var picker = (MaterialTextField)sender;
              Kawin kawin = (Kawin)picker.SelectedChoice;
            newPatient3.vc_k_status = kawin.vc_stkawin;
        }
      
    }
}