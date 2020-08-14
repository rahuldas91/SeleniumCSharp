using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using SeleWiz.PageObjects;
using System.Windows.Forms;
using AventStack.ExtentReports;
using System.IO;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;

namespace SeleWiz
{
    class Program
    {
        Login objLogin;
        Dictionary<string, string> dictionary;
        public ExtentReports _extent;
        public ExtentTest _test;


        [OneTimeSetUp]
        public void oneTimeSetup()
        {

            dictionary = new Dictionary<string, string>();

            dictionary.Add("username", "SampleUser");
            dictionary.Add("password", "DMyPasswordas");



            var path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            var actualPath = path.Substring(0, path.LastIndexOf("bin"));
            var projectPath = new Uri(actualPath).LocalPath;
            Directory.CreateDirectory(projectPath.ToString() + "Reports");
            var reportPath = projectPath + "Reports\\ExtentReport.html";
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            _extent = new ExtentReports();
            _extent.AttachReporter(htmlReporter);
            _extent.AddSystemInfo("Host Name", "LocalHost");
            _extent.AddSystemInfo("Environment", "QA");
            _extent.AddSystemInfo("UserName", "TestUser");

        }

        [OneTimeTearDown]
        public void oneTimeTearDown()
        {
            Console.WriteLine("Completed execution");
        }

        [SetUp]
        public void startBrowser()
        {
            DriverManager.Instance.startBrowser();
            DriverManager.Instance.driver.Url = "https://www.facebook.com/";

        }

        [Test]
        public void test()
        {
            objLogin = new Login();
            objLogin.enterUserName(dictionary["username"]);
            objLogin.enterpassword(dictionary["password"]);

            String pageTitle = DriverManager.Instance.driver.Title;
            //MessageBox.Show(pageTitle);
            //Assert.Equals("Facebook – log in or sign up", pageTitle);
            _test = _extent.CreateTest(TestContext.CurrentContext.Test.Name);
            Assert.IsTrue(pageTitle.Contains("Facebook"));

            _test.Log(Status.Fail, "Fail");

            DateTime time = DateTime.Now;
            String fileName = "Screenshot_" +time.ToString("h_mm_ss") + ".png";
            String screenShotPath = Capture(DriverManager.Instance.driver, fileName);
            _test.Log(Status.Fail, "Snapshot below: " +_test.AddScreenCaptureFromPath("Screenshots\\" +fileName));


            _test = _extent.CreateTest(TestContext.CurrentContext.Test.Name);
            Assert.IsTrue(pageTitle.Contains("Facebook"));

            _test.Log(Status.Pass,"THis is skipped");

            time = DateTime.Now;
            fileName = "Screenshot_" + time.ToString("h_mm_ss") + ".png";
            screenShotPath = Capture(DriverManager.Instance.driver, fileName);
            _test.Log(Status.Skip, "Snapshot below: " + _test.AddScreenCaptureFromPath("Screenshots\\" + fileName));


        }

        [TearDown]
        public void closeBrowser()
        {
            DriverManager.Instance.driver.Close();
            _extent.Flush();
        }


        public static string Capture(IWebDriver driver, String screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            var pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            var actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
            var reportPath = new Uri(actualPath).LocalPath;
            Directory.CreateDirectory(reportPath + "Reports\\" + "Screenshots");
            var finalpth = pth.Substring(0, pth.LastIndexOf("bin")) + "Reports\\Screenshots\\" +screenShotName;
            var localpath = new Uri(finalpth).LocalPath;
            screenshot.SaveAsFile(localpath, ScreenshotImageFormat.Png);
            return reportPath;
        }

    }
    
}
