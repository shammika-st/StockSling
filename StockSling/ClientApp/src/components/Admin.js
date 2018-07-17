import React, { Component } from 'react';

export class Admin extends Component {
    displayName = Admin.name

    constructor(props) {
        super(props);
        this.state = {
            stock: '',
            password: ''
        };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleChange(event) {
        this.setState({ [event.target.name]: event.target.value.toUpperCase() });
    }

    handleSubmit(event) {
        fetch('api/Admin/SetStock', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                stockName: this.state.stock,
                password: this.state.password
            })
        });
        event.preventDefault();
    }

    render() {
        return (
            <div>
                <div className="page-header">Enter Today's Stock</div>
                <form onSubmit={this.handleSubmit}>
                    <div className="row">
                        <div className="col-sm-2">
                            Stock:
                        </div>
                        <div className="col-sm-4">
                            <input type="text" name="stock" value={this.state.value} onChange={this.handleChange} />
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-sm-2">
                            Password:
                        </div>
                        <div className="col-sm-4">
                            <input type="text" name="password" value={this.state.value} onChange={this.handleChange} />
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-sm-2">
                            <input type="submit" value="Submit" />
                        </div>
                    </div>
                </form>
            </div>
        );
    }
}
