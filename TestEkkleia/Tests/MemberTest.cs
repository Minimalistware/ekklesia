using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Reflection;
using TestEkkleia.Helpers;
using TestEkkleia.Fixtures;

namespace TestEkkleia
{
    [Collection("Chrome Driver")]
    public class MemberTest
    {
        private readonly IWebDriver _driver;

        public MemberTest(TestFixture fixture)
        {
            _driver = fixture.Driver;
        }

        [Fact]
        public void Test1()
        {

            _driver.Navigate().GoToUrl("https://localhost:44389/");

            Assert.Contains("Ekklésia", _driver.Title);
        }

        [Fact]
        public void AddMember()
        {
            IWebDriver driver = new ChromeDriver(TestHelper.GekoDriverPath);
        }

        [Fact]
        public void EditMember()
        {
            IWebDriver driver = new ChromeDriver(TestHelper.GekoDriverPath);
        }

        [Fact]
        public void DeleteMember()
        {
            IWebDriver driver = new ChromeDriver(TestHelper.GekoDriverPath);
        }


    }
}
