using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using Xunit;

namespace DangerSwap.Test.Automated;

public sealed class ConverterTest
{
    private readonly string Url = "https://localhost:7145/";
    private readonly IWebDriver _driver = new ChromeDriver();

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
    public void Logout_AfterUserSuccessfullyLoggedIn()
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
            var dropdownMenuButton = _driver.FindElement(By.Id("dropdownMenuButton"));
            dropdownMenuButton.Click();
            var logoutButton = _driver.FindElement(By.XPath("/html/body/header/nav/div/div[2]/div/div/form/button"));
            logoutButton.Click();


            // Assert
            Assert.Equal(Url + expectedUri, _driver.Url);
        }
    }

    [Theory(Skip = "Automation test")]
    [MemberData(nameof(ConverterData))]
    public void ConvertCurrency(int firstOption, int secondOption, int amount, bool isExpectedToBeConverted)
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
            var fiatCurrencyDropDown = _driver.FindElement(By.Id("TransactionCurrency_FromId"));
            var cryptoCurrencyDropDown = _driver.FindElement(By.Id("TransactionCurrency_ToId"));
            var amountInput = _driver.FindElement(By.Id("Amount"));
            var convertButton = _driver.FindElement(By.XPath("/html/body/div/main/form/div[3]/input"));
            SelectElement fiatCurrencySelect = new SelectElement(fiatCurrencyDropDown);
            fiatCurrencySelect.Options[firstOption].Click();
            SelectElement cryptoCurrencySelect = new SelectElement(cryptoCurrencyDropDown);
            cryptoCurrencySelect.Options[secondOption].Click();
            amountInput.SendKeys(amount.ToString());
            convertButton.Click();
            var equivalent = _driver.FindElement(By.XPath("/html/body/div/main/form/div[2]/input[2]"));
            bool isConverted = equivalent.GetAttribute("Value") != "0.";

            // Assert
            Assert.Equal(isConverted, isExpectedToBeConverted);
        }
    }

    private static IEnumerable<object[]> ConverterData()
    {
        yield return new object[] {
                1,
                1,
                40000,
                true
            };
        yield return new object[] {
                0,
                1,
                40000,
                false,
            };
        yield return new object[] {
                1,
                0,
                40000,
                false,
            };
        yield return new object[] {
                0,
                0,
                40000,
                false,
            };
        yield return new object[] {
                0,
                0,
                0,
                false,
            };
    }
}

