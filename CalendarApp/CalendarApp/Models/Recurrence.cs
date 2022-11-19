using System;
using System.Collections.Generic;
using System.Text;

namespace CalendarApp.Models
{
    public enum TypeStartRecurrence
    {
        Date,
        Week,
        Month,
        None,
    };

    public enum TypeEndRecurrence
    {
        Infinity,
        Date,
        Count,
    };

    public class Recurrence
    {
        private TypeStartRecurrence typeStartRecurrence;
        public void SetTypeStartRecurrence(TypeStartRecurrence value)
        {
            typeStartRecurrence = value;
        }
        public TypeStartRecurrence GetTypeStartRecurrence()
        {
            return typeStartRecurrence;
        }

        private TypeEndRecurrence typeEndRecurrence;
        public void SetTypeEndRecurrence(TypeEndRecurrence value)
        {
            typeEndRecurrence = value;
        }
        public TypeEndRecurrence GetTypeEndRecurrence()
        {
            return typeEndRecurrence;
        }

        public List<string> ChoosenWeeksDay { get;set; }
        public int QuantityRecurrence { get; set; }
        public DateTime EndDate { get; set; }
        public int EndCount { get; set; }
        public string LabelDisplay { get; set; }
    }
}
