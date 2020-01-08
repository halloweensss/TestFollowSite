using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using Tests.Models;

namespace Tests.Pages
{
    public class RegisterPage : IPage
    {
        private readonly IWebDriver _driver;
        private readonly string _url = @"http://127.0.0.1:8080/register";
        
        [FindsBy(How = How.Id, Using = "login")] 
        private IWebElement _loginInput;
        
        [FindsBy(How = How.Id, Using = "email")] 
        private IWebElement _emailInput;
        
        [FindsBy(How = How.Id, Using = "password")] 
        private IWebElement _passwordInput;
        
        [FindsBy(How = How.Id, Using = "gender")] 
        private IWebElement _genderSelect;
        
        [FindsBy(How = How.Id, Using = "fromToggle")] 
        private IWebElement _fromRadio;
        
        [FindsBy(How = How.Id, Using = "social")] 
        private IWebElement _socialInput;
        
        [FindsBy(How = How.Id, Using = "register")] 
        private IWebElement _submitButton;
        
        [FindsBy(How = How.Id, Using = "exit")] 
        private IWebElement _exitButton;


        private static readonly string LOGIN_EMPTY = "loginEmpty";
        private static readonly string LOGIN_LESS_CHARACTER = "loginLessCharacters";
        private static readonly string EMAIL_EMPTY = "emailEmpty";
        private static readonly string EMAIL_INCORRECT = "emailIncorrect";
        private static readonly string USER_EXIST = "userExist";
        private static readonly string PASSWORD_LESS_CHARACTER = "passwordLessCharacter";
        private static readonly string GENDER_EMPTY = "genderEmpty";
        private static readonly string HOME_PAGE_NICKNAME = "userName";
        private static readonly string LOGIN_PAGE_REGISTER = "toRegister";
        
        
        public RegisterPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver,this);
        }
        
        public RegisterPage Navigate()
        {
            _driver.Navigate().GoToUrl(_url);
            return this;
        }

        public RegisterPage FillUser(User user)
        {
            _loginInput.SendKeys(user.Login);
            _emailInput.SendKeys(user.Email);
            _passwordInput.SendKeys(user.Password);
            SelectElement selectElement = new SelectElement(_genderSelect);
            selectElement.SelectByValue(user.GetGender());
            _fromRadio.FindElement(By.Id(user.GetServiceName())).Click();
            
            if(user.FromService == FromService.Social)
                _socialInput.SendKeys(user.NameService);
            
            return this;
        }

        public HomePage Submit()
        {
            _submitButton.Click();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(200);
            try
            {
                if (_driver.FindElement(By.Id(LOGIN_EMPTY)) != null)
                {
                    throw new Exception("Login is empty");
                }

                if (_driver.FindElement(By.Id(LOGIN_LESS_CHARACTER)) != null)
                {
                    throw new Exception("Login is small");
                }

                if (_driver.FindElement(By.Id(EMAIL_EMPTY)) != null)
                {
                    throw new Exception("Email is empty");
                }

                if (_driver.FindElement(By.Id(EMAIL_INCORRECT)) != null)
                {
                    throw new Exception("Email is incorrect");
                }

                if (_driver.FindElement(By.Id(USER_EXIST)) != null)
                {
                    throw new Exception("User exist");
                }

                if (_driver.FindElement(By.Id(PASSWORD_LESS_CHARACTER)) != null)
                {
                    throw new Exception("Password is small");
                }

                if (_driver.FindElement(By.Id(GENDER_EMPTY)) != null)
                {
                    throw new Exception("Gender is empty");
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

        public LoginPage ToLogin()
        {
            _exitButton.Click();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            try
            {
                if (_driver.FindElement(By.Id(LOGIN_PAGE_REGISTER)) != null)
                {
                    return new LoginPage(_driver);
                }
            }
            catch (Exception e)
            {
                
            }

            return null;
        }

        public string GetPageName()
        {
            return "Регистрация";
        }

        public bool AreEqual()
        {
            return _driver.Title == GetPageName();
        }
    }
}