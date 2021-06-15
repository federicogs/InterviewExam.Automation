using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Com.Test.API.Federico.Contracts.Responses
{
    public class BookingResponse
    {
        [JsonPropertyName("bookingid")]
        public int BookingId { get; set; }

        [JsonPropertyName("booking")]
        public BookingContract Booking { get; set; }
    }
}
