import React from 'react';
import { PayButton } from './PayButton';
import './Description.css';

export const Description = () => {
  const roubles = 2;
  return (
    <div className="description">
       <div className="description__content">
          <div className="description__money">
            <div className="description__content--item description__content--bold">{roubles} ₽</div>
            <PayButton className="description__content--item" roubles={roubles}/>
          </div>
          <div className="description__licence">За один раз можно купить только один билет. Оплата производится на Яндекс.Кошелек. В момент оплаты яндекс потребует указать имейл - после оплаты на него придет электронный билет со штрихкодом, который нужно будет показать на входе. Не покупайте билеты с рук, потому что они могут быть с неверным штрихкодом. Сделано с любовью</div>
        </div>
    </div>
  )
}
