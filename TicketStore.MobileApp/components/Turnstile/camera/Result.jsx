import React, { Component } from 'react';

export class Result extends Component {
  render() {
    const result = this.props.result;

    if (result === undefined || result.codeResult.code === "") {
      return null;
    }
    return (
      <li>{result.codeResult.code} [{result.codeResult.format}]</li>
    );
  }
}
