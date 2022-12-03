using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace DangerSwap.Test.Automated;

public sealed class GeneralTest
{
    protected readonly string Url = "https://localhost:7145/";
    protected readonly IWebDriver _driver = new ChromeDriver();

    [Fact(Skip = "Automation test")]
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

    [Fact(Skip = "Automation test")]
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

