import React, { Component } from "react";
import { Alert } from "react-bootstrap";
import { Button } from "react-bootstrap"

export class FinanceList extends Component {

    constructor(props) {
        super(props);
        this.state = { variant: "primary" };

        this.buttonClicked = this.buttonClicked.bind(this);
    }

    buttonClicked() {
        const variants = ["primary", "secondary", "success", "danger", "warning", "info", "light", "dark"];
        this.setState(() => ({
            variant: variants[Math.floor(Math.random() * variants.length)]
        }));
    }

    render() {
        return (
            <div>
                <Alert variant={this.state.variant}>This is an alert!</Alert>
                <p>Hello</p>
                <Button onClick={this.buttonClicked} variant={this.state.variant}>Hello! click me!</Button>
            </div>
        );
    }

}