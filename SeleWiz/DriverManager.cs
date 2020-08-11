using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleWiz
{
    class DriverManager
    {
        public IWebDriver driver;
        private static DriverManager instance = null;
        private static readonly object padlock = new object();

        DriverManager()
        {
        }

        public static DriverManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new DriverManager();
                    }
                    return instance;
                }
            }
        }

        

        public void startBrowser()
        {
            driver = new ChromeDriver(@"E:\Work\VisualStudio\SeleWiz\SeleWiz\3rdParty");
        }
    }


}
