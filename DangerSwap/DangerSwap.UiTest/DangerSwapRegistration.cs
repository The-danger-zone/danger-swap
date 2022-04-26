using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace DangerSwap.UiTest
{
    public class DangerSwapRegistrationTest
    {
        public const string RegistrationUrl = "https://localhost:7145/Authorization/Registration";
        public const string SuccessfulRegistrationUrl = "https://localhost:7145/Authorization/Register";

        [Fact]
        public void LoadRegistrationPage()
        {
            var options = new OpenQA.Selenium.Chrome.ChromeOptions();
            options.BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
            using (IWebDriver driver = new OpenQA.Selenium.Chrome.ChromeDriver(options))
            {
                driver.Navigate().GoToUrl(RegistrationUrl);
            }
        }
        [Fact]
        public void NoFieldsFilled()
        {
            var options = new OpenQA.Selenium.Chrome.ChromeOptions();
            options.BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
            using (IWebDriver driver = new OpenQA.Selenium.Chrome.ChromeDriver(options))
            {
                driver.Navigate().GoToUrl(RegistrationUrl);
                driver.FindElement(By.ClassName("btn-primary")).Click();
                var validationErrors = driver.FindElements(By.ClassName("text-danger"));
                Assert.Equal(6, validationErrors.Count);
            }
        }
        [Fact]
        public void NoUsernameEntered()
        {
            var options = new OpenQA.Selenium.Chrome.ChromeOptions();
            options.BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
            using (IWebDriver driver = new OpenQA.Selenium.Chrome.ChromeDriver(options))
            {
                driver.Navigate().GoToUrl(RegistrationUrl);
                var validationErrors = driver.FindElements(By.ClassName("text-danger"));
                var inputFields = driver.FindElements(By.ClassName("form-control"));
                inputFields[1].SendKeys("aa@aa.aa");
                inputFields[2].SendKeys("!A123a456");
                IWebElement nationality = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div/div[5]/select"));
                SelectElement selectNationality = new SelectElement(nationality);
                selectNationality.SelectByIndex(2);
                IWebElement citizenship = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div/div[4]/select"));
                SelectElement selectCitizenship = new SelectElement(citizenship);
                selectCitizenship.SelectByIndex(2);
                IWebElement datePicker = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div/div[6]/input"));
                datePicker.SendKeys("07061995");
                driver.FindElement(By.ClassName("btn-primary")).Click();
                Assert.Equal("The UserName field is required.", driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div/div[1]/span")).Text);
            }
        }

        [Fact]
        public void NoEmailEntered()
        {
            var options = new OpenQA.Selenium.Chrome.ChromeOptions();
            options.BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
            using (IWebDriver driver = new OpenQA.Selenium.Chrome.ChromeDriver(options))
            {
                driver.Navigate().GoToUrl(RegistrationUrl);
                var validationErrors = driver.FindElements(By.ClassName("text-danger"));
                var inputFields = driver.FindElements(By.ClassName("form-control"));
                inputFields[0].SendKeys("trewsad");
                inputFields[2].SendKeys("!A123a456");
                IWebElement nationality = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div/div[5]/select"));
                SelectElement selectNationality = new SelectElement(nationality);
                selectNationality.SelectByIndex(2);
                IWebElement citizenship = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div/div[4]/select"));
                SelectElement selectCitizenship = new SelectElement(citizenship);
                selectCitizenship.SelectByIndex(2);
                IWebElement datePicker = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div/div[6]/input"));
                datePicker.SendKeys("07061995");
                driver.FindElement(By.ClassName("btn-primary")).Click();
                Assert.Equal("The Email field is required.", driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div/div[2]/span")).Text);
            }
        }

        [Fact]
        public void NoPasswordEntered()
        {
            var options = new OpenQA.Selenium.Chrome.ChromeOptions();
            options.BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
            using (IWebDriver driver = new OpenQA.Selenium.Chrome.ChromeDriver(options))
            {
                driver.Navigate().GoToUrl(RegistrationUrl);
                var validationErrors = driver.FindElements(By.ClassName("text-danger"));
                var inputFields = driver.FindElements(By.ClassName("form-control"));
                inputFields[0].SendKeys("trewsad");
                inputFields[1].SendKeys("asdasd@asda.lt");
                IWebElement nationality = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div/div[5]/select"));
                SelectElement selectNationality = new SelectElement(nationality);
                selectNationality.SelectByIndex(2);
                IWebElement citizenship = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div/div[4]/select"));
                SelectElement selectCitizenship = new SelectElement(citizenship);
                selectCitizenship.SelectByIndex(2);
                IWebElement datePicker = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div/div[6]/input"));
                datePicker.SendKeys("07061995");
                driver.FindElement(By.ClassName("btn-primary")).Click();
                Assert.Equal("The Password field is required.", driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div/div[3]/span")).Text);
            }
        }

        [Fact]
        public void PasswordToShort()
        {
            var options = new OpenQA.Selenium.Chrome.ChromeOptions();
            options.BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
            using (IWebDriver driver = new OpenQA.Selenium.Chrome.ChromeDriver(options))
            {
                driver.Navigate().GoToUrl(RegistrationUrl);
                var validationErrors = driver.FindElements(By.ClassName("text-danger"));
                var inputFields = driver.FindElements(By.ClassName("form-control"));
                inputFields[0].SendKeys("trewsad");
                inputFields[1].SendKeys("asdasd@asda.lt");
                inputFields[2].SendKeys("a");
                IWebElement nationality = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div/div[5]/select"));
                SelectElement selectNationality = new SelectElement(nationality);
                selectNationality.SelectByIndex(2);
                IWebElement citizenship = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div/div[4]/select"));
                SelectElement selectCitizenship = new SelectElement(citizenship);
                selectCitizenship.SelectByIndex(2);
                IWebElement datePicker = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div/div[6]/input"));
                datePicker.SendKeys("07061995");
                driver.FindElement(By.ClassName("btn-primary")).Click();
                Assert.Equal("The field Password must be a string or array type with a minimum length of '8'.", driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div/div[3]/span")).Text);
            }
        }

        [Fact]
        public void NoCitizenshipSelected()
        {
            var options = new OpenQA.Selenium.Chrome.ChromeOptions();
            options.BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
            using (IWebDriver driver = new OpenQA.Selenium.Chrome.ChromeDriver(options))
            {
                driver.Navigate().GoToUrl(RegistrationUrl);
                var validationErrors = driver.FindElements(By.ClassName("text-danger"));
                var inputFields = driver.FindElements(By.ClassName("form-control"));
                inputFields[0].SendKeys("trewsad");
                inputFields[1].SendKeys("asdasd@asda.lt");
                inputFields[2].SendKeys("aaaaaaaa");
                IWebElement nationality = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div/div[5]/select"));
                SelectElement selectNationality = new SelectElement(nationality);
                selectNationality.SelectByIndex(2);
                IWebElement datePicker = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div/div[6]/input"));
                datePicker.SendKeys("07061995");
                driver.FindElement(By.ClassName("btn-primary")).Click();
                Assert.Equal("The Citizenship field is required.", driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div/div[4]/span")).Text);
            }
        }

        [Fact]
        public void NoNationalitySelected()
        {
            var options = new OpenQA.Selenium.Chrome.ChromeOptions();
            options.BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
            using (IWebDriver driver = new OpenQA.Selenium.Chrome.ChromeDriver(options))
            {
                driver.Navigate().GoToUrl(RegistrationUrl);
                var validationErrors = driver.FindElements(By.ClassName("text-danger"));
                var inputFields = driver.FindElements(By.ClassName("form-control"));
                inputFields[0].SendKeys("trewsad");
                inputFields[1].SendKeys("asdasd@asda.lt");
                inputFields[2].SendKeys("aaaaaaaa");
                IWebElement citizenship = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div/div[4]/select"));
                SelectElement selectCitizenship = new SelectElement(citizenship);
                selectCitizenship.SelectByIndex(2);
                IWebElement datePicker = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div/div[6]/input"));
                datePicker.SendKeys("07061995");
                driver.FindElement(By.ClassName("btn-primary")).Click();
                Assert.Equal("The Nationality field is required.", driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div/div[5]/span")).Text);
            }
        }

        [Fact]
        public void DateNotSelected()
        {
            var options = new OpenQA.Selenium.Chrome.ChromeOptions();
            options.BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
            using (IWebDriver driver = new OpenQA.Selenium.Chrome.ChromeDriver(options))
            {
                driver.Navigate().GoToUrl(RegistrationUrl);
                var validationErrors = driver.FindElements(By.ClassName("text-danger"));
                var inputFields = driver.FindElements(By.ClassName("form-control"));
                inputFields[0].SendKeys("trewsad");
                inputFields[1].SendKeys("asdasd@asda.lt");
                inputFields[2].SendKeys("aaaaaaaa");
                IWebElement nationality = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div/div[5]/select"));
                SelectElement selectNationality = new SelectElement(nationality);
                selectNationality.SelectByIndex(2);
                IWebElement citizenship = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div/div[4]/select"));
                SelectElement selectCitizenship = new SelectElement(citizenship);
                selectCitizenship.SelectByIndex(2);
                driver.FindElement(By.ClassName("btn-primary")).Click();
                Assert.Equal("The value '' is invalid.", driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div/div[6]/span")).Text);
            }
        }

        [Fact]
        public void AllDataIsCorrect()
        {
            var options = new OpenQA.Selenium.Chrome.ChromeOptions();
            options.BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
            using (IWebDriver driver = new OpenQA.Selenium.Chrome.ChromeDriver(options))
            {
                driver.Navigate().GoToUrl(RegistrationUrl);
                var validationErrors = driver.FindElements(By.ClassName("text-danger"));
                var inputFields = driver.FindElements(By.ClassName("form-control"));
                inputFields[0].SendKeys("trewsad");
                inputFields[1].SendKeys("asdasd@asda.lt");
                inputFields[2].SendKeys("!A1aaaaa");
                IWebElement nationality = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div/div[5]/select"));
                SelectElement selectNationality = new SelectElement(nationality);
                selectNationality.SelectByIndex(2);
                IWebElement citizenship = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div/div[4]/select"));
                SelectElement selectCitizenship = new SelectElement(citizenship);
                selectCitizenship.SelectByIndex(2);
                IWebElement datePicker = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div/div[6]/input"));
                datePicker.SendKeys("07061995");
                driver.FindElement(By.ClassName("btn-primary")).Click();
                Assert.Equal(SuccessfulRegistrationUrl, driver.Url);
            }
        }

        [Fact]
        public void IncorectPasswordPattern()
        {
            var options = new OpenQA.Selenium.Chrome.ChromeOptions();
            options.BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
            using (IWebDriver driver = new OpenQA.Selenium.Chrome.ChromeDriver(options))
            {
                driver.Navigate().GoToUrl(RegistrationUrl);
                var validationErrors = driver.FindElements(By.ClassName("text-danger"));
                var inputFields = driver.FindElements(By.ClassName("form-control"));
                inputFields[0].SendKeys("trewsad");
                inputFields[1].SendKeys("asdasd@asda.lt");
                inputFields[2].SendKeys("afsaaaaa");
                IWebElement nationality = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div/div[5]/select"));
                SelectElement selectNationality = new SelectElement(nationality);
                selectNationality.SelectByIndex(2);
                IWebElement citizenship = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div/div[4]/select"));
                SelectElement selectCitizenship = new SelectElement(citizenship);
                selectCitizenship.SelectByIndex(2);
                IWebElement datePicker = driver.FindElement(By.XPath("/html/body/div/main/div/div/form/div/div[6]/input"));
                datePicker.SendKeys("07061995");
                driver.FindElement(By.ClassName("btn-primary")).Click();
                Assert.Equal(SuccessfulRegistrationUrl, driver.Url);
            }
        }
    }
}
