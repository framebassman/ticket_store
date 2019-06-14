import React, { Component } from 'react';
import { withStyles } from '@material-ui/styles';
import { styles } from './Event.styles';
import Button from '@material-ui/core/Button';
import Card from '@material-ui/core/Card';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import CardMedia from '@material-ui/core/CardMedia';
import Typography from '@material-ui/core/Typography';
import { PayButton } from '../greetings/PayButton';
import { EventTime } from '../core/time/EventTime';
import { EventDialog } from './EventDialog';

class Event extends Component<any, any> {
  // constructor(props: any, state: any) {
  //   super(props, state);
  //   this.state = {
  //     isDialogOpened: false
  //   }
  // }
  
  _handleClickOpen() {
    this.setState({
      isDialogOpened: true
    });
  }

  _handleClickClose() {
    this.setState({
      isDialogOpened: false
    });
  }

  render() {
    const { classes, artist, roubles, yandexMoneyAccount, time, posterUrl } = this.props;
    // const { isDialogOpened } = this.state;
    return (
      <div>
        <Card className={classes.card}>
          <CardContent>
            <Typography align="center" variant="h5" component="h2">{artist}</Typography>
            <CardContent>
              <CardMedia width="140" component="img" image={posterUrl} />
            </CardContent>
            <EventTime startedAt={new Date(time)}/>
            <CardActions className={classes.action}>
              {/* <PayButton roubles={roubles} target={artist} yandexMoneyAccount={yandexMoneyAccount}/> */}
              <Button variant="contained" color="secondary" size="large" onClick={this._handleClickOpen}>Купить билет</Button>
            </CardActions>
          </CardContent>
        </Card>
        {/* <EventDialog open={isDialogOpened} handleClose={this._handleClickClose}/> */}
      </div>
    )
  }
}

export default withStyles(styles)(Event);
