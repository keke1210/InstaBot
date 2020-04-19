using System.Threading;

namespace SeleniumLearning
{
    class Program
    {

        static void Main(string[] args)
        {
            using (InstaApi instaApi = new InstaApi())
            {
                /// All the hashtags that we want to search for
                string[] newHashtags = {"car", "bmw", "supercar", "carporn", "auto", "audi", "jdm",
                "supercars", "carswithoutlimits", "carsofinstagram", "luxury", "speed", "mercedes",
                "instacar", "luxurycars", "racing", "sportscars", "turbo", "carlifestyle", "porsche",
                "sportscar", "drive", "subaruwrx", "photography", "love", "stance", "ferrari",
                "exoticcars", "instacars", "wheels", "richkidstirana","tirana","speedhunters"};

                /// Put your instagram credentials here
                instaApi.Login("name", "password");

                /// Foreach hashtag you 
                foreach (string hash in newHashtags)
                {
                    Thread.Sleep(5000);
                    instaApi.searchByHashtagAndLike(hash, 10);
                    Thread.Sleep(20000);
                }

                // Close the session
                instaApi.Quit();
            }
        }
    }
}
