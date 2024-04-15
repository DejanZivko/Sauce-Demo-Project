using Framework.PageObjects;
using Framework.Utilities;
using OpenQA.Selenium;
using System.Security.Cryptography.X509Certificates;

namespace Framework.Tests
{
    public class Testing : BaseClass
    {
        [Test]
        public void Login_WithInvalidCredentials_ShouldDisplayedValidationMessage()
        {
            LoginPage loginPage = new LoginPage(GetDriver());
            loginPage.GetUsername().SendKeys("Test username");
            loginPage.GetPassword().SendKeys("Test password");
            loginPage.GetLoginButton().Click();

            loginPage.AssertErrorMessage("Epic sadface: Username and password do not match any user in this service", driver);
        }

        [Test]
        public void Login_WithValidCredentils_ShouldSuccessfullyRedirectsUserToProductPage()
        {
            LoginPage loginPage = new LoginPage(GetDriver());
            loginPage.GetUsername().SendKeys("standard_user");
            loginPage.GetPassword().SendKeys("secret_sauce");
            loginPage.GetLoginButton().Click();

            loginPage.AssertSuccessfullyLogin("Products", driver);
        }

        [Test]
        public void Products_SuccessfullyAdded_ToCart()
        {
            LoginPage loginPage = new LoginPage(GetDriver());
            loginPage.ValidLogin("standard_user", "secret_sauce");

            InventoryPage inventoryPage = new InventoryPage(GetDriver());
            inventoryPage.AddProductsToCart();

            inventoryPage.AssertProductsInShoppingBag("3");
        }

        [Test]
        public void User_RedirectsTo_CartPage()
        {

        }
    }
}
