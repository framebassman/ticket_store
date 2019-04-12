import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../../store/Turnstile/actions';

import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import { Formik } from 'formik';

import { TurnstileState } from './TurnstileState';
import { TurnstileOnHold } from './TurnstileOnHold';
import ManualTurnstile from './manual/ManualTurnstile';
import './Turnstile.css';

export class Turnstile extends Component<any, TurnstileState> {
  constructor(props: any, state: TurnstileState) {
    super(props, state);
    this.state = {
        scanning: false,
        result: undefined,
        pass: false,
        isRequested: false,
    }
    this._toggle = this._toggle.bind(this);
  }

  render() {
    const { verify } = this.props;

    if (this.state.scanning === false) {
      return <TurnstileOnHold onClick={this._toggle}/>
    }

    return (
      <ManualTurnstile />
    )
  }

  _toggle() {
    this.setState({scanning: !this.state.scanning});
  }
}
