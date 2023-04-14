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
        public string[] GetControlInfo(string key)
        {
            Dictionary<string, string[]> controls = new Dictionary<string, string[]>();
            controls.Add("allUsers", new string[] { "All Users", "Button", "PartialLinkText", "All Users", "Click" });
            controls.Add("addEmployeelabel", new string[] { "Add Employee", "Button", "XPath", "//h2[text()='Add Employee']", "Click" });
            controls.Add("name", new string[] { "Name", "Textbox", "XPath", "//input[@name='name']", "SendKeys" });
            controls.Add("email", new string[] { "Email", "Textbox", "XPath", "//input[@name='email']", "SendKeys" });
            controls.Add("male", new string[] { "Male", "Radio", "XPath", "//input[@name='gender' and @id='radio-2']", "Click" });
            controls.Add("female", new string[] { "Female", "Radio", "XPath", "//input[@name='gender' and @id='radio-3']", "Click" });
            controls.Add("active", new string[] { "Active", "Radio", "XPath", "//input[@name='status' and @id='radio-4']", "Click" });
            controls.Add("inactive", new string[] { "InActive", "Radio", "XPath", "//input[@name='status' and @id='radio-5']", "Click" });
            controls.Add("proofSubmitted", new string[] { "ProofSubmitted", "Textbox", "XPath", "//input[@name='proofsubmitted']", "SendKeys" });
            controls.Add("department", new string[] { "Department", "Dropdown", "XPath", "//select[@id='departmentType']", "Select" });
            controls.Add("salary", new string[] { "Salary", "Textbox", "XPath", "//input[@name='salary']", "SendKeys" });
            controls.Add("save", new string[] { "Save", "Button", "XPath", "//button[text()='Save']", "Click" });
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
