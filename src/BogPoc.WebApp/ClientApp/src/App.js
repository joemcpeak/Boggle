import React, { Component } from 'react';
import { Game } from './components/Game';

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <div>
                <Game></Game>
            </div>
        );
    }
}
