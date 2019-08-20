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
        <a className={classes.link} href="https://vk.me/sudo_chertopolokh">Техподдержка</a>
        <a className={classes.link} href="http://status.chertopolokh.ru">Статус</a>
        <span>Сделано в компании</span>
        <span>Kolenka Inc.</span>
      </div>
    </Typography>
  )
}

export default withStyles(styles)(MenuFooter);
