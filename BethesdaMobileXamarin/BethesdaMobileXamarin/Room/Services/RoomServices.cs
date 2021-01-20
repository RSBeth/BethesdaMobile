using BethesdaMobileXamarin.Room.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BethesdaMobileXamarin.Room.Services
{
    class RoomServices
    {

        private HttpClient client;
        private string urlWebServices = (string)Application.Current.Resources["urlWebServices"];
        private string userNameWS = (string)Application.Current.Resources["userNameWS"];
        private string passwordWS = (string)Application.Current.Resources["passwordWS"];
        public RoomServices()
        {
            var authData = string.Format("{0}:{1}", userNameWS, passwordWS);
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }


        public async Task<List<EmptyRoom>> GetAvailableRoom()
        {
            List<EmptyRoom> listEmptyRoom = new List<EmptyRoom>();
            var uri = new Uri($"{urlWebServices}/dashboard/");
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    listEmptyRoom = JsonConvert.DeserializeObject<List<EmptyRoom>>(content);
                    
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
            return listEmptyRoom;
        }
    }
}
