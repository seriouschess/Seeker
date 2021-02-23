import React from 'react';
export class KeywordListComponent extends React.Component{

    constructor(props){
        super(props);
        this.state = {
            test_value:"please work",
            keyword_list:[],
            keywordCallback: props.keywordCallback,
            add_keyword_enabled: false,
            entered_keyword: ""
        };

        this.pushKeywordToList = this.pushKeywordToList.bind(this);
        this.updateKeywordList = this.updateKeywordList.bind(this);
        this.updateEnteredKeyword = this.updateEnteredKeyword.bind(this);
        this.toggleKeywordAdditionForm = this.toggleKeywordAdditionForm.bind(this);
    }

    componentDidMount(){
    }

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
        this.props.onKeywordListUpdate(local_keyword_list, this.props.myParent);
        console.log("Keyword List: "+this.state.keyword_list);
        this.setState( { keyword_list: local_keyword_list } );
        this.toggleKeywordAdditionForm();
    }

    toggleKeywordAdditionForm()
    {
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

        if( this.state.add_keyword_enabled == false ){
            submission_button = <div><p>Add a keyword:</p> <button onClick={ this.toggleKeywordAdditionForm }>ADD</button></div>;
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
                { submission_button }
                { keyword_reset_option }
            </div>    
        );
    }
}


