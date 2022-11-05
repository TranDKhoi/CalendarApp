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
            var content = new StringContent(JsonConvert.SerializeObject(new User
            {
                email = email,
                password = password
            }),
            Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/auth/login", content);
            var objResponse = await ApiService.ins.ParseResponse<User>(response);

            return objResponse;
        }

        public async Task<ApiResponse<string>> Register(string email, string password)
        {
            var content = new StringContent(JsonConvert.SerializeObject(new User
            {
                email = email,
                password = password
            }),
            Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/auth/register", content);
            var objResponse = await ApiService.ins.ParseResponse<string>(response);
            return objResponse;
        }

        public async Task<ApiResponse<User>> VerifyAccount(string email, string code)
        {
            var content = new StringContent(JsonConvert.SerializeObject(new { email = email, code = code }),
            Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/auth/verify-account", content);
            var objResponse = await ApiService.ins.ParseResponse<User>(response);
            return objResponse;
        }

        public async Task<ApiResponse<string>> ForgotPassword(string email)
        {
            var content = new StringContent(JsonConvert.SerializeObject(email),
            Encoding.UTF8, "application/json");
            Console.WriteLine(content.ReadAsStringAsync());

            var response = await client.PostAsync("api/auth/forgot-password", content);
            var objResponse = await ApiService.ins.ParseResponse<string>(response);
            return objResponse;
        }

        public async Task<ApiResponse<string>> VerifyRePass(string email, string code)
        {

            var content = new StringContent(JsonConvert.SerializeObject(new { email = email, code = code }),
            Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/auth/verify-code-repassword", content);
            var objResponse = await ApiService.ins.ParseResponse<string>(response);
            return objResponse;
        }

        public async Task<ApiResponse<User>> ResetPassword(string tempToken, string password)
        {
            var content = new StringContent(JsonConvert.SerializeObject(new { token = tempToken, password = password }),
            Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/auth/reset-password", content);
            var objResponse = await ApiService.ins.ParseResponse<User>(response);
            return objResponse;
        }
    }
}
