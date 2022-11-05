using CalendarApp.Models;
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


        //public async Task<ApiResponse<List<Models.Task>>> GetAllTaskByDay()
        //{
        //}

    }
}
