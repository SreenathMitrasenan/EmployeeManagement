using CoreAutomation.Utilities;
using EmployeeManagement.Pages;
using NUnit.Framework;
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
    public class GenericSteps
    {

        private readonly IWebDriver driver;
        private readonly string className;
        public GenericSteps(ScenarioContext scenarioContext)
        {
            driver = scenarioContext.Get<IWebDriver>("driver");
            className = this.GetType().Name.Replace("Steps", "");
        }

        [Then(@"I verify and accept alert message with text (.*) on (.*) page")]
        public void ThenIVerifyAndAcceptAlertMessageWithText_On_Page(string message, string pageName)
        {
            IAlert alert = driver.SwitchTo().Alert();
            string alertTextActual = alert.Text;
            string alertMessage = message.Trim();
            try
            {
                if (alertTextActual.Equals(alertMessage.Trim()))
                {
                    Assert.AreEqual(alertMessage, alertTextActual);
                    ReportLog.ReportStep(Status.Pass, string.Format(" Alert message '{0}' verified successfully on page '{1}' ", alertMessage, pageName));
                    alert.Accept();
                    Thread.Sleep(1000);
                    ReportLog.ReportStep(Status.Pass, string.Format(" Alert accepted successfully  "));
                }
                else
                {
                    ReportLog.ReportStep(Status.Fail, string.Format(" Alert message '{0}' verification failed , actual message is  '{1}' ", alertMessage, alertTextActual));
                }
            }
            catch (Exception ex)
            {
                ReportLog.ReportStep(Status.Fail, string.Format(" Unable to close alert message  , exception message : {0}", ex.Message));
            }
        }


    }
}
