import React, { Component } from 'react';
import axios from 'axios';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../../store/Turnstile/actions';
import { withStyles } from '@material-ui/styles';
import { styles } from './Afisha.styles';
import Grid from '@material-ui/core/Grid';
import CircularProgress from '@material-ui/core/CircularProgress';
import Event from './Event';

class Afisha extends Component<any, any> {
  constructor(props: any, state: any) {
    super(props, state);
    this.state = {
      hasErrored: false,
      isLoading: false,
      events: []
    }
  }

  fetchData() {
    this.setState({ isLoading: true });

    axios.get('api/events')
      .then((response) => {
        if (response.status != 200) {
            throw Error(response.statusText);
        }

        this.setState({ isLoading: false });

        return response;
      })
      .then((response) => response.data)
      .then((events) => this.setState({ events: events }))
      .catch(() => this.setState({ hasErrored: true }));
  }

  componentDidMount() {
    this.fetchData();
  }

  render() {
    const { classes } = this.props;
    if (this.state.hasErrored) {
      return <p>Sorry! There was an error loading the items</p>;
    }

    if (this.state.isLoading) {
      return <CircularProgress className={classes.progress} />
    }

    return (
      <div className={classes.afisha}>
        <Grid container justify="center">
          {this.state.events.map((event, key) => (
            <Event artist={event.artist} key={key}/>
          ))}
        </Grid>
      </div>
    )
  }
}

// export default connect(
//   (state: any) => state.afisha,
//   dispatch => bindActionCreators(actionCreators, dispatch)
// )(Afisha);

export default withStyles(styles)(Afisha);
