using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Safari;
using Tests.Exception;
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
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }
        
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _driver.Quit();
        }

        [Test]
        public void FailedRegistration()
        {
            RegisterPage registerPage = new RegisterPage(_driver);
            User user = User.GetRandomUser();
            user.Login = "";
            user.FromService = FromService.Social;
            try
            {
                registerPage.Navigate().FillUser(user).Submit();
            }
            catch (MessageException e)
            {
                Assert.AreEqual("Login is empty",e.Message);
            }

            user.Login = TextHelper.GetRandomWord(10);
            user.Email = "test@t";
            
            try
            {
                registerPage.Navigate().FillUser(user).Submit();
            }
            catch (MessageException e)
            {
                Assert.AreEqual("Email is incorrect",e.Message);
            }
        }

        [Test]
        public void SuccessRegistration()
        {
            RegisterPage registerPage = new RegisterPage(_driver);
            User user = User.GetRandomUser();
            HomePage homePage = registerPage.Navigate().FillUser(user).Submit();
            Assert.NotNull(homePage);
            homePage.ToLogin();
        }


        [Test]
        public void TransitionBetweenLoginAndRegisterPages()
        {
            LoginPage loginPage = new LoginPage(_driver);
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

        [Test]
        public void SuccessLogin()
        {
            LoginPage loginPage = new LoginPage(_driver);
            User user = User.GetValidUserForLogin();
            HomePage homePage = loginPage.Navigate().FillUser(user).Submit();
            Assert.True(homePage.AreEqual());
            homePage.ToLogin();
        }

        [Test]
        public void FailedLogin()
        {
            LoginPage loginPage = new LoginPage(_driver);
            User user = User.GetValidUserForLogin();
            try
            {
                user.Email = "";
                loginPage.Navigate().FillUser(user).Submit();
            }
            catch (MessageException e)
            {
                Assert.AreEqual("Email is empty", e.Message);   
            }
            
            user = User.GetValidUserForLogin();
            try
            {
                user.Email = "teeeeeeest@test.te";
                loginPage.Navigate().FillUser(user).Submit();
            }
            catch (MessageException e)
            {
                Assert.AreEqual("Email is incorrect", e.Message);   
            }
            
            user = User.GetValidUserForLogin();
            try
            {
                user.Password = "";
                loginPage.Navigate().FillUser(user).Submit();
            }
            catch (MessageException e)
            {
                Assert.AreEqual("Password is empty", e.Message);   
            }
            
            user = User.GetValidUserForLogin();
            try
            {
                user.Password = "testtesttest";
                loginPage.Navigate().FillUser(user).Submit();
            }
            catch (MessageException e)
            {
                Assert.AreEqual("Password is incorrect", e.Message);   
            }
        }

        [Test]
        public void SuccessFollow()
        {
            LoginPage loginPage = new LoginPage(_driver);
            User user = User.GetValidUserForLogin();
            HomePage homePage = loginPage.Navigate().FillUser(user).Submit();
            homePage.Follow("anubeloredelana").Follow("wylsacom").Follow("apple");
            List<FollowUser> users = homePage.GetFollows();
            homePage.Navigate();
            homePage.ToLogin();
            Assert.AreEqual("anubeloredelana", users[0].Name);
            Assert.AreEqual("wylsacom", users[1].Name);
            Assert.AreEqual("apple", users[2].Name);
        }

        [Test]
        public void SuccessUnfollow()
        {
            LoginPage loginPage = new LoginPage(_driver);
            User user = User.GetValidUserForLogin();
            HomePage homePage = loginPage.Navigate().FillUser(user).Submit();
            homePage.Unfollow("apple").Unfollow("wylsacom").Unfollow("anubeloredelana");
            try
            {
                homePage.GetFollows();
            }
            catch (MessageException exception)
            {
                Assert.AreEqual("Follow is empty", exception.Message);
            }
            finally
            {
                homePage.Navigate();
                homePage.ToLogin();   
            }
        }


        [Test]
        public void LoginHomeLoginRegister()
        {
            LoginPage loginPage = new LoginPage(_driver);
            User user = User.GetValidUserForLogin();
            Assert.True(loginPage.Navigate().FillUser(user).Submit().ToLogin().ToRegister().FillUser(user).AreEqual());
        }

        [Test]
        public void SuccessSettings()
        {
            LoginPage loginPage = new LoginPage(_driver);
            User user = User.GetValidUserForLogin();
            user.FilePath =
                @"/Users/halloween/Downloads/white_mountains_peaks_lake_reflection_nature-wallpaper-1440x2560.jpg";

            Assert.True(loginPage.Navigate().FillUser(user).Submit().ToSettings().FillUser(user).Submit().ToHome()
                .ToLogin().AreEqual());
        }

        [Test]
        public void SuccessFindPosts()
        {
            LoginPage loginPage = new LoginPage(_driver);
            User user = User.GetValidUserForLogin();

            HomePage homePage = loginPage.Navigate().FillUser(user).Submit();
            List<Post> posts = homePage.Follow("apple").GetPosts();
            homePage.Unfollow("apple");
            homePage.ToLogin();
            
            Assert.True(posts.Count > 0);
        }

        [Test]
        public void FailedFindPosts()
        {
            LoginPage loginPage = new LoginPage(_driver);
            User user = User.GetValidUserForLogin();

            HomePage homePage = loginPage.Navigate().FillUser(user).Submit();
            try
            {
                List<Post> posts = homePage.GetPosts();
            }
            catch (MessageException exception)
            {
                Assert.AreEqual("No follows", exception.Message);
            }
            finally
            {
                homePage.ToLogin();
            }
        }

        [Test]
        public void RegistrationAndLogin()
        {
            RegisterPage registerPage = new RegisterPage(_driver);
            User user = User.GetRandomUser();
            HomePage homePage = registerPage.Navigate().FillUser(user).Submit();
            LoginPage loginPage = homePage.ToLogin();
            homePage = loginPage.Navigate().FillUser(user).Submit();
            Assert.True(homePage.AreEqual());
            homePage.ToLogin();
        }

    }
}