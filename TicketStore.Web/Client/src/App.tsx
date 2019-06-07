import React, { Component } from 'react';
import { BrowserRouter, Route, Switch} from 'react-router-dom';
import { Farewell } from './components/farewell/Farewell';
import Turnstile from './components/turnstile/Turnstile';
import { AfishaWithMenu } from './components/afisha/AfishaWithMenu';

class App extends Component {
  render() {
    return (
      <BrowserRouter>
        <Switch>
          <Route path="/tickets/farewell" component={Farewell}/>
          <Route path="/tickets/turnstile" component={Turnstile}/>
          <Route path="" component={AfishaWithMenu}/>
        </Switch>
      </BrowserRouter>
    );
  }
}

export default App;
