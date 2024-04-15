using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace Framework.Utilities
{
    public class BaseClass
    {
        public required IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Url = "https://www.saucedemo.com/";
            driver.Manage().Window.Maximize();
        }
        public IWebDriver GetDriver()
        {
            return driver;
        }

        [TearDown]
        public void AfterTest()
        {
            driver.Close();
        }
    }
}
