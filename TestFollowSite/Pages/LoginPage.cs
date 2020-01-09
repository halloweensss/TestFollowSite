using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using Tests.Exception;
using Tests.Helpers;
using Tests.Models;

namespace Tests.Pages
{
    public class LoginPage: IPage
    {
        private IWebDriver _driver;
        private WebDriverWait _driverWait;
        private readonly string _url = @"http://127.0.0.1:8082/login";
        
        [FindsBy(How = How.Id, Using = "email")] 
        private IWebElement _emailInput;
        
        [FindsBy(How = How.Id, Using = "password")] 
        private IWebElement _passwordInput;
        
        [FindsBy(How = How.Id, Using = "submit")] 
        private IWebElement _submitButton;
        
        [FindsBy(How = How.Id, Using = "toRegister")] 
        private IWebElement _registerButton;
        
        [FindsBy(How = How.Id, Using = "toRestore")] 
        private IWebElement _restoreButton;
        
        private static readonly string EMAIL_EMPTY = "emailEmpty";
        private static readonly string EMAIL_INCORRECT = "emailIncorrect";
        private static readonly string USER_NOT_FOUND = "userNotCreated";
        private static readonly string PASSWORD_EMPTY = "passwordEmpty";
        private static readonly string PASSWORD_INCORRECT = "passwordIncorrect";
        private static readonly string HOME_PAGE_NICKNAME = "userName";
        private static readonly string REGISTER_PAGE = "register";
        
        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver,this);
        }
        
        public LoginPage Navigate()
        {
            _driver.Navigate().GoToUrl(_url);
            return this;
        }
        
        public LoginPage FillUser(User user)
        {
            _emailInput.SendKeys(user.Email);
            _passwordInput.SendKeys(user.Password);

            return this;
        }

        public HomePage Submit()
        {
            _submitButton.Click();
            if (WebElementHelper.HasElement(_driver, By.Id(EMAIL_EMPTY),TimeSpan.FromMilliseconds(50)))
            {
                throw new MessageException("Email is empty");
            }

            if (WebElementHelper.HasElement(_driver, By.Id(EMAIL_INCORRECT),TimeSpan.FromMilliseconds(50)))
            {
                throw new MessageException("Email is incorrect");
            }

            if (WebElementHelper.HasElement(_driver, By.Id(USER_NOT_FOUND),TimeSpan.FromMilliseconds(50)))
            {
                throw new MessageException("User not created");
            }

            if (WebElementHelper.HasElement(_driver, By.Id(PASSWORD_EMPTY),TimeSpan.FromMilliseconds(50)))
            {
                throw new MessageException("Password is empty");
            }

            if (WebElementHelper.HasElement(_driver, By.Id(PASSWORD_INCORRECT),TimeSpan.FromMilliseconds(50)))
            {
                throw new MessageException("Password is incorrect");
            }

            if (WebElementHelper.HasElement(_driver, By.Id(HOME_PAGE_NICKNAME), TimeSpan.FromSeconds(1)))
            {
                return new HomePage(_driver);
            }

            return null;
        }

        public RegisterPage ToRegister()
        {
            _registerButton.Click();
            if (WebElementHelper.HasElement(_driver,By.Id(REGISTER_PAGE), TimeSpan.FromSeconds(1)))
            {
                return new RegisterPage(_driver);
            }
            return null;
        }

        public string GetPageName()
        {
            return "Вход";
        }

        public bool AreEqual()
        {
            return _driver.Title == GetPageName();
        }
    }
}