using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using Tests.Exception;
using Tests.Helpers;
using Tests.Models;

namespace Tests.Pages
{
    public class HomePage : IPage
    {
        private IWebDriver _driver;
        private readonly string _url = @"http://127.0.0.1:8082/";
        
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

        private static string POST_CLASS = "post";
        private static string POST_ACCOUNT_NAME_CLASS = "account-name";
        private static string POST_ACCOUNT_LOGIN_CLASS = "account-login";
        private static string POST_CONTENT_TEXT_CLASS = "post-text";
        private static string NO_FOLLOWS_CLASS = "text-follow-none";
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
            name = name.ToLower();
            this.Navigate();
            
            _followsButton.Click();
            WebElementHelper.ValueInput(_suggetionInput, name);
            if (!WebElementHelper.HasElementIn(_driver, _suggestionContainer, By.Id(name), TimeSpan.FromSeconds(10)))
            {
                throw new MessageException("Profile not found");
            }

            IWebElement element = _suggestionContainer.FindElement(By.Id(name));
            IWebElement followButton = element.FindElement(By.ClassName(FOLLOW_BUTTON_CLASS));
            
            string followButtonText = followButton.Text;
            if (followButtonText == FOLLOW_BUTTON_UNSUBSCRIBE)
            {
                throw new MessageException("Already subscribed");
            }

            followButton.Click();
            while (!WebElementHelper.HasElementIn(_driver, _followContainer, By.Id(name), TimeSpan.FromSeconds(1)));

            return this;
        }

        public HomePage Unfollow(string name)
        {
            name = name.ToLower();
            _followsButton.Click();

            if (!WebElementHelper.HasElementIn(_driver, _followContainer, By.ClassName(FOLLOW_CARD),
                TimeSpan.FromSeconds(1)))
            {
                throw new MessageException("Follow is empty");
            }

            if (!WebElementHelper.HasElementIn(_driver, _followContainer, By.Id(name),
                TimeSpan.FromSeconds(1)))
            {
                throw new MessageException("Profile not found");
            }

            IWebElement element = _followContainer.FindElement(By.Id(name));
            IWebElement followButton = element.FindElement(By.ClassName(FOLLOW_BUTTON_CLASS));
            
            followButton.Click();
            while (WebElementHelper.HasElementIn(_driver, _followContainer, By.Id(name), TimeSpan.FromSeconds(1)));
            
            return this;
        }

        public List<FollowUser> GetFollows()
        {
            _followsButton.Click();

            if (!WebElementHelper.HasElementIn(_driver, _followContainer, By.ClassName(FOLLOW_CARD),
                TimeSpan.FromSeconds(1)))
            {
                throw new MessageException("Follow is empty");
            }

            ReadOnlyCollection<IWebElement> elements = _followContainer.FindElements(By.ClassName(FOLLOW_CARD));
            List<FollowUser> users = new List<FollowUser>();
            
            foreach (var element in elements)
            {
                string name = element.FindElement(By.ClassName(FOLLOW_NICKNAME_CLASS)).Text;
                FollowUser user = new FollowUser()
                {
                    Name = name
                };
                
                users.Add(user);
            }

            return users;
        }

        public FollowUser GetFollowByName(string name)
        {
            _followsButton.Click();
            
            name = name.ToLower();
            if (!WebElementHelper.HasElementIn(_driver, _followContainer, By.Id(name),
                TimeSpan.FromSeconds(1)))
            {
                throw new MessageException("Profile not found");
            }
            
            IWebElement element = _followContainer.FindElement(By.Id(name));
            string nickname = element.FindElement(By.ClassName(FOLLOW_NICKNAME_CLASS)).Text;

            FollowUser user = new FollowUser()
            {
                Name = nickname
            };

            return user;
        }

        public List<Post> GetPosts()
        {
            _newsButton.Click();

            if (WebElementHelper.HasElement(_driver, By.ClassName(NO_FOLLOWS_CLASS), TimeSpan.FromSeconds(2)))
            {
                throw new MessageException("No follows");
            }
            
            if (!WebElementHelper.HasElementIn(_driver, _masonryContainer, By.ClassName(POST_CLASS), TimeSpan.FromSeconds(10)))
            {
                throw new MessageException("Posts not found");
            }
            
            ReadOnlyCollection<IWebElement> elements = _masonryContainer.FindElements(By.ClassName(POST_CLASS));
            List<Post> posts = new List<Post>();

            foreach (var element in elements)
            {
                string accountName = element.FindElement(By.ClassName(POST_ACCOUNT_NAME_CLASS)).Text;
                string accountLogin = element.FindElement(By.ClassName(POST_ACCOUNT_LOGIN_CLASS)).Text;
                string contentText = element.FindElement(By.ClassName(POST_CONTENT_TEXT_CLASS))
                    .FindElement(By.TagName("p")).Text;
                Post post = new Post()
                {
                    Name = accountName,
                    Login = accountLogin,
                    Text = contentText
                };
                
                posts.Add(post);
            }

            return posts;
        }

        public LoginPage ToLogin()
        {
            _userNameInput.Click();
            _exitInput.Click();

            if (WebElementHelper.HasElement(_driver, By.Id(LOGIN_PAGE_REGISTER), TimeSpan.FromSeconds(2)))
            {
                return new LoginPage(_driver);
            }

            return null;
        }

        public SettingsPage ToSettings()
        {
            _userNameInput.Click();
            _settingsInput.Click();

            if (WebElementHelper.HasElement(_driver, By.ClassName(SETTINGS_PAGE_AVATAR_CLASS), TimeSpan.FromSeconds(2)))
            {
                return new SettingsPage(_driver);
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