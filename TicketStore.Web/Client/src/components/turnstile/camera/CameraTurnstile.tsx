import React, { Component } from 'react';
import { Scanner } from './Scanner';
import { Result } from './Result';
import { TurnstileState } from '../TurnstileState';
import { beep } from './Beep';
import { Status } from '../Status';
import './CameraTurnstile.css';
import { DetectedBarcode } from './DetectedBarcode';

export class CameraTurnstile extends Component<any, TurnstileState> {
  constructor(props: any, state: TurnstileState) {
    super(props, state);
    this.state = {
      scanning: false,
      result: new DetectedBarcode(),
      pass: false,
      isRequested: false,
      wait: false
    }
    this._scan = this._scan.bind(this);
    this._onDetected = this._onDetected.bind(this);
  }

  _scan() {
    this.setState({scanning: !this.state.scanning});
  }

  _onDetected(current: DetectedBarcode) {
    console.log('detect barcode');
    beep();
    this.props.verify(current.codeResult.code);
  }

  render() {
    const { pass, wait } = this.props;
    return (
      <div className="turnstile">
        {/* <div className="button_stop">
          <Button size="large" variant="contained" onClick={this._scan}>Остановить сканирование</Button>
        </div> */}
        <Status pass={pass} wait={wait}/>
        <Scanner onDetected={this._onDetected}/>
        <ul className="results">
          <Result result={this.state.result}/>
        </ul>
      </div>
    );
  }
}
