import React from 'react';
import Typography from '@material-ui/core/Typography';
import Box from '@material-ui/core/Box';
import { withStyles } from '@material-ui/styles';
import { styles } from './Logo.styles';
import logo from './../../assets/img/logo.svg';

const Logo = (props: any) => {
  const { classes } = props;
  return (
    <Typography className={classes.logo} component="div">
      <div className={classes.title}>
        <img className={classes.image} src={logo} alt="Чертополох"/>
      </div>
      <Box className={classes.description}>Место, где покупают билеты</Box>
    </Typography>
  )
}

export default withStyles(styles)(Logo);
