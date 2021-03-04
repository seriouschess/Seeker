import React from 'react';
import { ApiClient } from '../services/api-client';

export class RemoveCommonKeywordsComponent extends React.Component{

    constructor(props){
        super(props);
        console.log("Child Props! "+props.found_keywords);
        this.state = {
            found_keywords: props.found_keywords
        };

        this.addToCommonWords = this.addToCommonWords.bind(this);
    }

    componentDidMount(){
        this._apiClient = new ApiClient();
    }

    componentDidUpdate(updatedProps){
        if(updatedProps.found_keywords !== this.props.found_keywords){
            this.setState( {found_keywords:this.props.found_keywords} );
        }
    }

    async addToCommonWords(event){
        console.log("Attempting to add "+this.state.found_keywords[event]);
        let response = await this._apiClient.addCommonKeyword(this.state.found_keywords[event]);
        console.log("Got back "+response);
    }

    render(){
        console.log("Keywords: "+this.state.found_keywords);
        return (
            <div>
                <p>Most Frequent Keywords:</p>  
                <div>{  this.state.found_keywords.map( (keyword,index) => (
                        <p key={keyword}>{keyword} {index} <button key={index} value={index} onClick={() => this.addToCommonWords(index)}>Make Word Common</button></p>
                )) }</div>
               
            </div>    
        ); 
    }
}