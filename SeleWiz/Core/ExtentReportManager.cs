using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleWiz.Core
{
    class ExtentReportManager
    {
        public ExtentReports _extent;
        public ExtentTest _test;

        private static DateTime time;
        private static ExtentReportManager instance = null;
        private static readonly object padlock = new object();
        public static ExtentReportManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ExtentReportManager();
                    }
                    return instance;
                }
            }
        }

        public void ConfigureReport()
        {
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

        public void StartTest()
        {
            _test = _extent.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        public void WrtiteReport()
        {
            _extent.Flush();
        }


        public static string Capture()
        {
            time = DateTime.Now;
            String screenShotName = TestContext.CurrentContext.Test.Name + "_" + time.ToString("h_mm_ss") + ".png";
            ITakesScreenshot ts = (ITakesScreenshot)DriverManager.Instance.driver;
            Screenshot screenshot = ts.GetScreenshot();
            var pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            var actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
            var reportPath = new Uri(actualPath).LocalPath;
            Directory.CreateDirectory(reportPath + "Reports\\" + "Screenshots");
            var finalpth = pth.Substring(0, pth.LastIndexOf("bin")) + "Reports\\Screenshots\\" + screenShotName;
            var localpath = new Uri(finalpth).LocalPath;
            screenshot.SaveAsFile(localpath, ScreenshotImageFormat.Png);
            return reportPath;
        }

    }

}

