using System;
using System.Collections.Generic;
using app.ServerClasses.Interfaces;

namespace app.ServerClasses
{
    public class LanguageFormatter:ILanguageFormatter
    {
        private string ascii_letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private List<string> common_articles;
        private string _commonFilePath = "common_words.txt";
        public LanguageFormatter(){
            this.common_articles = RetrieveCommonWords();
        }

        private List<string> RetrieveCommonWords(){
            if (!System.IO.File.Exists( _commonFilePath ))
            {
                return new List<string>();
            }else{
                List<string> output = new List<string>();
                string[] lines = System.IO.File.ReadAllLines( _commonFilePath );
                foreach (string s in lines)
                {
                    output.Add(s);
                }
                return output;
            }
        }

        public void AddCommonWord(string word_to_add){

            //to lower is important. All words in this file should be lowercase
            string new_word = StripPunctuation(word_to_add).ToLower();

            foreach(string word in this.common_articles){
                if(word == new_word){
                    return; //end the function, word already exists in file
                }
            }

            // This text is added only once to the file.
            if (!System.IO.File.Exists(_commonFilePath))
            {
                // Create a file to write to.
                string[] createText = { new_word };
                System.IO.File.WriteAllLines(_commonFilePath, createText);
            }else{
                System.IO.File.AppendAllText(_commonFilePath, new_word + Environment.NewLine); //writes the additional line to the file
            }

            this.common_articles = RetrieveCommonWords();
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
        public bool NotACommonWord(string word){
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