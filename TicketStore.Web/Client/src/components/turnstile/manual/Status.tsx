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
  typography: {
    useNextVariants: true,
  },
});

export const Status = (props: any) => {
  const { className, pass } = props;
  console.log("pass: ", pass);
  return (
    <div className={className}>
      <MuiThemeProvider theme={theme}>
        {pass
          ? <Fab color="primary" ><CheckIcon /></Fab>
          : <Fab color="secondary" ><CheckIcon /></Fab>
        }
      </MuiThemeProvider>
    </div>
  )
}
