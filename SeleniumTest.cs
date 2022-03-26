using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace TestProject1
{
    public class SeleniumTests
    {

        IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            //Below code is to get the drivers folder path dynamically
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            driver = new ChromeDriver(path+@"\drivers\");
            driver.Manage().Window.Maximize();

        }

        [Test, Order(3)]
        public void verifyLink()
        {
            driver.Navigate().GoToUrl("http://testing.todorvachev.com");

            Thread.Sleep(3000);

            Assert.Pass();

            driver.Quit();

    
        }

        [Test, Order(2)]
        public void verifyLink2()
        {
            driver.Navigate().GoToUrl("http://testing.todorvachev.com");

            Thread.Sleep(3000);

            driver.Quit();

            //Assert.Pass();
        }

        [Test, Order(1)]
        public void verifyPricingPage()
        {
            driver.Navigate().GoToUrl("http://browserstack.com/pricing");

            IWebElement contactUsPageHeader = driver.FindElement(By.TagName("h1"));

            Assert.IsTrue(contactUsPageHeader.Text.Contains("Replace your device lab and VMs with any of these plans"));

            TearDown();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    
    }
}