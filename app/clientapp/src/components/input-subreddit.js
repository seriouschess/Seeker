import React, { Component } from 'react';
import { PresentString } from './present-string.js';

export class InputSubredditComponent extends React.Component{
    constructor(props){
        super(props);
        this.state = {
            value: 'Enter a subreddit string',
            entered_string:""
        };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    componentDidMount(){
        
    }

    handleChange(event){
        this.state.entered_string = "";
        this.setState ({value: event.target.value});
    }

    handleSubmit(event){
        event.preventDefault();
        this.state.entered_string = this.state.value;
        console.log('Your subreddit string is: ' + this.state.value);
        this.forceUpdate();
    }

    render() {
        let presentation;
        if(this.state.entered_string != ""){
            presentation = <PresentString input_subreddit={this.state.entered_string}/>;
        }else{
            presentation = <></>;
        }
        return(
            <div>
                <p>Hi, this is a component.</p>
                <p>{this.state.value}</p>
                <form>

                <input value={this.state.value} onChange={this.handleChange} type="text" />
                <input type="submit" value="Submit" onClick={this.handleSubmit} />
                {presentation}

                </form>
            </div>
        );
    }

}