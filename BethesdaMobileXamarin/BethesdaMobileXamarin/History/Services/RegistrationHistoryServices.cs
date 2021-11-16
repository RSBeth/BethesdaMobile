using BethesdaMobileXamarin.History.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BethesdaMobileXamarin.History.Services
{
    class RegistrationHistoryServices
    {


        private HttpClient client;
        private string urlWebServices = (string)Application.Current.Resources["urlWebServices"];
        private string userNameWS = (string)Application.Current.Resources["userNameWS"];
        private string passwordWS = (string)Application.Current.Resources["passwordWS"];

        public  RegistrationHistoryServices()
        {
            var authData = string.Format("{0}:{1}", userNameWS, passwordWS);
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }


        public async Task<List<RegistrationHistory>> GetKunjunganServices(string noRM)
        {
            List<RegistrationHistory> registrationHistoriList = new List<RegistrationHistory>();
            var uri = new Uri($"{urlWebServices}/getkunjungdiatashariini/?cNoRM=" +noRM);
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    registrationHistoriList = JsonConvert.DeserializeObject<List<RegistrationHistory>>(content);
                    
                }
                else
                {
                    throw new Exception("Gagal Mendapatkan Data Kunjungan");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return registrationHistoriList;
        }
    }
}
