import React, { Component } from 'react';
import { connect } from 'react-redux';
import { eventsFetchData } from '../../store/Afisha/actions';
import { AfishaState } from '../../store/Afisha/state';

import Event from './Event';

import { withStyles } from '@material-ui/styles';
import { styles } from './Afisha.styles';
import Grid from '@material-ui/core/Grid';
import { CenteredProgress } from '../../components/core/progress/CenteredProgress';


class Afisha extends Component<any, AfishaState> {
  componentDidMount() {
    this.props.fetchData();
  }

  render() {
    const { classes, events, hasErrored, isLoading } = this.props;
    if (hasErrored) {
      return <p>Sorry! There was an error loading the items</p>;
    }

    if (isLoading) {
      return <CenteredProgress />
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

const mapStateToProps = (state) => {
  return {
      events: state.afisha.events,
      hasErrored: state.afisha.eventsHasErrored,
      isLoading: state.afisha.eventsIsLoading
  };
};
const mapDispatchToProps = (dispatch) => {
  return {
      fetchData: () => dispatch(eventsFetchData())
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(withStyles(styles)(Afisha));
