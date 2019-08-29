import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import * as throttle from 'lodash/throttle';
import { connect } from 'react-redux';

import { actionCreators } from '../../../store/Turnstile/actions';
import { Scanner } from './Scanner';
import { TurnstileState } from '../TurnstileState';
import { beep } from './Beep';
import { Status } from './Status';
import { DetectedBarcode } from './DetectedBarcode';
import './CameraTurnstile.css';

export class CameraTurnstile extends Component<any, TurnstileState> {
  constructor(props: any, state: TurnstileState) {
    super(props, state);
    this.state = {
      scanning: false,
      result: new DetectedBarcode(),
      isRequested: false,
    }
    this._onDetected = throttle(
      this._onDetected.bind(this),
      3000,
      { 'trailing': false }
    );
  }

  _onDetected(current: DetectedBarcode) {
    beep();
    const { code } = current.codeResult;
    this.props.verify(code);
  }

  render() {
    const { pass, wait } = this.props;
    return (
      <div className="turnstile">
        <Status pass={pass} wait={wait}/>
        <Scanner onDetected={this._onDetected}/>
      </div>
    );
  }
}

export default connect(
  (state: any) => state.turnstile,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(CameraTurnstile);
