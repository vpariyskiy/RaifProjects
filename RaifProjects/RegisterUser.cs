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
    public class RegisterUser
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
            Driver?.Navigate().GoToUrl("http://localhost/litecart/en/");
        }


        [Test]
        public void UserRegistration()
        {
            var PartUniqEmail = Guid.NewGuid().ToString();
            var Password = "Qwerty123";
            Driver?.FindElement(By.LinkText("New customers click here")).Click();

            InsertData(PartUniqEmail, Password);

            //click button register
            Driver?.FindElement(By.XPath(".//table/tbody/tr[9]/td/button")).Click();

            var noticeSuccess = Driver?.FindElement(By.CssSelector(".notice"));
            Assert.IsTrue(noticeSuccess.Text.Contains("Your customer account has been created"));

            //click logout

            Driver?.FindElement(By.LinkText("Logout")).Click();
            var noticeLogout = Driver?.FindElement(By.CssSelector(".notice"));
            Assert.IsTrue(noticeLogout.Text.Contains("You are now logged out"));

            //click login
          
            var Email = Driver?.FindElement(By.XPath(".//table/tbody/tr[1]/td/input"));
            Email.Clear();
            Email.SendKeys($"{PartUniqEmail}@{PartUniqEmail}.com");

            var Pass = Driver?.FindElement(By.XPath(".//table/tbody/tr[2]/td/input"));
            Pass.Clear();
            Pass.SendKeys($"{Password}");
            
            Driver?.FindElement(By.XPath(".//table/tbody/tr[4]/td/span/button[1]")).Click();
            var noticeLogin = Driver?.FindElement(By.CssSelector(".notice"));
            Assert.IsTrue(noticeLogin.Text.Contains("You are now logged in"));

            //click logout

            Driver?.FindElement(By.LinkText("Logout")).Click();
            noticeLogout = Driver?.FindElement(By.CssSelector(".notice"));
            Assert.IsTrue(noticeLogout.Text.Contains("You are now logged out"));
        }

        private void InsertData(string email, string password)
        {
            var FirstName = Driver?.FindElement(By.XPath(".//table/tbody/tr[2]/td[1]/input"));
            FirstName.Clear();
            FirstName.SendKeys("Ivan");

            var LastName = Driver?.FindElement(By.XPath(".//table/tbody/tr[2]/td[2]/input"));
            LastName.Clear();
            LastName.SendKeys("Ivanov");

            var Address = Driver?.FindElement(By.XPath(".//table/tbody/tr[3]/td[1]/input"));
            Address.Clear();
            Address.SendKeys("Address");

            var Poscode = Driver?.FindElement(By.XPath(".//table/tbody/tr[4]/td[1]/input"));
            Poscode.Clear();
            Poscode.SendKeys("12345");

            var City = Driver?.FindElement(By.XPath(".//table/tbody/tr[4]/td[2]/input"));
            City.Clear();
            City.SendKeys("City");

            
            
            //работа со списком
            Driver?.FindElement(By.CssSelector(".select2-selection__arrow")).Click();

            var textField = Driver?.FindElement(By.CssSelector(".select2-search__field"));
            textField.SendKeys("United States");
            textField.SendKeys(Keys.Enter);
            //

            var Email = Driver?.FindElement(By.XPath(".//table/tbody/tr[6]/td[1]/input"));
            Email.Clear();
            Email.SendKeys($"{email}@{email}.com");

            var Phone = Driver?.FindElement(By.XPath(".//table/tbody/tr[6]/td[2]/input"));
            Phone.Clear();
            Phone.SendKeys($"1234567");
            
            var Pass = Driver?.FindElement(By.XPath(".//table/tbody/tr[8]/td[1]/input"));
            Pass.Clear();
            Pass.SendKeys($"{password}");

            var ConPass = Driver?.FindElement(By.XPath(".//table/tbody/tr[8]/td[2]/input"));
            ConPass.Clear();
            ConPass.SendKeys($"{password}");

        }
    }
}
