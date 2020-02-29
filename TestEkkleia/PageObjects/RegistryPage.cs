using OpenQA.Selenium;
using System;

namespace TestEkkleia.PageObjects
{
    public class RegistryPage
    {
        private IWebDriver _driver;
        private readonly By inputEmail;
        private readonly By inputPassword;
        private readonly By inputPasswordConfirmation;
        private readonly By buttonRegistry;
        private readonly string URL = "https://localhost:44389/account/register";

        public RegistryPage(IWebDriver driver)
        {
            _driver = driver;
            inputEmail = By.Id("Email");
            inputPassword = By.Id("Password");
            inputPasswordConfirmation = By.Id("ConfirmPassword");
            buttonRegistry = By.Id("");
        }

        internal void FillUpForm(string email, string password, string passwordConfirmation3)
        {
            _driver.FindElement(inputEmail).SendKeys(email);
            _driver.FindElement(inputPassword).SendKeys(password);
            _driver.FindElement(inputPasswordConfirmation)
                .SendKeys(passwordConfirmation3);
        }

        public void Visit()
        {
            _driver.Navigate().GoToUrl(URL);
        }

        public void SubmitForm()
        {
            _driver.FindElement(buttonRegistry).Click();
        }
    }
}
