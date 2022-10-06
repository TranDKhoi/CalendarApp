using CalendarApp.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.Services
{
    public class AuthService
    {
        private static AuthService _ins;
        public static AuthService ins
        {
            get
            {
                if (_ins == null)
                    _ins = new AuthService();
                return _ins;
            }
            set { _ins = value; }
        }

        private readonly HttpClient client = ApiService.ins.client;

        public async Task<ApiResponse<User>> Login(string email, string password)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(new User
                {
                    email = email,
                    password = password
                }),
                Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/auth/login", content);
                response.EnsureSuccessStatusCode();
                var responseRead = await response.Content.ReadAsStringAsync();
                var objResponse = JsonConvert.DeserializeObject<ApiResponse<User>>(responseRead);
                objResponse.IsSuccessStatusCode = response.IsSuccessStatusCode;
                return objResponse;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ApiResponse<string>> Register(string email, string password)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(new User
                {
                    email = email,
                    password = password
                }),
                Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/auth/register", content);
                response.EnsureSuccessStatusCode();
                var responseRead = await response.Content.ReadAsStringAsync();
                var objResponse = JsonConvert.DeserializeObject<ApiResponse<string>>(responseRead);
                objResponse.IsSuccessStatusCode = response.IsSuccessStatusCode;
                return objResponse;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
