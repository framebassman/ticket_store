import React, { Component } from 'react';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogTitle from '@material-ui/core/DialogTitle';
import { PayButton } from '../greetings/PayButton';
import TextField from '@material-ui/core/TextField';
import './EventDialog.css';

function declOfNum(num: number, titles: string[]) {
  const cases = [2, 0, 1, 1, 1, 2];
  return titles[ (num%100>4 && num%100<20)? 2 : cases[(num%10<5)?num%10:5] ];
};

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
                label={declOfNum(this.state.count, ['Билет', 'Билета', 'Билетов'])}
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
            Оплата производится на Яндекс.Кошелек. В момент оплаты яндекс потребует указать имейл - после оплаты на него придет электронный билет со штрихкодом, который нужно будет показать на входе. Не покупайте билеты с рук, потому что они могут быть с неверным штрихкодом. Если что-то пошло не так - всегда можно написать <a href="https://vk.com/framebassman">в техподдержку.</a> Сделано с любовью.
          </div>
        </DialogContent>
        <DialogActions className={"event-dialog__actions"}>
          <PayButton roubles={roubles * this.state.count} label={name} yandexMoneyAccount={yandexMoneyAccount}/>
        </DialogActions>
      </Dialog>
    )
  }
}
