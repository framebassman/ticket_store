import React from 'react';
import Divider from '@material-ui/core/Divider';
import Typography from '@material-ui/core/Typography';
import { styles } from './MenuFooter.styles';
import { withStyles } from '@material-ui/styles';

const MenuFooter = (props: any) => {
  const { classes } = props;
  return (
    <Typography component="div">
      <div className={classes.content}></div>
      <Divider />
      <div className={classes.footer}>
        <div>Сделано в компании</div>
        <div>Kolenka Inc.</div>
      </div>
    </Typography>
  )
}

export default withStyles(styles)(MenuFooter);
