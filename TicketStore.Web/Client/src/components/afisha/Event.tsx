import React, { Component } from 'react';
import { withStyles } from '@material-ui/styles';
import { styles } from './Event.styles';
import Card from '@material-ui/core/Card';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import Button from '@material-ui/core/Button';
import Typography from '@material-ui/core/Typography';
import { PayButton } from '../greetings/PayButton';

class Event extends Component<any, any> {
    render() {
        const { classes, artist } = this.props;
        return (
            <Card className={classes.card}>
                <CardContent>
                    <Typography align="center" variant="h5" component="h2">{artist}</Typography>
                </CardContent>
                <CardActions>
                    <PayButton roubles={2} target={artist}/>
                </CardActions>
            </Card>
        )
    }
}

export default withStyles(styles)(Event);
