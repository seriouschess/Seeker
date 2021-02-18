import axios from 'axios';

export class ApiClient {

    constructor(){}

    async getFromReddit(item_string) {
        const data = await axios.get('api/redditseeker/subreddit/'+item_string).then( res => {
            return res.data;
          });
          return data;
    }

    // async scanSubreddit(subreddit_param, keywords_param){
    //     const data = await axios({
    //         method: 'GET',
    //         url: 'api/redditseeker/scan', 
    //         data: {
    //             keywords:keywords_param,
    //             subreddit_name:subreddit_param
    //         }, 
    //         headers:{'Content-Type': 'application/json'}
    //     }).then(res => {
    //         console.log(res.data);
    //         return res.data;
    //     }).catch((error) => {
    //         console.log(error);
    //     });
    //     return data;
    // }

    // ------------------500------------------
    //************************************************ */
    // scanSubreddit(subreddit_param, keywords_param) {
    //     axios.post('/api/redditseeker/scan', {
    //         keywords: keywords_param,
    //         subreddit_name: subreddit_param
    //     }, {
    //         headers: {
    //             'Content-Type': 'application/json',
    //         }
    //     })
    //         .then(function (res) {
    //             console.log(res.data);
    //             return res.data;
    //         })
    //         .catch(function (error) {
    //             console.log(error);
    //             console.log(error.response);
    //             return error
    //         });
    // }

    scanSubreddit(subreddit_param, keywords_param) {
        let data = JSON.stringify({
            keywords: keywords_param,
            subreddit_name: subreddit_param
        })
        axios.get('api/redditseeker/scan', data, {
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(function (res) {
                console.log(res.data);
                return res.data;
            })
            .catch(function (error) {
                console.log(error);
                console.log(error.response);                return error
            });
    }
}