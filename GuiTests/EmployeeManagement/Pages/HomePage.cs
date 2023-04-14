using OpenQA.Selenium;

namespace EmployeeManagement.Pages
{

    public class HomePage 
    {
        private readonly IWebDriver _driver;

        public HomePage(IWebDriver driver) => _driver = driver;
        

        public IWebElement newEmployee => _driver.FindElement(By.XPath("//span[contains(text(),'New Employee')]"));
        public IWebElement employeeManagementLabel => _driver.FindElement(By.LinkText("Employee Management System"));
        public IWebElement EmployeeRecordTable => _driver.FindElement(By.CssSelector(".table"));

        public static string DeleteRow = "//td[text()='{0}']/following-sibling::td/a[@class='btn border-shadow delete']";
        public string[] GetControlInfo(string key)
        {
            Dictionary<string, string[]> controls = new Dictionary<string, string[]>();
            controls.Add("newEmployee", new string[] { "New Employee", "Button", "XPath", "//span[contains(text(),'New Employee')]", "Click" });
            controls.Add("employeeManagementLabel", new string[] { "Employee Management System", "Label", "LinkText", "Employee Management System", "Verify" });
            controls.Add("EmployeeRecordTable", new string[] { "Employee Table", "Table", "CssSelector", ".table", "Verify" });
            controls.Add("DeleteRow", new string[] { "String ", "Table", "XPath", "//td[text()='{0}']/following-sibling::td/a[@class='btn border-shadow delete']", "Click" });
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
            elementDictionary.Add("EmployeeRecordTable", EmployeeRecordTable);
            return elementDictionary.TryGetValue(key, out IWebElement webElement) ? webElement : null;
        }

    }





}

