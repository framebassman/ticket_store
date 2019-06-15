import React, { Component } from 'react';
import { BrowserRouter, Route, Switch} from 'react-router-dom';
import yellow from '@material-ui/core/colors/yellow';
import { MuiThemeProvider, createMuiTheme } from '@material-ui/core/styles';
import { Farewell } from './components/farewell/Farewell';
import { Greetings } from './components/greetings/Greetings';
import Turnstile from './components/turnstile/Turnstile';
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
          <Route path="/tickets/turnstile" component={Turnstile}/>
          <Route path="/greetings" component={Greetings}/>
          <Route path="" component={AfishaWithMenu}/>
        </Switch>
      </BrowserRouter>
    );
  }
}

export default App;
