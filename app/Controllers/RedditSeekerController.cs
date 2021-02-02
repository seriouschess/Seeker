﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Seeker.Configuration;
using Seeker.dtos;
using Seeker.ServerClasses;

namespace Seeker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Redditseeker : ControllerBase
    {
        private RedditApiServices _redditApiServices;

        private Scanner _scanner = new Scanner();

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

        [HttpGet]
        [Route("scan/{subreddit_name}")]
        public ActionResult<double> ScanSubreddit( string subreddit_name ){
            string content_string = _redditApiServices.GetSubredditString(subreddit_name);
            List<string> keywords = new List<string>(){
                "colossus",
                "stalker",
                "immortal",
                "zealot",
                "archon",
                "templar",
                "gateway"
            };
            return _scanner.ScanString(content_string, keywords);
        }
    }
}