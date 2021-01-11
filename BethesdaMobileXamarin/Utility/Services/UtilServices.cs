using BethesdaMobileXamarin.Utility.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BethesdaMobileXamarin.Utility.Services
{
    class UtilServices
    {

        private HttpClient client;
        private string urlWebServices = (string)Application.Current.Resources["urlWebServices"];
        private string userNameWS = (string)Application.Current.Resources["userNameWS"];
        private string passwordWS = (string)Application.Current.Resources["passwordWS"];

        public UtilServices()
        {

            var authData = string.Format("{0}:{1}", userNameWS, passwordWS);
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }
        public async Task<String> GetCurrentDateServer()
        {
            String currentDate;
            var uri = new Uri($"{urlWebServices}/currentdate/");
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    currentDate = JsonConvert.DeserializeObject<String>(content);

                }
                else
                {
                    throw new Exception("Gagal Current Date");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return currentDate;
        }
        public async Task<Hari>GetMaxDayServices()
        {
            Hari hari = new Hari();
            var uri = new Uri($"{urlWebServices}/GetOpenHari/01/");
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    hari = JsonConvert.DeserializeObject<Hari>(content);
                  
                }
                else
                {
                    throw new Exception("Gagal Mengambil Max Date");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return hari;
        }


    }
}
