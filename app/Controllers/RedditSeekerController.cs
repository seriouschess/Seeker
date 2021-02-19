using System;
using System.Collections.Generic;
using System.Net.Http;
using app.ServerClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Seeker.ServerClasses;

namespace Seeker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Redditseeker : ControllerBase
    {
        private RedditApiServices _redditApiServices;

        private IScanner _scanner = new Scanner();

        private readonly ILogger<Redditseeker> _logger;

        public Redditseeker(ILogger<Redditseeker> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _redditApiServices = new RedditApiServices(clientFactory);
        }

        [HttpGet]
        public string GetAuthToken(){
            return _redditApiServices.GetAuthToken();
        }

        [HttpGet]
        [Route("account")]
        public string GetRedditUserData(){
            return _redditApiServices.GetRedditUserData();
        }

        [HttpGet]
        [Route("test")]
        public string GetTestEndpoint(){
            return _redditApiServices.GetTestEndpoint();
        }

        [HttpGet]
        [Route("subreddit/{subreddit_name}")]
        public ActionResult<string> GetSubreddit(string subreddit_name){
            return _redditApiServices.GetSubredditString(subreddit_name);
        }

        [HttpPost]
        [Route("scan")]
        public ActionResult<double> ScanSubreddit( [FromBody] ScanOrder order ){
            string content_string = _redditApiServices.GetSubredditString( order.subreddit_name );
            return _scanner.ScanString(content_string, order.keywords);
        }
    }

    public class ScanOrder{
        public List<string> keywords {get;set;}
        public string subreddit_name{get;set;}
    }
}
