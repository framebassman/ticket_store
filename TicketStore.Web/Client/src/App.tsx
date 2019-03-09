import React, { Component } from 'react';
import { BrowserRouter, Route, Switch} from 'react-router-dom';
import { Greetings } from './components/Greetings';
import { Farewell } from './components/Farewell';

class App extends Component {
  render() {
    return (
      <BrowserRouter>
        <Switch>
          <Route path="/tickets/farewell" component={Farewell}/>
          <Route path="" component={Greetings}/>
        </Switch>
      </BrowserRouter>
    );
  }
}

export default App;
