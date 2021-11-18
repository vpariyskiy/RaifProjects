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
            var mostpopular = Driver?.FindElements(By.CssSelector("#box-most-popular > div>ul> li"));

            if (mostpopular!=null)
            foreach (var duck in mostpopular)
            {
                var stickerPop = duck.FindElements(By.XPath(".//div[starts-with(@class,'sticker')]"));
                Assert.IsTrue(stickerPop.Count == 1);
            }

            var campaigns = Driver?.FindElements(By.CssSelector("#box-campaigns > div>ul> li"));

            if (campaigns!=null)
            foreach (var duck in campaigns)
            {
                var stickerCam = duck.FindElements(By.XPath(".//div[starts-with(@class,'sticker')]"));
                Assert.IsTrue(stickerCam.Count == 1);
            }

            var latestProducts = Driver?.FindElements(By.CssSelector("#box-latest-products > div>ul> li"));

            if (latestProducts != null)
            foreach (var duck in latestProducts)
            {
                var stickerLP = duck.FindElements(By.XPath(".//div[starts-with(@class,'sticker')]"));
                //foreach (var a in stickerLP)
                //{
                //    var ff = a.Text;
                //}
                Assert.IsTrue(stickerLP.Count == 1);
            }

        }
    }
}
