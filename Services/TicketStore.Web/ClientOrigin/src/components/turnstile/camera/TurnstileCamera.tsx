import React, { Component } from 'react';
import { TurnstileOnHold } from './on-hold/TurnstileOnHold';
import Camera from './Camera';
import './TurnstileCamera.css';

type State = { 
  scanning: boolean;
}

export default class Turnstile extends Component<any, State> {
  constructor(props: any) {
    super(props);
    this.state = {
        scanning: false,
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
      return <Camera />
    }
  }
}
