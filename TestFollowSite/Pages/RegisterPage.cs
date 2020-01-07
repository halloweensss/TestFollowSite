using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using Tests.Models;

namespace Tests.Pages
{
    public class RegisterPage
    {
        private readonly IWebDriver _driver;
        private readonly string _url = @"http://127.0.0.1:8083/register";
        
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
            return new HomePage();
        }

        public LoginPage Exit()
        {
            _exitButton.Click();
            return new LoginPage();
        }

    }
}