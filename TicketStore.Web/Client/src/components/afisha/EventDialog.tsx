import React, { Component } from 'react';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import { PayButton } from '../greetings/PayButton';
import TextField from '@material-ui/core/TextField';

function declOfNum(num: number, titles: string[]) {
  const cases = [2, 0, 1, 1, 1, 2];
  return titles[ (num%100>4 && num%100<20)? 2 : cases[(num%10<5)?num%10:5] ];
};

export class EventDialog extends Component<any, any> {
  state: any = {
    count: 2,
  }

  handleChange = (count: any) => (event: any) => {
    this.setState({
      [count]: event.target.value
    });
  };
  
  render() {
    const { open, handleClose, roubles, target, yandexMoneyAccount } = this.props;
    return (
      <Dialog
        open={open}
        onClose={handleClose}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
      >
        <DialogTitle id="alert-dialog-title">{target}</DialogTitle>
        <DialogContent>
          <DialogContentText id="alert-dialog-description">
            <TextField
              id="ticket-count"
              label={declOfNum(this.state.count, ['Билет', 'Билета', 'Билетов'])}
              value={this.state.count}
              onChange={this.handleChange('count')}
              type="number"
              className="description__content--number description__content--item description__content--bold"
              variant="outlined"
            />
          </DialogContentText>
        </DialogContent>
        <DialogActions style={{justifyContent: 'center'}}>
          <PayButton roubles={roubles * this.state.count} target={target} yandexMoneyAccount={yandexMoneyAccount}/>
        </DialogActions>
      </Dialog>
    )
  }
}
