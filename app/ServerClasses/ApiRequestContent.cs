using System.Collections.Generic;
using System.Net.Http;
using app.ServerClasses.Interfaces;

namespace Seeker.ServerClasses
{
    public class ApiRequestContent: IApiRequestContent
    {
        private List<KeyValuePair<string,string>> _contentValues;
        public ApiRequestContent(){
            _contentValues = new List<KeyValuePair<string,string>>();
        }
        public void Add(string key, string value){
            _contentValues.Add(new KeyValuePair<string,string>(key, value));
        }

        public FormUrlEncodedContent Export(){
            return new FormUrlEncodedContent( _contentValues );
        }
    }
}