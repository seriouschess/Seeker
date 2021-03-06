using System.Collections.Generic;

namespace app.ServerClasses.Interfaces
{
    public interface IRedditApiServices
    {
        List<string> GetSubredditContentList(string subreddit_string);
        string GetSubredditString(string subreddit_string);
        string GetAuthToken();
        string GetRedditUserData();
        string GetTestEndpoint();
    }
}