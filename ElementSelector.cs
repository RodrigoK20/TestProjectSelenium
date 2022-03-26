using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;

namespace TestProject1
{
    class ElementSelector
    {
        IWebDriver driver;

        string urlGlobal = "http://testing.todorvachev.com/";

        [SetUp]
        public void Setup()
        {
            //Below code is to get the drivers folder path dynamically
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            driver = new ChromeDriver(path + @"\drivers\");
            driver.Manage().Window.Maximize();
        }

        [Test, Order(1)]
        public void NameSelector()
        {
            driver.Navigate().GoToUrl(urlGlobal + "name");

            IWebElement element = driver.FindElement(By.Name("myName"));

            if (element.Displayed)
            {
                TearDown();
                Assert.Pass("Yes, I can see the element it's right there!");
                
            }

            else
            {
                Assert.Fail("Something went wrong, I couldn't see the element");

            }

            TearDown();
        }

        [Test, Order(2)]
        public void IdSelector()
        {
            driver.Navigate().GoToUrl(urlGlobal + "id");

            IWebElement element = driver.FindElement(By.Id("testImage"));
       
            if (element.Displayed)
            {
                TearDown();
                Assert.Pass("Image exist!");
            }

            else
            {
                Assert.Fail("Something went wrong, I couldn't see the element");

            }

            TearDown();
        }


        [Test, Order(3)]
        public void ClassNameSelector()
        {
            driver.Navigate().GoToUrl(urlGlobal + "class-name/");

            IWebElement element = driver.FindElement(By.ClassName("testClass"));
            
            Console.WriteLine(element);

            try
            {

                if (element.Displayed)
                {
                    TearDown();
                    Assert.Pass();
                   
                }

                else
                {
                    Assert.Fail("Something went wrong, I couldn't see the element");
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Error");
            }

       
        }


        [Test, Order(4)]
        public void XPathSelector()
        {
            var xPath = "//*[@id=\"post-108\"]/div/figure/img";
            //Copy Selector
            var cssPath = "#post-108 > div > figure > img";

            driver.Navigate().GoToUrl(urlGlobal + "css-path/");

            IWebElement cssPathElement;
            IWebElement xPathElement = driver.FindElement(By.XPath(xPath));

            //Exception
            try
            {
                cssPathElement =  driver.FindElement(By.CssSelector(cssPath));

                if (cssPathElement.Displayed)
                {
                    TearDown();
                    Assert.Pass("I can see the CSS Path Element");
                  
                }

            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Error");
            }


            if (xPathElement.Displayed)
            {
                Assert.Pass("I can see the X Path Element");

            }

            else
            {
                Assert.Fail("I can't see the X Path Element");
            }

            TearDown();

        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
