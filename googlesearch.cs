using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace MyProject.Steps
{
    [Binding]
    public class GoogleSearchSteps
    {
        private readonly IWebDriver driver;

        public GoogleSearchSteps(ScenarioContext scenarioContext)
        {
            driver = scenarioContext["WebDriver"] as IWebDriver;
        }

        [Given(@"I am on the Google homepage")]
        public void GivenIAmOnTheGoogleHomepage()
        {
            driver.Navigate().GoToUrl("https://www.google.com");
        }

        [When(@"I search for ""(.*)""")]
        public void WhenISearchFor(string searchTerm)
        {
            var searchBox = driver.FindElement(By.Name("q"));
            searchBox.SendKeys(searchTerm);
            searchBox.SendKeys(Keys.Enter);
        }

        [Then(@"the page title should contain ""(.*)""")]
        public void ThenThePageTitleShouldContain(string expectedText)
        {
            Assert.IsTrue(driver.Title.Contains(expectedText),
                $"Expected title to contain '{expectedText}' but got '{driver.Title}'");
        }
    }
}