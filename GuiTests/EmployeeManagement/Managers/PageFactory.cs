using EmployeeManagement.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmployeeManagement.Managers.DriverFactory;

namespace EmployeeManagement.Managers
{
    public class PageFactory
    {

        private IWebDriver driver;
        private HomePage _homePage;
        private CreateEmployeePage _createEmployeePage;

        public PageFactory(IWebDriver driver)
        {
            this.driver = driver;
        }

        public HomePage getHomePage()
        {
            return (_homePage == null) ? _homePage = new HomePage(driver) : _homePage;
        }
        public CreateEmployeePage getCreateEmployeePage()
        {
            return (_createEmployeePage == null) ? _createEmployeePage = new CreateEmployeePage(driver) : _createEmployeePage;
        }


        public object GetPageObject(string pageName)
        {
            object pageObject = null;
            switch (pageName.ToLower().Trim())
            {
                case "home":
                    pageObject = getHomePage();
                    break;
                case "createemployee":
                    pageObject = getCreateEmployeePage();
                    break;
                default:
                    throw new ArgumentException($"Page {pageName} not found");
            }

            return pageObject;

        }

    }
}
