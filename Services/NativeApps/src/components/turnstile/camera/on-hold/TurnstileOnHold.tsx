import React from 'react';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';

import './TurnstileOnHold.css';

type Props = {
  onClick: () => any
}

export const TurnstileOnHold = ({ onClick }: Props) => {
  return (
    <div className="button_wrapper">
      <Button className="start_button" variant="contained" onClick={onClick}>
        <Typography variant="h4">Start</Typography>
      </Button>
    </div>
  )
};
