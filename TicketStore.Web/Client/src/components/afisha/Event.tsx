import React, { Component } from 'react';
import { withStyles } from '@material-ui/styles';
import { styles } from './Event.styles';
import Card from '@material-ui/core/Card';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import Typography from '@material-ui/core/Typography';
import { PayButton } from '../greetings/PayButton';
import { EventTime } from '../core/time/EventTime';

class Event extends Component<any, any> {
  render() {
    const { classes, artist, roubles, pressRelease, yandexMoneyAccount, time } = this.props;
    return (
      <Card className={classes.card}>
        <CardContent>
          <Typography align="center" variant="h5" component="h2">{artist}</Typography>
          <Typography align="center" variant="subtitle2">{pressRelease}</Typography>
          <EventTime startedAt={new Date(time)}/>
        </CardContent>
        <CardActions>
          <PayButton roubles={roubles} target={artist} yandexMoneyAccount={yandexMoneyAccount}/>
        </CardActions>
      </Card>
    )
  }
}

export default withStyles(styles)(Event);
