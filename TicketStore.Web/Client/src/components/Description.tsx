import React from 'react';
import { PayButton } from './PayButton';
import './Description.css';

export const Description = () => {
  return (
    <div className="description">
      <div className="description__content">
        <div>The Cellophane Heads - X лет</div>
        <PayButton />
      </div>
    </div>
  )
}
