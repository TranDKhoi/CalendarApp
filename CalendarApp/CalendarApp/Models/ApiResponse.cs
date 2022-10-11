using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarApp.Models
{
    public class ApiResponse<T>
    {
        public string message { get; set; }
        public T data { get; set; }
        public string token { get; set; }
        public bool isSuccess { get; set; }
    }
}
