import React, { Component } from 'react';
import axios from 'axios';

export class PresentString extends Component {
  static displayName = PresentString.name;

  constructor(props) {
    super(props);
    this.state = { 
        reddit: "", 
        loading: true 
    };
  }

  componentDidMount() {
    this.getFromReddit();
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
        <h1 id="tabelLabel" >Reddit Seeker</h1>
        <p>I really hope this works</p>
        {contents}
      </div>
    );
  }

  async getFromReddit() {
    axios.get('api/redditseeker/subreddit/allthingsprotoss').then( res => {
      const data = res.data;
      this.setState({ reddit:data, loading: false });
    });
  }
}
