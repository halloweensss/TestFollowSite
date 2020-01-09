using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using Tests.Exception;
using Tests.Helpers;
using Tests.Models;

namespace Tests.Pages
{
    public class RegisterPage : IPage
    {
        private readonly IWebDriver _driver;
        private readonly string _url = @"http://127.0.0.1:8082/register";
        
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
            if (WebElementHelper.HasElement(_driver, By.Id(LOGIN_EMPTY), TimeSpan.FromMilliseconds(50)))
            {
                throw new MessageException("Login is empty");
            }

            if (WebElementHelper.HasElement(_driver, By.Id(LOGIN_LESS_CHARACTER), TimeSpan.FromMilliseconds(50)))
            {
                throw new MessageException("Login is small");
            }

            if (WebElementHelper.HasElement(_driver, By.Id(EMAIL_EMPTY), TimeSpan.FromMilliseconds(50)))
            {
                throw new MessageException("Email is empty");
            }

            if (WebElementHelper.HasElement(_driver, By.Id(EMAIL_INCORRECT), TimeSpan.FromMilliseconds(50)))
            {
                throw new MessageException("Email is incorrect");
            }

            if (WebElementHelper.HasElement(_driver, By.Id(USER_EXIST), TimeSpan.FromMilliseconds(50)))
            {
                throw new MessageException("User exist");
            }

            if (WebElementHelper.HasElement(_driver, By.Id(PASSWORD_LESS_CHARACTER), TimeSpan.FromMilliseconds(50)))
            {
                throw new MessageException("Password is small");
            }

            if (WebElementHelper.HasElement(_driver, By.Id(GENDER_EMPTY), TimeSpan.FromMilliseconds(50)))
            {
                throw new MessageException("Gender is empty");
            }

            if (WebElementHelper.HasElement(_driver, By.Id(HOME_PAGE_NICKNAME), TimeSpan.FromSeconds(1)))
            {
                return new HomePage(_driver);
            }

            return null;
        }

        public LoginPage ToLogin()
        {
            _exitButton.Click();
            if (WebElementHelper.HasElement(_driver, By.Id(LOGIN_PAGE_REGISTER), TimeSpan.FromSeconds(2)))
            {
                return new LoginPage(_driver);
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