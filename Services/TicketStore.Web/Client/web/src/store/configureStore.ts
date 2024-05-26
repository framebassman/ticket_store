// import { createStore, applyMiddleware, combineReducers, compose, configureStore } from 'redux'
// import thunk from 'redux-thunk'
// import { composeWithDevTools } from 'redux-devtools-extension';
// import { reducer as turnstileReducer } from './Turnstile/reducer';
// import { reducer as afishaReducer } from './Afisha/reducer';

// export default function configureStore (initialState: any = undefined) {
//   const reducers = {
//     turnstile: turnstileReducer,
//     // afisha: afishaReducer,
//   };

//   const middleware = [
//     thunk
//   ];

//   // In development, use the browser's Redux dev tools extension if installed
//   const enhancers: any[] = [];
//   const isDevelopment = process.env.NODE_ENV === 'development';

//   const rootReducer = combineReducers({
//     ...reducers
//   });

//   let composed: any = null;

//   if (isDevelopment) {
//     composed = composeWithDevTools(applyMiddleware(...middleware), ...enhancers);
//   }
//   else {
//     composed = compose(applyMiddleware(...middleware), ...enhancers);
//   }

//   return configureStore(
//     rootReducer,
//     initialState,
//     composed
//   );
// }

// import { applyMiddleware, createStore, combineReducers } from 'redux'
// import { configureStore } from '@reduxjs/toolkit'
// import thunkMiddleware from 'redux-thunk'
// import { composeWithDevTools } from 'redux-devtools-extension'

// // import monitorReducersEnhancer from './enhancers/monitorReducers'
// import loggerMiddleware from './logger'
// // import rootReducer from './reducers'
// import { reducer as turnstileReducer } from './Turnstile/reducer';
// // import { reducer as afishaReducer } from './Afisha/reducer';

// export default function configureStore(preloadedState: any) {
//   const middlewares = [thunkMiddleware, thunkMiddleware]
//   const middlewareEnhancer = applyMiddleware(...middlewares)

//   const enhancers = [middlewareEnhancer]
//   const composedEnhancers = composeWithDevTools(...enhancers)

//   const reducers = {
//     turnstile: turnstileReducer,
//   }
//   const rootReducer = combineReducers(reducers)
//   const store = createStore(rootReducer, preloadedState, composedEnhancers)

//   return store
// }

import { configureStore } from '@reduxjs/toolkit'
import { reducer as turnstileReducer } from './Turnstile/reducer';

export const store = configureStore({
  reducer: {
    turnstile: turnstileReducer,
  }
})

// Get the type of our store variable
export type AppStore = typeof store
// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<AppStore['getState']>
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = AppStore['dispatch']
