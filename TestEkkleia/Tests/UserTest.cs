using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using TestEkkleia.Fixtures;
using Xunit;

namespace TestEkkleia.Tests
{
    [Collection("Chrome Driver")]
    public class UserTest
    {
        private readonly IWebDriver _driver;

        public UserTest(TestFixture fixture)
        {
            _driver = fixture.Driver;
        }

        [Fact]
        public void RegisterUser()
        {
            //Arrange
            _driver.Navigate().GoToUrl("https://localhost:44389/");

            var inputEmail = _driver.FindElement(By.Id("Email"));
            var inputPassword = _driver.FindElement(By.Id("Password"));
            var inputConfirmationPassword = _driver.FindElement(By.Id("ConfirmPassword"));

            inputEmail.SendKeys("userTest@email.com");
            inputPassword.SendKeys("eyw_6AbX?-CuQ+?");
            inputConfirmationPassword.SendKeys("eyw_6AbX?-CuQ+?");

            var registerButton = _driver.FindElement(By.Id("registerButton"));
            registerButton.Click();

            //Verify
            Assert.Contains("userTest@email.com", _driver.PageSource);
        }

        [Theory]
        [InlineData("", "", "")]
        [InlineData("test1@email.com", "", "")]
        [InlineData("test2@email.com", "Aa112", "Aa112")]
        [InlineData("test3@email.com", "Aa1123456789", "")]
        [InlineData("test4@email.com", "Ab1123456789", "Ac1123456789")]
        [InlineData("test5@email.com", "AB1123456789", "AB1123456789")]
        [InlineData("test6@email.com", "ab1123456789", "ab1123456789")]        
        public void TryToRegisterUserwrongly(string email, string password,
            string passwordConfirmation)
        {
            //Arrange
            _driver.Navigate().GoToUrl("https://localhost:44389/");
            var registerLink = _driver.FindElement(By.Id("registerLink"));
            registerLink.Click();

            var inputEmail = _driver.FindElement(By.Id("Email"));
            var inputPassword = _driver.FindElement(By.Id("Password"));
            var inputConfirmationPassword = _driver.FindElement(By.Id("ConfirmPassword"));

            inputEmail.SendKeys(email);
            inputPassword.SendKeys(password);
            inputConfirmationPassword.SendKeys(passwordConfirmation);

            var registerButton = _driver.FindElement(By.Id("registerButton"));
            registerButton.Click();

            //Verify
            Assert.Contains("text-danger validation-summary-errors", _driver.PageSource);
        }

    }
}
