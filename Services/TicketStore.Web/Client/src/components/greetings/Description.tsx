import React from 'react';
import { PayButton } from './PayButton';
import TextField from '@material-ui/core/TextField';
import './Description.css';

export class Description extends React.Component {
  state: any = {
    count: 2,
  }

  handleChange = (count: any) => (event: any) => {
    this.setState({
      [count]: event.target.value
    });
  };

  render() {
    const roubles = 300;
    return (
      <div className="description">
         <div className="description__content">
            <div className="description__money">
              <TextField
                  id="standard-number"
                  label="Tickets"
                  value={this.state.count}
                  onChange={this.handleChange('count')}
                  type="number"
                  className="description__content--number description__content--item description__content--bold"
                  variant="outlined"
              />
              <div className="description__content--item description__content--bold">
                &#215;
              </div>
              <div className="description__content--item description__content--bold">
                {roubles} ₽
              </div>
              <PayButton
                className="description__content--item"
                roubles={roubles * this.state.count}
                targets="The Cellophane Heads - X лет"
                label="The Cellophane Heads - X лет"
                yandexMoneyAccount="410011021763706"
              />
            </div>
            <div className="description__licence">Оплата производится на Яндекс.Кошелек. В момент оплаты яндекс потребует указать имейл - после оплаты на него придет электронный билет со штрихкодом, который нужно будет показать на входе. Не покупайте билеты с рук, потому что они могут быть с неверным штрихкодом. Если что-то пошло не так - всегда можно написать <a href="https://vk.com/framebassman">мне.</a> Сделано с любовью</div>
          </div>
      </div>
    )
  }
}
