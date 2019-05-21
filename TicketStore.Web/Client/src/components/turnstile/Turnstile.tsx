import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../../store/Turnstile/actions';
import { TurnstileState } from './TurnstileState';
import { TurnstileOnHold } from './TurnstileOnHold';
import { CameraTurnstile } from './camera/CameraTurnstile';
import './Turnstile.css';

class Turnstile extends Component<any, TurnstileState> {
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
      <CameraTurnstile />
    )
  }

  _toggle() {
    this.setState({scanning: !this.state.scanning});
  }
}

export default connect(
  (state: any) => state.turnstile,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(Turnstile);
