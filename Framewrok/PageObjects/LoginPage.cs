using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.PageObjects
{
    public class LoginPage
    {
        //driver.FindElement(By.Id("user-name")).SendKeys("test@gmail.com");
        //driver.FindElement(By.Name("password")).SendKeys("deuce");
        //driver.FindElement(By.CssSelector("input[type='submit']")).Click();
        //String errorMessage = driver.FindElement(By.XPath("//h3[@data-test='error']")).Text;
        //string expectedErrorMessage = "Epic sadface: Username and password do not match any user in this service";
        //Assert.That(errorMessage, Is.EqualTo(expectedErrorMessage));

        [FindsBy(How = How.Id, Using = "user-name")]
        private IWebElement username;

        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement password;

        [FindsBy(How = How.CssSelector, Using = "input[type='submit']")]
        private IWebElement loginBtn;

        private IWebDriver driver;
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        public IWebElement GetUsername()
        {
            return username;
        }
        public IWebElement GetPassword()
        {
            return password;
        }
        public IWebElement GetLoginButton()
        {
            return loginBtn;
        }

        public void AssertErrorMessage(string expectedErrorMessage, IWebDriver driver)
        {
            String errorMessage = driver.FindElement(By.XPath("//h3[@data-test='error']")).Text;
            errorMessage = "Epic sadface: Username and password do not match any user in this service";
            Assert.That(errorMessage, Is.EqualTo(expectedErrorMessage));
        }

        public void ValidLogin(string user, string pass)
        {
            username.SendKeys(user);
            password.SendKeys(pass);
            loginBtn.Click();
        }

        public void AssertSuccessfullyLogin(string expectedInvetoryPageText, IWebDriver driver)
        {
            String inventoryPageText = driver.FindElement(By.XPath("//span[@class='title']")).Text;
            string expectedInventoryPageText = "Products";
            Assert.That(inventoryPageText, Is.EqualTo(expectedInventoryPageText));
        }
    }
}


