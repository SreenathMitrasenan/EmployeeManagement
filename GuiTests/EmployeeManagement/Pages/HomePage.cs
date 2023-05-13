using OpenQA.Selenium;

namespace EmployeeManagement.Pages
{
    public class HomePage
    {
        private readonly IWebDriver _driver;
        public HomePage(IWebDriver driver) => _driver = driver;

        public IWebElement newEmployee => _driver.FindElement(By.XPath("//span[contains(text(),'New Employee')]"));
        public IWebElement employeeManagementLabel => _driver.FindElement(By.LinkText("Employee Management System"));
        public IWebElement employeeRecords => _driver.FindElement(By.CssSelector(".table"));

        public static string deleteRow = "//td[text()='{0}']/following-sibling::td/a[@class='btn border-shadow delete']";

        public object[] GetControlInfo(string key)
        {
            Dictionary<string, object[]> controls = new Dictionary<string, object[]>();
            controls.Add("newEmployee", new object[] { "New Employee", "Button", "Click", By.XPath("//span[contains(text(),'New Employee')]") });
            controls.Add("employeeManagementLabel", new object[] { "Employee Management System", "Label", "Verify", By.LinkText("Employee Management System") });
            controls.Add("employeeRecords", new object[] { "Employee Table", "Table", "Verify", By.CssSelector(".table") });
            controls.Add("deleteRow", new object[] { "String ", "Table", "Click", By.XPath("//td[text()='{0}']/following-sibling::td/a[@class='btn border-shadow delete']") });
            if (controls.ContainsKey(key))
                return controls[key];
            else
                return null;
        }

        public IWebElement GetWebElement(string key)
        {
            Dictionary<string, IWebElement> elementDictionary = new Dictionary<string, IWebElement>();
            elementDictionary.Add("newEmployee", newEmployee);
            elementDictionary.Add("employeeManagementLabel", employeeManagementLabel);
            elementDictionary.Add("employeeRecords", employeeRecords);
            return elementDictionary.TryGetValue(key, out IWebElement webElement) ? webElement : null;
        }

    }


}

