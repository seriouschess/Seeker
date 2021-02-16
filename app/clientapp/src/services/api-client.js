import axios from 'axios';

export class ApiClient {

    constructor(){}

    async getFromReddit(item_string) {
        const data = await axios.get('api/redditseeker/subreddit/'+item_string).then( res => {
            return res.data;
          });
        return data;
    }
}