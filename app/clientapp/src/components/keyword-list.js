import React from 'react';
export class KeywordListComponent extends React.Component{
    constructor(props){
        super(props);
        this.state = {
            keyword_list:[],
            keywordCallback: props.keywordCallback,
            add_keyword_enabled: false,
            entered_keyword: ""
        };

        this.pushKeywordToList = this.pushKeywordToList.bind(this);
        this.updateKeywordList = this.updateKeywordList.bind(this);
        this.updateEnteredKeyword = this.updateEnteredKeyword.bind(this);
    }

    componentDidMount(){}

    updateKeywordList(event){
        this.setState({ keyword_list: event.target.keyword_list });
    }

    updateEnteredKeyword(event){
        this.setState({entered_keyword: event.target.value});
    }

    pushKeywordToList(event){
        event.preventDefault();
        let local_keyword_list =  this.state.keyword_list;
        local_keyword_list.push( this.state.entered_keyword );
        this.state.keyword_list = local_keyword_list;
        console.log("For Child: "+this.state.keyword_list);
        this.props.onKeywordListUpdate(this.state.keyword_list, this.props.myParent);
        this.forceUpdate();
    }

    toggleKeywordAdditionForm(){
        this.setState({ add_keyword_enabled: !this.state.add_keyword_enabled });
    }

    resetKeywordList(){
        let new_value = [];
        this.setState({ keyword_list: new_value });
        this.props.onKeywordListUpdate({ keyword_list: new_value });
    }

    render(){
        let submission_button = <></>;
        let keyword_reset_option = <></>;

        if( this.state.add_word_enabled == true ){
            submission_button = <></>;//<p>Add a keyword: <button onClick={ this.toggleKeywordAdditionForm }>ADD</button></p>;

        }else{
            submission_button =
            <form>
                <input type="text" value={ this.state.entered_keyword } onChange={ this.updateEnteredKeyword } />
                <input type="submit" value="Submit" onClick={ this.pushKeywordToList } />
            </form>;
        }

        if( this.state.keyword_list.Length > 0 ){
            keyword_reset_option = <p> Reset all keywords: <button onClick={ this.resetKeywordList }>HERE</button> </p>;
        }
        
        return (
            <div>
                <h1> Keywords! </h1>
                <p onChange={ this.updateKeywordList }> { this.state.keyword_list } </p>
                { submission_button }
                { keyword_reset_option }
            </div>    
        );
    }
}
