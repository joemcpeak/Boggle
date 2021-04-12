import React, { Component } from 'react';
import { Board } from './Board';
import { WordsList } from './WordsList';
import { WordInput } from './WordInput';

import { Button, Container, Row, Col, Spinner } from 'react-bootstrap';

export class Game extends Component {

    constructor(props) {
        super(props);
        this.state = { gameReady: false };
    }

    // number of rows/columns of the board
    boardSize = 6;

    componentWillMount = async () => {
        await this.newGame();
    }

    wordAdded = (word) => {
        if (this.state.foundWords.includes(word) == false) {
            this.state.foundWords.push(word);
            this.setState({ foundWords: this.state.foundWords });
        }
    }

    wordClicked = (word) => {
        this.setState({ clickedWord: word, boardKey: this.state.boardKey + 1 });
    }

    getShuffledDice = () => {

        // game dice from https://www.boardgamegeek.com/thread/300565/review-boggle-veteran-and-beware-different-version (old version)
        let gameDice = [
            "AACIOT",
            "AHMORS",
            "EGKLUY",
            "ABILTY",
            "ACDEMP",
            "EGINTV",
            "GILRUW",
            "ELPSTU",
            "DENOSW",
            "ACELRS",
            "ABJMOQ",
            "EEFHIY",
            "EHINPS",
            "DKNOTU",
            "ADENVZ",
            "BIFORX"
        ];

        // fisher-yates shuffle from https://www.tutorialspoint.com/what-is-fisher-yates-shuffle-in-javascript
        let i = gameDice.length
        while (--i > 0) {
            let k = Math.floor(Math.random() * (i + 1));
            let temp = gameDice[k];
            gameDice[k] = gameDice[i];
            gameDice[i] = temp;
        }
        return gameDice;
    }

    newGame = async () => {

        // set some initial state
        let boardKey = this.state.boardKey || 0;
        this.setState({ boardLetters: null, validWords: null, foundWords: [], clickedWord: null, boardKey: ++boardKey, gameReady: false });

        // generate an array of shuffled dice for the board and save it as state
        let boardLetters = [];
        let newD = this.getShuffledDice();
        let arrIdx = -1;
        for (let i = 0; i < this.boardSize ** 2; i++) {
            // pointer into the current array of shuffled dice
            arrIdx++;
            // if we are out of shuffled dice, reshuffle and start again
            if (arrIdx == newD.length) {
                newD = this.getShuffledDice();
                arrIdx = 0;
            }
            // now that we've got our die, roll it to choose the face
            let face = newD[arrIdx][Math.floor(Math.random() * 6)];
            boardLetters.push(face);
        }
        this.setState({ boardLetters: boardLetters });

        // call the API to get the list of valid words based on the board dice
        let solution = await this.getBoardSolution(boardLetters);
        this.setState({ solution: solution, gameReady: true });
    }

    getBoardSolution = async (boardLetters) => {
        var letters = boardLetters.join("");
        const response = await fetch(`boggle\\solve?boardletters=${letters}`);
        const solution = await response.json();
        return solution;
    }

    render = () => {
        if (this.state.gameReady != true) {
            //return "Initializing...";

            return (
                <>
                    <Spinner animation="border" size="sm" /><span> Initializing...</span>
                </>
            );
        }

        let validWords = this.state.solution.words.map((w) => {
            return w.value;
        });

        return (
            <>
                {/*<button onClick={this.newGame}>New Game</button>*/}

                <Container>
                    <Row>
                        <Col>
                            <h1 style={{ height: "60px" }}>React Boggle</h1>
                        </Col>
                    </Row>
                    <Row>
                        <Col md="auto">

                            <Board boardLetters={this.state.boardLetters} key={this.state.boardKey} showWord={this.state.clickedWord} />
                            <p />
                            <WordInput boardLetters={this.state.boardLetters} validWords={validWords} wordAdded={this.wordAdded} />
                            <p />
                            <Button onClick={this.newGame}>New Game</Button>
                            <p />
                        </Col>

                        <Col>
                            <WordsList words={this.state.foundWords} solution={this.state.solution} wordClicked={this.wordClicked} />
                        </Col>

                    </Row>


                </Container>




                {/*}
                <table>
                    <tbody>
                        <tr>
                            <td valign="top">
                                <Board boardLetters={this.state.boardLetters} key={this.state.boardKey} showWord={this.state.clickedWord} />
                                <WordInput boardLetters={this.state.boardLetters} validWords={validWords} wordAdded={this.wordAdded} />
                            </td>
                            <td valign="top">
                                <WordsList words={this.state.foundWords} solution={this.state.solution} wordClicked={this.wordClicked} />
                            </td>
                        </tr>
                    </tbody>
                </table>
                */}
            </>
        );
    }
}
