using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmployeeManagement.Managers.DriverFactory;

namespace EmployeeManagement.Configuration
{
    public class TestSettings
    {
        public BrowserType BrowserType { get; set; } 
        
        public static Uri GridUri { get;set; }


    }
}
