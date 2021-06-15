using Com.Test.API.Federico.Contracts;
using Com.Test.API.Federico.Contracts.Requests;
using Com.Test.API.Federico.Contracts.Responses;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Com.Test.API.Federico.Helpers
{
    public class BookerAPIHelper
    {
        #region Fields
        private string _baseUrl = "https://restful-booker.herokuapp.com/";
        private string _credentialsUsername = "admin";
        private string _credentialsPassword = "password123";

        RestClient _client;

        #region Endpoints
        private static string AuthorizationEndpoint => "/auth";
        private static string BookingEndpoint => "/booking";
        private static string GetBookingByIdEndpoint => "/booking/{bookingId}";                
        #endregion
        #endregion

        #region Ctor
        public BookerAPIHelper()
        {
            // TODO: Move to AppConfig file
            // _baseUrl = ConfigurationManager.AppSettings["APIendpointaddress"];
            // _credentialsUsername = ConfigurationManager.AppSettings["APIendpointaddress"];
            // _credentialsPassword = ConfigurationManager.AppSettings["APIendpointaddress"];

            _client = new RestClient(_baseUrl);
        }
        #endregion

        #region Methods

        #region Headers
        public void AddHeaders(ref RestRequest request)
        {
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
        }

        public void AddAuthorizationHeader(ref RestRequest request)
        {
            var token = GetAuthToken();
            var headerValue = $"token={token}";
            request.AddHeader("Cookie", headerValue);
        } 
        #endregion

        public bool HealthCheck() 
        {
            
            var request = new RestRequest("ping", Method.GET);
            
            IRestResponse response = _client.Execute(request);

            return response.IsSuccessful;
        }

        internal BookingResponse Create_Booking(BookingContract oNewBooking)
        {
            // prepare request data
            var jsonRequest = JsonSerializer.Serialize(oNewBooking);

            var request = new RestRequest(BookingEndpoint, Method.POST);
            AddHeaders(ref request);
            request.AddParameter("application/json", jsonRequest, ParameterType.RequestBody);

            // call API            
            var postResponse = _client.Execute<BookingResponse>(request);

            // parse result
            var result = JsonSerializer.Deserialize<BookingResponse>(postResponse.Content);

            BookingResponse response = GetBookingById(result.BookingId);

            return response;
        }

        public BookingResponse GetBookingById(int bookingId)
        {
            var request = BookingByIdRequest(bookingId, Method.GET);

            BookingResponse result = null;

            try
            {
                var response_serialized = _client.Execute<BookingResponse>(request);
                result = JsonSerializer.Deserialize<BookingResponse>(response_serialized.Content);

            }
            catch (Exception e)
            {
                result = new BookingResponse();
            }

            return result;
        }

        public bool RemoveBookingById(int bookingId)
        {
            var request = BookingByIdRequest(bookingId, Method.DELETE);

            try
            {
                _client.Execute<BookingResponse>(request);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private RestRequest BookingByIdRequest(int bookingId, Method method)
        {
            var request = new RestRequest(GetBookingByIdEndpoint, method);
            request.AddUrlSegment("bookingId", bookingId);

            AddHeaders(ref request);
            AddAuthorizationHeader(ref request);

            return request;
        }

        public bool UpdateBooking(BookingContract updatedBooking, Int32 bookingId)
        {
            // prepare request data
            var jsonRequest = JsonSerializer.Serialize(updatedBooking);

            var request = new RestRequest(GetBookingByIdEndpoint, Method.PUT);
            request.AddUrlSegment("bookingId", bookingId);
            AddHeaders(ref request);
            AddAuthorizationHeader(ref request);

            request.AddParameter("application/json", jsonRequest, ParameterType.RequestBody);

            // call API            
            var postResponse = _client.Execute<BookingResponse>(request);

            // parse result
            var result = JsonSerializer.Deserialize<BookingResponse>(postResponse.Content);

            BookingResponse response = GetBookingById(result.BookingId);

            return (response.BookingId != 0);
        }

        public bool GetAllBookings()
        {
            throw new NotImplementedException();
        }

        public string GetAuthToken()
        {
            var client = new RestClient(_baseUrl);

            // fill contract / request with information
            var body = new AuthorizationRequest
            {
                Username = _credentialsUsername,
                Password = _credentialsPassword
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
                return "BAD_CREDENTIALS";
            }
        }
        #endregion

    }
}
