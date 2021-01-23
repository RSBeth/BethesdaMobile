using BethesdaMobileXamarin.Schedule.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BethesdaMobileXamarin.Schedule.Services
{
    class ScheduleServices
    {
        private HttpClient client;
        private string urlWebServices = (string)Application.Current.Resources["urlWebServices"];
        private string userNameWS = (string)Application.Current.Resources["userNameWS"];
        private string passwordWS = (string)Application.Current.Resources["passwordWS"];

        public ScheduleServices()
        {
            var authData = string.Format("{0}:{1}", userNameWS, passwordWS);
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }

        public async Task<List<KlinikSchedule>> GetKlinikSchedule(string klinikId)
        {
            List<KlinikSchedule> listKlinikSchedule = new List<KlinikSchedule>();
            var uri = new Uri($"{urlWebServices}/dokterPraktekberdasarKlinik/"+klinikId);
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    listKlinikSchedule = JsonConvert.DeserializeObject<List<KlinikSchedule>>(content);

                }
                else
                {
                    throw new Exception("Gagal Mengambil Jadwal Dokter Klinik");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listKlinikSchedule;
        }

        public async Task<List<DokterSchedule>> GetDokterkSchedule(string nid)
        {
            List<DokterSchedule> listDokterSchedule = new List<DokterSchedule>();
            var uri = new Uri($"{urlWebServices}/dokterPraktekberdasarNID/" + nid);
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    listDokterSchedule = JsonConvert.DeserializeObject<List<DokterSchedule>>(content);

                }
                else
                {
                    throw new Exception("Gagal Mengambil Jadwal Dokter");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listDokterSchedule;
        }
    }
}
