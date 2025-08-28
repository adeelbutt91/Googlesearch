using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using TechTalk.SpecFlow;

namespace Googlesearch
{
    public class UnitTest1
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            // Setup ChromeDriver using WebDriverManager
            new DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
        }

        [Test]
        public void GoogleSearchTest()
        {
            driver.Navigate().GoToUrl("https://www.google.com");
            Thread.Sleep(5000);
            var searchBox = driver.FindElement(By.Name("q"));
            Thread.Sleep(2000);
            searchBox.SendKeys("Selenium C# NUnit");
            searchBox.SendKeys(Keys.Enter);
            Thread.Sleep(5000);

            Assert.IsTrue(driver.Title.Contains("Selenium"));
        }

        [TearDown]
        public void Cleanup()
        {      
            driver.Quit();    
            driver.Dispose();
        }
    }
}