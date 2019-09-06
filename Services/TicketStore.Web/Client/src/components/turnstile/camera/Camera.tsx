import React, { Component } from 'react';
import * as throttle from 'lodash/throttle';
import { connect } from 'react-redux';

import { actionCreators, TurnstileActions } from '../../../store/Turnstile/actions';
import { VerificationMethod } from '../../../store/Turnstile/verificationMethods';
import { Scanner } from './Scanner';
import { beep } from './Beep';
import Status from './../status/Status';
import { DetectedBarcode } from './DetectedBarcode';
import './Camera.css';

class Camera extends Component<TurnstileActions> {
  constructor(props: TurnstileActions) {
    super(props);
    this._onDetected = throttle(
      this._onDetected.bind(this),
      3000,
      { 'trailing': false }
    );
  }

  _onDetected(detectedBarcode: DetectedBarcode) {
    beep();
    const { code } = detectedBarcode.codeResult;
    this.props.verify(code, VerificationMethod.Barcode);
  }

  render() {
    return (
      <div className="camera-container">
        <Status />
        <Scanner onDetected={this._onDetected}/>
      </div>
    );
  }
}

export default connect(
  () => ({}),
  actionCreators
)(Camera);
