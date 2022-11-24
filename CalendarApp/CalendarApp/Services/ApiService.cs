using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using CalendarApp.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CalendarApp.Services
{
    public class ApiService
    {
        private static ApiService _ins;
        public static ApiService ins
        {
            get
            {
                if (_ins == null)
                    _ins = new ApiService();
                return _ins;
            }
            set { _ins = value; }
        }

        public readonly HttpClient client;

        public ApiService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://sheca-api.azurewebsites.net/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private void GetUserToken()
        {
            var token = SharedPreferenceService.ins.GetUserToken();
            if (token != null)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"Bearer {token}");
        }

        public async Task<ApiResponse<T>> ParseResponse<T>(HttpResponseMessage response)
        {
            var responseRead = await response.Content.ReadAsStringAsync();
            try
            {
                if (!string.IsNullOrEmpty(responseRead))
                {
                    var objResponse = JsonConvert.DeserializeObject<ApiResponse<T>>(responseRead);
                    objResponse.isSuccess = response.IsSuccessStatusCode;
                    return objResponse;
                }
                else
                {
                    var objResponse = new ApiResponse<T>
                    {
                        isSuccess = response.IsSuccessStatusCode
                    };
                    return objResponse;
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            return null;
        }
    }
}
