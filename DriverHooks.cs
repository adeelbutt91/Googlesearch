using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace MyProject.Hooks
{
    [Binding]
    public class DriverHooks
    {
        private readonly ScenarioContext _scenarioContext;

        public DriverHooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void StartBrowser()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());

            var options = new ChromeOptions();

            // If running in CI/CD, run Chrome headless
            if (Environment.GetEnvironmentVariable("CI") == "true")
            {
                options.AddArguments("--headless=new", "--disable-gpu", "--no-sandbox", "--disable-dev-shm-usage");
            }

            var driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();

            _scenarioContext["WebDriver"] = driver;
        }

        [AfterScenario]
        public void CloseBrowser()
        {
            var driver = _scenarioContext["WebDriver"] as IWebDriver;
            driver?.Quit();
            driver.Dispose();
        }
    }
}
