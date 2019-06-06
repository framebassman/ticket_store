import React from 'react';
import Fab from '@material-ui/core/Fab';
import CheckIcon from '@material-ui/icons/Check';
import CancelIcon from '@material-ui/icons/Cancel';
import { createMuiTheme, MuiThemeProvider } from '@material-ui/core/styles';
import green from '@material-ui/core/colors/green';
import red from '@material-ui/core/colors/red';
import yellow from '@material-ui/core/colors/yellow';

const waitableTheme = createMuiTheme({
  palette: {
    primary: green,
    secondary: red,
  },
  typography: {
    useNextVariants: true,
  },
});
const nonWaitableTheme = createMuiTheme({
    palette: {
      primary: yellow,
    },
    typography: {
      useNextVariants: true,
    },
  });

export const Status = (props: any) => {
  const { className, pass, wait } = props;
  if (wait) {
    return (
      <MuiThemeProvider theme={waitableTheme}>
        <div className={className}>
          {pass
            ? <Fab color="primary"><CheckIcon /></Fab>
            : <Fab color="secondary"><CancelIcon /></Fab>
          }
        </div>
      </MuiThemeProvider>
    )
  } else {
    return (
      <MuiThemeProvider theme={nonWaitableTheme}>
        <div className={className}>
          <Fab color="primary"><CheckIcon /></Fab>
        </div>
      </MuiThemeProvider>
    )
  }
}