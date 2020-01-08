using System;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using Tests.Models;

namespace Tests.Pages
{
    public class LoginPage: IPage
    {
        private IWebDriver _driver;
        private readonly string _url = @"http://127.0.0.1:8080/login";
        
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
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(200);
            try
            {
                if (_driver.FindElement(By.Id(EMAIL_EMPTY)) != null)
                {
                    throw new Exception("Email is empty");
                }

                if (_driver.FindElement(By.Id(EMAIL_INCORRECT)) != null)
                {
                    throw new Exception("Email is incorrect");
                }
                
                if (_driver.FindElement(By.Id(USER_NOT_FOUND)) != null)
                {
                    throw new Exception("User not created");
                }
                
                if (_driver.FindElement(By.Id(PASSWORD_EMPTY)) != null)
                {
                    throw new Exception("Password is empty");
                }
                
                if (_driver.FindElement(By.Id(PASSWORD_INCORRECT)) != null)
                {
                    throw new Exception("Password is incorrect");
                }
            }
            catch (Exception e)
            {
                
            }
            
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            try
            {
                if (_driver.FindElement(By.Id(HOME_PAGE_NICKNAME)) != null)
                {
                    return new HomePage(_driver);
                }
            }
            catch (Exception e)
            {
                
            }

            return null;
        }

        public RegisterPage ToRegister()
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            _registerButton.Click();
            try
            {
                if (_driver.FindElement(By.Id(REGISTER_PAGE)) != null)
                {
                    return new RegisterPage(_driver);
                }
            }
            catch (Exception e)
            {
                
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