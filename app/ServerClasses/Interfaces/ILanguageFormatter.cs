namespace app.ServerClasses.Interfaces
{
    public interface ILanguageFormatter
    {
       string StripPunctuation(string input_string);
       bool NotACommonWord(string word);

       void AddCommonWord(string word);
    }
}