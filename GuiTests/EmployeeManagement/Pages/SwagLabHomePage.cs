using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Pages
{




    public class SwagLabHomePage
    {
        private readonly IWebDriver _driver;
        public SwagLabHomePage(IWebDriver driver) => _driver = driver;

        public IWebElement burgerButton => _driver.FindElement(By.Id("react-burger-menu-btn"));
        public IWebElement allItems => _driver.FindElement(By.Id("inventory_sidebar_link"));
        public IWebElement about => _driver.FindElement(By.Id("about_sidebar_link"));
        public IWebElement logout => _driver.FindElement(By.Id("logout_sidebar_link"));
        public IWebElement resetAppState => _driver.FindElement(By.Id("reset_sidebar_link"));
        public IWebElement shoppingContainer => _driver.FindElement(By.Id("shopping_cart_container"));
        public IWebElement sauceLabsBackPack_addToCart => _driver.FindElement(By.XPath("//div[contains(text(),'Sauce Labs Backpack')]//ancestor::div[@class='inventory_item_description']//button[text()='Add to cart']"));
        public string[] GetControlInfo(string key)
        {
            Dictionary<string, string[]> controls = new Dictionary<string, string[]>();
            controls.Add("burgerButton", new string[] { "Left Burger Button", "Button", "Id", "react-burger-menu-btn", "Click" });
            controls.Add("allItems ", new string[] { "All Items", "Button", "Id", "inventory_sidebar_link", "Click" });
            controls.Add("about", new string[] { "About", "Button", "Id", "about_sidebar_link", "Click" });
            controls.Add("logout", new string[] { "Logout", "Button", "Id", "logout_sidebar_link", "Click" });
            controls.Add("resetAppState", new string[] { "Reset App State", "Button", "Id", "reset_sidebar_link", "Click" });
            controls.Add("shoppingContainer", new string[] { "shopping_cart_container", "Label", "Id", "shopping_cart_container", "Click" });
            controls.Add("sauceLabsBackPack_addToCart", new string[] { "Sauce Labs Backpack_ Add cart", "Button", "XPath", "//div[contains(text(),'Sauce Labs Backpack')]//ancestor::div[@class='inventory_item_description']//button[text()='Add to cart']", "Click" });
            if (controls.ContainsKey(key))
                return controls[key];
            else
                return null;
        }

        public IWebElement GetWebElement(string key)
        {
            Dictionary<string, IWebElement> elementDictionary = new Dictionary<string, IWebElement>();
            elementDictionary.Add("burgerButton", burgerButton);
            elementDictionary.Add("allItems ", allItems);
            elementDictionary.Add("about", about);
            elementDictionary.Add("logout", logout);
            elementDictionary.Add("resetAppState", resetAppState);
            elementDictionary.Add("shoppingContainer", shoppingContainer);
            elementDictionary.Add("sauceLabsBackPack_addToCart", sauceLabsBackPack_addToCart);
            return elementDictionary.TryGetValue(key, out IWebElement webElement) ? webElement : null;
        }

    }




}
