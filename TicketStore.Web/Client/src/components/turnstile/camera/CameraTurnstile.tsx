import React, { Component } from 'react';

import Button from '@material-ui/core/Button';
import { Scanner } from './Scanner';
import { Result } from './Result';
import { TurnstileState } from '../TurnstileState';
import { beep } from './Beep';
import './CameraTurnstile.css';

export class CameraTurnstile extends Component<any, TurnstileState> {
  constructor(props: any, state: TurnstileState) {
    super(props, state);
    this.state = {
      scanning: false,
      result: undefined,
      pass: false,
      isRequested: false,
      wait: true
    }
    this._scan = this._scan.bind(this);
    this._onDetected = this._onDetected.bind(this);
  }

  render() {
    return (
      <div className="turnstile">
        {/* <div className="button_stop">
          <Button size="large" variant="contained" onClick={this._scan}>Остановить сканирование</Button>
        </div> */}
        <ul className="results">
          <Result result={this.state.result}/>
        </ul>
        <Scanner onDetected={this._onDetected}/>
      </div>
    );
  }

  _scan() {
    this.setState({scanning: !this.state.scanning});
  }

  async _onDetected(current: any) {
    const previous = this.state.result;
    const { verify } = this.props;
    if (previous === undefined || previous.codeResult.code !== current.codeResult.code) {
      beep();
      console.log("before scanning: ", this.state.pass);
      await verify(current.codeResult.code);
      console.log("after scanning: ", this.state.pass);
    }
  }
}
