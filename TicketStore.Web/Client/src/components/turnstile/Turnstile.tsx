import React, { Component } from 'react';
import Button from '@material-ui/core/Button';
import { Scanner } from './Scanner';
import './Turnstile.css';

interface TurnstileState {
  scanning: boolean,
  results: any[],
  pass: boolean,
  isRequested: boolean
}

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
    this._isPass = this._isPass.bind(this);
    this._waitNext = this._waitNext.bind(this);
  }

  render() {
    const color = this._isPass();
    this._waitNext(color);
    return (
      <div className="turnstile">
        <Button
          onClick={this._scan}
          style={{backgroundColor: color}}
        >
          {this.state.scanning ? 'Stop' : 'Start'}
        </Button>
        {this.state.scanning ? <Scanner onDetected={this._onDetected}/> : null}
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

  _isPass(): string {
    if (this.state.isRequested === false) {
      this.setState({isRequested: !this.state.isRequested});
      setTimeout(function() {}, 1000);
      return "#000000";
    }
    // this.setState({isRequested: !this.state.isRequested});
    if (this.state.pass === true) {
      return "#4caf50";
    } else {
      return "#e53935";
    }
  }

  _waitNext(color: string): void {
    // if (color === "#e53935") {
    //   setTimeout(function() {}, 1000);
    // }
  }
}
