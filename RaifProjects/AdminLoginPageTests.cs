using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace RaifProjects
{
    public class AdminLoginPageTests
    {
        public IWebDriver? Driver;

        [SetUp]
        protected void SetUp()
        {
            StartTestBrowser();
        }

        [TearDown]
        protected void TearDown()
        {
            Driver?.Quit();
        }


        private void StartTestBrowser()
        {

            Driver = new FirefoxDriver();
            Driver.Manage().Window.Maximize();

        }

        [Test]
        public void OpenAdminPage()
        {
            Driver?.Navigate().GoToUrl(" http://localhost/litecart/admin/");
            var username = Driver?.FindElement(By.Name ("username"));
            username?.Clear();
            username?.SendKeys("admin");
            var password = Driver?.FindElement(By.Name("password"));
            password?.Clear();
            password?.SendKeys("admin");
            var logButon = Driver?.FindElement(By.Name("login"));
            logButon?.Click();
        }
    }
}
