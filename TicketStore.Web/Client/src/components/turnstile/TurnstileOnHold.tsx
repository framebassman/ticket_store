import React from 'react';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';
import './TurnstileOnHold.css';

export const TurnstileOnHold = (props: any) => {
  const { onClick } = props;
  return (
    <div className="turnstile__parent">
      <div className="turnstile__child">
        <Button variant="contained" onClick={onClick}>
          <Typography variant="h1">Начать сканировать</Typography>
        </Button>
      </div>
    </div>
  )
}
