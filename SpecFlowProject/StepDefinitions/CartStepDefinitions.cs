using RaifProjects.Work19.Base;
using RaifProjects.Work19.Managers;
using RaifProjects.Work19.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject.StepDefinitions
{
    [Binding]
    public class CartStepDefinitions:TestBase
    {
        public WorkCartTest Cart;
        ApplicationManager AppManager;
        public CartStepDefinitions()
        {
            Cart = new WorkCartTest();
        }

        [Given("EmptyCart")]
        public void EmptyCart()
        {
            Cart.OpenHomePageSpecFlow();
        }

        [When("AddNewItems")]
        public void AddNewItems()
        {
            AppManager = new(Cart.Driver);
            for (var i = 0; i < 3; i++)
            {
                AppManager.mainPage.ClickToDuck(i);

                Cart.Refresh();

                AppManager.productPage.AddToCart(i, Cart);

                Cart.Back();
            }
        }

        [Then("DeleteAllItems")]
        public void DeleteAllItems()
        {
            AppManager.productPage.GoToCheckout();

            AppManager.cartPage.RemoveAllItemsFromCart(Cart);

            Cart.Back();
            Cart.ExitBrowser();
        }
    }
}
