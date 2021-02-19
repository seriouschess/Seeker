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
        const data = await axios.post('api/redditseeker/scan',{
            keywords: keywords_param,
            subreddit_name: subreddit_param
        })
            .then(function (res) {
                return res.data;
            })
            .catch(function (error) {              
                return error
            });
        return data;
    }
}