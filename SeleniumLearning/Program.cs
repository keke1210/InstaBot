using System.Collections.Generic;

namespace SeleniumLearning
{
    class Program
    {

        static void Main(string[] args)
        {
            using (InstaApi instaApi = new InstaApi())
            {
                /// All the hashtags that we want to search for
                instaApi.Hashtags = new List<string> {"car", "bmw", "supercar", "carporn", "auto", "audi", "jdm",
                "supercars", "carswithoutlimits", "carsofinstagram", "luxury", "speed", "mercedes",
                "instacar", "luxurycars", "racing", "sportscars", "turbo", "carlifestyle", "porsche",
                "sportscar", "drive", "subaruwrx", "photography", "love", "stance", "ferrari",
                "exoticcars", "instacars", "wheels", "richkidstirana","tirana","speedhunters"};

                /// Put your instagram credentials here
                instaApi.Login("name", "password");

                /// Foreach Hashtag access and like photos 
                instaApi.ProccessHashtags();

                // Close the session
                instaApi.Quit();
            }
        }
    }
}
