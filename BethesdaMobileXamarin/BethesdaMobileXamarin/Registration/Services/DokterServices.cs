using BethesdaMobileXamarin.Registration.Model;
using Java.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BethesdaMobileXamarin.Registration.Services
{
    class DokterServices
    {

        private HttpClient client;
        private string urlWebServices = (string)Application.Current.Resources["urlWebServices"];
        private string userNameWS = (string)Application.Current.Resources["userNameWS"];
        private string passwordWS = (string)Application.Current.Resources["passwordWS"];
        public DokterServices()
        {
            var authData = string.Format("{0}:{1}", userNameWS , passwordWS);
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }


        public async Task<List<Dokter>> GetAllDokterServices()
        {
           List<Dokter> listDokter = new List<Dokter>();
           var uri = new Uri($"{urlWebServices}/Dokter/");
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    listDokter = JsonConvert.DeserializeObject<List<Dokter>>(content);
                    foreach (Dokter dokter in listDokter)
                    {   //set true semua nya agar hitam tulisane
                        dokter.praktek = "true";
                    }
                }
                else
                {
                    throw new Exception("Gagal Mengambil Dokter");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listDokter;
        }
        public async Task<List<Dokter>> GetDokterByKlinik(String klinik, String tglReg)
        {
            List<Dokter> listDokter = new List<Dokter>();
            var uri = new Uri($"{urlWebServices}/DokterKlinikJanji/?cKodeKlinik=" + klinik + "&dTanggal=" + tglReg);
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    listDokter = JsonConvert.DeserializeObject<List<Dokter>>(content);
                   

                }
                else
                {
                    throw new Exception("Gagal Mengambi Dokter Klinik");
                };


            }


            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listDokter;
        }

    }
}
