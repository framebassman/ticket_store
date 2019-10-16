import React from 'react';
import './Farewell.css';

export const Farewell = () => {
  return (
    <div className="farewell">
      <div className="farewell__item">
        <div className="farewell__item--centered farewell__item--title">Спасибо за поддержку</div>
      </div>
      <div className="farewell__item block">
        <div>Сейчас мы отправим вам билет на имейл</div>
        <div>Увидимся на концерте</div>
        <div className="footer"><a href="https://vk.com/framebassman">Напишите</a>, если что то пошло не так</div>
      </div>
    </div>
  )
}
