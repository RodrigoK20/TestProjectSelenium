using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace TestProject1
{
    class MultipleWindow
    {
        IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            //Below code is to get the drivers folder path dynamically
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            driver = new ChromeDriver(path + @"\drivers\");
            driver.Manage().Window.Maximize();

        }

        [Test]
        public void Test()
        {
            driver.Navigate().GoToUrl("https://demoqa.com/browser-windows");

            //Store the parent window
            string parentWindowHandle = driver.CurrentWindowHandle;
            Console.WriteLine("Parent window's handle ->" + parentWindowHandle);

            IWebElement clickElement = driver.FindElement(By.Id("windowButton"));

            // Multiple click to open multiple window
            for (var i = 0; i < 3; i++)
            {
                clickElement.Click();
                Thread.Sleep(3000);
            }

            //Store all the opened window into the list
            //Print each and every items of the list
            List<string> lstWindow = driver.WindowHandles.ToList();
            string lastWindowHandle = "";

            foreach (var handle in lstWindow)
            {
                //Traverse each and every window
                Console.WriteLine("Switching to window ->" + handle);
                Console.WriteLine("Navigating to google.com");

                //Switch to the desired window first and then execute commands using driver
                driver.SwitchTo().Window(handle);
                driver.Navigate().GoToUrl("https://google.com");

                lastWindowHandle = handle;
            }

            //Switch to the parent window
            driver.SwitchTo().Window(parentWindowHandle);

            //Close the parent window
            driver.Close();


            //at this point there is no focused window, we have to explicitly switch back to some window.
            driver.SwitchTo().Window(lastWindowHandle);

            driver.Navigate().GoToUrl("https://toolsqa.com");
        }

    }
}
