using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace PA_Selenium_Tests
{
     public class FirstTestCase
    {
        
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://portal.futuregrowth.co.za";
        }
    }
}
