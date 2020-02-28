using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using TestEkkleia.Helpers;

namespace TestEkkleia.Fixtures
{
    public class TestFixture:IDisposable
    {
        public IWebDriver Driver { get; private set; }

        //Setup
        public TestFixture()
        {
            Driver = new ChromeDriver(TestHelper.GekoDriverPath);
        }

        //TearDown
        public void Dispose()
        {
            Driver.Quit();   
        }
    }
}
