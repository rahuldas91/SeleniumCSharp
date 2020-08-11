using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using SeleWiz.PageObjects;

namespace SeleWiz
{
    class Program
    {
        Login objLogin;
        Dictionary<string, string> dictionary;
     [OneTimeSetUp]
        public void oneTimeSetup()
        {

           dictionary =
                       new Dictionary<string, string>();

            dictionary.Add("username", "SampleUser");
            dictionary.Add("password", "DMyPasswordas");

            //foreach (KeyValuePair<string, string> ele2 in dictionary)
            //{

            //    Debug.WriteLine("{0} and {1}", ele2.Key, ele2.Value);
            //}
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
            // dm.driver.Url = "http://www.google.co.in";

            //driver = new ChromeDriver(@"E:\Work\VisualStudio\SeleWiz\SeleWiz\3rdParty");
        }

        [Test]
        public void test()
        {
            objLogin = new Login();
            objLogin.enterUserName(dictionary["username"]);
            objLogin.enterpassword(dictionary["password"]);
        }

        [TearDown]
        public void closeBrowser()
        {
            //DriverManager.Instance.driver.Close();
        }
    }
}
