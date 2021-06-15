﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Com.Test.API.Federico.Contracts.Requests
{
    class AuthorizationRequest
    {
        [Required]
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
