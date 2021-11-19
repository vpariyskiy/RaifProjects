using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;


namespace RaifProjects
{
    public class StickerTest
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
            Driver?.Navigate().GoToUrl(" http://localhost/litecart/en/");
        }

        [Test]
        public void CheckSticker()
        {
            var total = 0;
            var ducks = Driver?.FindElements(By.CssSelector("ul.listing-wrapper.products>li"));

            if (ducks.Count>0)
            foreach (var duck in ducks)
            {
                var stickerPop = duck.FindElements(By.XPath(".//div[contains(@class,'sticker')]"));
                Assert.IsTrue(stickerPop.Count == 1);
                    total++;
            }
            Assert.IsTrue(ducks.Count == total);


        }
    }
}
