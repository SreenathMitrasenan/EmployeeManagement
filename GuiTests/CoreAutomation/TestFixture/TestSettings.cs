using CoreAutomation.Managers;
using CoreAutomation.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace CoreAutomation.TestFixture
{
    public class TestSettings
    {
            public static int DefaultWaitTime { get; set; }= FileSystem.GetDefaultWaitTime();

    }
}
