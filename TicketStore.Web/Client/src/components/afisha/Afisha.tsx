import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../../store/Afisha/actions';
import { AfishaState } from '../../store/Afisha/state';

import Event from './Event';

import { withStyles } from '@material-ui/styles';
import { styles } from './Afisha.styles';
import Grid from '@material-ui/core/Grid';
import CircularProgress from '@material-ui/core/CircularProgress';


class Afisha extends Component<any, AfishaState> {
  constructor(props: any, state: any) {
    super(props, state);
    this.state = {
      hasErrored: false,
      isLoading: true,
      events: []
    }
  }

  componentDidMount() {
    console.log('inside didMount');
    const { fetchEvents } = this.props;
    fetchEvents();
    // const result = fetchEvents();
    // console.log('fetch result: ', result);
  }

  render() {
    const { classes, events, fetchEvents} = this.props;
    if (this.state.hasErrored) {
      return <p>Sorry! There was an error loading the items</p>;
    }

    if (this.state.isLoading === true) {
      return <CircularProgress />
    }

    return (
      <div className={classes.afisha}>
        <Grid container justify="center">
          {events.map((event, key) => (
            <Event artist={event.artist} key={key}/>
          ))}
        </Grid>
      </div>
    )
  }
}

export default connect(
  (state: any) => state.afisha,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(Afisha);

// export default withStyles(styles)(Afisha);
