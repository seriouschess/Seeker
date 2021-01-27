using Seeker.Configuration;

namespace Seeker.dtos
{
    public class RedditApiCredentials
    {
        //oAuth2.0 header requirements
        //grant_type=password&username=<account username>&password=<account password>

        public readonly string grant_type = "password";
        public readonly string username = ConfSettings.Configuration["RedditApiCredentials:Username"];
        public readonly string password = ConfSettings.Configuration["RedditApiCredentials:Password"];
        public readonly string user_agent = ConfSettings.Configuration["RedditApiCredentials:Useragent"];
    }
}