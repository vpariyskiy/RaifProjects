using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


namespace RaifProjects
{
    public class SortCountryAndGeozone
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

        //9-1-a
        [Test]
        public void CheckCountriesSort()
        {

            OpenAdminPage();
            var ListOfCountries = new List< string > ();
            Driver?.Navigate().GoToUrl("http://localhost/litecart/admin/?app=countries&doc=countries");
            var countries = Driver?.FindElements(By.XPath(".//form/table/tbody/tr/td[5]"));
            foreach(var country in countries)
            {
                ListOfCountries.Add(country.Text);
            }

            var sortIsCorrect = ListOfCountries.OrderBy(c => c).SequenceEqual(ListOfCountries);
            Assert.IsTrue(sortIsCorrect);
        }

        //9-1-б
        [Test]
        public void CheckTimeZoneCountrySort()
        {

            OpenAdminPage();
            var ListOfCountries = new List<string>();
            var ListOfTimeZones = new List<int>();
            Driver?.Navigate().GoToUrl("http://localhost/litecart/admin/?app=countries&doc=countries");

            var countries = Driver?.FindElements(By.XPath(".//form/table/tbody/tr/td[5]"));
            foreach (var country in countries)
            {
                ListOfCountries.Add(country.Text);
            }
            var timeZones = Driver?.FindElements(By.XPath(".//form/table/tbody/tr/td[6]"));
            foreach (var timezone in timeZones)
            {
                ListOfTimeZones.Add(Convert.ToInt32(timezone.Text));
            }

            foreach (var element in ListOfTimeZones)
            {
                if (element > 0)
                {
                    var index = ListOfTimeZones.IndexOf(element);
                    var ListOfPodcontries = new List<string>();
                    Driver?.FindElement(By.LinkText(ListOfCountries[index])).Click();
                   
                    var podCountries = Driver?.FindElements(By.XPath(".//form/table[2]/tbody/tr/td[3]"));
                    foreach (var podCountry in podCountries)
                    {
                        ListOfPodcontries.Add(podCountry.Text);
                    }
                    ListOfPodcontries.RemoveAt(ListOfPodcontries.IndexOf(ListOfPodcontries.Last()));
                    var sortPodCountriesIsCorrect = ListOfPodcontries.OrderBy(c => c).SequenceEqual(ListOfPodcontries);
                    Assert.IsTrue(sortPodCountriesIsCorrect);

                    Driver?.Navigate().Back();
                }
            }
            
        }

        //9-2
        [Test]
        public void CheckGeoZoneCountrySort()
        {

            OpenAdminPage();
            var ListOfCountries = new List<string>();
            Driver?.Navigate().GoToUrl("http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones");


            var countries = Driver?.FindElements(By.XPath(".//form/table/tbody/tr/td[3]"));
            foreach (var country in countries)
            {
                ListOfCountries.Add(country.Text);
            }

            foreach (var element in ListOfCountries)
            {
                var ListOfGeoZones = new List<string>();
                Driver?.FindElement(By.LinkText($"{element}")).Click();
                
                Thread.Sleep(10000);
               
                    var geoZones = Driver?.FindElements(By.XPath(".//form/table[2]/tbody/tr/td[3]/select/option")).Where(c=>c.GetDomProperty("selected").Equals("True"));
                    foreach (var gz in geoZones)
                    {
                            ListOfGeoZones.Add(gz.Text);
                    }
                    var sortGeoZoneCountriesIsCorrect = ListOfGeoZones.OrderBy(c => c).SequenceEqual(ListOfGeoZones);
                    Assert.IsTrue(sortGeoZoneCountriesIsCorrect);
                    Driver?.Navigate().Back();
                
            }

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
    }
}
