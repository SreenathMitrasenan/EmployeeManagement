using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Configuration
{
    public class TestSettings
    {
        public CoreAutomation.Managers.DriverFactory.BrowserType BrowserType { get; set; } 
        
        public static Uri GridUri { get;set; }


    }
}
