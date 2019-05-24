import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../../store/Turnstile/actions';
import { TurnstileState } from './TurnstileState';
import { TurnstileOnHold } from './TurnstileOnHold';
import { CameraTurnstile } from './camera/CameraTurnstile';
import './Turnstile.css';
import { DetectedBarcode } from './camera/DetectedBarcode';

class Turnstile extends Component<any, TurnstileState> {
  constructor(props: any, state: TurnstileState) {
    super(props, state);
    this.state = {
        scanning: false,
        result: new DetectedBarcode(),
        pass: false,
        wait: true,
        isRequested: false
    }
    this._toggle = this._toggle.bind(this);
  }

  _toggle() {
    this.setState({scanning: !this.state.scanning});
  }

  render() {
    const { verify, pass, wait } = this.props;
    if (this.state.scanning === false) {
      return <TurnstileOnHold onClick={this._toggle}/>
    } else {
      return <CameraTurnstile verify={verify} pass={pass} wait={wait}/>
    }
  }
}

export default connect(
  (state: any) => state.turnstile,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(Turnstile);
