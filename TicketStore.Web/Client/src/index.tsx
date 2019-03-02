import React from 'react';
import ReactDOM from 'react-dom';

import './index.css';
import App from './App';
import { YandexMetrica } from './YandexMetrica';
import * as serviceWorker from './serviceWorker';

ReactDOM.render(
  <div>
    <App/>
    <YandexMetrica accounts={[52190806]} />
  </div>,
  document.getElementById('root')
);

if (module.hot) {
  module.hot.accept();
}

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: http://bit.ly/CRA-PWA
serviceWorker.unregister();
