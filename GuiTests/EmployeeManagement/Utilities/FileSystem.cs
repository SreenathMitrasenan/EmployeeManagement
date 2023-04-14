using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmployeeManagement.Managers.DriverFactory;

namespace EmployeeManagement.Utilities
{
    class FileSystem
    {
        public static string GetApplicationRootDir()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
        public static string GetCurrentWorkingDir()
        {
            return Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        }
        public static JObject ReadJsonFile(string jsonfilePath)
        {
            return Newtonsoft.Json.Linq.JObject.Parse(File.ReadAllText(jsonfilePath));
        }
        public static string GetConfigFilePath()
        {
            return Path.Combine(GetConfigurationDir(), "Config.json");
        }

        public static string GetConfigurationDir()
        {
            return Path.Combine(GetCurrentWorkingDir(), "Configuration");
        }
        public Dictionary <string,string> MapJsonToDictionary(string jsontextfilepath, string token)
        {
            return JObject.Parse(File.ReadAllText(jsontextfilepath)).SelectToken(token).ToObject<Dictionary<string,string>>();
        }
        public static string GetAppUrl()
        {
            return ReadJsonFile(GetConfigFilePath()).SelectToken("url").SelectToken("QAT").ToString().Trim();
        }
        public static string GetCurrentBrowser()
        {
            return ReadJsonFile(GetConfigFilePath()).SelectToken("browserinfo").SelectToken("currentbrowser").ToString().Trim();
        }
        public static string GetCurrentEnvironmentType()
        {
            return ReadJsonFile(GetConfigFilePath()).SelectToken("environment").SelectToken("current").ToString().Trim();
        }

        public static BrowserType GetBrowser()
        {
            return FileSystem.GetCurrentBrowser() switch
            {
                "Chrome" => BrowserType.Chrome,
                "Firefox" => BrowserType.Firefox,
                "Safari" => BrowserType.Safari,
                "Edge" => BrowserType.Edge,
                _ => BrowserType.Chrome
            };

        }

        public static string GetEnvironment()
        {
            return FileSystem.GetCurrentEnvironmentType();
        
        }
        public static string GetDriverPath()
        {
            return Path.Combine(GetCurrentWorkingDir(), "Drivers");
        }
        public static string GetReportPath()
        {
            return Path.Combine(GetCurrentWorkingDir(), "Reports");
        }
        public static string GetScreenshotPath()
        {
            return Path.Combine(GetCurrentWorkingDir(), "Screenshots");
        }
        public static int GetDefaultWaitTime()
        {
            return Int32.Parse(ReadJsonFile(GetConfigFilePath()).SelectToken("defaultwait").SelectToken("wait").ToString().Trim());
        }
    }
}
