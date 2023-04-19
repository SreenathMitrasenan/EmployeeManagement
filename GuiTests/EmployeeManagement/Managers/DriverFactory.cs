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
using System.Security.Policy;
using TechTalk.SpecFlow;

namespace EmployeeManagement.Managers
{
    [Binding]
    public class DriverFactory
    {
        private IWebDriver driver;
        private readonly ScenarioContext _scenarioContext;

        public DriverFactory(ScenarioContext scenarioContext) => _scenarioContext = scenarioContext;



        public  IWebDriver SetBrowser()
        {
            if (FileSystem.GetCurrentEnvironmentType().ToLower().Trim().Equals("local"))
            {
                driver = GetLocalDriver(FileSystem.GetBrowser());
            }
            else
            {
                driver = GetRemoteDriver(FileSystem.GetBrowser());
            }
            _scenarioContext.Set(driver, "driver");
            return driver;
        }

     

        private static IWebDriver GetLocalDriver(BrowserType browserType)
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            return browserType switch
            {
                BrowserType.Chrome => new ChromeDriver(options),
                BrowserType.Firefox => new FirefoxDriver(),
                BrowserType.Safari => new SafariDriver(),
                BrowserType.Edge => new EdgeDriver(),
                _ => new ChromeDriver(options)
            };
        }

        private static IWebDriver GetRemoteDriver(BrowserType browserType)
        {
            Uri uri = new Uri("http://localhost:4444");
            var ChromeOptions = new ChromeOptions();
            ChromeOptions.AddArgument("--start-maximized");
            return browserType switch
            {
                BrowserType.Chrome => new RemoteWebDriver(uri, ChromeOptions),
                BrowserType.Firefox => new RemoteWebDriver(uri, new FirefoxOptions()),
                BrowserType.Safari => new RemoteWebDriver(uri, new SafariOptions()),
                BrowserType.Edge => new RemoteWebDriver(uri, new EdgeOptions()),
                _ => new RemoteWebDriver(uri, new ChromeOptions())
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