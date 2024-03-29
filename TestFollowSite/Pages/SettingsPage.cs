using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using Tests.Exception;
using Tests.Helpers;
using Tests.Models;

namespace Tests.Pages
{
    public class SettingsPage:IPage
    {
        private IWebDriver _driver;
        private readonly string _url = @"http://127.0.0.1:8082/update";
        
        [FindsBy(How = How.ClassName, Using = "file-input")] 
        private IWebElement _fileInput;
        
        [FindsBy(How = How.Id, Using = "login")] 
        private IWebElement _loginInput;
        
        [FindsBy(How = How.Id, Using = "password")] 
        private IWebElement _passwordInput;
        
        [FindsBy(How = How.Id, Using = "submit")] 
        private IWebElement _submitButton;
        
        [FindsBy(How = How.Id, Using = "exit")] 
        private IWebElement _exitInput;
        
        private static readonly string LOGIN_EMPTY = "loginEmpty";
        private static readonly string LOGIN_LESS_CHARACTER = "loginLessCharacters";
        private static readonly string PASSWORD_LESS_CHARACTER = "passwordLessCharacter";
        private static readonly string HOME_PAGE_NICKNAME = "userName";
        
        public SettingsPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver,this);
        }
        
        public SettingsPage FillUser(User user)
        {
            _loginInput.SendKeys(Keys.Shift + Keys.Home + Keys.Delete);
            _loginInput.SendKeys(user.Login);
            _passwordInput.SendKeys(user.Password);
            _fileInput.SendKeys(user.FilePath);
            return this;
        }

        public SettingsPage Submit()
        {
            _submitButton.Click();

            if (WebElementHelper.HasElement(_driver, By.Id(LOGIN_EMPTY), TimeSpan.FromMilliseconds(50)))
            {
                throw new MessageException("Login is empty");
            }

            if (WebElementHelper.HasElement(_driver, By.Id(LOGIN_LESS_CHARACTER), TimeSpan.FromMilliseconds(50)))
            {
                throw new MessageException("Login is small");
            }

            if (WebElementHelper.HasElement(_driver, By.Id(PASSWORD_LESS_CHARACTER), TimeSpan.FromMilliseconds(50)))
            {
                throw new MessageException("Password is small");
            }

            this.Navigate();
            return new SettingsPage(_driver);
        }

        public SettingsPage Navigate()
        {
            _driver.Navigate().GoToUrl(_url);
            return this;
        }
        public string GetPageName()
        {
            return "Обновление";
        }

        public HomePage ToHome()
        {
            _exitInput.Click();

            if (WebElementHelper.HasElement(_driver, By.Id(HOME_PAGE_NICKNAME), TimeSpan.FromSeconds(1)))
            {
                return new HomePage(_driver);
            }

            return null;
        }

        public bool AreEqual()
        {
            return _driver.Title == GetPageName();
        }
    }
}