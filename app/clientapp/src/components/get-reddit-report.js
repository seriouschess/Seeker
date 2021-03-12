import React, { Component } from 'react';
import { ApiClient } from '../services/api-client.js';
import { RemoveCommonKeywordsComponent } from './remove-common-keyword';

export class GetRedditReport extends Component {

  constructor(props) {
    super(props);
    this.state = { 
        reddit: "",
        scan_percentage: null, 
        most_frequent_keywords: ["not a real keyword"],
        error_message:null,
        loading: true,
        input_subreddit: props.input_subreddit
    };
  }

  componentDidMount() {
    this._apiClient = new ApiClient();
    this.getFromReddit();
    this.scanSubreddit();
  }

  static renderRedditTable(reddit) {
    return (
        <p>{reddit}</p>
    );
  }

  render() {

    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : GetRedditReport.renderRedditTable(this.state.reddit);
    
    let errorbox = <></>;
    if(this.state.error_message != null){
      errorbox = <p> Error: {this.state.error_message} </p>;
    }

    return (
      <div>
        <h1> Props: {this.state.input_subreddit} </h1>
        <h1 id="tabelLabel" >Reddit Seeker</h1>
        <p>Keywords scanned percentage: {this.state.scan_percentage}</p>

        {errorbox}

        <RemoveCommonKeywordsComponent found_keywords={this.state.most_frequent_keywords} />

        <p>{this.state.most_frequent_keywords}</p>

        {contents}
      </div>
    );
  }

  async getFromReddit() {
    const data = await this._apiClient.getFromReddit(this.state.input_subreddit);
    this.setState({ reddit: data, loading: false });
  }

  async scanSubreddit(){
    const data = await this._apiClient.scanSubreddit(
      this.state.input_subreddit, 
      this.props.keywordList
    );

    if(!data.isAxiosError){
      this.setState({
         error_message: null, 
         most_frequent_keywords: data.most_popular_words,
         scan_percentage: data.keyword_match_percentage
        });
    }else{
      console.log("Error: "+ data.message);
      this.setState({
        error_message: data.message
      });
    }
  }
}
