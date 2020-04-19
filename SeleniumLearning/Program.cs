using System.Threading;

namespace SeleniumLearning
{
    class Program
    {

        static void Main(string[] args)
        {
            InstaApi instaApi = new InstaApi();

            string[] newHashtags = {"car", "bmw", "supercar", "carporn", "auto", "audi", "jdm",
            "supercars", "carswithoutlimits", "carsofinstagram", "luxury", "speed", "mercedes",
            "instacar", "luxurycars", "racing", "sportscars", "turbo", "carlifestyle", "porsche",
            "sportscar", "drive", "subaruwrx", "photography", "love", "stance", "ferrari",
            "exoticcars", "instacars", "wheels", "richkidstirana","tirana","speedhunters"};


            instaApi.Login("name","password");

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
