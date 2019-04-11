import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../../store/Turnstile/actions';

import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import { Formik } from 'formik';

import { TurnstileState } from './TurnstileState';
import { TurnstileOnHold } from './TurnstileOnHold';
import './Turnstile.css';

class Turnstile extends Component<any, TurnstileState> {
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
        <Formik
          initialValues={{ code: '' }}
          onSubmit={values => {
              verify(values.code);
            }
          }
        >
          {({
            values,
            errors,
            touched,
            handleChange,
            handleBlur,
            handleSubmit,
            isSubmitting
          }) => (
            <form onSubmit={handleSubmit}>
              <TextField
                name="code"
                type="number"
                variant="outlined"
                onChange={handleChange}
                onBlur={handleBlur}
                value={values.code}
              />
              <Button type="submit">
                Проверить
              </Button>
            </form>
        )}
        </Formik>
    );
  }

  _toggle() {
    this.setState({scanning: !this.state.scanning});
  }
}

export default connect(
  (state: any) => state.turnstile,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(Turnstile);
