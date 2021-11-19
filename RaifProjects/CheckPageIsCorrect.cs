using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaifProjects
{
    public class CheckPageIsCorrect
    {
        public IWebDriver? Driver;
        List<string> MainPage = new();
        List<string> ItemPage = new();

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
           // Driver = new ChromeDriver();

            Driver.Manage().Window.Maximize();

        }

        [Test]
        public void CheckPage()
        {
            
            Driver?.Navigate().GoToUrl("http://localhost/litecart/en/");
            var element = Driver.FindElement(By.XPath(".//div[@id='box-campaigns']//li[1]"));
            GetAllInfo(element, "main");
            MainPage.Add(element.FindElement(By.CssSelector("div.name")).Text);
            element.Click();

            element = Driver.FindElement(By.CssSelector("div#box-product div.information"));
            GetAllInfo(element, "item");
            ItemPage.Add(Driver.FindElement(By.CssSelector("div#box-product h1.title")).Text);

            // на главной странице и на странице товара совпадает текст названия товара
            Assert.IsTrue(MainPage.Last().Equals(ItemPage.Last()));
            //на главной странице и на странице товара совпадают цены (обычная и акционная)
            Assert.IsTrue(MainPage.First().Equals(ItemPage.First()));
            Assert.IsTrue(MainPage[4].Equals(ItemPage[4]));
            //обычная цена зачёркнутая и серая (можно считать, что "серый" цвет это такой, у которого в RGBa представлении одинаковые значения для каналов R, G и B)
            Assert.IsTrue(MainPage[2].Contains("line-through"));

            var checkRGBM= ColorTransform(MainPage[1]);
            Assert.IsTrue(checkRGBM[0].Trim().Equals (checkRGBM[1].Trim()) && checkRGBM[0].Trim().Equals(checkRGBM[2].Trim()));

            Assert.IsTrue(ItemPage[2].Contains("line-through"));

            var checkRGBI = ColorTransform(ItemPage[1]);
            Assert.IsTrue(checkRGBI[0].Trim().Equals(checkRGBI[1].Trim()) && checkRGBI[0].Trim().Equals(checkRGBI[2].Trim()));

            //акционная жирная и красная (можно считать, что "красный" цвет это такой, у которого в RGBa представлении каналы G и B имеют нулевые значения)
            Assert.IsTrue(Convert.ToInt32(MainPage[6])>=700);

            var checkRGBMR = ColorTransform(MainPage[5]);
            Assert.IsTrue(Convert.ToInt32(checkRGBMR[0].Trim())!=0 && Convert.ToInt32(checkRGBMR[1].Trim()) == 0 && Convert.ToInt32(checkRGBMR[2].Trim()) == 0);

            Assert.IsTrue(Convert.ToInt32(ItemPage[6])>=700);

            var checkRGBIR = ColorTransform(ItemPage[5]);
            Assert.IsTrue(Convert.ToInt32(checkRGBIR[0].Trim()) != 0 && Convert.ToInt32(checkRGBIR[1].Trim()) == 0 && Convert.ToInt32(checkRGBIR[2].Trim()) == 0);

            //акционная цена крупнее, чем обычная
            Assert.IsTrue(Convert.ToDouble(MainPage[7].Substring(0,2)) > Convert.ToDouble(MainPage[3].Substring(0,2)));
            Assert.IsTrue(Convert.ToDouble(ItemPage[7].Substring(0,2)) > Convert.ToDouble(ItemPage[3].Substring(0,2)));
        }

        private void GetAllInfo(IWebElement element, string data)
        {
            if (data.Equals("main"))
            {
                MainPage.Add(element.FindElement(By.CssSelector("s.regular-price")).Text);
                MainPage.Add(element.FindElement(By.CssSelector("s.regular-price")).GetCssValue("color"));
                MainPage.Add(element.FindElement(By.CssSelector("s.regular-price")).GetCssValue("text-decoration"));
                MainPage.Add(element.FindElement(By.CssSelector("s.regular-price")).GetCssValue("font-size"));

                MainPage.Add(element.FindElement(By.CssSelector("strong.campaign-price")).Text);
                MainPage.Add(element.FindElement(By.CssSelector("strong.campaign-price")).GetCssValue("color"));
                MainPage.Add(element.FindElement(By.CssSelector("strong.campaign-price")).GetCssValue("font-weight"));
                MainPage.Add(element.FindElement(By.CssSelector("strong.campaign-price")).GetCssValue("font-size"));
            }
            else
            {
                ItemPage.Add(element.FindElement(By.CssSelector("s.regular-price")).Text);
                ItemPage.Add(element.FindElement(By.CssSelector("s.regular-price")).GetCssValue("color"));
                ItemPage.Add(element.FindElement(By.CssSelector("s.regular-price")).GetCssValue("text-decoration"));
                ItemPage.Add(element.FindElement(By.CssSelector("s.regular-price")).GetCssValue("font-size"));

                ItemPage.Add(element.FindElement(By.CssSelector("strong.campaign-price")).Text);
                ItemPage.Add(element.FindElement(By.CssSelector("strong.campaign-price")).GetCssValue("color"));
                ItemPage.Add(element.FindElement(By.CssSelector("strong.campaign-price")).GetCssValue("font-weight"));
                ItemPage.Add(element.FindElement(By.CssSelector("strong.campaign-price")).GetCssValue("font-size"));
            }
        }
        private string[] ColorTransform (string color)
        {
            var vrmStr = color;
            string firstPr, secondPr;
            if (vrmStr.Contains("rgba"))
            {
                 firstPr = vrmStr.Remove(0, 5);
                 secondPr = firstPr.Remove(firstPr.Count() - 1);
                return secondPr.Split(",");
            }
           firstPr = vrmStr.Remove(0, 4);
           secondPr = firstPr.Remove(firstPr.Count() - 1);
            return secondPr.Split(",");
        }
    }
}

