import React, { Component } from 'react';
import { TurnstileState } from './TurnstileState';
import { TurnstileOnHold } from './on-hold/TurnstileOnHold';
import CameraTurnstile from './Camera';
import { DetectedBarcode } from './DetectedBarcode';
import './Turnstile.css';

export default class Turnstile extends Component<any, TurnstileState> {
  constructor(props: any, state: TurnstileState) {
    super(props, state);
    this.state = {
        scanning: false,
        result: new DetectedBarcode(),
        isRequested: false
    }
    this._toggle = this._toggle.bind(this);
  }

  _toggle() {
    this.setState({ scanning: !this.state.scanning });
  }

  render() {
    const { scanning } = this.state;

    if (scanning === false) {
      return <TurnstileOnHold onClick={this._toggle}/>
    } else {
      return <CameraTurnstile />
    }
  }
}
