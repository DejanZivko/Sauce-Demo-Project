using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Linq;
using WebDriverManager.DriverConfigs.Impl;

namespace SauceDemo
{
    public class LocatorsTests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Url = "https://www.saucedemo.com/";
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void InavlidLogin()
        {
            driver.FindElement(By.Id("user-name")).SendKeys("test@gmail.com");
            driver.FindElement(By.Name("password")).SendKeys("deuce");
            driver.FindElement(By.CssSelector("input[type='submit']")).Click();
            String errorMessage = driver.FindElement(By.XPath("//h3[@data-test='error']")).Text;
            string expectedErrorMessage = "Epic sadface: Username and password do not match any user in this service";
            Assert.That(errorMessage, Is.EqualTo(expectedErrorMessage));
        }

        [Test]
        public void Login_WithValidCredentils_ShouldSuccessfullyRedirectsUserToProductPage() 
        {
            driver.FindElement(By.XPath("//input[@id='user-name']")).SendKeys("standard_user");
            driver.FindElement(By.XPath("//input[@id='password']")).SendKeys("secret_sauce");
            driver.FindElement(By.XPath("//input[@id='login-button']")).Click();

            String inventoryPageText = driver.FindElement(By.XPath("//span[@class='title']")).Text;
            TestContext.Progress.WriteLine(inventoryPageText);
            string expectedInventoryPageText = "Products";
            Assert.That(inventoryPageText, Is.EqualTo(expectedInventoryPageText));
        }

        [Test]
        public void Products_SuccessfullyAdded_ToCart()//Inventory Page
        {
            // TODO:Pozvati fju za validno logavanje
            driver.FindElement(By.XPath("//input[@id='user-name']")).SendKeys("standard_user");
            driver.FindElement(By.XPath("//input[@id='password']")).SendKeys("secret_sauce");
            driver.FindElement(By.XPath("//input[@id='login-button']")).Click();

            String[] productsToBuy = { "Sauce Labs Backpack", "Sauce Labs Fleece Jacket", "Sauce Labs Onesie" };
            IList<IWebElement> allProducts = driver.FindElements(By.XPath("//div[@class='inventory_item']"));

            foreach (IWebElement product in allProducts)
            {
                if (productsToBuy.Contains(product.FindElement(By.CssSelector(".inventory_item_label a")).Text))
                {
                    product.FindElement(By.CssSelector(".btn_inventory")).Click();
                }
                //TestContext.Progress.WriteLine(product.FindElement(By.CssSelector(".inventory_item_label a")).Text);
            }

            driver.FindElement(By.CssSelector("div[id='shopping_cart_container']")).Click();

            //Cart page
            IWebElement actualBtnCheckout = driver.FindElement(By.Id("checkout"));
            String actualBtnText = actualBtnCheckout.Text;
            string expectedBtnText = "Checkout";

            Assert.That(actualBtnText, Is.EqualTo(expectedBtnText));

            // Get the list of products in the cart
            IList<IWebElement> cartProducts = driver.FindElements(By.CssSelector(".inventory_item_name"));
            List<string> productNamesInCart = new List<string>();
            foreach (IWebElement product in cartProducts)
            {
                productNamesInCart.Add(product.Text);
                //string productName = product.Text;
                //Console.WriteLine(productName);             
            }

            //Assert.That(productNamesInCart, Is.EqualTo(productsToBuy));
            Assert.That(productNamesInCart, Is.EquivalentTo(productsToBuy));

            IWebElement cartBadgeElement = driver.FindElement(By.CssSelector(".shopping_cart_badge"));
            string actualCartItem = cartBadgeElement.Text;
            string expectedItemInShoppingBag = "3";

            Assert.That(actualCartItem, Is.EqualTo(expectedItemInShoppingBag));

            IList<IWebElement> productQuantity = driver.FindElements(By.CssSelector(".cart_item .cart_quantity"));
            List<string> selectedQuantity = new List<string>();
            foreach (IWebElement quantity in productQuantity)
            {
                selectedQuantity.Add(quantity.Text);
                Console.WriteLine(selectedQuantity);
            }

            String[] expectedQuanity = ["1", "1", "1"];

            //Assert.That(selectedQuantity, Is.EqualTo(expectedQuanity));
            Assert.That(selectedQuantity, Is.EquivalentTo(expectedQuanity));

            //Navigate to Checkout Page - Step 1
            actualBtnCheckout.Click();
            string expectedCheckoutTitle = "Checkout: Your Information";
            IWebElement actualCheckoutTitle = driver.FindElement(By.ClassName("title"));
            string actualTitle = actualCheckoutTitle.Text;
            //Console.WriteLine(actualTitle);
            Assert.That(actualTitle, Is.EqualTo(expectedCheckoutTitle));

            //Navigate to Checkout Page - Step 1 -> Fill out the form
            driver.FindElement(By.Id("first-name")).SendKeys("Dejan");
            driver.FindElement(By.Id("last-name")).SendKeys("Zivkovic");
            driver.FindElement(By.Id("postal-code")).SendKeys("10087");
            driver.FindElement(By.CssSelector("input[type='submit']")).Click();

            //Navigate to Checkout Page - Step 2
            string exptectedOverviewTitle = "Checkout: Overview";
            IWebElement actualOverviewTitle = driver.FindElement(By.XPath("//span[@class='title']"));
            string actualCheckoutStepTwoText = actualOverviewTitle.Text;
            Assert.That(actualCheckoutStepTwoText, Is.EqualTo(exptectedOverviewTitle));

            driver.FindElement(By.Id("finish")).Click();

            //Navigate to checkout complete page
            string exptectedNotification = "Thank you for your order!";
            IWebElement actualCompleteOrderNotification = driver.FindElement(By.ClassName("complete-header"));
            string actualOrderNotification = actualCompleteOrderNotification.Text;
            //Console.WriteLine(actualOrderNotification);
            Assert.That(actualOrderNotification, Is.EqualTo(exptectedNotification));
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Dispose();
        }
    }
}