import React, { Component } from 'react';
import { BrowserRouter, Route, Switch } from 'react-router-dom';

import TurnstileMenu from './components/turnstile/TurnstileMenu';
import { FirstLoaderRemover } from './components/core/FirstLoaderRemover/FirstLoaderRemover';

class App extends Component {
  render() {
    return (
      <BrowserRouter>
        <FirstLoaderRemover />
        <Switch>
          <Route path="" component={TurnstileMenu} />
        </Switch>
      </BrowserRouter>
    );
  }
}

export default App;
