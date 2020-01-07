using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Safari;
using Tests.Models;
using Tests.Pages;

namespace Tests
{
    public class Tests
    {
        private IWebDriver _safariDriver;
        
        [SetUp]
        public void Setup()
        {
            _safariDriver = new ChromeDriver("/Users/halloween/RiderProjects/TestFollowSite/TestFollowSite/bin/Debug/netcoreapp2.1/");
        }

        [Test]
        public void Test1()
        {
            RegisterPage registerPage = new RegisterPage(_safariDriver);
            User user = User.GetRandomUserForRegistration();
            user.FromService = FromService.Social;
            registerPage.Navigate().FillUser(user).Submit();
            //Assert.IsNull(User.GetRandomUserForRegistration());
            //Assert.AreEqual("123",User.GetRandomUserForRegistration().Login);
            // _safariDriver = new SafariDriver();
            //_safariDriver.Navigate().GoToUrl("http://127.0.0.1:8083/login");

        }
    }
}