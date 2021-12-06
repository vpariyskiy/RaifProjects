using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaifProjects
{
    public class BrowserLogs
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

           // Driver = new FirefoxDriver();
            Driver = new ChromeDriver();
            Driver.Manage().Window.Maximize();

        }
        private void OpenAdminPage()
        {
            Driver?.Navigate().GoToUrl(" http://localhost/litecart/admin/");
            var username = Driver?.FindElement(By.Name("username"));
            username?.Clear();
            username?.SendKeys("admin");
            var password = Driver?.FindElement(By.Name("password"));
            password?.Clear();
            password?.SendKeys("admin");
            var logButon = Driver?.FindElement(By.Name("login"));
            logButon?.Click();
        }

        [Test]
        public void CheckLogs()
        {
            OpenAdminPage();
            Driver?.Navigate().GoToUrl("http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1");

            var products = Driver?.FindElements(By.CssSelector("a[href*='product_id']"));
            var productNames = new List<string>();
            foreach (var p in products)
            {
                productNames.Add(p.Text);
            }

            foreach (var pn in productNames)
            {
                Driver?.FindElement(By.LinkText(pn)).Click();
                Driver?.Navigate().Back();

            }

            var logs = Driver?.Manage().Logs.GetLog("browser");
            Assert.IsTrue(logs.Count == 0);

        }
    }
}
