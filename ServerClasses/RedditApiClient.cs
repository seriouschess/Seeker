using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Seeker.dtos;

namespace Seeker.ServerClasses
{
    public class RedditApiClient
    {
        private static string _token;
        //Assumed to require a refresh after 1 hour of requesting
        private static DateTime _refreshTime;
        private IHttpClientFactory _clientFactory;
        private RedditApiCredentials _apiCredentials = new RedditApiCredentials();

        static RedditApiClient()
        {
            _token = null;
            _refreshTime = DateTime.Now;
        }
        
        public RedditApiClient(IHttpClientFactory clientFactory){
            _clientFactory = clientFactory;
        }

        //oAuth2 Token Retrieval
        public string GetAuthToken(){
            if( _token == null || _refreshTime < DateTime.Now.AddHours(-1) ){
                _refreshTime = DateTime.Now;
                _token = this.RequestAuthToken();
                System.Console.WriteLine($"New token refresh requested. Token is now {_token} at time {_refreshTime}");
                return _token; 
            }else{
                System.Console.WriteLine($"Returning token {_token} with refresh time {_refreshTime}"); 
                return _token;
            }
        }

        private string RequestAuthToken(){
                HttpClient client = _clientFactory.CreateClient();

                ApiRequestContent content = new ApiRequestContent();
                content.Add("grant_type", "password");
                content.Add("username", _apiCredentials.username);
                content.Add("password", _apiCredentials.password);

                string clientId = "hw8zbkPFGguiVQ";
                string clientSecret = "dH9be6KM24Ki5PJ_xcg3agqksX9KsQ";
                string authenticationString = $"{clientId}:{clientSecret}";
                string base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));

                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://www.reddit.com/api/v1/access_token");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
                requestMessage.Content = content.Export();

                //make the request
                Task<HttpResponseMessage> task = client.SendAsync(requestMessage);
                HttpResponseMessage response = task.Result;
                response.EnsureSuccessStatusCode();
                HttpContent responseBody = response.Content;
                string jsonContent = responseBody.ReadAsStringAsync().Result;
                oAuthToken token = JsonConvert.DeserializeObject<oAuthToken>(jsonContent);
                System.Console.WriteLine(token.access_token);
                if(token.access_token != null){
                    return token.access_token;
                }else{
                    throw new System.Exception("Unexpected API behavior: "+jsonContent);
                }
        }

        //class tools
        private HttpClient InitialiseClient(){
            HttpClient client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("User-Agent",  _apiCredentials.user_agent);
            return client;
        }

        private HttpRequestMessage PrepareGetRequest(string url){
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.GetAuthToken() );
            return requestMessage;
        }

        private string ReturnResponseAsString(HttpResponseMessage response){
            //response.EnsureSuccessStatusCode();
            HttpContent responseBody = response.Content;
            string jsonContent = responseBody.ReadAsStringAsync().Result;
            return jsonContent;
        }

        //complete request response cycles
        public string GetRedditUserData(){
            HttpClient client = this.InitialiseClient();
            HttpRequestMessage requestMessage = this.PrepareGetRequest("https://oauth.reddit.com/api/v1/me");
            Task<HttpResponseMessage> task = client.SendAsync(requestMessage);
            return this.ReturnResponseAsString(task.Result);
        }

        public string GetTestEndpoint(){
            HttpClient client = this.InitialiseClient();
            HttpRequestMessage requestMessage = this.PrepareGetRequest("https://oauth.reddit.com/api/trending_subreddits");
            Task<HttpResponseMessage> task = client.SendAsync(requestMessage);
            return this.ReturnResponseAsString(task.Result);
        }
    }
}