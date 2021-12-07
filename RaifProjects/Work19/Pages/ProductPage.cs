using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaifProjects.Work19.Pages
{
    public class ProductPage
    {
        public IWebDriver Driver;
        public ProductPage(IWebDriver driver)
        {
            Driver = driver;
        }
        public void AddToCart()
        {
            Driver?.FindElement(By.Name("add_cart_product")).Click();
        }

        public int CartCounter()
        {
            return Convert.ToInt32(Driver?.FindElement(By.CssSelector("span.quantity")).Text);
        }
        public void GoToCheckout()
        {
            Driver?.FindElement(By.CssSelector("div#cart a.link")).Click();
        }
        public void ChechkSizeOptions()
        {
            if (Driver.FindElements(By.Name("options[Size]")).Count >= 1)
                Driver.FindElement(By.Name("options[Size]")).SendKeys("Small");
        }
    }
}
