using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.MarkupUtils;
using AventStack.ExtentReports.Reporter;
using EmployeeManagement.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using TechTalk.SpecFlow;

namespace EmployeeManagement.Hooks
{  [Binding]
    public class SpecflowHooks
    {
        private static ExtentTest featureName;
        private static ExtentTest scenario;
        public static AventStack.ExtentReports.ExtentReports extent;
        private readonly FeatureContext featureContext;
        public IWebDriver driver;
        private ScenarioContext scenarioContext;

        public SpecflowHooks(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            this.featureContext = featureContext;

        }

        [BeforeTestRun]
        public static void InitializeReport()
        {
            string reportPath = FileSystem.GetReportPath() + $"\\ExtentReport.html";
            var htmlReporter = new ExtentV3HtmlReporter(reportPath);
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            htmlReporter.Config.DocumentTitle = "EMAutomation";
            htmlReporter.Config.ReportName = "ATR";
            htmlReporter.Config.CSS = ".extent .card-panel-test { font-family: 'Barlow', sans-serif !important; font-size: 16px; color: #333; }";
            extent = new AventStack.ExtentReports.ExtentReports();
            //htmlReporter.LoadConfig(FileSystem.GetConfigurationDir()+@"\ExtentConfig.xml");
            extent = new AventStack.ExtentReports.ExtentReports();
            extent.AddSystemInfo("Machine Name", Environment.MachineName);
            extent.AddSystemInfo("Username", Environment.UserName);
            extent.AddSystemInfo("OS Version", Environment.OSVersion.ToString());
            extent.AddSystemInfo("Processor Count", Environment.ProcessorCount.ToString());
            extent.AddSystemInfo("Process ID", Environment.ProcessId.ToString());
            extent.AddSystemInfo("System Dir", Environment.SystemDirectory);
            extent.AddSystemInfo("Virtual Memory Size", Environment.SystemPageSize.ToString()+"MB");
            extent.AddSystemInfo("Environment", "EM Test [ QAT ]");
            extent.AddSystemInfo("IP Address", Dns.GetHostByName(Dns.GetHostName()).AddressList[1].ToString());
            extent.AddSystemInfo("Run Time STart", DateTime.Now.ToString());
            
            extent.AttachReporter(htmlReporter);

        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            extent.Flush();

        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            featureName = extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);
            string[] featureTags = featureContext.FeatureInfo.Tags.Select(s => s.ToUpperInvariant()).ToArray();
            if (featureTags.Contains("IGNORE")) featureName.Skip("Skipped");
        }


        [AfterStep]
        public void InsertReportingSteps(ScenarioContext scenariocontext)
        {
            this.scenarioContext = scenariocontext;
            driver = scenarioContext.Get<IWebDriver>("driver");
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            var table = ReportLog.GetLogTable();

            if (scenarioContext.TestError == null)
            {

                if (stepType == "Given")
                    scenario.CreateNode<Given>("Given " + ScenarioStepContext.Current.StepInfo.Text)
                        .Pass("PASS")
                        .Log(Status.Pass, table)
                       ;

                else if (stepType == "When")
                    scenario.CreateNode<When>("When " + ScenarioStepContext.Current.StepInfo.Text)
                        .Pass("PASS")
                        .Log(Status.Pass, table);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>("Then " + ScenarioStepContext.Current.StepInfo.Text)
                        .Pass("PASS")
                        .Log(Status.Pass, table);
                else if (stepType == "And")
                    scenario.CreateNode<And>("And " + ScenarioStepContext.Current.StepInfo.Text).Pass("PASS");
            }
            else if (scenarioContext.TestError != null)
            {
                string stepInfo = scenarioContext.StepContext.StepInfo.Text;

                if (stepType == "Given")
                {
                    scenario.CreateNode<Given>(stepType + ": " + stepInfo).Fail(scenarioContext.TestError.Message).Fail("FAIL", MediaEntityBuilder.CreateScreenCaptureFromPath(CaptureScreen.TakeSnap(driver)).Build());
                }
                else if (stepType == "When")
                {
                    scenario.CreateNode<When>(stepType + ": " + stepInfo).Fail(scenarioContext.TestError.Message).Fail("FAIL", MediaEntityBuilder.CreateScreenCaptureFromPath(CaptureScreen.TakeSnap(driver)).Build());
                }
                else if (stepType == "Then")
                {
                    scenario.CreateNode<Then>(stepType + ": " + stepInfo).Fail(scenarioContext.TestError.Message).Fail("FAIL", MediaEntityBuilder.CreateScreenCaptureFromPath(CaptureScreen.TakeSnap(driver)).Build());
                }
                else if (stepType == "And")
                {
                    scenario.CreateNode<And>(stepType + ": " + stepInfo).Fail(scenarioContext.TestError.Message).Fail("FAIL", MediaEntityBuilder.CreateScreenCaptureFromPath(CaptureScreen.TakeSnap(driver)).Build());
                }
            
            }
            ReportLog.Clear();

        }


        [BeforeScenario]
        void InitializeReportTitle(ScenarioContext scenariocontext)
        {
            scenario = featureName.CreateNode<Scenario>(scenariocontext.ScenarioInfo.Title);
        }
         /* [AfterScenario]
         void CleanUp()
         {
             Thread.Sleep(500);
             driver.Close();
         }*/

        [AfterScenario]
        void AfterScenario()
        {
            Thread.Sleep(500);
            //driver.Close();
            extent.Flush();
        }
    }
}
