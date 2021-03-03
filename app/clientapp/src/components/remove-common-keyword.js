import React from 'react';
export class RemoveCommonKeywordsComponent extends React.Component{

    constructor(props){
        super(props);
        console.log("Child Props! "+props.found_keywords);
        this.state = {
            found_keywords: props.found_keywords
        };

        this.removeFromKeywordList = this.removeFromKeywordList.bind(this);
    }

    componentDidMount(){
    }

    componentDidUpdate(updatedProps){
        if(updatedProps.found_keywords !== this.props.found_keywords){
            this.setState( {found_keywords:this.props.found_keywords} );
        }
    }

    removeFromKeywordList(event){
        console.log(event);
    }

    render(){
        console.log("Keywords: "+this.state.found_keywords)
        return (
            <div>
                <p>Most Frequent Keywords:</p>  
                <div>{  this.state.found_keywords.map( (keyword,index) => (
                        <p key={keyword}>{keyword} {index} <button key={index} value={index} onClick={() => this.removeFromKeywordList(index)}>Make Word Common</button></p>
                )) }</div>
               
            </div>    
        ); 
    }
}