import React from 'react';
import { Title } from './Title';
import { Description } from './Description';
import './Greetings.css';

export const Greetings = () => {
  return (
    <div className="greetings">
      <Title />
      <Description />
    </div>
  )
}
