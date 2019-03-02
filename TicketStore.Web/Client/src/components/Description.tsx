import React from 'react';
import { PayButton } from './PayButton';
import './Description.css';

export const Description = () => {
  return (
    <div className="description">
       <div className="description__content">
          <div className="description__money">
            <div className="description__content--item">Стоимость</div>
            <div className="description__content--item description__content--bold">250 ₽</div>
            <PayButton className="description__content--item" />
          </div>
          <div className="description__licence">Licence</div>
        </div>
    </div>
  )
}
