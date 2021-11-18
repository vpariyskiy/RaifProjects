using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;


namespace RaifProjects
{
    public class AdminMenuTest
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
           // Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);

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

        [Test]
        public void MenuClick()
        {
            OpenAdminPage();
            var listOfMenuElementsCount = Driver?.FindElements(By.CssSelector("#box-apps-menu > li")).Count;

            if (listOfMenuElementsCount > 0)
            {
                
                for (int list = 0; list < listOfMenuElementsCount; list++)
                {
                    var element = Driver?.FindElements(By.CssSelector("#box-apps-menu > li"))[list];

                    element?.Click();

                    var headerElement = Driver?.FindElement(By.TagName("h1"));

                    

                    Assert.IsNotNull(headerElement);

                    var podElementsCount = Driver?.FindElements(By.CssSelector(".docs > *")).Count;

                    if (podElementsCount > 0)
                    {
                        for (int podlist = 0; podlist < podElementsCount; podlist++)
                        {
                            var podElement = Driver?.FindElements(By.CssSelector(".docs > *"))[podlist];

                            var clickElement = Driver?.FindElement(By.LinkText($"{podElement?.Text}"));

                            clickElement?.Click();

                            var headerPodElement = Driver?.FindElement(By.TagName("h1"));

                           
                            Assert.IsNotNull(headerPodElement);
                        }
                    }
                }
            }

        }


    }
}
