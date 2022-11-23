using CalendarApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CalendarApp.Services
{
    public class CourseService
    {
        private static CourseService _ins;
        public static CourseService ins
        {
            get
            {
                if (_ins == null)
                    _ins = new CourseService();
                return _ins;
            }
            set { _ins = value; }
        }

        private readonly HttpClient client = ApiService.ins.client;

        private CourseService()
        {
            AuthorizeClientHeader();
        }
        private void AuthorizeClientHeader()
        {
            var token = SharedPreferenceService.ins.GetUserToken();
            if (token != null)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<ApiResponse<List<Subject>>> GetAllCourse()
        {
            var response = await client.GetAsync("api/courses");
            var objResponse = await ApiService.ins.ParseResponse<List<Subject>>(response);
            return objResponse;
        }

        public async Task<ApiResponse<Subject>> CreateNewCourse(Subject newCourse)
        {
            Console.WriteLine(JsonConvert.SerializeObject(newCourse));
            var content = new StringContent(JsonConvert.SerializeObject(newCourse), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/courses", content);
            var objResponse = await ApiService.ins.ParseResponse<Subject>(response);

            return objResponse;
        }

        public async Task<ApiResponse<Subject>> UpdateCourse(Subject updateCourse)
        {
            var content = new StringContent(JsonConvert.SerializeObject(updateCourse), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"api/courses/{updateCourse.id}", content);
            var objResponse = await ApiService.ins.ParseResponse<Subject>(response);

            return objResponse;
        }

        public async Task<bool> DeleteCourse(int courseId)
        {
            var response = await client.DeleteAsync($"api/courses/{courseId}");

            return response.IsSuccessStatusCode;
        }
    }
}
