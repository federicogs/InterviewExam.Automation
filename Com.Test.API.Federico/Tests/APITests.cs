using Com.Test.API.Federico.Contracts;
using Com.Test.API.Federico.Contracts.Responses;
using NUnit.Framework;
using RestSharp;
using System;

namespace Com.Test.API.Federico
{
    public class APITests
    {

        #region Fields        
        private  Helpers.BookerAPIHelper apiHelper = new Helpers.BookerAPIHelper();
        #endregion

        #region Tests
        [OneTimeSetUp]
        public void TestClassInitialize()
        {
            Health_Check();
        }

        [Test]
        public void Health_Check()
        {

            //// Act
            bool bHealthCheckResult = apiHelper.HealthCheck();

            // Assert
            Assert.IsTrue(bHealthCheckResult);            
        }

        [Test]
        public void Credential_Check()
        {
            //// Peepare + Act
            String strToken = apiHelper.GetAuthToken();

            // Assert
            Assert.IsTrue(!String.IsNullOrEmpty(strToken));
        }

        [Test]
        public void Create_New_Booking()
        {
            // Arrange
            BookingContract oNewBooking = new BookingContract()
            {
                    FirstName = "Bill",
                    LastName = "Gates",
                    TotalPrice = 2250,
                    DepositPaid = true,
                    BookinDates = new BookingDates()
                    {
                        CheckIn = DateTime.Now.AddDays(10).ToString("yyyy/MM/dd"),
                        CheckOut = DateTime.Now.AddDays(20).ToString("yyyy/MM/dd")
                    },
                    AdditionalNeeds = "Non Smoking"
            };

            // Act

            BookingResponse res = apiHelper.Create_Booking(oNewBooking);

        // Assert

        Assert.Pass();
        }

        [Test]
        public void Update_Booking()
        {
            Assert.Pass();
        }

        [Test]
        public void Delete_Booking()
        {
            Assert.Pass();
        }

        [Test]
        public void Get_Bookings()
        {
            //// Arrange
            //var client = new RestClient(_baseUrl);
            //var request = new RestRequest("booking", Method.GET);

            //// Act
            //IRestResponse response = client.Execute(request);

            //// response.Content.dese

            //// Assert
            //Assert.IsTrue(response.IsSuccessful);
        }
        #endregion
    }
}