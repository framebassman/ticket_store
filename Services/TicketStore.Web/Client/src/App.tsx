import React, { Component } from 'react';
import { BrowserRouter, Route, Redirect, Switch} from 'react-router-dom';
import yellow from '@material-ui/core/colors/yellow';
import { MuiThemeProvider, createMuiTheme } from '@material-ui/core/styles';

import { Farewell } from './components/farewell/Farewell';
import TurnstileMenu from './components/turnstile/TurnstileMenu';
import { Menu } from './components/menu/Menu';
import Afisha from './components/afisha/Afisha';
import { FirstLoaderRemover } from './components/core/FirstLoaderRemover/FirstLoaderRemover';

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
        <FirstLoaderRemover />
        <Switch>
          <Route path="/tickets/farewell" component={Farewell}/>
          <Redirect from="/tickets/turnstile" to="/turnstile/camera" />
          <Route path="/turnstile" component={TurnstileMenu} />
          <Route path="" component={AfishaWithMenu}/>
        </Switch>
      </BrowserRouter>
    );
  }
}

export default App;
