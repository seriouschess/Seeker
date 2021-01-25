using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Seeker.Configuration;

namespace Seeker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Redditseeker : ControllerBase
    {
        private IHttpClientFactory _clientFactory;

        private readonly ILogger<Redditseeker> _logger;

        public Redditseeker(ILogger<Redditseeker> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<Object> GetAsync()
        {
            HttpClient client = _clientFactory.CreateClient();
            System.Console.WriteLine(ConfSettings.Configuration["test"]);
            var response = await client.GetAsync("https://pokeapi.co/api/v2/pokemon/gengar");
            return response;
        }
    }
}
