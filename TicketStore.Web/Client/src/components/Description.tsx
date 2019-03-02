import React from 'react';
import { PayButton } from './PayButton';
import './Description.css';

export const Description = () => {
  return (
    <div className="description">
      <div className="description__content">
        <div className="description__money">
          <div className="description__money--item">Стоимость</div>
          <div className="description__money--item description__money--bold">250 ₽</div>
        </div>
        <PayButton className="description__money--item" />
      </div>
    </div>
  )
}
