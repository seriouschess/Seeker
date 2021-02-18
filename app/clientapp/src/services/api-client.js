import axios from 'axios';

export class ApiClient {

    constructor(){}

    async getFromReddit(item_string) {
        const data = await axios.get('api/redditseeker/subreddit/'+item_string).then( res => {
            return res.data;
          });
          return data;
    }

    async scanSubreddit(subreddit_param, keywords_param) {
        console.log(subreddit_param);
        console.log(keywords_param);        
        const data = await axios.post('api/redditseeker/scan',{
            keywords: keywords_param,
            subreddit_name: subreddit_param
        })
            .then(function (res) {
                console.log(res.data);
                return res.data;
            })
            .catch(function (error) {
                console.log(error);
                console.log(error.response);                
                return error
            });
        return data;
    }
}