using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Pages
{


    public class SwagLabLoginPage
    {
        private readonly IWebDriver _driver;
        public SwagLabLoginPage(IWebDriver driver) => _driver = driver;

        public IWebElement userName => _driver.FindElement(By.Id("user-name"));
        public IWebElement password => _driver.FindElement(By.Id("password"));
        public IWebElement login => _driver.FindElement(By.Id("login-button"));
        public IWebElement swaglabs => _driver.FindElement(By.XPath("//div[text()='Swag Labs']"));
        public string[] GetControlInfo(string key)
        {
            Dictionary<string, string[]> controls = new Dictionary<string, string[]>();
            controls.Add("userName", new string[] { "Username", "Textbox", "Id", "user-name", "SendKeys" });
            controls.Add("password", new string[] { "Password", "Textbox", "Id", "password", "SendKeys" });
            controls.Add("login", new string[] { "Login", "Button", "Id", "login-button", "Click" });
            controls.Add("swaglabs", new string[] { "Swag Labs", "Label", "XPath", "//div[text()='Swag Labs']", "Verify" });
            if (controls.ContainsKey(key))
                return controls[key];
            else
                return null;
        }

        public IWebElement GetWebElement(string key)
        {
            Dictionary<string, IWebElement> elementDictionary = new Dictionary<string, IWebElement>();
            elementDictionary.Add("userName", userName);
            elementDictionary.Add("password", password);
            elementDictionary.Add("login", login);
            elementDictionary.Add("swaglabs", swaglabs);
            return elementDictionary.TryGetValue(key, out IWebElement webElement) ? webElement : null;
        }

    }


}