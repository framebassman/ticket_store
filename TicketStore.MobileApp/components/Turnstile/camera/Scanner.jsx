import React, { Component } from 'react';
import Quagga from 'quagga';

export class Scanner extends Component {
  constructor(props) {
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
          facingMode: "environment", // or user
        }
      },
      locator: {
        patchSize: "medium",
        halfSample: true
      },
      numOfWorkers: navigator.hardwareConcurrency,
      decoder: {
        readers : [ "code_128_reader"],
        debug: {
          drawBoundingBox: true,
          showFrequency: true,
          drawScanline: false,
          showPattern: false
        }
      },
      locate: true,
      frequency: 10,
    }, function(err) {
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

  _onDetected(result) {
    this.props.onDetected(result);
  }

  render() {
    return (
      <div id="interactive" className="viewport"/>
    )
  }
}
