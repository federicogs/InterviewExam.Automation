using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Configuration;

namespace Com.Test.Federico
{
    public class Tests
    {
        //FEDE: https://saucelabs.com/resources/articles/getting-started-with-webdriver-in-c-using-visual-studio

        private IWebDriver driver;
        public string homeURL = "http://automationpractice.com";

        #region Activities
        [SetUp]
        public void Setup()
        {   

            homeURL = "http://automationpractice.com";
            driver = new ChromeDriver();

            // apply general brower settings to all tests (maximize windows, etc)
            Utils.UtilHelper.SetupDriverWindow(ref driver);
        }

        [TearDown]
        public void Cleanup()
        {
            // make sure brower window(s) are closed after tests
            driver.Close();
            driver.Quit();
        }
        #endregion

        #region Tests
        [Test(Description = "Browse to site, input credentials, access personal details form, update name, and save changes. Validate that the operation has been competed without errors based on text.")]
        public void Login_And_Update_Personal_Information()
        {
            // navigate to test site
            driver.Navigate().GoToUrl(homeURL);

            // click on login link
            IWebElement element = driver.FindElement(By.XPath("//a[contains(text(),'Sign in')]"));
            element.Click();

            // find credential fields
            IWebElement eEmail = driver.FindElement(By.Id("email"));
            IWebElement ePassword = driver.FindElement(By.Id("passwd"));

            // get credentials from app config
            String strAccountEmail = ConfigurationManager.AppSettings["accountEmail"];
            String strAccountPassword = ConfigurationManager.AppSettings["accountPassword"];

            // type crendentials
            eEmail.SendKeys(strAccountEmail);
            ePassword.SendKeys(strAccountPassword);

            // click on submit button
            IWebElement eSubmit = driver.FindElement(By.Id("SubmitLogin"));
            eSubmit.Click();

            // find logout link
            IWebElement eLogout = driver.FindElement(By.ClassName("logout"));

            // if logout link is shown, login process has succeeded
            //Assert.IsNotNull(eLogout);

            // "View my customer account"
            IWebElement eAccountDetails = driver.FindElement(By.ClassName("account"));
            eAccountDetails.Click();

            // click on Personal information item
            IWebElement ePersonalInfo = driver.FindElement(By.XPath("//a[contains(@title, 'Information')]"));
            ePersonalInfo.Click();

            // update first name
            IWebElement ePersonalInfo_FirstName = driver.FindElement(By.Id("firstname"));
            ePersonalInfo_FirstName.Clear();
            // ePersonalInfo_FirstName.SendKeys("EDITED on " + DateTime.Now.ToString("yyyyMMddHHmmss") );
            ePersonalInfo_FirstName.SendKeys("New Name");

            // fill password field
            IWebElement ePersonalInfo_Password = driver.FindElement(By.Id("old_passwd"));
            ePersonalInfo_Password.SendKeys(strAccountPassword);

            // click on save buton
            IWebElement eSaveChanges = driver.FindElement(By.Name("submitIdentity"));
            eSaveChanges.Click();

            // validate result based on text
            IWebElement eInfoChangeResult = driver.FindElement(By.XPath("//p[contains(text(),'Your personal information has been successfully updated.')]"));
            Assert.IsNotNull(eInfoChangeResult);

        }

        [Test(Description = "Browse to site, add a specific item type to cart, login, confirm adding item to cart, confim shipment details, confirm payment details as check, get order#, validate that order # is displayed on order list.")]
        public void Order_TShirt_And_Validate_Order_Creation()
        {
            // navigate to test site
            driver.Navigate().GoToUrl(homeURL);

            // select Women item on menu
            IWebElement eWoman = driver.FindElement(By.LinkText("Women"));
            eWoman.Click();

            // select Tops item on menu
            IWebElement eTops = driver.FindElement(By.LinkText("Tops"));
            eTops.Click();

            // select t-shirts item on menu
            IWebElement eTshirts = driver.FindElement(By.LinkText("T-shirts"));
            eTshirts.Click();

            // select first item image displayed
            // TODO: Validate corner case of no items being displayed            
            // IWebElement ePurchaseItem = driver.FindElement(By.XPath("xpath =//li/div/div/div/a/img"));            
            // class=product_img_link
            IWebElement ePurchaseItem = driver.FindElement(By.ClassName("product_img_link"));
            ePurchaseItem.Click();

            // add item to cart            
            IWebElement eAddToCart = driver.FindElement(By.Id("add_to_cart"));
            eAddToCart.Click();

            // validate that  the text "Product successfully added to your shopping cart" is displayed
            // IWebElement eItemAddedConfirmation = driver.FindElement(By.XPath("//*[contains(text(),'Product successfully added')]"));
            // Assert.IsNotNull(eItemAddedConfirmation);

            // click on green "Proceed to check out button" after adding item to cart
            // IWebElement eProceedToCheckOutAfterAddingItem = driver.FindElement(By.XPath("xpath=//p[@id='add_to_cart']/button/span"));
            IWebElement eProceedToCheckOutAfterAddingItem = driver.FindElement(By.Id("add_to_cart"));
            eProceedToCheckOutAfterAddingItem.Click();

            // click on green "Proceed to check out button" on Summary
            IWebElement eProceedToCheckOutOnSummary = driver.FindElement(By.XPath("//a[contains(@title, 'Proceed to checkout')]"));
            eProceedToCheckOutOnSummary.Click();

            // click on green "Proceed to check out button" on Shopping Cart - Summary
            IWebElement eProceedToCheckOutOnSummary2 = driver.FindElement(By.XPath("//a[contains(@title, 'Proceed to checkout')]"));
            eProceedToCheckOutOnSummary2.Click();

            // find credential fields for logging in 
            IWebElement eEmail = driver.FindElement(By.Id("email"));
            IWebElement ePassword = driver.FindElement(By.Id("passwd"));

            // get credentials from app config
            String strAccountEmail = ConfigurationManager.AppSettings["accountEmail"];
            String strAccountPassword = ConfigurationManager.AppSettings["accountPassword"];

            // type crendentials on login form fields
            eEmail.SendKeys(strAccountEmail);
            ePassword.SendKeys(strAccountPassword);

            // submit login form            
            IWebElement eSubmit = driver.FindElement(By.Id("SubmitLogin"));
            eSubmit.Click();

            // click on green "Proceed to check out button" on Delivery Address confirmation            
            IWebElement eProceedToCheckOutOnSummary3 = driver.FindElement(By.XPath("//a[contains(@title, 'Proceed to checkout')]"));
            eProceedToCheckOutOnSummary3.Click();

            // Shipping - Click on my carrier
            // TODO 

            // Shipping - Agree to Terms of Service           
            IWebElement eTermsAndCondition = driver.FindElement(By.Id("cgv"));
            eTermsAndCondition.Click();

            // click on green "Proceed to check out button" on Delivery Address confirmation
            IWebElement eProceedToCheckOutOnSummary4 = driver.FindElement(By.XPath("//a[contains(@title, 'Proceed to checkout')]"));
            eProceedToCheckOutOnSummary4.Click();

            // Payment Method - Check
            IWebElement ePaymentMethodCheck = driver.FindElement(By.XPath("//a[contains(@title, 'Pay by check')]"));
            ePaymentMethodCheck.Click();

            // Confirm order button
            IWebElement eConfirmOrder = driver.FindElement(By.XPath("//*[contains(@text, 'I confirm my order')]"));
            eConfirmOrder.Click();


            // Capture Order details
            String strOrderId = String.Empty;

            IWebElement eOrderInfo = driver.FindElement(By.ClassName("order-confirmation"));
            string strOrderInfo = eOrderInfo.Text;
            String[] parts = strOrderInfo.Split("order reference");

            if (parts.Length >= 2) 
            {
                String[] parts2 = parts[1].Split(".");
                if (parts2.Length >= 1)
                {
                    strOrderId = parts2[0];
                }
            }            

            IWebElement eBackToOrders = driver.FindElement(By.LinkText("Back to orders"));
            eBackToOrders.Click();

            // search for new order in list            
            IWebElement eNewOrderInList = driver.FindElement(By.XPath("//td[text()='" + strOrderId + "']"));
            Assert.IsNotNull(eNewOrderInList);
            
            
        }        
        #endregion       

    }
}