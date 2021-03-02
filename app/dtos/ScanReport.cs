using System.Collections.Generic;

namespace app.dtos
{
    public class ScanReport
    {
        public double keyword_match_percentage {get;set;}
        public List<string> most_popular_words {get;set;}
    }
}