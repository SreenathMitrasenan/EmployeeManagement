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
    public  class SwagLabHomeSteps
    {

        private readonly IWebDriver driver;
        private readonly SwagLabHomePage _homePage;
        private readonly string className;

        public SwagLabHomeSteps(ScenarioContext scenarioContext)
        {
            driver = scenarioContext.Get<IWebDriver>("driver");
            _homePage = new SwagLabHomePage(driver);
            className = this.GetType().Name.Replace("Steps", "");
        }


        [Then(@"I click on (.*) webelement present in SwagLabHome page")]
        public void ThenIClickOn_WebelementPresentInSwagLabHomePage(string webElement)
        {

            string txtToType = string.Empty;
            if (_homePage.GetControlInfo(webElement) != null)
            {
                PerformAction(_homePage.GetWebElement(webElement), _homePage.GetControlInfo(webElement), txtToType, className);
            }
            else
            {
                CoreAutomation.Utilities.ReportLog.ReportStep(Status.Fail, String.Format("Web element {0} not found in {1} page  ", webElement, className));
            }
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
