using CoreAutomation.Utilities;
using Microsoft.VisualBasic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RazorEngine.Compilation.ImpromptuInterface.Dynamic;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static CoreAutomation.Utilities.ReportLog;

namespace CoreAutomation.Extensions
{
    public static class SeleniumExtensions
    {
                
        public static void ClickElement(this IWebElement element, string controlName, string controlType , string value,string pageName)
        {
            string cType= controlType.ToLower().Trim();
            string bValue = value.ToLower().Trim();
            try
            { 
                if (cType.Equals("button"))
                {                  
                    element.Click();
                    ReportLog.ReportStep(Status.Pass, string.Format(" Performed action 'CLICK' on element '{0}'  in page '{1}'", controlName, pageName));
                }
                else if (cType.Equals("radio"))
                {
                    if (bValue.Equals("true"))
                    {
                        element.Click();
                        ReportLog.ReportStep(Status.Pass, string.Format(" Performed action 'CLICK' on element '{0}'  in page '{1}'", controlName, pageName));
                    }
                    else
                    {
                        ReportLog.ReportStep(Status.Info, string.Format(" No action 'CLICK' on element '{0}' in page '{1}' for control '{2}' , value should be set to true", controlName, pageName, cType));
                    }
                    
                }
                
            }
            catch (Exception ex)
            {
                ReportLog.ReportStep(Status.Fail, string.Format(" Performed action 'CLICK' on element {0} ", controlName, pageName));
            }
        }


        public static bool WaitForElementToLoad(this IWebDriver driver, By locator, string controlName, string page,bool checkForExistance=true, int waitTime=10)
        {
            var begin = DateTime.UtcNow;
             Thread.Sleep(100);
             while (true)
             {
                 try
                 {
                     if (checkForExistance)
                     {
                        if (ExpectedConditions.ElementIsVisible(locator).Invoke(driver).Displayed && ExpectedConditions.ElementIsVisible(locator).Invoke(driver).Enabled)
                            return true;
                     }
                    else
                    {
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
                        if (!driver.FindElement(locator).Displayed)
                        {
                            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                            return true;

                        }
                    }
                 }
                 catch (Exception )
                 {
                    
                    if (!checkForExistance)
                    {
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                        return true;
                    }
                    Thread.Sleep(100);             

                }
                if ((DateTime.UtcNow - begin).TotalSeconds >= waitTime)
                {
                    //ReportLog.ReportStep(Status.Fail, string.Format(" Element {0} not found on page {1} ", controlName, page));
                    return false;
                }                   
                    
                Thread.Sleep(100);
             }
            
        }

        public static bool WaitForElementPresent(this IWebDriver driver, By locator, string controlName, string page, bool checkForExistance = true, int waitTime = 10)
        {
            var begin = DateTime.UtcNow;
            Thread.Sleep(100);
            while (true)
            {
                try
                {
                    if (checkForExistance)
                    {
                        if (ExpectedConditions.ElementIsVisible(locator).Invoke(driver).Displayed)
                            return true;
                    }
                    else
                    {
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
                        if (!driver.FindElement(locator).Displayed)
                        {
                            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                            return true;

                        }
                    }
                }
                catch (Exception)
                {

                    if (!checkForExistance)
                    {
                        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                        return true;
                    }
                    Thread.Sleep(100);

                }
                if ((DateTime.UtcNow - begin).TotalSeconds >= waitTime)
                {
                    return false;
                }

                Thread.Sleep(100);
            }

        }


        public static void SendKeysToElement(this IWebElement element, string controlName, string texttoSend, string className)
        {
            try
            {
                element.SendKeys(texttoSend);
                ReportLog.ReportStep(Status.Pass, string.Format(" Performed action 'SendKeys' on element '{0}', value '{1}'", controlName, texttoSend));
            }
            catch (Exception ex)
            {
                ReportLog.ReportStep(Status.Fail, string.Format(" Performed action 'CLICK' on element {0} , value '{1}'", controlName, texttoSend));
            }
        }


        public static void SelectElement(this IWebElement element,string controlName, string selBy, string value, string pageName)
        {
            var select = new SelectElement(element);               
            try
            {
                if (selBy.Equals("value"))
                {
                    select.SelectByValue(value);
                    ReportLog.ReportStep(Status.Pass, string.Format(" Performed action 'SELECT' on element '{0}'  in page '{1}'", controlName, pageName));
                }
                else if (selBy.Equals("text"))
                {
                    select.SelectByText(value);
                    ReportLog.ReportStep(Status.Pass, string.Format(" Performed action 'SELECT' on element '{0}'  in page '{1}'", controlName, pageName));
                }
            }
            catch (Exception ex)
            {
                ReportLog.ReportStep(Status.Fail, string.Format(" Performed action 'SELECT' on element {0} ", controlName, pageName));
            }
        }
        public static void SelectElement(this IWebElement element, string controlName, int selBy, int value, string pageName)
        {
            var select = new SelectElement(element);
            try
            {
                select.SelectByIndex(value);
                ReportLog.ReportStep(Status.Pass, string.Format(" Performed action 'SELECT' on element '{0}'  in page '{1}'", controlName, pageName));
            }
            catch (Exception ex)
            {
                ReportLog.ReportStep(Status.Fail, string.Format(" Performed action 'SELECT' on element {0} ", controlName, pageName));
            }
        }

        public enum SelectionMethod
        {
            ByValue,
            ByIndex,
            ByVisibleText
        }


    }
}
