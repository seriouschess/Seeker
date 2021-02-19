import React, { Component } from 'react';
import { ApiClient } from '../services/api-client.js';

export class PresentString extends Component {

  constructor(props) {
    super(props);
    this.state = { 
        reddit: "",
        scan_percentage: null, 
        loading: true,
        input_subreddit: props.input_subreddit
    };
  }

  componentDidMount() {
    this._client = new ApiClient();
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
      : PresentString.renderRedditTable(this.state.reddit);

    return (
      <div>
        <h1> Props: {this.state.input_subreddit} </h1>
        <h1 id="tabelLabel" >Reddit Seeker</h1>
        <p>Keywords scanned percentage: {this.state.scan_percentage}</p>
        {contents}
      </div>
    );
  }

  async getFromReddit() {
    const data = await this._client.getFromReddit(this.state.input_subreddit);
    this.setState({ reddit: data, loading: false });
  }

  async scanSubreddit(){
    const data = await this._client.scanSubreddit(
      this.state.input_subreddit, 
      this.props.keywordList
    );
    this.setState({scan_percentage: data});
  }
}
