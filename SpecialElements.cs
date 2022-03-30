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
    class SpecialElements
    {
        IWebDriver driver;
        IWebElement textBox, checkBox, radioButton, dropDownMenu, elementFromTheDropDownMenu, image;
        IAlert alert;
       

        [SetUp]
        public void Setup()
        {
            //Below code is to get the drivers folder path dynamically
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            driver = new ChromeDriver(path + @"\drivers\");
            driver.Manage().Window.Maximize();

        }

        [Test, Order(1)]
        public void TextBox()
        {
            string url = "https://testing.todorvachev.com/text-input-field/";

            driver.Navigate().GoToUrl(url);

            textBox = driver.FindElement(By.Name("username"));
            textBox.SendKeys("Test text");

            //Screenshot
            Screenshot screenshot = (driver as ITakesScreenshot).GetScreenshot();
            screenshot.SaveAsFile("C://Users//RODRIGO//Desktop//screen1.png", ScreenshotImageFormat.Png);

            Thread.Sleep(3000);

            // textBox.Clear();
            //Getting value
            string result = textBox.GetAttribute("value");

            string maxLength = textBox.GetAttribute("maxlength");

            Thread.Sleep(3000);

            driver.Quit();

            Assert.Pass(result +  "length:" + maxLength);
           
        }

        [Test, Order(2)]
        public void CheckBox()
        {
            string url = "https://testing.todorvachev.com/check-button-test-3/";
            string option = "1";


            driver.Navigate().GoToUrl(url);

            checkBox = driver.FindElement(By.CssSelector("#post-33 > div > p:nth-child(8) > input[type=checkbox]:nth-child(" + option + ")"));

            checkBox.Click();

            Thread.Sleep(3000);

            if(checkBox.GetAttribute("checked") == "true")
            {
                Console.WriteLine("The checkbox is checked!");
            }

            else
            {
                Console.WriteLine("The checkbox is not checked!");
            }

            driver.Quit();
        }
        
        [Test, Order(3)]
        public void RadioButton()
        {
            string url = "https://testing.todorvachev.com/radio-button-test/";

            //Array option
            string[] option = { "1", "3", "5" };

            driver.Navigate().GoToUrl(url);

            for(int i=0; i < option.Length; i++)
            {
                radioButton = driver.FindElement(By.CssSelector("#post-10 > div > form > p:nth-child(6) > input[type=radio]:nth-child(" + option[i] + ")"));

                if (radioButton.GetAttribute("checked") == "true")
                {
                    Console.WriteLine("The " + (i+1) + " radio button is checked and his value is: " + radioButton.GetAttribute("value"));
                    
                }

                else
                {
                    Console.WriteLine("This is one the unchecked radio buttons");

                }

            }

            driver.Quit();

        }

        [Test, Order(4)]
        public void DropDownMenu()
        {
            string url = "https://testing.todorvachev.com/drop-down-menu-test/";

            string dropDownMenuElements = "#post-6 > div > p:nth-child(6) > select > option:nth-child(3)";

            driver.Navigate().GoToUrl(url);

            dropDownMenu = driver.FindElement(By.Name("DropDownTest"));

            Console.WriteLine("The selected value is: "+ dropDownMenu.GetAttribute("value"));

            elementFromTheDropDownMenu = driver.FindElement(By.CssSelector(dropDownMenuElements));

            elementFromTheDropDownMenu.Click();

            Console.WriteLine("The third value is:" + elementFromTheDropDownMenu.GetAttribute("value"));

            //Loop
            for(int i=1; i<=4; i++)
            {
                dropDownMenuElements = "#post-6 > div > p:nth-child(6) > select > option:nth-child("+ i +")";

                elementFromTheDropDownMenu = driver.FindElement(By.CssSelector(dropDownMenuElements));

                Console.WriteLine("The: " + i +  " option from the drop down menu is:" + elementFromTheDropDownMenu.GetAttribute("value"));

            }

            driver.Quit();

        }

        [Test, Order(5)]
        public void AlertBox()
        {
            string url = "https://testing.todorvachev.com/alert-box/";

            driver.Navigate().GoToUrl(url);

            alert = driver.SwitchTo().Alert();

            Console.WriteLine(alert.Text);

            alert.Accept();

            image = driver.FindElement(By.CssSelector("#post-119 > div > figure > img"));

            try
            {
                if (image.Displayed)
                {
                    Console.WriteLine("The alert was succesfully accepted an I can see the Image");
                }

            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Something went wrong");
            }

            //Thread.Sleep(3000);

            driver.Quit();
        }

    }
}
