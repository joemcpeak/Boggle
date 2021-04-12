import React, { Component } from 'react';
import { InputGroup, FormControl, Button  } from 'react-bootstrap';

export class WordInput extends Component {

    componentWillMount = () => {
        this.setState({ currentWord: "" });
    }

    handleChanged = (e) => {
        // keep track of the current word
        this.setState({ currentWord: e.target.value });
    }

    currentWordIsValid = () => {
        if (this.props.validWords == null)
            return false;

        return this.props.validWords.includes(this.state.currentWord.toUpperCase());
    }

    handleKeyPress = (e) => {
        if (e.key === 'Enter' && this.currentWordIsValid()) {
            this.submitWord();
        }
    }

    submitWord = () => {
        this.props.wordAdded(this.state.currentWord.toUpperCase());
        this.setState({ currentWord: "" });
    }

    render() {
        console.log("in wordinput render");

        return (
            <>
                <InputGroup>
                    <FormControl onChange={this.handleChanged} onKeyPress={this.handleKeyPress} value={this.state.currentWord} style={{ textTransform: 'uppercase', backgroundColor: this.currentWordIsValid() ? "lightgreen" : "white" }} />
                    <InputGroup.Append>
                        <Button variant="outline-secondary" onClick={this.submitWord} disabled={!this.currentWordIsValid()}>-></Button>
                    </InputGroup.Append>
                </InputGroup>
            </>
        );
    }
}