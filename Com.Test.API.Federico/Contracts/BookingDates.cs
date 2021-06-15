using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Com.Test.API.Federico.Contracts
{
    public class BookingDates
    {
        [JsonPropertyName("checkin")]
        public string CheckIn { get; set; }

        [JsonPropertyName("checkout")]
        public string CheckOut { get; set; }
    }
}
