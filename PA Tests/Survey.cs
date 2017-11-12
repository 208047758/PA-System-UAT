using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAutomation;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.IO;
using context = System.Web.HttpContext;
using System.Configuration;
using System.Threading;

namespace PA_Tests
{
    [TestClass]
    public class Survey : FluentTest
    {
        
        /* Given I am on the Complete Employee Survey screen
         * And I should a list of all the surveys I am supposed to complete
         * And I select a survey the survey I would like to complete
         * And I should see a list of questions that I need to answer
         * And I select a question to answer/click on the edit icon
         * And I select a rating
         * And I add a comment in the text box
         * And I click the next button
         * And I should see the selected rating and response in line with the question answered
         * And I select a rating
         * And I add a comment in the text box
         * And I continue to do so for the remainder of the questions
         * And I click on the Back button
         * And I click on the Finalise Ratings button
         * And I add a comment
         * And I click on the Cancel button
         * And I click on the Finalise Rating button again
         * And I add a comment
         * And I click on the Finalise button
         * And I should see the Average rating
         * And I click on the Re-Open button
         * And I should no longer see the Average Rating
         */
        [Fact, TestMethod]
        public void CompleteSurvey()
        {
            var defaultPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            IWebDriver driver = new ChromeDriver(@defaultPath);

            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));

            driver.Navigate().GoToUrl("http://devapps.futuregrowth.co.za/PAS/EmployeeSurvey/CompleteSurvey");

            driver.FindElement(By.XPath("//*[@id='tableApproveQuestions']/tbody/tr/td[7]/a/i")).Click();
            Thread.Sleep(5000);

            driver.FindElement(By.XPath("//*[@id='btnEditQuestion']/i")).Click();
            Thread.Sleep(5000);

            SelectElement rating = new SelectElement(driver.FindElement(By.Id("ratingScale")));
            rating.SelectByValue("3");

            driver.FindElement(By.Id("questionComment")).SendKeys("Richard is just too awesome for words");
            IWebElement next = driver.FindElement(By.Id("btnSaveResponse"));
            next.Click();
            Thread.Sleep(5000);

            SelectElement rating2 = new SelectElement(driver.FindElement(By.Id("ratingScale")));
            rating2.SelectByValue("4");

            driver.FindElement(By.Id("questionComment")).SendKeys("Well, I wouldn't quite say so but hey, if that's what youwant to go with, fine by me.");
            Thread.Sleep(5000);

            next.Click();
            Thread.Sleep(5000);

            driver.FindElement(By.XPath("//*[@id='fromCompleteSurvey']/div/div/div/div/div/div[3]/div[2]/div/input")).Click();
            Thread.Sleep(5000);

            driver.FindElement(By.XPath("//*[@id='tableApproveQuestions']/tbody/tr/td[8]/a/i")).Click();
            driver.FindElement(By.Id("txtOverallComment")).SendKeys("And that's a wrap folks");

            IWebElement finalise = driver.FindElement(By.Id("btnFinaliseSurvey"));
            finalise.Click();
            Thread.Sleep(5000);

            driver.FindElement(By.XPath("//*[@id='tableApproveQuestions']/tbody/tr/td[9]/a/i")).Click();

            driver.FindElement(By.XPath("//*[@id='tableApproveQuestions']/tbody/tr/td[8]/a/i")).Click(); 
            driver.FindElement(By.Id("txtOverallComment")).SendKeys("And that's a wrap folks");

            finalise.Click();
            Thread.Sleep(5000);
            driver.Close();
            
        }

    }
}
