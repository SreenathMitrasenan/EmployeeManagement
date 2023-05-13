using EmployeeManagement.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;
using SeleniumExtras.WaitHelpers;
using CoreAutomation.Utilities;
using CoreAutomation.Extensions;
using static CoreAutomation.Utilities.ReportLog;
using CoreAutomation.TestFixture;

namespace EmployeeManagement.StepDefinitions
{
    [Binding]
    public class CreateEmployeeSteps 
    {
        private readonly IWebDriver driver;     
        private readonly CreateEmployeePage _createEmployeePage;
        private readonly string className;
        private readonly int defaultWait;
        public CreateEmployeeSteps(ScenarioContext scenarioContext)
        {
            driver = scenarioContext.Get<IWebDriver>("driver");
            _createEmployeePage = new CreateEmployeePage(driver);
            className = this.GetType().Name.Replace("Steps","");
            defaultWait = TestSettings.DefaultWaitTime;
        }


        [Then(@"I set below values in CreateEmployee page")]
        public void ThenISetBelowValuesInCreateEmployeePage(Table table)
        {
            foreach (var row in table.Rows)
            {
                var key = row["key"];
                var value = row["value"].Trim();
                if (_createEmployeePage.GetControlInfo(key) != null)
                {
                    PerformAction(_createEmployeePage.GetWebElement(key), _createEmployeePage.GetControlInfo(key), value, className);
                }
                else
                {
                    ReportLog.ReportStep(Status.Fail, String.Format("Web element {0} not found in {1} page  ", key, className));
                }

            }
        }

        [Then(@"I click on (.*) webelement present in CreateEmployee page")]
        public void ThenIClickOn_WebelementPresentInCreateEmployeePage(string webElement)
        {
            string txtToType = string.Empty;
            if (_createEmployeePage.GetControlInfo(webElement) != null)
            {
                PerformAction(_createEmployeePage.GetWebElement(webElement), _createEmployeePage.GetControlInfo(webElement), txtToType, className);
            }
            else
            {
                ReportLog.ReportStep(Status.Fail, String.Format("Web element {0} not found in {1} page  ", webElement, className));
            }
        }


        private void PerformAction(IWebElement iElement, object[] objProperties, string value, string className)
        {

            string controlName = objProperties[0].ToString();
            string controlType = objProperties[1].ToString().ToLower().Trim();
            By locator = (By)objProperties[3];
            string txtToSend = value;
            string pageName = className;
            SeleniumExtensions.WaitForElementToLoad(driver, locator, controlName, pageName,true,20);
            try
            {
                switch (controlType)
                {
                    case "button":
                        iElement.ClickElement(controlName, controlType, txtToSend, pageName);
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
