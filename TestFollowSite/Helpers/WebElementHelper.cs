using System;
using OpenQA.Selenium;

namespace Tests.Helpers
{
    public class WebElementHelper
    {
        public static IWebElement ValueInput(IWebElement element, string value)
        {
            string currentValue = element.GetAttribute("value");
            element.SendKeys(value);
            while (currentValue != value)
            {
                currentValue = element.GetAttribute("value");
            }

            return element;
        }

        public static bool HasElement(IWebDriver driver, By selector, TimeSpan time)
        {
            TimeSpan prevTime = driver.Manage().Timeouts().ImplicitWait;
            driver.Manage().Timeouts().ImplicitWait = time;
            bool result = driver.FindElements(selector).Count > 0;
            driver.Manage().Timeouts().ImplicitWait = prevTime;
            return result;
        }

        public static bool HasElementIn(IWebDriver driver, IWebElement element, By selector, TimeSpan time)
        {
            TimeSpan prevTime = driver.Manage().Timeouts().ImplicitWait;
            driver.Manage().Timeouts().ImplicitWait = time;
            bool result = element.FindElements(selector).Count > 0;
            driver.Manage().Timeouts().ImplicitWait = prevTime;
            return result;
        }
    }
}