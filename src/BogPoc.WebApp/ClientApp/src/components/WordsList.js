import React, { Component } from 'react';
import { Form } from 'react-bootstrap';
import { Row, Col } from 'react-bootstrap';


export class WordsList extends Component {

    componentWillMount = () => {
        this.setState({ showAll: false, sortAlpha: true });
    }

    toggleShow = () => {
        this.setState({ showAll: !this.state.showAll });
    }

    toggleSort = () => {
        this.setState({ sortAlpha: !this.state.sortAlpha });
    }

    wordClick = (e) => {
        console.log("in wordlist wordclick");
        let clickedWord = this.props.solution.words.find((x) => { return x.value == e.target.innerText });
        this.props.wordClicked(clickedWord);
    }

    render() {

        let wordsFound = this.props.words.map((w) => this.props.solution.words.find((x) => { return x.value == w }));
        let wordsFoundPoints = wordsFound.reduce(((accumulator, currentValue) => accumulator + currentValue.points), 0);

        let wordsToShow = this.state.showAll ? this.props.solution.words : wordsFound;

        if (this.state.sortAlpha) {
            wordsToShow.sort((a, b) => a.value == b.value ? 0 :
                a.value < b.value ? -1 : 1);
        }
        else {
            wordsToShow.sort((a, b) => (a.points - b.points) * -1);
        }

        return (
            <>

                <p>
                    This board contains {this.props.solution.words.length} words, {this.props.solution.score} total points. You've found {wordsFound.length} words, {wordsFoundPoints} total points.
                </p>
                <p>
                    <Row>
                        <Col>
                            <Form.Check type="radio" label="show found words" inline name="ShowWords" defaultChecked="true" onChange={this.toggleShow} />
                            <Form.Check type="radio" label="show all words" inline name="ShowWords" onChange={this.toggleShow} />

                        </Col>
                        <Col>
                            <Form.Check type="radio" label="sort alphabetically" inline name="SortBy" defaultChecked="true" onChange={this.toggleSort} />
                            <Form.Check type="radio" label="sort by points" inline name="SortBy" onChange={this.toggleSort} />
                        </Col>
                    </Row>
                </p>
                <p>
                    <i>{wordsToShow.length == 0 ? "(no words found)" : "Click a word below to hilight it on the board."}</i>
                </p>
                <p>
                    {wordsToShow.map((w) => <span><b onClick={this.wordClick} style={{ cursor: "pointer" }}>{w.value}</b> {`(${w.points} ${w.points == 1 ? "point" : "points"}) `}</span>)}
                </p>
            </>
        );
    }
}