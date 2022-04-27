using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using Xunit;

namespace DangerSwap.Test.Automated
{
    public class GeneralTest
    {
        protected readonly string Url = "https://localhost:7145/";
        protected readonly IWebDriver _driver = new ChromeDriver();

        [SkippableFact]
        public void LoadApplicationPage()
        {
            // Arrange
            string expectedUri = "Authorization/Login?ReturnUrl=%2F";
            string expectedTitle = "- DangerSwap";
            using (_driver)
            {
                // Act
                _driver.Navigate().GoToUrl(Url);

                // Assert
                Assert.Equal(Url + expectedUri, _driver.Url);
                Assert.Equal(expectedTitle, _driver.Title);
            }
        }

        [SkippableFact]
        public void ReloadApplicationPage()
        {
            // Arrange
            string expectedUri = "Authorization/Login?ReturnUrl=%2F";
            string expectedTitle = "- DangerSwap";
            using (_driver)
            {
                // Act
                _driver.Navigate().GoToUrl(Url);

                _driver.Navigate().Refresh();

                Assert.Equal(Url + expectedUri, _driver.Url);
                Assert.Equal(expectedTitle, _driver.Title);
            }
        }
    }
}
