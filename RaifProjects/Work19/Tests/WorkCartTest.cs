using NUnit.Framework;
using RaifProjects.Work19.Base;
using RaifProjects.Work19.Managers;

namespace RaifProjects.Work19.Tests
{
    public class WorkCartTest:TestBase
    {
        public WorkCartTest Cart;
        public ApplicationManager AppManager => new(Driver);
        public WorkCartTest():base()
        {
            Cart = this;
        }
        [Test]
        public void WorkWithCart ()
        {
            int counter = default;
            Cart.OpenHomePage();
            
            for (var i=0;i<3;i++)
            {
                AppManager.mainPage.ClickToDuck(i);
                Cart.Refresh();
                AppManager.productPage.ChechkSizeOptions();

                AppManager.productPage.AddToCart();
                if (i == 0)
                   counter = 0;
                
                    Cart.wdv.Until(x=>AppManager.productPage.CartCounter() == counter + 1);
                   counter += 1;
                Cart.Back();
            }
            counter = 0;
            AppManager.productPage.GoToCheckout();
            counter = AppManager.cartPage.OrderSummaryCounter();
            while (counter > 0)
            {
                AppManager.cartPage.RemoveItemFromCart();
                Cart.wdv.Until(x => AppManager.cartPage.OrderSummaryCounter() < counter);
                counter = AppManager.cartPage.OrderSummaryCounter();
            }
            Cart.Back();
        }
    }
}
