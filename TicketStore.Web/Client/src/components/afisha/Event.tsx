import React, { Component } from 'react';
import { withStyles } from '@material-ui/styles';
import { styles } from './Event.styles';
import Card from '@material-ui/core/Card';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import CardMedia from '@material-ui/core/CardMedia';
import Typography from '@material-ui/core/Typography';
import { PayButton } from '../greetings/PayButton';
import { EventTime } from '../core/time/EventTime';

class Event extends Component<any, any> {
  render() {
    const { classes, artist, roubles, pressRelease, yandexMoneyAccount, time, posterUrl } = this.props;
    return (
      <Card className={classes.card}>
        <CardContent>
          <Typography align="center" variant="h5" component="h2">{artist}</Typography>
          <CardContent>
            <CardMedia width="140" component="img" image={posterUrl} />
          </CardContent>
          <EventTime startedAt={new Date(time)}/>
          <CardActions>
            <PayButton roubles={roubles} target={artist} yandexMoneyAccount={yandexMoneyAccount}/>
          </CardActions>
        </CardContent>
      </Card>
    )
  }
}

export default withStyles(styles)(Event);
