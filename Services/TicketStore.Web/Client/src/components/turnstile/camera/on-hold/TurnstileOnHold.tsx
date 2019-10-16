import React from 'react';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';

import './TurnstileOnHold.css';

type Props = {
  onClick: () => any
}

export const TurnstileOnHold = ({ onClick }: Props) => {
  return (
    <Button variant="contained" onClick={onClick}>
      <Typography variant="h4">Начать сканировать</Typography>
    </Button>
  )
};
