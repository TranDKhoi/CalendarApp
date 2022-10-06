using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;

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
    }
}
