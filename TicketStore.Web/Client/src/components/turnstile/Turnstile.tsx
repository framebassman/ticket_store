import React, { Component } from 'react';
import Button from '@material-ui/core/Button';
import { Scanner } from './Scanner';
import { Result } from './Result';
import { TurnstileState } from './TurnstileState';
import { TurnstileOnHold } from './TurnstileOnHold';
import './Turnstile.css';

export class Turnstile extends Component<any, TurnstileState> {
  constructor(props: any, state: TurnstileState) {
    super(props, state);
    this.state = {
      scanning: false,
      results: [],
      pass: false,
      isRequested: false,
    }
    this._scan = this._scan.bind(this);
    this._onDetected = this._onDetected.bind(this);
  }

  render() {
    if (this.state.scanning === false) {
      return <TurnstileOnHold onClick={this._scan}/>
    }

    const lastResult = this.state.results[this.state.results.length - 1];
    return (
      <div className="turnstile">
        <Button variant="raised" onClick={this._scan}>Остановить сканирование</Button>
        <ul className="results">
          <Result result={lastResult}/>
        </ul>
        <Scanner onDetected={this._onDetected}/>
      </div>
    );
  }

  _scan() {
    this.setState({scanning: !this.state.scanning});
  }

  _onDetected(result: any) {
    this.setState({
      results: this.state.results.concat([result]),
      pass: true
    });
  }
}
