using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Safari;
using Tests.Helpers;
using Tests.Models;
using Tests.Pages;

namespace Tests
{
    public class Tests
    {
        private IWebDriver _driver;
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _driver = new ChromeDriver("/Users/halloween/RiderProjects/TestFollowSite/TestFollowSite/bin/Debug/netcoreapp2.1/");
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        }
        
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _driver.Quit();
        }

        [Test]
        public void FailedRegistrationTest()
        {
            RegisterPage registerPage = new RegisterPage(_driver);
            User user = User.GetRandomUser();
            user.Login = "";
            user.FromService = FromService.Social;
            try
            {
                registerPage.Navigate().FillUser(user).Submit();
            }
            catch (Exception e)
            {
                Assert.AreEqual("Login is empty",e.Message);
            }

            user.Login = TextHelper.GetRandomWord(10);
            user.Email = "test@t";
            
            try
            {
                registerPage.Navigate().FillUser(user).Submit();
            }
            catch (Exception e)
            {
                Assert.AreEqual("Email is incorrect",e.Message);
            }
        }

        [Test]
        public void SuccessRegistrationTest()
        {
            RegisterPage registerPage = new RegisterPage(_driver);
            User user = User.GetRandomUser();
            try
            {
                HomePage homePage = registerPage.Navigate().FillUser(user).Submit();
                Assert.NotNull(homePage);
            }
            catch (Exception e)
            {
                
            }
        }
    }
}