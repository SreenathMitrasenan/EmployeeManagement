using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Utilities
{
    public class ReflectionHelper
    {

        public static Dictionary<string, By> GetElementLocators(Type type)
        {
            var elementLocators = new Dictionary<string, By>();
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var field in fields)
            {
                if (field.FieldType == typeof(IWebElement) || field.FieldType == typeof(By))
                {
                    var elementName = field.Name;
                    var elementLocator = field.GetValue(null);

                    if (elementLocator is string locatorString)
                    {
                        var locatorType = GetLocatorType(locatorString);
                        var locatorValue = GetLocatorValue(locatorString);
                        elementLocator = CreateLocator(locatorType, locatorValue);
                    }

                    if (elementLocator is By locator)
                    {
                        elementLocators.Add(elementName, locator);
                    }
                }
            }

            return elementLocators;
        }

        private static By CreateLocator(string locatorType, string locatorValue)
        {
            switch (locatorType.ToLower())
            {
                case "xpath":
                    return By.XPath(locatorValue);
                case "id":
                    return By.Id(locatorValue);
                case "name":
                    return By.Name(locatorValue);
                case "classname":
                    return By.ClassName(locatorValue);
                case "cssselector":
                    return By.CssSelector(locatorValue);
                case "linktext":
                    return By.LinkText(locatorValue);
                case "partiallinktext":
                    return By.PartialLinkText(locatorValue);
                case "tagname":
                    return By.TagName(locatorValue);
                default:
                    throw new ArgumentException($"Invalid locator type: {locatorType}");
            }
        }

        private static string GetLocatorType(string locatorString)
        {
            var parts = locatorString.Split(new[] { '=' }, 2);
            if (parts.Length != 2)
            {
                throw new ArgumentException($"Invalid locator: {locatorString}");
            }

            return parts[0].Trim();
        }

        private static string GetLocatorValue(string locatorString)
        {
            var parts = locatorString.Split(new[] { '=' }, 2);
            if (parts.Length != 2)
            {
                throw new ArgumentException($"Invalid locator: {locatorString}");
            }

            return parts[1].Trim();
        }
    }
}
