using CalendarApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.Services
{
    public class EventService
    {
        private static EventService _ins;
        public static EventService ins
        {
            get
            {
                if (_ins == null)
                    _ins = new EventService();
                return _ins;
            }
            set { _ins = value; }
        }
        private readonly HttpClient client = ApiService.ins.client;

        private EventService()
        {
            AuthorizeClientHeader();

        }
        private void AuthorizeClientHeader()
        {
            var token = SharedPreferenceService.ins.GetUserToken();
            if (token != null)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }


        public async Task<ApiResponse<List<Event>>> GetAllTaskByDay(DateTime selectedDate)
        {
            var response = await client.GetAsync($"api/events?FromDate={selectedDate.ToString("yyyy-MM-dd")}&ToDate={selectedDate.ToString("yyyy-MM-dd")}");
            var objResponse = await ApiService.ins.ParseResponse<List<Event>>(response);

            return objResponse;
        }

        public async Task<ApiResponse<Event>> CreateNewTask(Event newTask)
        {
            Console.WriteLine(JsonConvert.SerializeObject(newTask));
            var content = new StringContent(JsonConvert.SerializeObject(newTask), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/events", content);
            var objResponse = await ApiService.ins.ParseResponse<Event>(response);

            return objResponse;
        }

        public async Task<ApiResponse<Event>> UpdateTask(Event updateTask)
        {
            var content = new StringContent(JsonConvert.SerializeObject(updateTask), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"api/events/update", content);
            var objResponse = await ApiService.ins.ParseResponse<Event>(response);

            return objResponse;
        }

        public async Task<ApiResponse<Event>> DeleetTask(Event deleteTask)
        {
            var content = new StringContent(JsonConvert.SerializeObject(deleteTask), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"api/events/delete", content);
            var objResponse = await ApiService.ins.ParseResponse<Event>(response);

            return objResponse;
        }

    }
}
