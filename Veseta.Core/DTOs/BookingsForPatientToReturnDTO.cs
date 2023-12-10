using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Veseta.Core.entites;

namespace Veseta.CoreCore.DTOs
{
    public class BookingsForPatientToReturnDTO
    {
        public string ImageUrl { get; set; }
        public string DoctorName { get; set; }
        public string SpecializationName { get; set; }
        public DayOfWeek Day { get; set; }
        public string Time { get; set; }
        public decimal Price { get; set; }
        public string DiscountCodeName { get; set; }
        public decimal FinalPrice { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BookingStatus Status { get; set; }
    }
}
