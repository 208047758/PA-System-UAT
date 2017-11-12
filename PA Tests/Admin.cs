using FluentAutomation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI;
using Xunit;


namespace PA_Tests
{
    [TestClass]
    public class Admin : FluentTest
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://devapps.futuregrowth.co.za/PAS";
        }
        /* Given I am on the Upload Employee Survey screen
        * And I choose an interim period
        * And I choose an excel file to upload
        * And I select a year
        * And I select a month
        * And I click the submit button
        * Then I should see a confirmation message informing me the upload is successful
        */
        [Fact, TestMethod]
        public void UploadEmployeeSurvey()
        {
           
            var defaultPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            IWebDriver driver = new ChromeDriver(@defaultPath);

            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));

            driver.Navigate().GoToUrl("http://devapps.futuregrowth.co.za/PAS/EmployeeSurvey/UploadEmployeeSurvey");

            driver.FindElement(By.CssSelector("a")).Click();
            Thread.Sleep(5000);

            IWebElement upload = driver.FindElement(By.Id("fileUploadClientFile")); //XPath("//*[@id='divFileUpload']/div[1]/label[2]/i")).Click();
            upload.SendKeys("C://Users//andilem//Documents//Reza Daniels - Survey.xlsx");
            Thread.Sleep(5000);


            IWebElement submit = driver.FindElement(By.Id("btnUpload"));
            submit.Click();

            Thread.Sleep(5000);
            driver.Close();

        }

        /**Given I am on the Update Rater Information screen
           * And I select the department
           * And I select the individual's survey whom I want to update by clicking on the review icon
           * And I select/deselect raters
           * */
        [Fact, TestMethod]
        public void UpdateSurveyRaters()
        {
            var defaultPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            IWebDriver driver = new ChromeDriver(@defaultPath);

            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));

            driver.Navigate().GoToUrl("http://devapps.futuregrowth.co.za/PAS/Rater/UpdateSurveyRater");

            driver.FindElement(By.XPath("//*[@id='tableApproveQuestions']/tbody/tr/td[6]/a/i")).Click();
            Thread.Sleep(5000);

            driver.FindElement(By.XPath("//*[@id='btnShowSelectedRaters']/i")).Click();
            Thread.Sleep(5000);

            driver.FindElement(By.XPath("//*[@id='UpdateRaters']/div[2]/div[1]/div[1]/div/div[1]/button/span")).Click();
            Thread.Sleep(2000);

            driver.FindElement(By.XPath("//*[@id='UpdateRaters']/div[2]/div[1]/div[1]/div/div[1]/div/ul/li[6]/label/span")).Click();
            Thread.Sleep(2000);

            driver.FindElement(By.XPath("//*[@id='UpdateRaters']/div[2]/div[1]/div[1]/div/div[1]/div/ul/li[45]/label/span")).Click();
            Thread.Sleep(2000);

            driver.FindElement(By.XPath("//*[@id='UpdateRaters']/div[2]/div[1]/div[1]/div/div[1]/button/span")).Click();
            Thread.Sleep(2000);

            IWebElement submit = driver.FindElement(By.Id("btnUpdateSurveyRater"));
            submit.Click();

            driver.Close();
         

        }

        /**Given I am on the View Survey Completion screen
           * And I filter by Business Unit
           * And I select a specific business unit
           * And I should see the completion status of the that unit
           * And I filter by rater
           * And I select a Rater
           * Then I should see the number of surveys the selected rater has completed, 
           * the total surveys that the rater has been assigned and how many surveys in total the rater has been assigned
           * */
        [Fact, TestMethod]
        public void ViewSurveyCompletion()
        {
            var defaultPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            IWebDriver driver = new ChromeDriver(@defaultPath);

            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));

            driver.Navigate().GoToUrl("http://devapps.futuregrowth.co.za/PAS/Admin/ViewSurveyCompletion");

            driver.FindElement(By.Id("optDepartment")).Click();

            SelectElement unit = new SelectElement(driver.FindElement(By.Id("drpBusinessUnit")));
            unit.SelectByText("INFORMATION TECHNOLOGY");
            Thread.Sleep(5000);

            driver.FindElement(By.Id("optRaterName")).Click();

            SelectElement rater = new SelectElement(driver.FindElement(By.Id("optRater")));
            rater.SelectByText("Andile Madiba");
            Thread.Sleep(5000);

            driver.Close();
            

        }

        /**Given I am on the Update Rater Information screen
           * And I select an option to filter by survey 
           * And I select a business unit
           * And I click on the Next button
           * And I select the Rater(s) to be notified
           * And I click on the Send Email button
           * Then I should see a messaging informing me the email was sent to the rater
           * */
        /**Given I am on the Update Rater Information screen
       * And I select an option to filter by  rater
       * And I select a rater
       * And I click on the Send Email button
       * Then I should see a messaging informing me the email was sent to the rater
       * */
        [Fact, TestMethod]
        public void SendNotification()
        {
           
            var defaultPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            IWebDriver driver = new ChromeDriver(@defaultPath);

            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));

            driver.Navigate().GoToUrl("http://devapps.futuregrowth.co.za/PAS/EmailNotification/NotifyRaters");

            SelectElement option = new SelectElement(driver.FindElement(By.Id("drpSelectOption")));
            option.SelectByText("By Survey");
            SelectElement unit = new SelectElement(driver.FindElement(By.Id("drpSelectedBusinessUnit")));
            unit.SelectByText("INFORMATION TECHNOLOGY");
            SelectElement emp = new SelectElement(driver.FindElement(By.Id("drpSurveyFor")));
            emp.SelectByText("Richard Forsyth");

            IWebElement next = driver.FindElement(By.Id("btnNext"));
            next.Click();

            Thread.Sleep(5000);

            driver.FindElement(By.XPath("//*[@id='divSelectedRatersToNotify']/div/button/span")).Click();

            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//*[@id='divSelectedRatersToNotify']/div/div/ul/li[3]/label/span")).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//*[@id='divSelectedRatersToNotify']/div/button/span")).Click();
            IWebElement submit = driver.FindElement(By.Id("btnSendEmail"));
            submit.Click();
            Thread.Sleep(5000);

            driver.Close();

            var default_Path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            IWebDriver _driver = new ChromeDriver(@default_Path);

            _driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));

            _driver.Navigate().GoToUrl("http://devapps.futuregrowth.co.za/PAS/EmailNotification/NotifyRaters");

            SelectElement option2 = new SelectElement(_driver.FindElement(By.Id("drpSelectOption")));
            option2.SelectByText("By Rater");
            _driver.FindElement(By.XPath("//*[@id='divSelectedRatersToNotify']/div/button/span")).Click();

            Thread.Sleep(5000);
            _driver.FindElement(By.XPath("//*[@id='divSelectedRatersToNotify']/div/div/ul/li[2]/label/span")).Click();
            _driver.FindElement(By.XPath("//*[@id='divSelectedRatersToNotify']/div/div/ul/li[3]/label/span")).Click();
            Thread.Sleep(5000);
            _driver.FindElement(By.XPath("//*[@id='divSelectedRatersToNotify']/div/button/span")).Click();

            IWebElement submit2 = _driver.FindElement(By.Id("btnSendEmail"));
            submit2.Click();

        }

        /**Given I am on the Review Survey screen
          * And I select a business unit
          * And I click on the Review icon of a Survey I'd like to review
          * And I click on the Review Survey button
          * And I click on the Back button
          * Then I should see the Surevy Status has changed to Reviewed
          * */

        [Fact, TestMethod]
        public void ReviewSurvey()
        {
            var defaultPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            IWebDriver driver = new ChromeDriver(@defaultPath);

            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));

            driver.Navigate().GoToUrl("http://devapps.futuregrowth.co.za/PAS/Admin/ReviewSurvey");

            SelectElement option = new SelectElement(driver.FindElement(By.Id("drpSelectOption")));
            option.SelectByText("By Survey");

            //SelectElement unit = new SelectElement(driver.FindElement(By.Id("drpBusinessUnit")));
            // unit.SelectByText("INFORMATION TECHNOLOGYY");
            // Thread.Sleep(5000);
            //IWebElement baseTable = driver.FindElement(By.TagName("table"));
            // IWebElement tableRow = baseTable.FindElement(By.XPath("//tr[2]"));
            //tableRow.FindElement(By.CssSelector("a")).Click();

            // SelectElement unit = new SelectElement(driver.FindElement(By.XPath("//*[@id='formApproveAsurvey']/div/div/div/div/div/div[4]/div/select"))); // Id("optDataSource")));
            //  unit.SelectByValue("INFORMATION TECHNOLOGY");
            //  Thread.Sleep(5000);
            // IWebElement baseTable = driver.FindElement(By.TagName("table"));
            //  Thread.Sleep(5000);
            //  IWebElement tableRow = baseTable.FindElement(By.XPath("//tr[5]"));
            // Thread.Sleep(5000);
            //  tableRow.FindElement(By.CssSelector("a")).Click();


            driver.Close();
           


        }
    }
}
