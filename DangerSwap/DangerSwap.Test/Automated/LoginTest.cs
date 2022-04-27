
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using Xunit;

namespace DangerSwap.Test.Automated
{
    public class LoginTest
    {
        protected readonly string Url = "https://localhost:7145/";
        protected readonly IWebDriver _driver = new ChromeDriver();

        [SkippableFact]
        public void PasswordIsShown_AfterEyeIconBeingClicked()
        {
            // Arrange
            using (_driver)
            {
                _driver.Manage().Window.Maximize();
                _driver.Navigate().GoToUrl(Url);
                var passwordInput = _driver.FindElement(By.Id("Password"));
                var eyeIcon = _driver.FindElement(By.Id("toggle-password"));

                // Act
                string inputTypeBeforeClick = passwordInput.GetAttribute("type");
                bool isContainingClassBeforeClick = eyeIcon.GetAttribute("class").Contains("fa-eye-slash");
                eyeIcon.Click();
                string inputTypeAfterClick = passwordInput.GetAttribute("type");
                bool isContainingClassAfterClick = eyeIcon.GetAttribute("class").Contains("fa-eye-slash");


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
                string expectedUri = "Authorization/Login";
                _driver.Manage().Window.Maximize();
                _driver.Navigate().GoToUrl(Url);
                var passwordInput = _driver.FindElement(By.Id("Password"));
                var emailInput = _driver.FindElement(By.Id("Email"));
                var loginButton = _driver.FindElement(By.XPath("/html/body/div/main/div[2]/div/form/div/div[5]/input[1]"));

                // Act
                emailInput.SendKeys("random@email.com");
                passwordInput.SendKeys("randompsw");
                loginButton.Click();
                var validationErrors = _driver.FindElements(By.ClassName("text-danger"));


                // Assert
                Assert.Equal(Url + expectedUri, _driver.Url);
                Assert.NotEqual(0, validationErrors.Count);
            }
        }

        
        [SkippableFact]
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
    }
}
