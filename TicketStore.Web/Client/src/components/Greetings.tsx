import React from 'react';
import Grid from '@material-ui/core/Grid';
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import { PayButton } from './PayButton';
import './Greetings.css';

export const Greetings = () => {
  return (
    <Grid
      container
      spacing={0}
      direction="column"
      alignItems="center"
      justify="center"
      style={{ minHeight: '100vh' }}
    >
      <Grid item xs={10}>
        <Card>
          <CardContent>
            <div className="content">Купить билет на концерт</div>
            <div className="content black">The Cellophane Heads</div>
            <div className="content">20 апреля 2019 года</div>
            <div className="content">Чердаке, Иваново</div>
            <PayButton />
          </CardContent>
        </Card>
      </Grid>      
   </Grid>
  )
}
