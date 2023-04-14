using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using EmployeeManagement.Utilities;
using Microsoft.Extensions.Options;

namespace EmployeeManagement.Managers
{
    [Binding]
    public  class DriverFactory
    {
        private static IWebDriver driver;
        private static readonly Lazy<Logger> _logger = new Lazy<Logger>(() => Logger.Instance);
        public static Logger LoggerInstance => _logger.Value;

        [BeforeTestRun]
        private static void BeforeTestRun()
        {
            driver = GetDriver(FileSystem.GetBrowser());
        }

        [AfterTestRun]
        private static void AfterTestRun()
        {
            driver.Quit();
        }

        [BeforeScenario]
        public static void BeforeScenario()
        {
            var scenarioContext = ScenarioContext.Current;
            scenarioContext.Set(driver, "driver");

            var logger = LoggerInstance;
            ScenarioContext.Current.Set(logger, "logger");

        }

        


        private static IWebDriver GetDriver(BrowserType browserType)
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            return browserType switch
            {
                BrowserType.Chrome =>new ChromeDriver(options),
                BrowserType.Firefox => new FirefoxDriver(),
                BrowserType.Safari => new SafariDriver(),
                BrowserType.Edge => new EdgeDriver(),
                _ => new ChromeDriver(options)
            }; 
        }

        public enum BrowserType
        {
            Chrome,
            Firefox,
            Safari,
            Edge
        }



    }
}
