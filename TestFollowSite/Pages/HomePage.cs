using OpenQA.Selenium;

namespace Tests.Pages
{
    public class HomePage
    {
        private IWebDriver _driver;
        public HomePage(IWebDriver driver)
        {
            _driver = driver;
        }
    }
}