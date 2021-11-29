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
    public class WorkCart
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
        public void WorkWithCart()
        {
            Driver?.Navigate().GoToUrl("http://localhost/litecart/en/");
            int counter = default;
            TimeSpan timeout = new TimeSpan(0, 0, 10);
            for (var i =0;i<3;i++)
            {            
                var ducks = Driver?.FindElements(By.XPath(".//ul[@class='listing-wrapper products']//li"));
                ducks[i].FindElement(By.XPath("./a[@class='link']")).Click();
                Driver?.Navigate().Refresh();
                Driver?.FindElement(By.Name("add_cart_product")).Click();
               
                if (i == 0)
                    counter = 0;
               
                var wdv = new WebDriverWait(Driver, timeout);
                wdv.Until(x => Convert.ToInt32(x.FindElement(By.CssSelector("span.quantity")).Text) == counter + 1);
                counter += 1;
                Driver?.Navigate().Back();
            }
            counter = 0;
            Driver?.FindElement(By.CssSelector("div#cart a.link")).Click();
            counter = Driver.FindElements(By.CssSelector("table.dataTable tr")).Count();

            while (counter>0)
            {
                Driver?.FindElement(By.Name("remove_cart_item")).Click();
                var wdv = new WebDriverWait(Driver, timeout);
                wdv.Until(x => x.FindElements(By.CssSelector("table.dataTable tr")).Count() < counter);
                counter = Driver.FindElements(By.CssSelector("table.dataTable tr")).Count();
            }
            Driver?.Navigate().Back();
        }
    }
}
