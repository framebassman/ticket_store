import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import configureStore from './store/configureStore';
import * as serviceWorker from './serviceWorker';

import './index.css';
import App from './App';
import { YandexMetrica } from './YandexMetrica';

const initialState = (window as any).initialReduxState;
const store = configureStore(initialState);

ReactDOM.render(
  <Provider store={store}>
    <App/>
    <YandexMetrica accounts={[53979967]} />
  </Provider>,
  document.getElementById('root')
);

if (module.hot) {
  module.hot.accept();
}

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: http://bit.ly/CRA-PWA
serviceWorker.unregister();
