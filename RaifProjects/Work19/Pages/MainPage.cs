using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaifProjects.Work19.Pages
{
    public class MainPage
    {
        public IWebDriver Driver;
        public MainPage(IWebDriver driver)
        {
            Driver = driver;
        }
        public ReadOnlyCollection<IWebElement> Ducks => Driver.FindElements(By.XPath(".//ul[@class='listing-wrapper products']//li"));

       
        public void ClickToDuck(int iterator)
        {
            Ducks[iterator].FindElement(By.XPath("./a[@class='link']")).Click();
        }
    }
}
