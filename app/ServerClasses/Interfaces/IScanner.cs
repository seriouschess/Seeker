using System.Collections.Generic;

namespace app.ServerClasses.Interfaces
{
    public interface IScanner
    {
        double ScanString(string content, List<string> keywords); 
        List<string> ReturnMostFrequentKeywords(string content, int return_count = 1);
    }
}