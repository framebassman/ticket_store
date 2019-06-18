import React, { Component } from 'react';
import { connect } from 'react-redux';
import { merchantsFetchData } from '../../store/Afisha/merchants/actions';
import { eventsFetchData } from '../../store/Afisha/events/actions';
import { AfishaState } from '../../store/Afisha/state';

import Event from './Event';

import { withStyles } from '@material-ui/styles';
import { styles } from './Afisha.styles';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import Box from '@material-ui/core/Box';
import { CenteredProgress } from '../core/progress/CenteredProgress';


class Afisha extends Component<any, AfishaState> {
  componentDidMount() {
    const { fetchMerchants, fetchEvents } = this.props;
    fetchMerchants()
      .then((merchants: any[]) => {
        fetchEvents(merchants[0].id)
      })
  }

  render() {
    const {
      classes,
      events, eventsHasErrored, eventsIsLoading,
      merchants, merchantsHasErrored, merchantsIsLoading,
    } = this.props;
    if (merchantsHasErrored || eventsHasErrored) {
      return (
        <Typography align="center" component="div">
          <Box marginTop={16}>
            <div>У нас что-то сломалось.</div>
            <div>Мы уже знаем об этом и уже чиним.</div>
          </Box>
        </Typography>
      );
    }

    if (merchantsIsLoading || eventsIsLoading) {
      return <CenteredProgress />
    }

    console.log("merchants in render: ", merchants);

    return (
      <div className={classes.afisha}>
        <Grid container justify="center">
          {events.map((event, key) => (
            <Event
              key={key}
              artist={event.artist}
              roubles={event.roubles}
              pressRelease={event.pressRelease}
              yandexMoneyAccount={event.yandexMoneyAccount}
              time={event.time}
              posterUrl={event.posterUrl}
            />
          ))}
        </Grid>
      </div>
    )
  }
}

const mapStateToProps = (state) => {
  return {
    events: state.afisha.events.events,
    eventsHasErrored: state.afisha.events.eventsHasErrored,
    eventsIsLoading: state.afisha.events.eventsIsLoading,
    merchants: state.afisha.merchants.merchants,
    merchantsHasErrored: state.afisha.merchants.merchantsHasErrored,
    merchantsIsLoading: state.afisha.merchants.merchantsIsLoading,
  };
};
const mapDispatchToProps = (dispatch) => {
  return {
    fetchMerchants: () => dispatch(merchantsFetchData()),
    fetchEvents: (merchantId: number) => dispatch(eventsFetchData(merchantId))
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(withStyles(styles)(Afisha));
