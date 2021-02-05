using System.Collections.Generic;

namespace app.ServerClasses
{
    public interface IScanner
    {
        double ScanString(string content, List<string> keywords); 
    }
}