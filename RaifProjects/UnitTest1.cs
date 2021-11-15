using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace RaifProjects
{
    public class Tests
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
        public void OpenYandexPage()
        {
            Driver?.Navigate().GoToUrl("https://www.yandex.ru");
        }
    }
}