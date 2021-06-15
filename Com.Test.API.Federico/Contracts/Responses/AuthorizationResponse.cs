using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Com.Test.API.Federico.Contracts.Responses
{
    public class AuthorizationResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("reason")]
        public string Reason { get; set; }
    }
}
