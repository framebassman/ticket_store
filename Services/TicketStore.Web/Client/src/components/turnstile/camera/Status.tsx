import React from 'react';
import Fab from '@material-ui/core/Fab';
import CheckIcon from '@material-ui/icons/Check';
import CancelIcon from '@material-ui/icons/Cancel';
import ScannerIcon from '@material-ui/icons/Scanner';
import { createMuiTheme, MuiThemeProvider } from '@material-ui/core/styles';
import green from '@material-ui/core/colors/green';
import red from '@material-ui/core/colors/red';

import './Status.css';
import { ScannedTicket } from '../TurnstileState';

const theme = createMuiTheme({
  palette: {
    primary: green,
    secondary: red,
  }
});

const Description = ({ message }: { message: string }) => {
  return (
    <span className="description">{message}</span>
  )
}

type Props = {
  pass: boolean,
  wait: boolean,
  scannedTicket: ScannedTicket,
};

export const Status = ({ pass, wait, scannedTicket }: Props) => {
  console.log(scannedTicket);
  // const { used, concertLabel } = scannedTicket;

  const Container = ({ children }) => (
    <MuiThemeProvider theme={theme}>
      <div className="status-container">
        {children}
      </div>
    </MuiThemeProvider>
  )

  if (!wait) {
    return (
      <Container>
        <Fab><ScannerIcon /></Fab>
        <Description message="Готов сканировать!"/>
      </Container>
    )
  }

  return (
    <Container>
      {pass
        ? (
          <div>
            <Fab color="primary"><CheckIcon /></Fab>
            <Description message="Успешно!"/>
          </div>
        )
        : (
          <div>
            <Fab color="secondary"><CancelIcon /></Fab>
            <Description message="Ошибочка вышла!"/>
          </div>
        )
      }
    </Container>
  )
}
