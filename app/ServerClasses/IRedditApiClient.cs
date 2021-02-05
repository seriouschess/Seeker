namespace app.ServerClasses
{
    public interface IRedditApiClient
    {
        //0Auth2
        string GetAuthToken();
        //complete request response cycles
        string GetRedditUserData();

        string GetTestEndpoint();

        string PlanGetRequest(string get_url);
    }
}