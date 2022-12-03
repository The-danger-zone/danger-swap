
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace DangerSwap.Test.Automated;

public class LoginTest
{
    private readonly string _url = "https://localhost:7145/";
    private readonly IWebDriver _driver = new ChromeDriver();

    [SkippableFact]
    public void PasswordIsShown_AfterEyeIconBeingClicked()
    {
        // Arrange
        using (_driver)
        {
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(_url);
            var passwordInput = _driver.FindElement(By.Id("Password"));
            var eyeIcon = _driver.FindElement(By.Id("toggle-password"));

            // Act
            var inputTypeBeforeClick = passwordInput.GetAttribute("type");
            var isContainingClassBeforeClick = eyeIcon.GetAttribute("class").Contains("fa-eye-slash");
            eyeIcon.Click();
            var inputTypeAfterClick = passwordInput.GetAttribute("type");
            var isContainingClassAfterClick = eyeIcon.GetAttribute("class").Contains("fa-eye-slash");

            // Assert
            Assert.NotEqual(isContainingClassBeforeClick, isContainingClassAfterClick);
            Assert.NotEqual(inputTypeBeforeClick, inputTypeAfterClick);
        }
    }

    [SkippableFact]
    public void ValidationErrorIsShown_AfterTryingToLoginWithNonExistingUser()
    {
        // Arrange
        using (_driver)
        {
            const string expectedUri = "Authorization/Login";
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(_url);
            var passwordInput = _driver.FindElement(By.Id("Password"));
            var emailInput = _driver.FindElement(By.Id("Email"));
            var loginButton = _driver.FindElement(By.XPath("/html/body/div/main/div[2]/div/form/div/div[5]/input[1]"));

            // Act
            emailInput.SendKeys("random@email.com");
            passwordInput.SendKeys("randompsw");
            loginButton.Click();
            var validationErrors = _driver.FindElements(By.ClassName("text-danger"));

            // Assert
            Assert.Equal(_url + expectedUri, _driver.Url);
            Assert.NotEmpty(validationErrors);
        }
    }

    [SkippableFact]
    public void ValidationErrorIsShown_AfterTryingToLoginWithoutPassword()
    {
        // Arrange
        using (_driver)
        {
            const string expectedUri = "Authorization/Login";
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(_url);
            var emailInput = _driver.FindElement(By.Id("Email"));
            var loginButton = _driver.FindElement(By.XPath("/html/body/div/main/div[2]/div/form/div/div[5]/input[1]"));

            // Act
            emailInput.SendKeys("random@email.com");
            loginButton.Click();
            var validationErrors = _driver.FindElements(By.ClassName("text-danger"));
            
            // Assert
            Assert.Equal(_url + expectedUri, _driver.Url);
            Assert.NotEmpty(validationErrors);
        }
    }

    [SkippableFact]
    public void ValidationErrorIsShown_NoDataProvided()
    {
        // Arrange
        using (_driver)
        {
            const string expectedUri = "Authorization/Login";
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(_url);
            var loginButton = _driver.FindElement(By.XPath("/html/body/div/main/div[2]/div/form/div/div[5]/input[1]"));

            // Act
            loginButton.Click();
            var validationErrors = _driver.FindElements(By.ClassName("text-danger"));

            // Assert
            Assert.Equal(_url + expectedUri, _driver.Url);
            Assert.NotEmpty(validationErrors);
        }
    }

    [SkippableFact]
    public void ValidationErrorIsShown_AfterTryingToLoginWithoutEmail()
    {
        // Arrange
        using (_driver)
        {
            const string expectedUri = "Authorization/Login";
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(_url);
            var passwordInput = _driver.FindElement(By.Id("Password"));
            var loginButton = _driver.FindElement(By.XPath("/html/body/div/main/div[2]/div/form/div/div[5]/input[1]"));

            // Act
            passwordInput.SendKeys("randompsw");
            loginButton.Click();
            var validationErrors = _driver.FindElements(By.ClassName("text-danger"));

            // Assert
            Assert.Equal(_url + expectedUri, _driver.Url);
            Assert.NotEmpty(validationErrors);
        }
    }

    [SkippableFact]
    public void RedirectedToConverterPage_AfterSuccessfulLogin()
    {
        // Arrange
        const string expectedTitle = "Home Page - DangerSwap";
        using (_driver)
        {
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(_url);
            var passwordInput = _driver.FindElement(By.Id("Password"));
            var emailInput = _driver.FindElement(By.Id("Email"));
            var loginButton = _driver.FindElement(By.XPath("/html/body/div/main/div[2]/div/form/div/div[5]/input[1]"));

            // Act
            emailInput.SendKeys("admin@admin.com");
            passwordInput.SendKeys("Admin123#");
            loginButton.Click();

            // Assert
            Assert.Equal(_url, _driver.Url);
            Assert.Equal(expectedTitle, _driver.Title);
        }
    }
}
