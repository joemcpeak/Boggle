import React, { Component } from 'react';

export class Die extends Component {

    render() {

        let bgColor = this.props.hilight ? "yellow" : "white";

        return (
            <div style={{ width: "50px", height: "50px", textAlign: "center", verticalAlign: "middle", lineHeight: "40px", backgroundColor: bgColor, fontSize: "20px" }}>
                <b>{this.props.letter}{this.props.letter == "Q" ? "u" : ""}</b>
            </div>

        );
    }
}