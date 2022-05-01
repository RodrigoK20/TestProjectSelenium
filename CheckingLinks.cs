using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;

namespace TestProject1
{
    class CheckingLinks
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

        //Check all links for a single page 
        [Test]
        public async Task CheckingURL()
        {
            driver.Url = "https://www.lambdatest.com/blog";

            int validLinks = 0;
            int invalidLinks = 0;

            using var client = new HttpClient();
            var links = driver.FindElements(By.TagName("a"));

            Console.WriteLine("Looking at all the URLs in LambdaTest: ");

            //Loop through all the urls
            foreach (var link in links)
            {
                if (!(link.Text.Contains("Email") || link.Text.Contains("https://www.linkedin.com") || link.Text == "" || link.Equals(null)))
                {
                    try
                    {
                        //Get the URI
                        HttpResponseMessage response = await client.GetAsync(link.GetAttribute("href"));
                        System.Console.WriteLine($"URL: {link.GetAttribute("href")} status is :{response.StatusCode}");
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            validLinks++;
                        }

                        else
                        {
                            invalidLinks++;
                        }
                    }
                    catch (Exception ex)
                    {
                        //Important to add exception (http and https error)
                        if ((ex is ArgumentNullException) ||
                             (ex is NotSupportedException))
                        {
                            System.Console.WriteLine("Exception occured\n");
                        }
                    }
                }

            }


            Console.WriteLine("Detection of broken links completed with: " + invalidLinks + " and " + validLinks + " valid links");

            driver.Quit();

        }



    }
}
