using CoreAutomation.Extensions;
using CoreAutomation.Utilities;
using EmployeeManagement.Pages;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CoreAutomation.Utilities.ReportLog;

namespace EmployeeManagement.StepDefinitions
{
    [Binding]
    public  class SwagLabLoginSteps
    {

        private readonly IWebDriver driver;
        private readonly SwagLabLoginPage _loginPage;
        private readonly string className;

        public SwagLabLoginSteps(ScenarioContext scenarioContext)
        {
            driver = scenarioContext.Get<IWebDriver>("driver");
            _loginPage = new SwagLabLoginPage(driver);
            className = this.GetType().Name.Replace("Steps", "");
        }

        [Then(@"I set below values in SwagLabLogin page")]
        public void ThenISetBelowValuesIn_Page(Table table)
        {
            foreach (var row in table.Rows)
            {
                var key = row["key"];
                var value = row["value"].Trim();
                if (_loginPage.GetControlInfo(key) != null)
                {
                    PerformAction(_loginPage.GetWebElement(key), _loginPage.GetControlInfo(key), value, className);
                }
                else
                {
                    ReportLog.ReportStep(Status.Fail, String.Format("Web element {0} not found in {1} page  ", key, className));
                }

            }
        }


        [Then(@"I click on (.*) webelement present in SwagLabLogin page")]
        public void ThenIClickOn_WebelementPresentInSwagLabLoginPage(string webElement)
        {
            string txtToType = string.Empty;
            if (_loginPage.GetControlInfo(webElement) != null)
            {
                PerformAction(_loginPage.GetWebElement(webElement), _loginPage.GetControlInfo(webElement), txtToType, className);
            }
            else
            {
                ReportLog.ReportStep(Status.Fail, String.Format("Web element {0} not found in {1} page  ", webElement, className));
            }
        }


        [Then(@"I logout from swaglab application")]
        public void ThenILogoutFromSwaglabApplication()
        {
            
        }


        private void PerformAction(IWebElement iElement, string[] objProperties, string value, string className)
        {
            //string action = objProperties[4];
            string controlName = objProperties[0];
            string controlType = objProperties[1].ToLower().Trim();
            string txtToSend = value;
            string pageName = className;
            try
            {
                switch (controlType)
                {
                    case "button":
                        iElement.ClickElement(controlName, controlType, txtToSend, pageName); ;
                        break;
                    case "textbox":
                        iElement.SendKeysToElement(controlName, txtToSend, pageName);
                        break;
                    case "radio":
                        iElement.ClickElement(controlName, controlType, txtToSend, pageName);
                        break;
                    case "dropdown":
                        iElement.SelectElement(controlName, "value", txtToSend, pageName);
                        break;
                    case "":
                        ReportLog.ReportStep(Status.Fail, String.Format("Control type not found for element {0} ", controlName));
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ReportLog.ReportStep(Status.Fail, String.Format("Execption occured while performing action, message {0} ", ex.Message));
            }
        }


    }
}
