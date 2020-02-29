using OpenQA.Selenium;
using TestEkkleia.Fixtures;
using TestEkkleia.PageObjects;
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
            var registryPage = new RegistryPage(_driver);
            //Arrange
            registryPage.Visit();
            //Act
            registryPage.
                FillUpForm("userTest@email.com", "eyw_6AbX?-CuQ+?", "eyw_6AbX?-CuQ+?");
            //Verify
            Assert.Contains("userTest@email.com", _driver.PageSource);
        }

        [Theory]
        [InlineData("", "", "")]
        [InlineData("test.com", "Aa1123456789", "Aa1123456789")]
        [InlineData("test1@email.com", "", "")]
        [InlineData("test2@email.com", "Aa112", "Aa112")]
        [InlineData("test3@email.com", "Aa1123456789", "")]
        [InlineData("test4@email.com", "Ab1123456789", "Ac1123456789")]
        [InlineData("test5@email.com", "AB1123456789", "AB1123456789")]
        [InlineData("test6@email.com", "ab1123456789", "ab1123456789")]
        public void TryToRegisterUserwrongly(string email, string password,
            string passwordConfirmation)
        {
            var registryPage = new RegistryPage(_driver);
            //Arrange
            registryPage.Visit();
            //Act
            registryPage.
                FillUpForm(email, password, passwordConfirmation);
            //Verify
            var spans = _driver.FindElements(By.TagName("span"));

            //Verify
            Assert.True(spans.Count >= 1);
        }

    }
}
