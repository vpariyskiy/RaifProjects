using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaifProjects
{
    public class AddNewProduct
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
        public void AddNewItem()
        {
            OpenAdminPage();

            var NameNewProduct = Guid.NewGuid().ToString();

            Driver?.FindElement(By.LinkText("Catalog")).Click();
            Driver?.FindElement(By.LinkText("Add New Product")).Click();

            Driver?.FindElement(By.Name("status")).Click();
            

            FillData(Driver, "name[en]", NameNewProduct);
            FillData(Driver, "quantity", "15");
            FillData(Driver, "code", "this is code");
            FillData(Driver, "product_groups[]", "1-1");

            Driver?.FindElement(By.Name("new_images[]")).SendKeys($"{Environment.CurrentDirectory}" + @"\duck.png"); 

            //FillData(Driver, "new_images[]", @"\duck.png");
            FillData(Driver, "date_valid_from", "2021-11-25");
            FillData(Driver, "date_valid_to", "2022-11-25");

            //Вкладка Information
            Driver?.FindElement(By.LinkText("Information")).Click();
            Driver?.FindElement(By.XPath("//div[@id='tab-information']//select[normalize-space(.)='-- Select -- ACME Corp.']//option[2]")).Click();

            FillData(Driver, "keywords", "orange_duck");
            FillData(Driver, "short_description[en]", "o_d");

                     

            //Вкладка Prices
            Driver?.FindElement(By.LinkText("Prices")).Click();
            Driver?.FindElement(By.XPath("//div[@id='tab-prices']/table[1]/tbody/tr/td/select//option[2]")).Click();
            FillData(Driver, "prices[USD]", "180");

            //
            Driver?.FindElement(By.Name("save")).Click();

            

            //Проверка

            Driver?.FindElement(By.LinkText("Catalog")).Click();
            var findStr = "//a[text()='" + NameNewProduct + "']";
            var countOfFindElement = Driver?.FindElements(By.XPath($"{findStr}")).Count;
            Assert.IsTrue(countOfFindElement == 1);
           
        }



        private void FillData(IWebDriver Driver,string elName, string Data)
        {
            Driver?.FindElement(By.Name($"{elName}")).Click();
            Driver?.FindElement(By.Name($"{elName}")).SendKeys($"{Data}");
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
