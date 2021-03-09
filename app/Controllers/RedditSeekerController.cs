using System.Collections.Generic;
using System;
using System.IO;
using System.Net.Http;
using app.dtos;
using app.ServerClasses.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Seeker.ServerClasses;
using app.ServerClasses;

namespace Seeker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Redditseeker : ControllerBase
    {
        private RedditApiServices _redditApiServices;

        private IScanner _scanner = new Scanner();

        private ILanguageFormatter _formatter = new LanguageFormatter();

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
        public ActionResult<ScanReport> ScanSubreddit( [FromBody] ScanOrder order ){
            string content_string = _redditApiServices.GetSubredditString( order.subreddit_name );

            ScanReport output = new ScanReport{
                keyword_match_percentage = _scanner.ScanString(content_string, order.keywords),
                most_popular_words = _scanner.ReturnMostFrequentKeywords(content_string,15)
            };
            
            return output;
        }

        [HttpPost]
        [Route("addcommon/{word_to_add}")]
        public ActionResult<string> AddNewCommonWord(string word_to_add){
            _formatter.AddCommonWord(word_to_add);
            return word_to_add;
        }
        
        [HttpPost]
        [Route("removecommon/{word_to_remove}")]
        public ActionResult<string> RemoveNewCommonWord(string word_to_remove){

                string[] lines = System.IO.File.ReadAllLines("testfile.txt");

                // Write the new file over the old file.
                using (StreamWriter writer = new StreamWriter("testfile.txt"))
                {
                    for (int currentLine = 1; currentLine <= lines.Length; ++currentLine)
                    {
                        if (lines[currentLine] == word_to_remove)
                        {
                            //do nothing :O
                        }
                        else
                        {
                            writer.WriteLine(lines[currentLine - 1]);
                        }
                    }
                }

                return $"Common word {word_to_remove} deleted";
        }
    }


    public class ScanOrder{
        public List<string> keywords {get;set;}
        public string subreddit_name{get;set;}
    }
}
