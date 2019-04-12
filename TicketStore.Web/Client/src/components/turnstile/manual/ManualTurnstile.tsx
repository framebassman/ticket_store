import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../../../store/Turnstile/actions';

import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import { Formik } from 'formik';
import './ManualTurnstile.css';

class ManualTurnstile extends Component<any, any> {
    render() {
      const { verify } = this.props;
      return (
        <div>
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
              <form className="turnstile__form" onSubmit={handleSubmit}>
                <div className="turnstile__barcode">
                  <TextField
                    name="code"
                    type="number"
                    variant="outlined"
                    autoComplete="off"
                    autoFocus={true}
                    onChange={handleChange}
                    onBlur={handleBlur}
                    value={values.code}
                  />
                </div>
                <Button variant="contained" type="submit">
                  Нажмите, чтобы проверить
                </Button>
              </form>
          )}
          </Formik>
        </div>
      );
    }
}

export default connect(
    (state: any) => state.turnstile,
    dispatch => bindActionCreators(actionCreators, dispatch)
  )(ManualTurnstile);
