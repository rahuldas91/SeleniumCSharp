using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleWiz.PageObjects
{
    class Login
    {
       

        public Login()
        {
            
        }

        public void enterUserName(String username)
        {
            IWebElement usernameElement = DriverManager.Instance.driver.FindElement(By.Id("email"));
            usernameElement.EnterText(username);
            
        }

        public void enterpassword(String password)
        {
            IWebElement passwordElement = DriverManager.Instance.driver.FindElement(By.Id("pass"));
            passwordElement.EnterText(password);
        }
    }
}
