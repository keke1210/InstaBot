using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeleniumLearning.Helpers
{
    public class InstaApi
    {
        // Create the reference for the brpowser
        public IWebDriver driver = new ChromeDriver();

        public void quit()
        {
            driver.Quit();
        }


        public void Login(string usrname, string pass)
        {
            driver.Navigate().GoToUrl("https://www.instagram.com/accounts/login/?source=auth_switcher");

            Thread.Sleep(2000);

            IWebElement username = driver.FindElement(By.Name("username"));
            IWebElement password = driver.FindElement(By.Name("password"));

            // Perform Ops 
            username.SendKeys(usrname);
            password.SendKeys(pass);

            Thread.Sleep(500);
            username.SendKeys(Keys.Enter);
            Thread.Sleep(2000);

        }



        public void searchByHashtagAndLike(string hashtag, int loop)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("https://www.instagram.com/explore/tags/");
            Thread.Sleep(1000);
            sb.Append(hashtag);
            sb.Append("/");

            driver.Navigate().GoToUrl(sb.ToString());
            Thread.Sleep(3000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            for (int i = 0; i < loop; i++)
            {
                var scroll = js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
                Thread.Sleep(3000);
            }
            Thread.Sleep(3000);

            var hrefs = driver.FindElements(By.TagName("a"));

            string pic_uri = "https://www.instagram.com/p/";

            List<string> links = new List<string>();

            foreach (var elem in hrefs)
            {
                var pic_hrefs = elem.GetAttribute("href");

                if (pic_hrefs.Contains(pic_uri))
                {
                    links.Add(pic_hrefs);
                }
            }


            foreach (var piclink in links)
            {
                driver.Navigate().GoToUrl(piclink);
                Thread.Sleep(500);
                var scroll = js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
                Thread.Sleep(1000);

                var likeButtons = driver.FindElements(By.ClassName("u-__7"));

                try
                {
                    foreach (var likeButton in likeButtons)
                    {
                        string ariaLabelAttr = likeButton.GetAttribute("aria-label");

                        if (ariaLabelAttr == "Like")
                        {
                            likeButton.Click();
                            break;
                        }
                    }

                }
                catch (Exception ex)
                {
                    Thread.Sleep(2000);
                }

                Thread.Sleep(16000);
            }

        }
    }
}
