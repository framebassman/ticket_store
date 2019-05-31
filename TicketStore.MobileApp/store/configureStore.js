import { applyMiddleware, combineReducers, compose, createStore } from 'redux';
import thunk from 'redux-thunk';
import { reducer as turnstileReducer } from './turnstile/reducer';

export default function configureStore (initialState) {
  const reducers = {
    turnstile: turnstileReducer,
  };

  const middleware = [
    thunk
  ];

  const enhancers = [];

  const rootReducer = combineReducers({
    ...reducers
  });

  return createStore(
    rootReducer,
    initialState,
    compose(applyMiddleware(...middleware), ...enhancers)
  );
}
