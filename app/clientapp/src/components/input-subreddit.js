import React, { Component } from 'react';
import { PresentString } from './present-string.js';
import { KeywordListComponent } from './keyword-list.js';

export class InputSubredditComponent extends React.Component{
    constructor(props){
        super(props);
        this.state = {
            entered_string:"",
            keyword_list:[]
        };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    componentDidMount(){
        
    }

    handleChange(event){
        this.state.entered_string = "";
    }

    handleKeywordChange(event){
        this.setState( { keyword_list:event.keyword_list } );
    }


    handleSubmit(event){
        event.preventDefault();
        this.setState({entered_string:this.state.value});
    }

    getKeywordList(keyword_list_param, me){
        console.log("Parent's Keyword List"+keyword_list_param);
        me.setState( {keyword_list:keyword_list_param} );
        me.forceUpdate();
    }

    render(){
        let presentation;
        let keyword_entry_form;

        if(this.state.entered_string != ""){
            presentation = <PresentString keywordList={this.state.keyword_list} input_subreddit={ this.state.entered_string }/>;
            keyword_entry_form = <></>;
        }else{
            presentation = <></>;
            keyword_entry_form = <KeywordListComponent myParent={this} onKeywordListUpdate={this.getKeywordList}></KeywordListComponent>;
        }
        return(
            <div>
                <p>{this.state.value}</p>
                <p>Current Keywords:</p>
                <div>
                    {this.state.keyword_list.map(keyword => (
                        <span key={keyword}>{keyword} </span>
                    ))}
                </div>
                {keyword_entry_form}
                <p>Enter a subreddit to search:</p>

                <form>

                <input value={this.state.value} onChange={this.handleChange} type="text" />
                <input type="submit" value="Submit" onClick={this.handleSubmit} />
                {presentation}

                </form>
            </div>
        );
    }

}