using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
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

                var values = new List<KeyValuePair<string, string>>();
                values.Add(new KeyValuePair<string, string>("grant_type", "password"));
                values.Add(new KeyValuePair<string, string>("username", _apiCredentials.username));
                values.Add(new KeyValuePair<string, string>("password", _apiCredentials.password));
                var content = new FormUrlEncodedContent(values);


                string clientId = "hw8zbkPFGguiVQ";
                string clientSecret = "dH9be6KM24Ki5PJ_xcg3agqksX9KsQ";
                var authenticationString = $"{clientId}:{clientSecret}";
                var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://www.reddit.com/api/v1/access_token");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
                requestMessage.Content = content;

                //make the request
                var task = client.SendAsync(requestMessage);
                var response = task.Result;
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

        public string GetRedditUserData(){
            HttpClient client = _clientFactory.CreateClient();
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://oauth.reddit.com/api/v1/me");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.GetAuthToken() );
            client.DefaultRequestHeaders.Add("User-Agent",  _apiCredentials.user_agent);

            var task = client.SendAsync(requestMessage);
            var response = task.Result;
            //response.EnsureSuccessStatusCode();
            HttpContent responseBody = response.Content;
            string jsonContent = responseBody.ReadAsStringAsync().Result;
            return jsonContent;
        }
    }
}