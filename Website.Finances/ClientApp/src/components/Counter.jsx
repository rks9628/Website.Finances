import React, { Component } from "react";

export class Counter extends Component {
    displayName = Counter.name

    constructor(props) {
        super(props);
        this.state = { loading: true, value: 0 };
    }

    componentDidMount() {
        window.fetch("/api/SampleData/Counter")
            .then(response => response.json())
            .then(response => {
                this.setState({ loading: false, value: response });
            }).catch(error => {
                this.setState({ loading: false, value: error.statusText });
            });
    }

    render() {

        let loadingText = this.state.loading ? "Yes" : "No";

        return (
            <div>
                <h1>Counter</h1>
                <p>This shows a server-side counter value!</p>
                <p>Loading: {loadingText}</p>
                <p>Value: {this.state.value}</p>
            </div>
        );
    }
}