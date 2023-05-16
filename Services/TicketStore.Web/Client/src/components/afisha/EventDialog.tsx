import React, { Component } from 'react';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogTitle from '@material-ui/core/DialogTitle';
import { PayButton } from '../greetings/PayButton';
import TextField from '@material-ui/core/TextField';
import './EventDialog.css';

export class EventDialog extends Component<any, any> {
  state: any = {
    count: 1,
  }

  handleChange = (count: any) => (event: any) => {
    this.setState({
      [count]: event.target.value
    });
  };
  
  render() {
    const { open, handleClose, roubles, name, yandexMoneyAccount } = this.props;
    return (
      <Dialog
        open={open}
        onClose={handleClose}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
      >
        <DialogTitle id="alert-dialog-title">{name}</DialogTitle>
        <DialogContent>
          <div className={"event-dialog__money-section"}>
            <div className="event-dialog__item">
              <TextField
                id="ticket-count"
                className={"event-dialog__bold"}
                label="Tickets"
                value={this.state.count}
                onChange={this.handleChange('count')}
                type="number"
                variant="outlined"
              />
            </div>
            <div className="event-dialog__item event-dialog__bold">&#215;</div>
            <div className="event-dialog__item event-dialog__bold">{roubles + '\u00A0₽'}</div>
          </div>
          <div className="event-dialog__license">
          Tickets cannot be returned or exchanged. Payment is made with YooMoney. At the time of payment, the service will ask for an email - after payment will receive an electronic ticket with a barcode, which must be shown at the entrance. Do not buy tickets by hand, because they may have the wrong barcode. If something went wrong - you can always text <a href="https://vk.me/sudo_chertopolokh">to tech support.</a> Made with love.
          </div>
        </DialogContent>
        <DialogActions className={"event-dialog__actions"}>
          <PayButton roubles={roubles * this.state.count} targets={name} label={name} yandexMoneyAccount={yandexMoneyAccount}/>
        </DialogActions>
      </Dialog>
    )
  }
}
