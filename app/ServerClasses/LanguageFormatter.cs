using System.Collections.Generic;
using app.ServerClasses.Interfaces;

namespace app.ServerClasses
{
    public class LanguageFormatter:ILanguageFormatter
    {
        private string ascii_letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private List<string> common_articles = new List<string>(){
            "the","a","or",
            "in","on","under",
            "im","i","me", "you", "your",
            "their", "there", "theyre","that",
            "to", "of", "be", "and", "is", "for",
            "this", "but", "it",
            "have", "would", "can", "with"
        };
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

        //does not check for punctuation or spaces
        public bool NotAnArticle(string word){
            string lowercase_word = word.ToLower();
            foreach(string article in common_articles){
                if(article == lowercase_word){
                    return false;
                }
            }
            return true;        
        }
    }
}