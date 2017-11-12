using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PA_Tests
{
    class Notifications
    {
        [Fact, TestMethod]
        public void TriggerSurveySending()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://devapps.futuregrowth.co.za/PAS/EmailNotification/NotifyRaters");

            SelectElement department = new SelectElement(driver.FindElement(By.Id("drpDepartment")));
            department.SelectByValue("");

            SelectElement status = new SelectElement(driver.FindElement(By.Id("optSurveyStatus")));
            status.SelectByValue("");


            IWebElement startDate = driver.FindElement(By.Id("txtStartDate"));
            startDate.SendKeys("dd-mm-yyyy");

            IWebElement endDate = driver.FindElement(By.Id("txtEndDate"));
            endDate.SendKeys("dd-mm-yyyy");

            SelectElement action = new SelectElement(driver.FindElement(By.Id("drpSurveyRater")));
            action.SelectByValue("");

            IWebElement send = driver.FindElement(By.Id("btnSendNotification"));
            send.Click();
        }
    }
}
