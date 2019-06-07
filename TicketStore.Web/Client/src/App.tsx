import React, { Component } from 'react';
import { BrowserRouter, Route, Switch} from 'react-router-dom';
import yellow from '@material-ui/core/colors/yellow';
import grey from '@material-ui/core/colors/grey';
import { MuiThemeProvider, createMuiTheme } from '@material-ui/core/styles';
import { Farewell } from './components/farewell/Farewell';
import Turnstile from './components/turnstile/Turnstile';
import { AfishaWithMenu } from './components/afisha/AfishaWithMenu';

class App extends Component {
  render() {
    const theme = createMuiTheme({
      palette: {
        primary: {
          main: '#FFFFFF'
        },
        secondary: yellow
      }
    });

    return (
      <BrowserRouter>
        <Switch>
          <Route path="/tickets/farewell" component={Farewell}/>
          <Route path="/tickets/turnstile" component={Turnstile}/>
          <MuiThemeProvider theme={theme}>
            <Route path="" component={AfishaWithMenu}/>
          </MuiThemeProvider>
        </Switch>
      </BrowserRouter>
    );
  }
}

export default App;
