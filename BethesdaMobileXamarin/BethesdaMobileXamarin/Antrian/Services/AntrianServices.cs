using BethesdaMobileXamarin.Antrian.Model;
using BethesdaMobileXamarin.Antrian.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BethesdaMobileXamarin.Antrian.Services
{
    class AntrianServices
    {

        private HttpClient client;
        private string urlWebServices = (string)Application.Current.Resources["urlWebServices"];
        private string userNameWS = (string)Application.Current.Resources["userNameWS"];
        private string passwordWS = (string)Application.Current.Resources["passwordWS"];

        public AntrianServices()
        {
            var authData = string.Format("{0}:{1}", userNameWS, passwordWS);
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }


        public async Task<AntrianResults> GetAntrianServices(Antrians antrian)
        {
            AntrianResults antrianResults = new AntrianResults();
            var uri = new Uri($"{urlWebServices}/getAntrianKlinik/?dtanggal="+antrian.tgl+"&cKlinik=" + antrian.kodeKLinik + "&cNid=" +antrian.kodeDokter);
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    antrianResults = JsonConvert.DeserializeObject<AntrianResults>(content);
                }
                else
                {
                    throw new Exception("Gagal Mendapatkan Data Antrian");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return antrianResults;
        }

    }
}
