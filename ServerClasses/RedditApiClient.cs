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
        private IHttpClientFactory _clientFactory;
        private RedditApiCredentials _apiCredentials = new RedditApiCredentials();

        public RedditApiClient(IHttpClientFactory clientFactory){
            _clientFactory = clientFactory;
        }
        
        public string GetAuthToken(){
            HttpClient client = _clientFactory.CreateClient();

            var values = new List<KeyValuePair<string, string>>();
            System.Console.WriteLine(_apiCredentials.username);
            System.Console.WriteLine(_apiCredentials.password);
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
            System.Console.WriteLine(jsonContent);
            oAuthToken token = JsonConvert.DeserializeObject<oAuthToken>(jsonContent);
            System.Console.WriteLine(token.access_token);
            if(token.access_token != null){
                return token.access_token;
            }else{
                throw new System.Exception("Unexpected API behavior: "+jsonContent);
            }
            
        }
    }
}