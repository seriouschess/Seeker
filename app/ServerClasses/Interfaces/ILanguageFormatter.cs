namespace app.ServerClasses.Interfaces
{
    public interface ILanguageFormatter
    {
       string StripPunctuation(string input_string);
       bool NotAnArticle(string word);
    }
}