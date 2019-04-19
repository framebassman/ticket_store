import React, { Component } from 'react';
import { TurnstileState } from './TurnstileState';
import { TurnstileOnHold } from './TurnstileOnHold';
import ManualTurnstile from './manual/ManualTurnstile';
import './Turnstile.css';

export class Turnstile extends Component<any, TurnstileState> {
  constructor(props: any, state: TurnstileState) {
    super(props, state);
    this.state = {
        scanning: false,
        result: undefined,
        pass: false,
        wait: true,
        isRequested: false,
        myArray: [""]
    }
    this._toggle = this._toggle.bind(this);
  }

  render() {
    if (this.state.scanning === false) {
      return <TurnstileOnHold onClick={this._toggle}/>
    }

    return (
      <ManualTurnstile />
    )
  }

  _toggle() {
    this.setState({scanning: !this.state.scanning});
  }
}
