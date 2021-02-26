using app.ServerClasses.Interfaces;

namespace app.ServerClasses
{
    public class LanguageFormatter:ILanguageFormatter
    {
        private string ascii_letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public LanguageFormatter(){

        }

        public string StripPunctuation(string input_string){
            string stripped_string = input_string;
            foreach(char item in input_string){
                if(!ascii_letters.Contains(item)){
                    stripped_string = stripped_string.Replace(item+"", "");
                }
            }
            return stripped_string;
        }
    }
}