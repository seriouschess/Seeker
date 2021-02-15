import React, { Component } from 'react';
import { ApiClient } from '../services/api-client.js';

export class PresentString extends Component {

  constructor(props) {
    super(props);
    this.state = { 
        reddit: "", 
        loading: true 
    };
  }

  componentDidMount() {
    this._client = new ApiClient();
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
    const data = await this._client.getFromReddit();
    this.setState({ reddit: data, loading: false });
  }
}
