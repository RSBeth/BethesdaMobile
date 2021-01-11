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
    class KlinikServices
    {

        private HttpClient client;
        private string urlWebServices = (string)Application.Current.Resources["urlWebServices"];
        private string userNameWS = (string)Application.Current.Resources["userNameWS"];
        private string passwordWS = (string)Application.Current.Resources["passwordWS"];

        public KlinikServices()
        {
            var authData = string.Format("{0}:{1}", userNameWS, passwordWS);
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }


        public async Task<List<Klinik>> GetAllKlinikServices()
        {
            List<Klinik> listKlinik = new List<Klinik>();
            var uri = new Uri($"{urlWebServices}/Klinik/");
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    listKlinik = JsonConvert.DeserializeObject<List<Klinik>>(content);
                    foreach (Klinik klinik in listKlinik)
                    {   //set true semua nya agar hitam tulisane
                        klinik.praktek = "true";
                    }
                }
                else
                {
                    throw new Exception("Gagal mengakses Daftar Klinik");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listKlinik;
        }


        public async Task<List<Klinik>> GetKlinikByDokter(String dokter,String tglReg)
        {
            List<Klinik> listKlinik = new List<Klinik>();
            var uri = new Uri($"{urlWebServices}/KlinikDokterJanji/?cNID="+dokter+ "&dTanggal="+tglReg);
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    listKlinik = JsonConvert.DeserializeObject<List<Klinik>>(content);

                    
                }
                else
                {
                    throw new Exception("Gagal Mengambil Dokter Klinik");
                };

            
            }


            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listKlinik;
        }

    }
}
