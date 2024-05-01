import React from 'react';
import './Farewell.css';

export const Farewell = () => {
  return (
    <div className="farewell">
      <div className="farewell__item">
        <div className="farewell__item--centered farewell__item--title">Thank you for your support</div>
      </div>
      <div className="farewell__item block">
        <div>We'll email you the ticket now</div>
        <div>See you at the concert</div>
        <div className="footer">Please <a href="https://vk.com/framebassman">let us know </a>if anything went wrong</div>
      </div>
    </div>
  )
}
