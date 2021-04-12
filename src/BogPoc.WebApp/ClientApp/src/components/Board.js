import React, { Component } from 'react';
import { Die } from './Die';

export class Board extends Component {

    constructor(props) {
        super(props);
        this.state = { hilightDie: -1 };
    }

    render() {

        var letters = this.props.boardLetters;

        // make sure the board will be a square
        var size = Math.sqrt(letters.length);
        if (Number.isInteger(size) == false)
            throw `Internal error - board was initialized with ${letters.length} letters, which would not result in a square board`

        let rendered = [];

        for (let currRow = 0; currRow < size; currRow++) {

            let thisRowsLetters = letters.slice(currRow * size, currRow * size + size);

            rendered.push(
                <tr key={currRow}>
                    {thisRowsLetters.map((letter, mapIndex) => {

                        let shouldHilightThisDie = false;
                        if (this.props.showWord && this.state.hilightDie > -1) {
                            let thisNode = this.props.showWord.path.nodes[this.state.hilightDie];
                            if (thisNode.x == mapIndex && thisNode.y == currRow) {
                                shouldHilightThisDie = true;
                            }
                        }

                        return (
                            <td key={mapIndex}>
                                <Die letter={letter} hilight={shouldHilightThisDie} />
                            </td>);
                    })}
                </tr>
            );
        }

        if (this.props.showWord && this.state.hilightDie == -1) {
            this.startHilighting();
        }

        return (
            <table border="1">
                <tbody>
                    {rendered}
                </tbody>
            </table>
        );
    }

    intervalId = null;

    startHilighting = () => {

        this.intervalId = setInterval(() => {

            // if we've hilighted all the dice, turn off the timer
            if (this.state.hilightDie == this.props.showWord.value.length - 1) {
                clearInterval(this.intervalId);
                this.setState({ hilightDie: -2 })
                return;
            }

            this.setState({ hilightDie: this.state.hilightDie + 1 })
        }, 500);
    }

    componentWillUnmount = () => {
        // if we are unmounting, turn off the interval
        clearInterval(this.intervalId);
    }
}