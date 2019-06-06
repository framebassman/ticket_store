import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../../store/Turnstile/actions';
import { withStyles } from '@material-ui/styles';
import { styles } from './Afisha.styles';
import Grid from '@material-ui/core/Grid';
import CircularProgress from '@material-ui/core/CircularProgress';
import Event from './Event';

class Afisha extends Component<any, any> {
  constructor(props: any, state: any) {
    super(props, state);
    this.state = {
      hasErrored: false,
      isLoading: false,
      events: [
        {
          artist: "Oxxxymiron",
          price: 5,
          pressRelease: "Товарищи! сложившаяся структура организации способствует подготовки и реализации новых предложений. Разнообразный и богатый опыт рамки и место обучения кадров позволяет оценить значение позиций, занимаемых участниками в отношении поставленных задач. Повседневная практика показывает, что новая модель организационной деятельности обеспечивает широкому кругу (специалистов) участие в формировании соответствующий условий активизации.",
          yandexMoneyAccount: "410011021763706",
          time: "2019-06-06T21:27:16.661655+03:00"
        },
        {
          artist: "Face",
          price: 4,
          pressRelease: "Товарищи! сложившаяся структура организации способствует подготовки и реализации новых предложений. Разнообразный и богатый опыт рамки и место обучения кадров позволяет оценить значение позиций, занимаемых участниками в отношении поставленных задач. Повседневная практика показывает, что новая модель организационной деятельности обеспечивает широкому кругу (специалистов) участие в формировании соответствующий условий активизации.",
          yandexMoneyAccount: "410011021763706",
          time: "2019-06-06T21:27:16.661696+03:00"
        },
        {
          artist: "XXXTenacion",
          price: 1,
          pressRelease: "Товарищи! сложившаяся структура организации способствует подготовки и реализации новых предложений. Разнообразный и богатый опыт рамки и место обучения кадров позволяет оценить значение позиций, занимаемых участниками в отношении поставленных задач. Повседневная практика показывает, что новая модель организационной деятельности обеспечивает широкому кругу (специалистов) участие в формировании соответствующий условий активизации.",
          yandexMoneyAccount: "410011021763706",
          time: "2019-06-06T21:27:16.661697+03:00"
        },
        {
          artist: "Виктор Цой",
          price: 3,
          pressRelease: "Товарищи! сложившаяся структура организации способствует подготовки и реализации новых предложений. Разнообразный и богатый опыт рамки и место обучения кадров позволяет оценить значение позиций, занимаемых участниками в отношении поставленных задач. Повседневная практика показывает, что новая модель организационной деятельности обеспечивает широкому кругу (специалистов) участие в формировании соответствующий условий активизации.",
          yandexMoneyAccount: "410011021763706",
          time: "2019-06-06T21:27:16.661697+03:00"
        },
        {
          artist: "Филипп Киркоров",
          price: 2,
          pressRelease: "Товарищи! сложившаяся структура организации способствует подготовки и реализации новых предложений. Разнообразный и богатый опыт рамки и место обучения кадров позволяет оценить значение позиций, занимаемых участниками в отношении поставленных задач. Повседневная практика показывает, что новая модель организационной деятельности обеспечивает широкому кругу (специалистов) участие в формировании соответствующий условий активизации.",
          yandexMoneyAccount: "410011021763706",
          time: "2019-06-06T21:27:16.661698+03:00"
        }
      ]
    }
  }

  render() {
    const { classes } = this.props;
    if (this.state.hasErrored) {
      return <p>Sorry! There was an error loading the items</p>;
    }

    if (this.state.isLoading) {
      return <CircularProgress className={classes.progress} />
    }

    return (
      <div className={classes.afisha}>
        <Grid container justify="center">
          {this.state.events.map((event, key) => (
            <Event artist={event.artist} key={key}/>
          ))}
        </Grid>
      </div>
    )
  }
}

// export default connect(
//   (state: any) => state.afisha,
//   dispatch => bindActionCreators(actionCreators, dispatch)
// )(Afisha);

export default withStyles(styles)(Afisha);
