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
using OpenQA.Selenium.Remote;
using EmployeeManagement.Configuration;
using System.Security.Policy;

namespace EmployeeManagement.Managers
{
    [Binding]
    public  class DriverFactory
    {
        private static IWebDriver driver;

        [BeforeTestRun]
        private static void BeforeTestRun()
        {
            if (FileSystem.GetCurrentEnvironmentType().ToLower().Trim().Equals("local"))
            {
                driver = GetDriver(FileSystem.GetBrowser());
            }
            else
            {
                driver = GetRemoteDriver(FileSystem.GetBrowser());
            }
            
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

        private static IWebDriver GetRemoteDriver(BrowserType browserType)
        {
            TestSettings.GridUri = new Uri("http://localhost:4444");
            var ChromeOptions = new ChromeOptions();
            ChromeOptions.AddArgument("--start-maximized");
            return browserType switch
            {
                BrowserType.Chrome => new RemoteWebDriver(TestSettings.GridUri, ChromeOptions),
                BrowserType.Firefox => new RemoteWebDriver(TestSettings.GridUri, new FirefoxOptions()),
                BrowserType.Safari => new RemoteWebDriver(TestSettings.GridUri, new SafariOptions()),
                BrowserType.Edge => new RemoteWebDriver(TestSettings.GridUri, new EdgeOptions()),
                _ => new RemoteWebDriver(TestSettings.GridUri,new ChromeOptions())
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
