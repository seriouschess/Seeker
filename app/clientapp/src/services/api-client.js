import axios from 'axios';

export class ApiClient {

    constructor(){}

    async getFromReddit() {
        const data = await axios.get('api/redditseeker/subreddit/allthingsprotoss').then( res => {
            return res.data;
          });
        return data;
    }
}