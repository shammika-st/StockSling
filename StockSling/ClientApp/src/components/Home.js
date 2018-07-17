import React, { Component } from 'react';
import { Glyphicon} from 'react-bootstrap';
import './Home.css';

export class Home extends Component {
  displayName = Home.name

    constructor(props) {
        super(props);
        this.state = {
            stock: '',
            currentLikeCount: 0,
            currentDislikeCount: 0
        };
        this.incrementDislikeCounter = this.incrementDislikeCounter.bind(this);
        this.incrementLikeCounter = this.incrementLikeCounter.bind(this);

        fetch('api/Counter/like')
            .then(response => response.json())
            .then(data => {
                this.setState({ currentLikeCount: data });
            });

        fetch('api/Counter/dislike')
            .then(response => response.json())
            .then(data => {
                this.setState({ currentDislikeCount: data });
            });

        fetch('api/Admin/stock')
            .then(response => response.json())
            .then(data => {
                this.setState({ stock: data["stockName"] });
            });
    }

    incrementDislikeCounter() {
        this.setState({
            currentDislikeCount: this.state.currentDislikeCount + 1
        });
        fetch('api/Counter/dislike', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            }
        });
    }

    incrementLikeCounter() {
        this.setState({
            currentLikeCount: this.state.currentLikeCount + 1
        });
        fetch('api/Counter/like', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            }
        });
    }

  render() {
    return (
        <div>
            <div className="page-header">Today's StockSling</div>
            <button onClick={this.incrementDislikeCounter}>{this.state.currentDislikeCount} <Glyphicon glyph='thumbs-down' /></button>
            <span className="stock-name"> {this.state.stock} </span>
            <button onClick={this.incrementLikeCounter}>{this.state.currentLikeCount} <Glyphicon glyph='thumbs-up' /></button>
        </div>
    );
  }
}
