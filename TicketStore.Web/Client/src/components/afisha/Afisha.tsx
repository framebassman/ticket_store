import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { itemsFetchData } from '../../store/Afisha/actions';
import { AfishaState } from '../../store/Afisha/state';

import Event from './Event';

import { withStyles } from '@material-ui/styles';
import { styles } from './Afisha.styles';
import Grid from '@material-ui/core/Grid';
import CircularProgress from '@material-ui/core/CircularProgress';


class Afisha extends Component<any, any> {
  componentDidMount() {
    this.props.fetchData();
  }

  render() {
    console.log('inside render');
    const { classes, events, hasErrored, isLoading } = this.props;
    if (hasErrored) {
      return <p>Sorry! There was an error loading the items</p>;
    }

    if (isLoading) {
      return <CircularProgress />
    }

    return (
      <div>
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
      fetchData: () => dispatch(itemsFetchData())
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(withStyles(styles)(Afisha));
