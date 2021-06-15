using Com.Test.API.Federico.Contracts;
using Com.Test.API.Federico.Contracts.Requests;
using Com.Test.API.Federico.Contracts.Responses;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Text.Json;

namespace Com.Test.API.Federico.Helpers
{
    class BookerAPIHelper
    {
        #region Fields
        private string _baseUrl = "https://restful-booker.herokuapp.com/";

        #region Endpoints
        private static string AuthorizationEndpoint => "/auth";
        private static string BookingEndpoint => "/booking";
        private static string GetBookingByIdEndpoint => "/booking/{bookingId}";                
        #endregion
        #endregion

        #region Ctor
        public BookerAPIHelper()
        {
            _baseUrl = ConfigurationManager.AppSettings["APIendpointaddress"];
        }
        #endregion

        #region Methods
        public bool HealthCheck() 
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("ping", Method.GET);

            
            IRestResponse response = client.Execute(request);

            return response.IsSuccessful;
        }

        internal BookingResponse Create_Booking(BookingContract oNewBooking)
        {

            // prepare data

            // call API
             // _baseUrl

            // parse result
            BookingResponse response = new BookingResponse();

            return response;
        }

        public string GetAuthToken()
        {
            var client = new RestClient(_baseUrl);

            // fill contract / request with information
            var body = new AuthorizationRequest
            {
                Username = ConfigurationManager.AppSettings["authUsername"],
                Password = ConfigurationManager.AppSettings["authPassword"]
            };

            // build request
            var request = new RestRequest(AuthorizationEndpoint, Method.POST);

            var json = JsonSerializer.Serialize(body);
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            // send request
            var response = client.Execute<AuthorizationResponse>(request);

            // deserialize

            var result = JsonSerializer.Deserialize<AuthorizationResponse>(response.Content);
            if (result.Token != null)
            {
                return result.Token;
            }
            else
            {
                throw new Exception("Bad credentials");
            }
        }
        #endregion

    }
}
