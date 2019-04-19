import React, { Component } from 'react';
import { BrowserRouter, Route, Switch} from 'react-router-dom';
import { Greetings } from './components/greetings/Greetings';
import { Farewell } from './components/farewell/Farewell';
import { Turnstile } from './components/turnstile/Turnstile';
import ManualTurnstile from './components/turnstile/manual/ManualTurnstile';

class App extends Component {
  render() {
    return (
      <BrowserRouter>
        <Switch>
          <Route path="/tickets/farewell" component={Farewell}/>
          <Route path="/tickets/turnstile" component={ManualTurnstile}/>
          <Route path="" component={Greetings}/>
        </Switch>
      </BrowserRouter>
    );
  }
}

export default App;
