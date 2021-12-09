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
            
            Cart.OpenHomePage();
            
            for (var i=0;i<3;i++)
            {
                AppManager.mainPage.ClickToDuck(i);
                
                Cart.Refresh();
               
                AppManager.productPage.AddToCart(i,Cart);
               
                Cart.Back();
            }
           
            AppManager.productPage.GoToCheckout();
           
            AppManager.cartPage.RemoveAllItemsFromCart(Cart);
             
            Cart.Back();
        }
    }
}
