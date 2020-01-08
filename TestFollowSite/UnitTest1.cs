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
        }
        
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _driver.Quit();
        }
/*
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
*/
        [Test]
        public void TransitionBetweenLoginAndRegisterPages()
        {
            LoginPage loginPage = new LoginPage(_driver);
            try
            {
                RegisterPage registerPage = loginPage.Navigate().ToRegister();
                User user = User.GetRandomUser();
                registerPage.FillUser(user);
                Assert.True(registerPage.AreEqual());
                loginPage = registerPage.ToLogin();
                Assert.True(loginPage.AreEqual());
                user = User.GetValidUserForLogin();
                HomePage homePage = loginPage.FillUser(user).Submit();
                Assert.True(homePage.AreEqual());
            }
            catch (Exception e)
            {
                
            }
        }
    }
}