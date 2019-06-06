import React, { Component } from 'react';
import { BrowserRouter, Route, Switch} from 'react-router-dom';
import { Greetings } from './components/greetings/Greetings';
import { Farewell } from './components/farewell/Farewell';
import Turnstile from './components/turnstile/Turnstile';
import Afisha from './components/afisha/Afisha';

class App extends Component {
  render() {
    return (
      <BrowserRouter>
        <Switch>
          <Route path="/tickets/farewell" component={Farewell}/>
          <Route path="/tickets/turnstile" component={Turnstile}/>
          <Route path="" component={Afisha}/>
        </Switch>
      </BrowserRouter>
    );
  }
}

export default App;
