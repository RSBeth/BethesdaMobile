using BethesdaMobileXamarin.Main;
using BethesdaMobileXamarin.Registration.Model;
using BethesdaMobileXamarin.Registration.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace BethesdaMobileXamarin.Registration
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckKTPForm : ContentPage
    {
        PatientCheckKTP patientCheckKTP;
        PatientServices patientServices;
        public CheckKTPForm()
        {
            InitializeComponent();
            patientServices = new PatientServices();
        }

        private async void btnCheckKTP_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtNOKTP.Text))
            {
                txtNOKTP.HasError = true;
                return;
            }
            if (txtNOKTP.Text.Length <16)
            {
                txtNOKTP.HasError = true;
                return;
            }
            using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Cek No KTP Pasien"))
                {
                    patientCheckKTP = new PatientCheckKTP();
                    patientCheckKTP = await patientServices.getPasienByKTP(txtNOKTP.Text);
                    if (patientCheckKTP.response.Equals("gagal"))
                    {
                        NewPatient newPatient = new NewPatient();
                        newPatient.vc_no_ktp = txtNOKTP.Text;
                        await Navigation.PushAsync(new RegistrationNewPatientForm(newPatient));
                    }
                else
                {
                    StringBuilder sb = new StringBuilder();

                    for (int i = 0; i <patientCheckKTP.NamaPasien.Length; i++)
                    {
                        if (i == 0 || i == 1 || i == patientCheckKTP.NamaPasien.Length - 1 || i ==  patientCheckKTP.NamaPasien.Length - 2)
                        {
                            sb.Append(patientCheckKTP.NamaPasien.ToString()[i]);

                        }
                        else
                        {
                            sb.Append("*");

                        }

                    }
                    string textKonfirmasi = "Pasien Dengan No KTP " + txtNOKTP.Text + " Sudah ada , Dengan No RM : " + patientCheckKTP.NoRM.ToString() + " & Nama : " + sb.ToString() + " Silahkan Melanjutkan Dengan Login Menggunakan NO RM  & Tgl Lahir Anda";
                    bool answer = await DisplayAlert("Informasi", textKonfirmasi, "Yes", "Cancel");
                    if (answer)
                    {
                        await Navigation.PushAsync(new LoginForm(patientCheckKTP.NoRM,"Registration"));
                    }
                }
                }
            }
        }
    }
