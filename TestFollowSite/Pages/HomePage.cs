using OpenQA.Selenium;

namespace Tests.Pages
{
    public class HomePage : IPage
    {
        private IWebDriver _driver;
        public HomePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public string GetPageName()
        {
            return "Главная";
        }

        public bool AreEqual()
        {
            return _driver.Title == GetPageName();
        }
    }
}