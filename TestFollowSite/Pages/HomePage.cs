using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace Tests.Pages
{
    public class HomePage : IPage
    {
        private IWebDriver _driver;
        private readonly string _url = @"http://127.0.0.1:8080/";
        
        [FindsBy(How = How.Id, Using = "pills-news-tab")] 
        private IWebElement _newsButton;
        
        [FindsBy(How = How.Id, Using = "pills-follow-tab")] 
        private IWebElement _followsButton;
        
        [FindsBy(How = How.Id, Using = "SuggestionInput")] 
        private IWebElement _suggetionInput;
        
        [FindsBy(How = How.Id, Using = "dropdownUserInfo")] 
        private IWebElement _userNameInput;
        
        [FindsBy(How = How.Id, Using = "settings")] 
        private IWebElement _settingsInput;
        
        [FindsBy(How = How.Id, Using = "exit")] 
        private IWebElement _exitInput;
        
        [FindsBy(How = How.Id, Using = "masonry-container")] 
        private IWebElement _masonryContainer;
        
        [FindsBy(How = How.ClassName, Using = "suggestion-container-items")] 
        private IWebElement _suggestionContainer;
        
        [FindsBy(How = How.ClassName, Using = "follow-container-items")] 
        private IWebElement _followContainer;

        private static string FOLLOW_CONTAINER_ITEMS_CLASS = "follow-container-items";
        private static string FOLLOW_BUTTON_CLASS = "follow-button";
        private static string FOLLOW_NICKNAME_CLASS = "follow-nickname";
        private static string FOLLOW_CARD = "follow-card";
        private static string FOLLOW_BUTTON_SUBSCRIBE = "Подписаться";
        private static string FOLLOW_BUTTON_UNSUBSCRIBE = "Отписаться";
        private static string LOGIN_PAGE_REGISTER = "toRegister";
        private static string SETTINGS_PAGE_AVATAR_CLASS = "avatar-input";
        
        
        public HomePage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver,this);
        }

        public HomePage Follow(string name)
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Message = "Profile not found";
            name = name.ToLower();
            this.Navigate();
            _followsButton.Click();
            _suggetionInput.SendKeys(name);
            _suggetionInput.SendKeys(Keys.Enter);
            try
            {

                while(!wait.Until(d => _suggestionContainer.FindElements(By.Id(name)).Count > 0));
                IWebElement element = _suggestionContainer.FindElement(By.Id(name));
                string nameElement = element.FindElement(By.ClassName(FOLLOW_NICKNAME_CLASS)).Text.ToLower();
                if (nameElement == name)
                {
                    IWebElement followButton = element.FindElement(By.ClassName(FOLLOW_BUTTON_CLASS));
                    string followButtonText = followButton.Text;
                    if (followButtonText == FOLLOW_BUTTON_UNSUBSCRIBE)
                    {
                        throw new Exception("Already subscribed");
                    }
                    
                    followButton.Click();
                    return this;
                }

                throw new Exception("Profile not found");
            }
            catch (Exception exception)
            {
                if (exception.Message.Contains("stale"))
                {
                    IWebElement element = _suggestionContainer.FindElement(By.Id(name));
                    IWebElement followButton = element.FindElement(By.ClassName(FOLLOW_BUTTON_CLASS));
                    followButton.Click();
                }
                else
                {
                    throw exception;
                }
            }

            return this;
        }

        public HomePage Unfollow(string name)
        {
            name = name.ToLower();
            this.Navigate();
            _followsButton.Click();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(5);
            try
            {
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                wait.Message = "Profile not found";
                if (wait.Until(d => _followContainer.FindElements(By.ClassName(FOLLOW_CARD)).Count > 0) == false)
                {
                    throw new Exception("Profile not found");
                }

                int countElements = _followContainer.FindElements(By.ClassName(FOLLOW_CARD)).Count;
                
                while (!wait.Until(d => _followContainer.FindElements(By.Id(name)).Count > 0)) ;
                IWebElement element = _followContainer.FindElement(By.Id(name));
                IWebElement elementUnfollow = element.FindElement(By.ClassName(FOLLOW_NICKNAME_CLASS));
                string nameElement = elementUnfollow.Text.ToLower();
                if (nameElement == name)
                {
                    IWebElement followButton = element.FindElement(By.ClassName(FOLLOW_BUTTON_CLASS));
                    followButton.Click();
                    if (wait.Until(d =>
                        _followContainer.FindElements(By.ClassName(FOLLOW_CARD)).Count < countElements))
                    {
                        return this;
                    }

                    followButton.Click();
                }

                throw new Exception("Profile not found");
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public LoginPage ToLogin()
        {
            _userNameInput.Click();
            _exitInput.Click();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
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

        public SettingsPage ToSettings()
        {
            _userNameInput.Click();
            _settingsInput.Click();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            try
            {
                if (_driver.FindElement(By.ClassName(SETTINGS_PAGE_AVATAR_CLASS)) != null)
                {
                    return new SettingsPage(_driver);
                }
            }
            catch (Exception e)
            {
                
            }

            return null;
        }
        
        public HomePage Navigate()
        {
            _driver.Navigate().GoToUrl(_url);
            return this;
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