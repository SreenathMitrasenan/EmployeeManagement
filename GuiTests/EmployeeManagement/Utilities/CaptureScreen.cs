using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Utilities
{
    public class CaptureScreen
    {
        public static string finalpath;
        public static string TakeSnap(IWebDriver driver)
        {
            if (driver == null)
            {
                throw new ArgumentNullException("driver", "Driver cannot be null");
            }

            try
            {
                string screenshotFolder = FileSystem.GetScreenshotPath();
                if (!Directory.Exists(screenshotFolder))
                {
                    Directory.CreateDirectory(screenshotFolder);
                }

                ITakesScreenshot ts = (ITakesScreenshot)driver;
                Screenshot screenshot = ts.GetScreenshot();
                finalpath = screenshotFolder + @"\" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Trim() + ".png";           
                string screenshot_path = new Uri(finalpath).LocalPath;
                screenshot.SaveAsFile(screenshot_path, OpenQA.Selenium.ScreenshotImageFormat.Png);
                return screenshot_path;
            }

            catch (Exception ex)
            {
                throw new Exception("Error taking screenshot", ex);
            }

        }
    }
}
