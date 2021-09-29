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
        public RegistrationNewPatientForm_3(NewPatient newPatient)
        {
            InitializeComponent();
            patientServices = new PatientServices();
            this.newPatient3 = newPatient;
        }

        public RegistrationNewPatientForm_3()
        {
            InitializeComponent();
            patientServices = new PatientServices();

        }
        protected override async void OnAppearing()

        {
            base.OnAppearing();
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
            
        }

        private async void MaterialButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void MaterialButton_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationNewPatientForm_4(newPatient3));
        }

        private void txtNegara_ChoiceSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var picker = (MaterialTextField)sender;
            Negara negara = (Negara)picker.SelectedChoice;
            newPatient3.vc_k_negara = negara.vc_k_negara;
        }

        private void txtStatusKawin_ChoiceSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}