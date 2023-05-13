using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace EmployeeManagement.Pages
{


    public class CreateEmployeePage
    {
        private readonly IWebDriver _driver;
        public CreateEmployeePage(IWebDriver driver) => _driver = driver;

        public IWebElement allUsers => _driver.FindElement(By.PartialLinkText("All Users"));
        public IWebElement addEmployeelabel => _driver.FindElement(By.XPath("//h2[text()='Add Employee']"));
        public IWebElement name => _driver.FindElement(By.XPath("//input[@name='name']"));
        public IWebElement email => _driver.FindElement(By.XPath("//input[@name='email']"));
        public IWebElement male => _driver.FindElement(By.XPath("//input[@name='gender' and @id='radio-2']"));
        public IWebElement female => _driver.FindElement(By.XPath("//input[@name='gender' and @id='radio-3']"));
        public IWebElement active => _driver.FindElement(By.XPath("//input[@name='status' and @id='radio-4']"));
        public IWebElement inactive => _driver.FindElement(By.XPath("//input[@name='status' and @id='radio-5']"));
        public IWebElement proofSubmitted => _driver.FindElement(By.XPath("//input[@name='proofsubmitted']"));
        public IWebElement department => _driver.FindElement(By.XPath("//select[@id='departmentType']"));
        public IWebElement salary => _driver.FindElement(By.XPath("//input[@name='salary']"));
        public IWebElement save => _driver.FindElement(By.XPath("//button[text()='Save']"));
        public object[] GetControlInfo(string key)
        {
            Dictionary<string, object[]> controls = new Dictionary<string, object[]>();
            controls.Add("allUsers", new object[] { "All Users", "Button", "Click", By.PartialLinkText("All Users") });
            controls.Add("addEmployeelabel", new object[] { "Add Employee", "Button", "Click", By.XPath("//h2[text()='Add Employee']") });
            controls.Add("name", new object[] { "Name", "Textbox", "SendKeys", By.XPath("//input[@name='name']") });
            controls.Add("email", new object[] { "Email", "Textbox", "SendKeys", By.XPath("//input[@name='email']") });
            controls.Add("male", new object[] { "Male", "Radio", "Click", By.XPath("//input[@name='gender' and @id='radio-2']") });
            controls.Add("female", new object[] { "Female", "Radio", "Click", By.XPath("//input[@name='gender' and @id='radio-3']") });
            controls.Add("active", new object[] { "Active", "Radio", "Click", By.XPath("//input[@name='status' and @id='radio-4']") });
            controls.Add("inactive", new object[] { "InActive", "Radio", "Click", By.XPath("//input[@name='status' and @id='radio-5']") });
            controls.Add("proofSubmitted", new object[] { "ProofSubmitted", "Textbox", "SendKeys", By.XPath("//input[@name='proofsubmitted']") });
            controls.Add("department", new object[] { "Department", "Dropdown", "Select", By.XPath("//select[@id='departmentType']") });
            controls.Add("salary", new object[] { "Salary", "Textbox", "SendKeys", By.XPath("//input[@name='salary']") });
            controls.Add("save", new object[] { "Save", "Button", "Click", By.XPath("//button[text()='Save']") });
            if (controls.ContainsKey(key))
                return controls[key];
            else
                return null;
        }

        public IWebElement GetWebElement(string key)
        {
            Dictionary<string, IWebElement> elementDictionary = new Dictionary<string, IWebElement>();
            elementDictionary.Add("allUsers", allUsers);
            elementDictionary.Add("addEmployeelabel", addEmployeelabel);
            elementDictionary.Add("name", name);
            elementDictionary.Add("email", email);
            elementDictionary.Add("male", male);
            elementDictionary.Add("female", female);
            elementDictionary.Add("active", active);
            elementDictionary.Add("inactive", inactive);
            elementDictionary.Add("proofSubmitted", proofSubmitted);
            elementDictionary.Add("department", department);
            elementDictionary.Add("salary", salary);
            elementDictionary.Add("save", save);
            return elementDictionary.TryGetValue(key, out IWebElement webElement) ? webElement : null;
        }

    }


}
