using System.Collections.Generic;
using app.ServerClasses;

namespace Seeker.ServerClasses
{
    public class Scanner: IScanner
    {
        public Scanner(){

        }

        //returns a percentage that the keywords were featured in the content
        //100% means every word in the content was a keyword
        public double ScanString(string content, List<string> keywords){
            double output_percentage = 0;
            int word_count = 0;
            int flagged_keyword_count = 0;
            bool reading_word = false;
            char current_letter;
            string current_word = "";
            for(int x=0; x<content.Length; x++){
                current_letter = content[x];
                if(reading_word == false && current_letter !=  ' '){
                    reading_word = true;
                    word_count += 1;
                }

                if( current_letter == ' ' ){
                    reading_word = false;
                    current_word = "";
                }else if(current_letter != '.'){
                    current_word += current_letter;

                    foreach(string keyword in keywords){
                        if(keyword == current_word){
                        flagged_keyword_count += 1; 
                        break; 
                        }
                    }
                    reading_word = true;
                }                
            }

            output_percentage = (double)flagged_keyword_count/word_count;
            return output_percentage;
        }
    }
}