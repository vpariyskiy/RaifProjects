using OpenQA.Selenium;
using RaifProjects.Work19.Managers;
using RaifProjects.Work19.Tests;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaifProjects.Work19.Pages
{
    public class CartPage
    {
        public IWebDriver Driver;
        public CartPage(IWebDriver driver)
        {
            Driver = driver;
        }
       
        public int OrderSummaryCounter()
        {
           return Driver.FindElements(By.CssSelector("table.dataTable tr")).Count();
        }
        
        public void RemoveAllItemsFromCart(WorkCartTest Cart)
        {
            int counter = OrderSummaryCounter();

            while (counter > 0)
            {
                Driver.FindElement(By.Name("remove_cart_item")).Click();
                Cart.wdv.Until(x => OrderSummaryCounter() < counter);
                counter = OrderSummaryCounter();
            }   
        }
    }
}
