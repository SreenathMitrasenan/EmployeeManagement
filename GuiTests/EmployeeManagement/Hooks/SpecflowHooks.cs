using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.MarkupUtils;
using AventStack.ExtentReports.Reporter;
using CoreAutomation.Extensions;
using CoreAutomation.Managers;
using CoreAutomation.Utilities;
using NUnit.Framework;
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
//[assembly: Parallelizable(ParallelScope.Fixtures)]

namespace EmployeeManagement.Hooks
{
    [Binding]
    public class SpecflowHooks
    {
        private static ExtentTest featureName;
        private static ExtentTest scenario;
        public static AventStack.ExtentReports.ExtentReports extent;
        private readonly FeatureContext featureContext;
        public IWebDriver driver;
        private ScenarioContext _scenarioContext;
        private string stepType;
        private string stepInfo;
        private string cstepInfo;
       

        public SpecflowHooks(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            this.featureContext = featureContext;
            _scenarioContext = scenarioContext;

        }

        [BeforeScenario]
        void InitializeReportTitle()
        {
            DriverFactory driverfactory = new DriverFactory(_scenarioContext);
            _scenarioContext.Set(driverfactory.GetDriver(), "driver");
            scenario = featureName.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
        }

        [AfterScenario]
        void AfterScenario()
        {
            Thread.Sleep(500);
            _scenarioContext.Get<IWebDriver>("driver").Quit();
            extent.Flush();
        }

        [BeforeTestRun]
        public static void InitializeReport()
        {
            string threadID = "10";//Thread.CurrentThread.ManagedThreadId.ToString();
            string reportPath = FileSystem.GetReportPath() + $"\\ExtentReport_{threadID}.html";
            var htmlReporter = new ExtentV3HtmlReporter(reportPath);
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            htmlReporter.Config.DocumentTitle = "EMAutomation";
            htmlReporter.Config.ReportName = "ATR";
            htmlReporter.Config.CSS = ".extent .card-panel-test { font-family: 'Barlow', sans-serif !important; font-size: 16px; color: #333; }";
            extent = new AventStack.ExtentReports.ExtentReports();
            extent.AddSystemInfo("Machine Name", Environment.MachineName);
            extent.AddSystemInfo("Username", Environment.UserName);
            extent.AddSystemInfo("OS Version", Environment.OSVersion.ToString());
            extent.AddSystemInfo("Processor Count", Environment.ProcessorCount.ToString());
            extent.AddSystemInfo("Process ID", Environment.ProcessId.ToString());
            extent.AddSystemInfo("System Dir", Environment.SystemDirectory);
            extent.AddSystemInfo("Virtual Memory Size", Environment.SystemPageSize.ToString() + "MB");
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
            this._scenarioContext = scenariocontext;
            driver = _scenarioContext.Get<IWebDriver>("driver");
            // For parallel execution
            string stepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            string stepInfo = _scenarioContext.StepContext.StepInfo.Text;
            var table = ReportLog.GetLogTable();

            if (_scenarioContext.TestError == null)
            {

                if (stepType == "Given")
                    scenario.CreateNode<Given>("Given " + stepInfo)
                        .Pass("PASS")
                        .Log(Status.Pass, table);
                else if (stepType == "When")
                    scenario.CreateNode<When>("When " + stepInfo)
                        .Pass("PASS")
                        .Log(Status.Pass, table);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>("Then " + stepInfo)
                        .Pass("PASS")
                        .Log(Status.Pass, table);
                else if (stepType == "And")
                    scenario.CreateNode<And>("And " + stepInfo)
                        .Pass("PASS")
                        .Log(Status.Pass, table);
            }

            else if (_scenarioContext.TestError != null)
            {


                if (stepType == "Given")
                {
                    scenario.CreateNode<Given>(stepType + ": " + stepInfo).Fail(_scenarioContext.TestError.Message).Fail("FAIL", MediaEntityBuilder.CreateScreenCaptureFromPath(CaptureScreen.TakeSnap(driver)).Build());
                }
                else if (stepType == "When")
                {
                    scenario.CreateNode<When>(stepType + ": " + stepInfo).Fail(_scenarioContext.TestError.Message).Fail("FAIL", MediaEntityBuilder.CreateScreenCaptureFromPath(CaptureScreen.TakeSnap(driver)).Build());
                }
                else if (stepType == "Then")
                {
                    scenario.CreateNode<Then>(stepType + ": " + stepInfo).Fail(_scenarioContext.TestError.Message).Fail("FAIL", MediaEntityBuilder.CreateScreenCaptureFromPath(CaptureScreen.TakeSnap(driver)).Build());
                }
                else if (stepType == "And")
                {
                    scenario.CreateNode<And>(stepType + ": " + stepInfo).Fail(_scenarioContext.TestError.Message).Fail("FAIL", MediaEntityBuilder.CreateScreenCaptureFromPath(CaptureScreen.TakeSnap(driver)).Build());
                }

            }
            ReportLog.Clear();

        }



    }
}