import React, { Component } from 'react';
import { GetRedditReport } from './get-reddit-report.js';
import { KeywordListComponent } from './keyword-list.js';

export class InputSubredditComponent extends React.Component{
    constructor(props){
        super(props);
        this.state = {
            submitted:false,
            entered_string:"",
            keyword_list:[]
        };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    componentDidMount(){
        
    }

    handleChange(event){
        this.setState( { entered_string:event.target.value } );
    }

    handleKeywordChange(event){
        this.setState( { keyword_list:event.keyword_list } );
    }


    handleSubmit(event){
        event.preventDefault();
        this.setState({ submitted:true });
    }

    getKeywordList(keyword_list_param, me){
        me.setState( {keyword_list:keyword_list_param} );
    }

    render(){
        let presentation;
        let keyword_entry_form;

        if(this.state.submitted == true){
            presentation = <GetRedditReport keywordList={this.state.keyword_list} input_subreddit={ this.state.entered_string }/>;
            keyword_entry_form = <></>;
        }else{
            presentation = <></>;
            keyword_entry_form = <KeywordListComponent myParent={this} onKeywordListUpdate={this.getKeywordList}></KeywordListComponent>;
        }
        return(
            <div>
                <p>Current Keywords:</p>
                <div>
                    {this.state.keyword_list.map(keyword => (
                        <span key={keyword}>{keyword} </span>
                    ))}
                </div>
                {keyword_entry_form}
                <p>Enter a subreddit to search:</p>

                <form>

                <input value={this.state.entered_string} onChange={this.handleChange} type="text" />
                <input type="submit" value="Submit" onClick={this.handleSubmit} />

                </form>
                
                {presentation}
            </div>
        );
    }

}