using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaifProjects
{
    public class NewWindowLink
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
        public void CheckWindow()
        {
            TimeSpan timeout = new TimeSpan(0, 0, 10);
            OpenAdminPage();
            Driver?.Navigate().GoToUrl("http://localhost/litecart/admin/?app=countries&doc=countries");
            Driver?.FindElement(By.CssSelector("[title='Edit']")).Click();
            var existingWindow = Driver.CurrentWindowHandle;
            string? externalWindow=default;
            List<IWebElement> links = Driver.FindElements(By.ClassName("fa-external-link")).ToList();

            for (int i = 0; i < links.Count(); i++)
            {
                links[i].Click();

                var wdv = new WebDriverWait(Driver, timeout);
                wdv.Until(x=>x.WindowHandles.Count==2);
                foreach(var z in Driver.WindowHandles)
                {
                    if (z != existingWindow)
                    {
                         externalWindow = z;
                    }

                }
                Driver.SwitchTo().Window(externalWindow);
                Driver.Close();
                Driver.SwitchTo().Window(existingWindow);
               
            }
        }
    }
}
