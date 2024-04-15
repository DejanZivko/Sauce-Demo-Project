using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.PageObjects
{
    public class InventoryPage
    {
        //[FindsBy(How = How.CssSelector, Using = ".btn_inventory")]
        //private IWebElement addToCartBtn;

        //[FindsBy(How = How.CssSelector, Using = ".inventory_item_label a")]
        //private IWebElement productName;

        //[FindsBy(How = How.XPath, Using = "//div[@class='inventory_item']")]
        //private IWebElement products;

        private IWebDriver driver;
        private readonly string[] productsToBuy = { "Sauce Labs Backpack", "Sauce Labs Fleece Jacket", "Sauce Labs Onesie" };
        public InventoryPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public void AddProductsToCart()
        {
            IList<IWebElement> allProducts = driver.FindElements(By.XPath("//div[@class='inventory_item']"));

            foreach (IWebElement product in allProducts)
            {
                string productName = product.FindElement(By.CssSelector(".inventory_item_label a")).Text;

                if (productsToBuy.Contains(productName))
                {
                    product.FindElement(By.CssSelector(".btn_inventory")).Click();
                }
            }
        }

        //IWebElement cartBadgeElement = driver.FindElement(By.CssSelector(".shopping_cart_badge"));
        //string actualCartItem = cartBadgeElement.Text;
        //string expectedItemInShoppingBag = "3";

        //Assert.That(actualCartItem, Is.EqualTo(expectedItemInShoppingBag));

        [FindsBy(How = How.CssSelector, Using = ".shopping_cart_badge")]
        private IWebElement actualProductsInShoppingBag;
        public string GetAllProductsFromBag()
        {
            //var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            //wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[@class='shopping_cart_badge']")));
            return actualProductsInShoppingBag.Text;
        }
        public void AssertProductsInShoppingBag(string expectedItemInBag)
        {
            Assert.That(GetAllProductsFromBag(), Is.EqualTo(expectedItemInBag));
        }      
    }
}
