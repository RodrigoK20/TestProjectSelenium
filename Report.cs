using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;


namespace TestProject1
{
    class Report
    {
        IWebDriver driver;
        IWebElement textBox;

        //Instance of extents reports
        protected ExtentReports _extent;
        protected ExtentTest _test;

        [SetUp]
        public void Setup()
        {
            //Below code is to get the drivers folder path dynamically
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            driver = new ChromeDriver(path + @"\drivers\");
            driver.Manage().Window.Maximize();

        }

        //Setup Report
        [OneTimeSetUp]
        public void SetupReporting()
        {
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            var filename = "Report" + ".html";
            var htmlReporter = new ExtentHtmlReporter(path + @"\reports\" + filename);
     

            _extent = new ExtentReports();
            _extent.AddSystemInfo("Username", "Rodrigo");
            _extent.AddSystemInfo("Enviroment", "Test");
            _extent.AddSystemInfo("App name", "ASESUISA");
            _extent.AttachReporter(htmlReporter);
           
        }

        [Test, Order(1)]
        public void TextBoxTest()
        {
            //Parameters test
            _test = _extent.CreateTest("TextBox Test").Info("TextBox Test");
            _test.AssignAuthor("Rodrigo Alejandro");
            //var feature = _extent.CreateTest<Feature>("Login");
            //var scenario = feature.CreateNode<Scenario>("Login user as Administrator");
            //scenario.CreateNode<Given>("I navigate to the app");

            string url = "https://testing.todorvachev.com/text-input-field/";

            driver.Navigate().GoToUrl(url);

            textBox = driver.FindElement(By.Name("username"));

            if (textBox.Displayed == true)
            {
                textBox.SendKeys("Test text");
                string result = textBox.GetAttribute("value");
                string maxLength = textBox.GetAttribute("maxlength");


                //Assert.Pass method allows you to immediately end the test, recording it as successful

                //Screenshot
                string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
                path = path + @"\reports\screen1.png";
                Screenshot screenshot = (driver as ITakesScreenshot).GetScreenshot();
                screenshot.SaveAsFile(path, ScreenshotImageFormat.Png);

                _test.Pass("TextBox Test Passed").AddScreenCaptureFromPath(path);

                finishTest();
                Assert.Pass("Test Successfull");

            }

            else
            {
                Assert.Fail("Error");
                //  _test.Log(Status.Fail, "Test Fail");
                _test.Fail("TextBox Test Fail!");
                //throw;
            }

        }

        [Test, Order(2)]
        public void TestNumber()
        {

            _test = _extent.CreateTest("Test Number").Info("Number Test");
            _test.AssignAuthor("Rodrigo Alejandro");

            int number1 = 12;
            int number2 = 6;


            if(number1 + number2 == 12)
            {
                Assert.Pass("Success");
                _test.Pass("Test passed");
            }

            else
            {
                _test.Fail("Error");
                Assert.Fail("Error");
            }
           
        }

        [OneTimeTearDown]
        public void GenerateReport()
        {

            _extent.Flush();
        }

        [OneTimeTearDown]
        public void finishTest()
        {
            driver.Quit();
        }


    }
}
