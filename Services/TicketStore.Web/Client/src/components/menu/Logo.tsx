import React from 'react';
import Typography from '@material-ui/core/Typography';
import Box from '@material-ui/core/Box';
import { withStyles } from '@material-ui/styles';
import { styles } from './Logo.styles';
import logo from './../../assets/img/logo_en.png';

const Logo = (props: any) => {
  const { classes } = props;
  return (
    <Typography className={classes.logo} component="div">
      <div className={classes.title}>
        <img className={classes.image} src={logo} alt="Chert-o-polokh"/>
      </div>
      <Box className={classes.description}>The place where you buy tickets</Box>
    </Typography>
  )
}

export default withStyles(styles)(Logo);
