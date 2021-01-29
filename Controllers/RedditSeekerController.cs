using System;
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
        private RedditApiClient _redditApiClient;

        private readonly ILogger<Redditseeker> _logger;

        public Redditseeker(ILogger<Redditseeker> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _redditApiClient = new RedditApiClient(clientFactory);
        }

        [HttpGet]
        public string GetAuthToken(){
            return _redditApiClient.GetAuthToken();
        }

        [HttpGet]
        [Route("account")]
        public string GetRedditUserData(){
            return _redditApiClient.GetRedditUserData();
        }

        [HttpGet]
        [Route("test")]
        public string GetTestEndpoint(){
            return _redditApiClient.GetTestEndpoint();
        }
    }
}
