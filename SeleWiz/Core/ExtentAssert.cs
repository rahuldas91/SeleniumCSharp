using AventStack.ExtentReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleWiz.Core
{
    class ExtentAssert
    {
        public void Equals(object expected, object actual)
        {
            bool flag = expected.Equals(actual);

            if (flag)
            {
                ExtentReportManager.Instance._test.Log(Status.Pass,"");
            }
            else
            {
                ExtentReportManager.Instance._test.Log(Status.Fail, "");
            }
        }
            
         


        }
    }
}
