using FluentAutomation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PA_Tests
{
    [TestClass]
    public class Manager : FluentTest
    {
        /**Given I am on the Verify Survey screen
         * And I select a survey to review by clicking on the review icon
         * And I click on the accept icon for each question
         * And I should see the status change to verified
         * And I click the Approve Survey button
         * And I click on the Back button
         * Then I should see the Surevy Status has changed to Verified
         * */
        [Fact, TestMethod]
        public void VerifySurvey()
        {
            var defaultPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            IWebDriver driver = new ChromeDriver(@defaultPath);

            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));

            driver.Navigate().GoToUrl("http://devapps.futuregrowth.co.za/PAS/EmployeeSurvey/ApproveQuestionnaire");

            driver.Close();
            //  IWebDriver driver = new ChromeDriver();

            //  driver.Navigate().GoToUrl("http://devapps.futuregrowth.co.za/PAS/EmployeeSurvey/ApproveQuestionnaire");
            //  IWebElement baseTable = driver.FindElement(By.TagName("table"));
            //  IWebElement tableRow = baseTable.FindElement(By.XPath("//tr[3]"));
            // tableRow.FindElement(By.CssSelector("a")).Click();

        }

    }
}
