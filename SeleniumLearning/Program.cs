using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SeleniumLearning.Models;

namespace SeleniumLearning
{
    class Program
    {

        static void Main(string[] args)
        {
            InstaApi instaApi = new InstaApi();

            instaApi.Login();
            instaApi.searchByHashtagAndLike("losangelos");

        }
    }
}
