using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleWiz
{
    public static class IWebElementExtension
    {

        public static void EnterText(this IWebElement dummy, String text)
        {
            
           
            dummy.SendKeys(text);
        }

        public static IWebElement WaitUntilElementExist(this By tmpBy)
        {
            WebDriverWait wait = new WebDriverWait(DriverManager.Instance.driver, TimeSpan.FromSeconds(10));
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(tmpBy));
        }

    }
}
