using BethesdaMobileXamarin.Registration.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace BethesdaMobileXamarin.Registration.Services
{
    class PatientServices
    {


        private HttpClient client;
        private string urlWebServices = (string)Application.Current.Resources["urlWebServices"];
        private string userNameWS = (string)Application.Current.Resources["userNameWS"];
        private string passwordWS = (string)Application.Current.Resources["passwordWS"];
        public PatientServices()
        {
            var authData = string.Format("{0}:{1}", userNameWS, passwordWS);
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }

        public async Task<List<Agama>> GetAgamaServices()
        {
            List<Agama> listAgama = new List<Agama>();
            var uri = new Uri($"{urlWebServices}/GetAgama/");
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    listAgama = JsonConvert.DeserializeObject<List<Agama>>(content);
                   
                }
                else
                {
                    throw new Exception("Gagal Mengambil Agama");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listAgama;
        }
        public async Task<List<Pendidikan>> GetPendidikanServices()
        {
            List<Pendidikan> listPendidikan = new List<Pendidikan>();
            var uri = new Uri($"{urlWebServices}/GetPendidikan/");
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    listPendidikan = JsonConvert.DeserializeObject<List<Pendidikan>>(content);

                }
                else
                {
                    throw new Exception("Gagal Mengambil Data Pendidikan");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listPendidikan;
        }


        public async Task<List<Pekerjaan>> GetPekerjaanServices()
        {
            List<Pekerjaan> listPekerjaaan = new List<Pekerjaan> ();
            var uri = new Uri($"{urlWebServices}/GetPekerjaan/");
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    listPekerjaaan = JsonConvert.DeserializeObject<List<Pekerjaan>>(content);

                }
                else
                {
                    throw new Exception("Gagal Mengambil Data Pekerjaan");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listPekerjaaan;
        }
        public async Task<List<Kawin>> GetKawinServices()
        {
            List<Kawin> listKawin = new List<Kawin>();
            var uri = new Uri($"{urlWebServices}/Getkawin/");
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    listKawin = JsonConvert.DeserializeObject<List<Kawin>>(content);

                }
                else
                {
                    throw new Exception("Gagal Mengambil Data Kawin");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listKawin;
        }


        public async Task<List<Negara>> GetNegaraServices()
        {
            List<Negara> listNegara = new List<Negara>();
            var uri = new Uri($"{urlWebServices}/GetNegara/");
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    listNegara = JsonConvert.DeserializeObject<List<Negara>>(content);

                }
                else
                {
                    throw new Exception("Gagal Mengambil Data Negara");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listNegara;
        }

        public async Task<List<Propinsi>> GetPropinsiServices()
        {
            List<Propinsi> listPropinsi = new List<Propinsi>();
            var uri = new Uri($"{urlWebServices}/GetPropinsi/");
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    listPropinsi = JsonConvert.DeserializeObject<List<Propinsi>>(content);

                }
                else
                {
                    throw new Exception("Gagal Mengambil Data Propinsi");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listPropinsi;
        }


        public async Task<List<Kabupaten>> GetKabupantenByIdServices(string kodePropinsi)
        {
            List<Kabupaten> listKabupaten = new List<Kabupaten>();
            var uri = new Uri($"{urlWebServices}/GetKabupaten/"+kodePropinsi);
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    listKabupaten = JsonConvert.DeserializeObject<List<Kabupaten>>(content);

                }
                else
                {
                    throw new Exception("Gagal Mengambil Data Kabupaten");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listKabupaten;
        }
        public async Task<RegistrationResults> postRegistrationNewPatient(NewRegistration newRegistration)
        {
            RegistrationResults registrationResults = new RegistrationResults();
            var uri = new Uri($"{urlWebServices}/pendaftarantgl/");
            try
            {
                var jsonObj = JsonConvert.SerializeObject(newRegistration);
                var content = new StringContent(jsonObj,
                                                Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri, content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Gagal Registrasi Online");
                }
                else
                {
                    var result = await response.Content.ReadAsStringAsync();
                    registrationResults = JsonConvert.DeserializeObject<RegistrationResults>(result);
                    return registrationResults;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Kecamatan>> GetKecamatanByIdServices(string kodeKabupaten)
        {
            List<Kecamatan> listKecamatan = new List<Kecamatan>();
            var uri = new Uri($"{urlWebServices}/GetKecamatan/" + kodeKabupaten);
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    listKecamatan = JsonConvert.DeserializeObject<List<Kecamatan>>(content);

                }
                else
                {
                    throw new Exception("Gagal Mengambil Data Kecamatan");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listKecamatan;
        }

        public async Task<NewPatientResults> postNewPatient(NewPatient newPatient)
        {
            NewPatientResults newPatientResults = new NewPatientResults();
            var uri = new Uri($"{urlWebServices}/pasienbaruybyktp/");
            try
            {
                var jsonObj = JsonConvert.SerializeObject(newPatient);
                var content = new StringContent(jsonObj,
                                                Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri, content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Gagal Proses Pembuatan Pasien Baru");
                }
                else
                {
                    var result = await response.Content.ReadAsStringAsync();
                    newPatientResults = JsonConvert.DeserializeObject<NewPatientResults>(result);
                    return newPatientResults;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PatientCheckKTP> getPasienByKTP(String noKTP)
        {
            PatientCheckKTP patientCheckKtp = new PatientCheckKTP();
            var uri = new Uri($"{urlWebServices}/getpasienbyktp/" + noKTP);
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    patientCheckKtp = JsonConvert.DeserializeObject<PatientCheckKTP>(content);

                }
                else
                {
                    throw new Exception("Gagal Mengambil KTP Pasien");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return patientCheckKtp;
        }

    }
}
