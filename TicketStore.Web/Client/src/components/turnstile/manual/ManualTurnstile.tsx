import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../../../store/Turnstile/actions';

import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import { Formik } from 'formik';

class ManualTurnstile extends Component<any, any> {
    render() {
        const { verify } = this.props;
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
}

export default connect(
    (state: any) => state.turnstile,
    dispatch => bindActionCreators(actionCreators, dispatch)
  )(ManualTurnstile);
