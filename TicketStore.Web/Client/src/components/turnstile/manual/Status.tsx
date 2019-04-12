import React from 'react';

import Fab from '@material-ui/core/Fab';
import CheckIcon from '@material-ui/icons/Check';
import { createMuiTheme, MuiThemeProvider } from '@material-ui/core/styles';
import green from '@material-ui/core/colors/green';
import red from '@material-ui/core/colors/red';

const theme = createMuiTheme({
  palette: {
    primary: green,
    secondary: red,
  },
});

export const Status = (props: any) => {
  const { className } = props;
  return (
    <div className={className}>
      <MuiThemeProvider theme={theme}>
        <Fab color="primary" >
          <CheckIcon />
        </Fab>
      </MuiThemeProvider>
    </div>
  )
}
