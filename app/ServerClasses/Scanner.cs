using System.Collections.Generic;
using System.Linq;
using app.ServerClasses;
using app.ServerClasses.Interfaces;

namespace Seeker.ServerClasses
{
    public class Scanner: IScanner
    {
        private ILanguageFormatter _formatter;
        public Scanner(){
            _formatter = new LanguageFormatter();
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

        public List<string> ReturnMostFrequentKeywords(string input_string, int return_count = 1){
            if(return_count < 1){
                throw new System.ArgumentException("return_count parameter may not be less than a single item.");
            }

            List<string> output = new List<string>();
            bool found_one = false;
            List<CandidateKeyword> CandidateKeywords = new List<CandidateKeyword>();
            string[] words = input_string.Split(' ');
            foreach(string word in words){
                found_one = false;
                string stripped_word = _formatter.StripPunctuation(word).ToLower();
                foreach( CandidateKeyword candidate in CandidateKeywords ){
                    if(candidate.word == stripped_word){
                        candidate.found_count += 1;
                        found_one = true;
                        break;
                    }
                }
                if(found_one == false){
                    CandidateKeywords.Add(new CandidateKeyword(){
                        word = stripped_word,
                        found_count = 1
                    });
                }
                
            }

            List<CandidateKeyword> output_candidates = new List<CandidateKeyword>();
            int minimum_candidate_count;
            foreach(CandidateKeyword candidate in CandidateKeywords){
                if(output_candidates.Count > 0){
                    minimum_candidate_count = output_candidates.Min(x => x.found_count);

                    //higher frequency keyword found that is also not a common word
                    if((candidate.found_count >= minimum_candidate_count || output_candidates.Count == 0) && _formatter.NotAnArticle(candidate.word)){ 
                        if(output_candidates.Count >= return_count ){
                            output_candidates.RemoveAt(output_candidates.FindIndex( x => x.found_count == minimum_candidate_count));
                            //lowest_three[lowest_three.IndexOf(lowest_three.Max())] 
                        }
                        output_candidates.Add( new CandidateKeyword(){
                            word = candidate.word,
                            found_count = candidate.found_count
                        });
                        System.Console.WriteLine($"Word: {candidate.word} Frequency: {candidate.found_count}");
                    }
                    
                }else{
                    output_candidates.Add( new CandidateKeyword(){ //return count must be at least 1
                            word = candidate.word,
                            found_count = candidate.found_count
                        });
                }
            }

            output = new List<string>();
            foreach(CandidateKeyword keyword in output_candidates){
                output.Add(keyword.word);
            }

            return output;
        }

        class CandidateKeyword{
            public string word{get;set;}
            public int found_count{get;set;}
        }
    }
}