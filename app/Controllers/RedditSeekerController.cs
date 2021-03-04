using System.Collections.Generic;
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
            // Create a FileStream Object 
            // to write to a text file 
            // The parameters are complete  
            // path of the text file in the  
            // system, in Create mode, the 
            // access to this process is  
            // Write and for other  
            // processes is None 
            FileStream fWrite = new FileStream("testfile.txt", 
                        FileMode.Create, FileAccess.Write, FileShare.None); 
    
            // Store the text in a byte array with 
            // UTF8 encoding (8-bit Unicode  
            // Transformation Format) 
            byte[] writeArr = Encoding.UTF8.GetBytes(word_to_add); 
    
            // Using the Write method write 
            // the encoded byte array to 
            // tword_to_addfile 
            fWrite.Write(writeArr, 0, word_to_add.Length); 
    
            // Closee the FileStream object 
            fWrite.Close(); 
    
            // Create a FileStream Object 
            // to read from a text file 
            // The parameters are complete 
            // path of the text file in  
            // the system, in Open mode, 
            // the access to this process is 
            // Read and for other processes 
            // is Read as well 
            FileStream fRead = new FileStream("testfile.txt",  
                        FileMode.Open, FileAccess.Read, FileShare.Read); 
    
            // Create a byte array  
            // to read from the  
            // text file 
            byte[] readArr = new byte[word_to_add.Length]; //little goofy using word_to_add here
            int count; 
    
            // Using the Read method  
            // read until end of file 
            while ((count = fRead.Read(readArr, 0, readArr.Length)) > 0) { 
                System.Console.WriteLine(Encoding.UTF8.GetString(readArr, 0, count)); 
            } 
    
            // Close the FileStream Object 
            fRead.Close(); 
            System.Console.ReadKey(); 
            return word_to_add;
        }
    }

    public class ScanOrder{
        public List<string> keywords {get;set;}
        public string subreddit_name{get;set;}
    }
}
