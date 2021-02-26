using System.Net.Http;

namespace app.ServerClasses.Interfaces
{
    public interface IApiRequestContent
    {
        void Add(string key, string value);
        FormUrlEncodedContent Export();
    }
}