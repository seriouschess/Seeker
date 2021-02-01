using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Seeker.ServerClasses
{
    public class RedditApiServices
    {
        private RedditApiClient _apiClient;
        public RedditApiServices(IHttpClientFactory clientFactory){
            _apiClient = new RedditApiClient(clientFactory);
        }

        //General Reddit

        public dynamic GetSubreddit(string subreddit_string){
            string something = _apiClient.PlanGetRequest($"/r/{subreddit_string}.json");
            return JObject.Parse(something); //just sinful
        }

        public List<string> GetSubredditContentList(string subreddit_string){
            dynamic converted_json = GetSubreddit(subreddit_string);
            IEnumerable subreddit_items = converted_json.data.children;
            List<string> all_the_things = new List<string>();
            string subtext_string;

            foreach(dynamic thing in subreddit_items){
                subtext_string = (string)thing.data.selftext;
                all_the_things.Add(subtext_string);
            }
            
            return all_the_things;
        }

        public string GetSubredditString(string subreddit_string){
            dynamic converted_json = GetSubreddit(subreddit_string);
            IEnumerable subreddit_items = converted_json.data.children;
            string all_the_things = "";
            string subtext_string;

            foreach(dynamic thing in subreddit_items){
                subtext_string = (string)thing.data.selftext;
                all_the_things += subtext_string;
            }
            
            return all_the_things;
        }

        //oAuth2 stuff
        public string GetAuthToken(){
            return _apiClient.GetAuthToken();
        }

        public string GetRedditUserData(){
            return _apiClient.GetRedditUserData();
        }

        public string GetTestEndpoint(){
            return _apiClient.GetTestEndpoint();
        }
    }
}