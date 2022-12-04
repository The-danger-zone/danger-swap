using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Xunit;

namespace DangerSwap.Test.Automated;

public sealed class PageTest
{
    private readonly string Url = "https://localhost:7145/";
    private readonly IWebDriver _driver = new ChromeDriver();

    [Fact(Skip = "Automation test")]
    public void RedirectedToConverterPage_AfterSuccessfulLogin()
    {
        // Arrange
        string expectedTitle = "Home Page - DangerSwap";
        using (_driver)
        {
            string expectedUri = "Authorization/Login";
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(Url);
            var passwordInput = _driver.FindElement(By.Id("Password"));
            var emailInput = _driver.FindElement(By.Id("Email"));
            var loginButton = _driver.FindElement(By.XPath("/html/body/div/main/div[2]/div/form/div/div[5]/input[1]"));

            // Act
            emailInput.SendKeys("admin@admin.com");
            passwordInput.SendKeys("Admin123#");
            loginButton.Click();


            // Assert
            Assert.Equal(Url, _driver.Url);
            Assert.Equal(expectedTitle, _driver.Title);
        }
    }

    [Fact(Skip = "Automation test")]
    public void RedirectedToProfilePage_SuccessfulLogin_ProfileIconClicked()
    {
        // Arrange
        using (_driver)
        {
            string expectedUri = "Profile/Index";
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(Url);
            var passwordInput = _driver.FindElement(By.Id("Password"));
            var emailInput = _driver.FindElement(By.Id("Email"));
            var loginButton = _driver.FindElement(By.XPath("/html/body/div/main/div[2]/div/form/div/div[5]/input[1]"));


            // Act
            emailInput.SendKeys("admin@admin.com");
            passwordInput.SendKeys("Admin123#");
            loginButton.Click();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            var dropdownButton = _driver.FindElement(By.Id("dropdownMenuButton"));
            dropdownButton.Click();
            var profilePageButton = _driver.FindElement(By.XPath("/html/body/header/nav/div/div[2]/div/div/a"));
            profilePageButton.Click();

            // Assert
            Assert.Equal(Url + expectedUri, _driver.Url);
        }
    }

    [Fact(Skip = "Automation test")]
    public void RedirectedToLoginPage_SuccessfulLogin_LogoutButtonClicked()
    {
        // Arrange
        using (_driver)
        {
            string expectedUri = "Authorization/Login";
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(Url);
            var passwordInput = _driver.FindElement(By.Id("Password"));
            var emailInput = _driver.FindElement(By.Id("Email"));
            var loginButton = _driver.FindElement(By.XPath("/html/body/div/main/div[2]/div/form/div/div[5]/input[1]"));

            // Act
            emailInput.SendKeys("admin@admin.com");
            passwordInput.SendKeys("Admin123#");
            loginButton.Click();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            var dropdownButton = _driver.FindElement(By.Id("dropdownMenuButton"));
            dropdownButton.Click();
            var profilePageButton = _driver.FindElement(By.XPath("/html/body/header/nav/div/div[2]/div/div/form/button"));
            profilePageButton.Click();

            // Assert
            Assert.Equal(Url + expectedUri, _driver.Url);
        }
    }

    [Fact(Skip = "Automation test")]
    public void RedirectedToHomePage_SuccessfulLogin_HeaderHomeButtonClicked()
    {
        // Arrange
        using (_driver)
        {
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(Url);
            var passwordInput = _driver.FindElement(By.Id("Password"));
            var emailInput = _driver.FindElement(By.Id("Email"));
            var loginButton = _driver.FindElement(By.XPath("/html/body/div/main/div[2]/div/form/div/div[5]/input[1]"));

            // Act
            emailInput.SendKeys("admin@admin.com");
            passwordInput.SendKeys("Admin123#");
            loginButton.Click();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            var homeButton = _driver.FindElement(By.XPath("/html/body/header/nav/div/div[1]/ul/li/a"));
            homeButton.Click();

            // Assert
            Assert.Equal(Url, _driver.Url);
        }
    }

    [Fact(Skip = "Automation test")]
    public void RedirectedToHomePage_SuccessfulLogin_FooterConverterButtonClicked()
    {
        // Arrange
        using (_driver)
        {
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(Url);
            var passwordInput = _driver.FindElement(By.Id("Password"));
            var emailInput = _driver.FindElement(By.Id("Email"));
            var loginButton = _driver.FindElement(By.XPath("/html/body/div/main/div[2]/div/form/div/div[5]/input[1]"));

            // Act
            emailInput.SendKeys("admin@admin.com");
            passwordInput.SendKeys("Admin123#");
            loginButton.Click();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            var homeButton = _driver.FindElement(By.XPath("/html/body/footer/div/a"));
            homeButton.Click();

            // Assert
            Assert.Equal(Url, _driver.Url);
        }
    }


    [Fact(Skip = "Automation test")]
    public void RedirectToLoginPage_WithoutLogin_FooterConverterButtonClicked()
    {
        // Arrange
        using (_driver)
        {
            string expectedUri = "Authorization/Login";
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(Url);
            var homeButton = _driver.FindElement(By.XPath("/html/body/footer/div/a"));

            // Act

            homeButton.Click();

            // Assert
            Assert.Contains(expectedUri, _driver.Url);
        }
    }
}

