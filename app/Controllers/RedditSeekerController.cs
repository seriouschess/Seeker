using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using app.dtos;
using app.ServerClasses.Interfaces;
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
        public ActionResult<ScanReport> ScanSubreddit( [FromBody] ScanOrder order ){
            string content_string = _redditApiServices.GetSubredditString( order.subreddit_name );

            ScanReport output = new ScanReport{
                keyword_match_percentage = _scanner.ScanString(content_string, order.keywords),
                most_popular_words = _scanner.ReturnMostFrequentKeywords(content_string,5)
            };
            
            return output;
        }

        [HttpPost]
        [Route("addcommon/{word_to_add}")]
        public ActionResult<string> AddNewCommonWord(string word_to_add){

            string path = "testfile.txt";

            // This text is added only once to the file.
            if (!System.IO.File.Exists(path))
            {
                // Create a file to write to.
                string[] createText = { word_to_add };
                System.IO.File.WriteAllLines(path, createText);
            }else{
                System.IO.File.AppendAllText(path, word_to_add + Environment.NewLine); //writes the additional line to the file
            }

            // Open the System.IO.File to read from.
            string[] readText = System.IO.File.ReadAllLines(path);
            foreach (string s in readText)
            {
                Console.WriteLine(s);
            }
 

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
