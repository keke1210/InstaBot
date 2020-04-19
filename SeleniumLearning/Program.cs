using System.Collections.Generic;

namespace SeleniumLearning
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var instaApi = new InstaApi())
            {
                /// We are adding these new hashtags that we want to search for
                instaApi.Hashtags.AddRange(new List<string> {"supercar", "auto", "audi", "jdm",
                "supercars", "carswithoutlimits", "carsofinstagram", "luxury", "speed", "mercedes",
                "instacar", "luxurycars", "racing", "sportscars", "turbo", "carlifestyle", "porsche",
                "sportscar", "drive", "subaruwrx", "photography", "love", "stance", "ferrari",
                "exoticcars", "instacars", "wheels", "richkidstirana","tirana","speedhunters"});

                /// Put your instagram credentials here
                instaApi.Login("username", "password");

                /// Foreach Hashtag access and like photos, by specifying the times of scroll
                instaApi.ProcessHashtags();

                // Close the session
                instaApi.Quit();
            }
        }
    }
}
