using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaifProjects.Work19.Base
{
    public class TestBase
    {
        public IWebDriver Driver;
        public TimeSpan timeout = new(0, 0, 10);
        public WebDriverWait wdv;

        [SetUp]
        protected void SetUp()
        {
            Init();
        }

        [TearDown]
        protected void TearDown()
        {
            Driver?.Quit();
        }

        private void Init()
        {
            StartTestBrowser();
            wdv = new(Driver, timeout);
            
        }
        private void StartTestBrowser()
        {

            Driver = new FirefoxDriver();
            Driver.Manage().Window.Maximize();

        }

        public void Refresh()
        {

            Driver.Navigate().Refresh();

        }

        public void Back()
        {
            Driver.Navigate().Back();
        }
        public void OpenHomePage()
        {
            Driver.Navigate().GoToUrl("http://localhost/litecart/en/");
        }
        public void OpenHomePageSpecFlow()
        {
            Init();
            Driver.Navigate().GoToUrl("http://localhost/litecart/en/");
        }
        public void ExitBrowser()
        {
            Driver?.Quit();
        }
    }
}
