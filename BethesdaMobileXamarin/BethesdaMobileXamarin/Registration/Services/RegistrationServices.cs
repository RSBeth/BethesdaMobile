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
    class RegistrationServices
    {

        private HttpClient client;
        private string urlWebServices = (string)Application.Current.Resources["urlWebServices"];
        private string userNameWS = (string)Application.Current.Resources["userNameWS"];
        private string passwordWS = (string)Application.Current.Resources["passwordWS"];


        public RegistrationServices()
        {
            var authData = string.Format("{0}:{1}", userNameWS, passwordWS);
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }

        public async Task<HolidayDate> GetHolidayDate(string tglRegis)
        {
            List<HolidayDate> holidayDateList = new List<HolidayDate>();
            HolidayDate holidayDate = new HolidayDate();
            var uri = new Uri($"{urlWebServices}/getliburnasional/?dTanggal="+tglRegis);
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    holidayDateList = JsonConvert.DeserializeObject<List<HolidayDate>>(content);

                }
                else
                {
                    throw new Exception("Gagal Mengambil Libur Nasional");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            for (int i = 0; i < holidayDateList.Count; i++)
            {
                holidayDate.deskripsiresponse = holidayDateList[i].deskripsiresponse;
                holidayDate.libur = holidayDateList[i].libur;
                holidayDate.response = holidayDateList[i].response;
            }
            return holidayDate;
        }
        public async Task<RegistrationResults> postRegistrationNewPatient(NewRegistration newRegistration)
        {
            RegistrationResults registrationResults = new RegistrationResults();
            var uri = new Uri($"{urlWebServices}/kunjunganbarubyktp/");
            try
            {
                var jsonObj = JsonConvert.SerializeObject(newRegistration);
                var content = new StringContent(jsonObj,
                                                Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri, content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Gagal Membuat Registrasi Pasien Baru");
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
        public async Task<RegistrationResults> postRegistration(Registrations registration)
        {
            RegistrationResults registrationResults = new RegistrationResults();
            var uri = new Uri($"{urlWebServices}/pendaftarantgl/");
            try
            {
                var jsonObj = JsonConvert.SerializeObject(registration);
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
    }

}



