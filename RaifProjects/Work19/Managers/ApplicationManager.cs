using OpenQA.Selenium;
using RaifProjects.Work19.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaifProjects.Work19.Managers
{
    public class ApplicationManager
    {
        public ProductPage productPage;
        public MainPage mainPage;
        public CartPage cartPage;

        public ApplicationManager(IWebDriver Driver)
        {
            productPage = new(Driver);
            mainPage = new(Driver);
            cartPage = new(Driver);
        }
    }
}
