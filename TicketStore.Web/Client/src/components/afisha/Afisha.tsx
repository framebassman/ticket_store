import React, { Component } from 'react';
import { withStyles } from '@material-ui/styles';
import { styles } from './Afisha.styles';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import Event from './Event';

class Afisha extends Component<any, any> {
  render() {
    const { classes } = this.props;
    return (
      <div className={classes.afisha}>
        <Grid container justify="center">
          <Event artist="Филлип Киркоров"/>
          <Event artist="Николай Басков"/>
          <Event artist="Алла Пугачева"/>
          <Event artist="Владимир Кузьмин"/>
          <Event artist="Максим Галкин"/>
          <Event artist="Face"/>
          <Event artist="Тимати"/>
        </Grid>
      </div>
    )
  }
}

export default withStyles(styles)(Afisha);
