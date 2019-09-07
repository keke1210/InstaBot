using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SeleniumLearning.Helpers;

namespace SeleniumLearning
{
    class Program
    {

        static void Main(string[] args)
        {
            InstaApi instaApi = new InstaApi();

            string[] newHashtags = {/*"car", "bmw", "supercar", "carporn", "auto", "audi", "jdm",
            "supercars", */"carswithoutlimits", "carsofinstagram", "luxury", "speed", "mercedes",
            "instacar", "luxurycars", "racing", "sportscars", "turbo", "carlifestyle", "porsche",
            "sportscar", "drive", "subaruwrx", "photography", "love", "stance", "ferrari",
            "exoticcars", "instacars", "wheels", "richkidstirana","tirana","speedhunters"};


            instaApi.Login("test","test");

            foreach(string hash in newHashtags)
            {
                Thread.Sleep(5000);
                instaApi.searchByHashtagAndLike(hash, 10);
                Thread.Sleep(20000);

            }

            instaApi.quit();
        }
    }
}
