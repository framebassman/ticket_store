import React, { Component } from 'react';
import { BrowserRouter, Route, Redirect, Switch} from 'react-router-dom';
import yellow from '@material-ui/core/colors/yellow';
import { MuiThemeProvider, createMuiTheme } from '@material-ui/core/styles';
import { Farewell } from './components/farewell/Farewell';
import Turnstile from './components/turnstile/camera/TurnstileCamera';
import TurnstileManual from './components/turnstile/manual/TurnstileManual';
import { Menu } from './components/menu/Menu';
import Afisha from './components/afisha/Afisha';

const AfishaWithMenu = () => {
  const theme = createMuiTheme({
    palette: {
      primary: {
        main: '#FFFFFF'
      },
      secondary: yellow
    }
  });
  return (
    <MuiThemeProvider theme={theme}>
      <Menu>
        <Afisha/>
      </Menu>
    </MuiThemeProvider>
  )
}

class App extends Component {
  render() {
    return (
      <BrowserRouter>
        <Switch>
          <Route path="/tickets/farewell" component={Farewell}/>
          <Redirect from="/tickets/turnstile" to="/turnstile/camera" />
          <Route path="/turnstile/camera" component={Turnstile}/>
          <Route path="/turnstile/manual" component={TurnstileManual}/>
          <Route path="" component={AfishaWithMenu}/>
        </Switch>
      </BrowserRouter>
    );
  }
}

export default App;
