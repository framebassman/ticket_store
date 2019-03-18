import React, { Component } from 'react';
import Button from '@material-ui/core/Button';
import { Scanner } from './Scanner';
import { Result } from './Result';
import './Turnstile.css';

interface TurnstileState {
  scanning: boolean,
  results: any[]
}

export class Turnstile extends Component<any, TurnstileState> {
  constructor(props: any, state: TurnstileState) {
    super(props, state);
    this.state = {
      scanning: false,
      results: []
    }
    this._scan = this._scan.bind(this);
    this._onDetected = this._onDetected.bind(this);
  }

  render() {
    return (
      <div>
        <Button className="start" onClick={this._scan}>{this.state.scanning ? 'Stop' : 'Start'}</Button>
          <ul className="results">
            {this.state.results.map((result) => (<Result key={result.codeResult.code} result={result} />))}
          </ul>
          {this.state.scanning ? <Scanner onDetected={this._onDetected}/> : null}
      </div>
    );
  }

  _scan() {
    this.setState({scanning: !this.state.scanning});
  }

  _onDetected(result: any) {
    this.setState({results: this.state.results.concat([result])});
  }
}
