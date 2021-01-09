using BethesdaMobileXamarin.Login.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BethesdaMobileXamarin.Login.Service
{
    class LoginServices
    {
        private HttpClient client;
        private string urlWebServices = (string)Application.Current.Resources["urlWebServices"];
        private string userNameWS = (string)Application.Current.Resources["userNameWS"];
        private string passwordWS = (string)Application.Current.Resources["passwordWS"];

        public LoginServices()
        {
            var authData = string.Format("{0}:{1}", userNameWS, passwordWS);
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }


        public async Task<Pasien> GetLoginByNoRmServices(string noRM)
        {
            Pasien pasien = new Pasien();
            var uri = new Uri($"{urlWebServices}/Pasien/{noRM}");
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    pasien = JsonConvert.DeserializeObject<Pasien>(content);
                }
                else
                {
                    throw new Exception("Gagal Koneksi Server Login");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return pasien;
        }

       

    }
}
