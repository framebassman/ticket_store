import { applyMiddleware, combineReducers, compose, createStore } from 'redux';
import thunk from 'redux-thunk';
import { composeWithDevTools } from 'redux-devtools-extension';
import { reducer as turnstileReducer } from './Turnstile/reducer';

export default function configureStore (initialState: any = undefined) {
  const reducers = {
    turnstile: turnstileReducer
  };

  const middleware = [
    thunk
  ];

  // In development, use the browser's Redux dev tools extension if installed
  const enhancers: any[] = [];
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
