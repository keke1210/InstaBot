using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeleniumLearning
{
    public class InstaApi : IDisposable
    {
        /// Create the reference for the brpowser
        private IWebDriver driver = new ChromeDriver();

        public List<string> Hashtags { get; set; } = new List<string>();

        /// <summary>
        /// Foreach hashtag you get photos and like them
        /// </summary>
        public void ProccessHashtags()
        {
           
            foreach (string hash in Hashtags)
            {
                Thread.Sleep(5000);
                SearchByHashtagAndLike(hash, 10);
                Thread.Sleep(20000);
            }
        }

        /// <summary>
        /// Close the driver session 
        /// </summary>
        public void Quit()
        {
            driver.Quit();
        }

        /// <summary>
        /// Logs in to Instagram account
        /// </summary>
        /// <param name="usrname"></param>
        /// <param name="pass"></param>
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


        /// <summary>
        /// Searches pictures by hashtag specified and scrolls down as much as you specify with the loop parameter. Then likes the photos.
        /// (This function is not well written if you care about principles)
        /// </summary>
        /// <param name="hashtag"></param>
        /// <param name="loop"></param>
        public void SearchByHashtagAndLike(string hashtag, int loop)
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
               
                var likeButtons = driver.FindElements(By.ClassName("_8-yf5"));

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
                catch (Exception)
                {
                    Thread.Sleep(2000);
                }

                Thread.Sleep(16000);
            }

        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                driver.Dispose();
                this.Dispose();
            }
        }
    }
}
