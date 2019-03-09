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

  declOfNum = (num: number, titles: string[]) => {
    const cases = [2, 0, 1, 1, 1, 2];
    return titles[ (num%100>4 && num%100<20)? 2 : cases[(num%10<5)?num%10:5] ];
  };

  render() {
    const roubles = 150;
    return (
      <div className="description">
         <div className="description__content">
            <div className="description__money">
              <TextField
                  id="standard-number"
                  label={this.declOfNum(this.state.count, ['Билет', 'Билета', 'Билетов'])}
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
              <PayButton className="description__content--item" roubles={roubles * this.state.count}/>
            </div>
            <div className="description__licence">Оплата производится на Яндекс.Кошелек. В момент оплаты яндекс потребует указать имейл - после оплаты на него придет электронный билет со штрихкодом, который нужно будет показать на входе. Не покупайте билеты с рук, потому что они могут быть с неверным штрихкодом. Если что-то пошло не так - всегда можно написать <a href="https://vk.com/framebassman">мне.</a> Сделано с любовью</div>
          </div>
      </div>
    )
  }
}
