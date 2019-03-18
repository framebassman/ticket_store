import React, { Component } from 'react';
const Quagga = require('quagga');

export class Scanner extends Component<any, any> {
  constructor(props: any) {
    super(props);
    this._onDetected = this._onDetected.bind(this);
  }

  componentDidMount() {
    Quagga.init({
        inputStream: {
            type : "LiveStream",
            constraints: {
                width: 640,
                height: 480,
                facing: "environment" // or user
            }
        },
        locator: {
            patchSize: "medium",
            halfSample: true
        },
        numOfWorkers: 2,
        decoder: {
            readers : [ "code_128_reader"]
        },
        locate: true
    }, function(err: any) {
        if (err) {
            return console.log(err);
        }
        Quagga.start();
    });
    Quagga.onDetected(this._onDetected);
  }

  componentWillUnmount() {
    Quagga.offDetected(this._onDetected);
  }

  _onDetected(result: any) {
    this.props.onDetected(result);
  }

  render() {
    return (
      <div>
        <div id="interactive" className="viewport"/>
        <div>Hi</div>
      </div>
    )
  }
}
