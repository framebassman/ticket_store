import { applyMiddleware, combineReducers, compose, createStore } from 'redux';
import thunk from 'redux-thunk';
import { reducer as turnstileReducer } from './Turnstile/reducer';
import { composeWithDevTools } from 'redux-devtools-extension';

export default function configureStore (initialState: any) {
  const reducers = {
    turnstile: turnstileReducer,
  };

  const middleware = [
    thunk
  ];

  // In development, use the browser's Redux dev tools extension if installed
  const enhancers = [];
  const isDevelopment = process.env.NODE_ENV === 'development';

  const rootReducer = combineReducers({
    ...reducers
  });

  let composed: any = null;

  if (isDevelopment) {
    composed = composeWithDevTools(applyMiddleware(...middleware), ...enhancers);
  }
  else {
    composed = compose(applyMiddleware(...middleware), ...enhancers);
  }

  return createStore(
    rootReducer,
    initialState,
    composed
  );
}
