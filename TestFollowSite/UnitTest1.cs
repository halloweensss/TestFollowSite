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
        public void SuccessRegistration()
        {
            RegisterPage registerPage = new RegisterPage(_driver);
            User user = User.GetRandomUser();
            try
            {
                HomePage homePage = registerPage.Navigate().FillUser(user).Submit();
                Assert.NotNull(homePage);
                homePage.ToLogin();
            }
            catch (Exception e)
            {
                
            }
        }


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


        [Test]
        public void SuccessLogin()
        {
            LoginPage loginPage = new LoginPage(_driver);
            User user = User.GetValidUserForLogin();
            try
            {
                HomePage homePage = loginPage.Navigate().FillUser(user).Submit();
                Assert.True(homePage.AreEqual());
                homePage.ToLogin();
            }
            catch (Exception e)
            {
                
            }
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
            catch (Exception e)
            {
                Assert.AreEqual("Email is empty", e.Message);   
            }
            
            user = User.GetValidUserForLogin();
            try
            {
                user.Email = "teeeeeeest@test.te";
                loginPage.Navigate().FillUser(user).Submit();
            }
            catch (Exception e)
            {
                Assert.AreEqual("Email is incorrect", e.Message);   
            }
            
            user = User.GetValidUserForLogin();
            try
            {
                user.Password = "";
                loginPage.Navigate().FillUser(user).Submit();
            }
            catch (Exception e)
            {
                Assert.AreEqual("Password is empty", e.Message);   
            }
            
            user = User.GetValidUserForLogin();
            try
            {
                user.Password = "testtesttest";
                loginPage.Navigate().FillUser(user).Submit();
            }
            catch (Exception e)
            {
                Assert.AreEqual("Password is incorrect", e.Message);   
            }
        }
        */
/*
        [Test]
        public void SuccessFollow()
        {
            LoginPage loginPage = new LoginPage(_driver);
            User user = User.GetValidUserForLogin();
            try
            {
                HomePage homePage = loginPage.Navigate().FillUser(user).Submit();
                homePage.Follow("apple").Follow("wylsacom").Follow("anubeloredelana");
                homePage.Navigate();
                homePage.ToLogin();
            }
            catch (Exception e)
            {
                Assert.AreEqual("", e.Message);
            }
        }

        [Test]
        public void SuccessUnfollow()
        {
            LoginPage loginPage = new LoginPage(_driver);
            User user = User.GetValidUserForLogin();
            try
            {
                HomePage homePage = loginPage.Navigate().FillUser(user).Submit();
                homePage.Unfollow("apple").Unfollow("wylsacom").Unfollow("anubeloredelana");
                homePage.Navigate();
                homePage.ToLogin();
            }
            catch (Exception e)
            {
                Assert.AreEqual("", e.Message);
            }
        }
        */
        /*
        [Test]
        public void LoginHomeLoginRegister()
        {
            LoginPage loginPage = new LoginPage(_driver);
            User user = User.GetValidUserForLogin();
            try
            {
                Assert.True(loginPage.Navigate().FillUser(user).Submit().ToLogin().ToRegister().FillUser(user).AreEqual());
            }
            catch (Exception exception)
            {
                
            }
        }*/
        
        [Test]
        public void SuccessSettings()
        {
            LoginPage loginPage = new LoginPage(_driver);
            User user = User.GetValidUserForLogin();
            user.FilePath = @"/Users/halloween/Downloads/white_mountains_peaks_lake_reflection_nature-wallpaper-1440x2560.jpg";
            try
            {
                Assert.True(loginPage.Navigate().FillUser(user).Submit().ToSettings().FillUser(user).Submit().ToHome().ToLogin().AreEqual());
            }
            catch (Exception e)
            {
                
            }
        }
    }
}