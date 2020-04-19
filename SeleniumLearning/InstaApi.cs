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

        public List<string> Hashtags { get; set; } = new List<string>() { "car", "bmw" };

        /// <summary>
        /// Foreach hashtag specified, goes to the link, scrolls down and gets likes all the photos
        /// </summary>
        /// <param name="times">Specifies how many times to scroll</param>
        public void ProcessHashtags()
        {
            foreach (var hashtag in Hashtags)
            {
                Thread.Sleep(5000);

                SearchByHashtag(hashtag);

                ScrollDownWithJs(1);

                var photoLinks = GetPhotosByHashtag(hashtag);

                LikePhotos(photoLinks);

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

            var username = driver.FindElement(By.Name("username"));
            var password = driver.FindElement(By.Name("password"));

            // Perform Ops 
            username.SendKeys(usrname);
            password.SendKeys(pass);

            Thread.Sleep(500);
            username.SendKeys(Keys.Enter);
            Thread.Sleep(2000);
        }


        /// <summary>
        /// Redirects to the route that have the hashtag that you pass as a parameter
        /// </summary>
        /// <param name="hashtag"></param>
        private void SearchByHashtag(string hashtag)
        {
            var urlString = $@"https://www.instagram.com/explore/tags/{hashtag}/";

            // Goes to the specified url
            driver.Navigate().GoToUrl(urlString);
            Thread.Sleep(3000);
        }

        /// <summary>
        /// Scrols to bottom every 3 seconds by the times you specify
        /// </summary>
        /// <param name="timesToScroll"></param>
        private void ScrollDownWithJs(int timesToScroll = 10)
        {
            var js = (IJavaScriptExecutor)driver;
            // scrolls down when
            for (int i = 0; i < timesToScroll; i++)
            {
                js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
                Thread.Sleep(3000);
            }
        }

        /// <summary>
        /// Creates a collection of photo links based on the hashtag you pass
        /// </summary>
        /// <param name="hashtag"></param>
        /// <returns></returns>
        private List<string> GetPhotosByHashtag(string hashtag)
        {
            // Gets all tags (links)
            var hrefs = driver.FindElements(By.TagName("a"));

            var base_pic_uri = "https://www.instagram.com/p/";

            var links = new List<string>();

            foreach (var elem in hrefs)
            {
                var pic_hrefs = elem.GetAttribute("href");

                if (pic_hrefs.Contains(base_pic_uri))
                {
                    links.Add(pic_hrefs);
                }
            }
            return links;
        }

        /// <summary>
        /// Likes the photos
        /// </summary>
        /// <param name="links"></param>
        private void LikePhotos(List<string> links)
        {
            foreach (var piclink in links)
            {
                driver.Navigate().GoToUrl(piclink);
                Thread.Sleep(500);

                try
                {
                    var likeButton = driver.FindElements(By.ClassName("_8-yf5")).FirstOrDefault(x => x.GetAttribute("aria-label").Equals("Like"));

                    if (likeButton != null)
                        likeButton.Click();
                }
                catch (Exception)
                {
                    Thread.Sleep(2000);
                }
                finally
                {
                    Thread.Sleep(16000);
                }
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
