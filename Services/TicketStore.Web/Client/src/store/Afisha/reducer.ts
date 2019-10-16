import { combineReducers } from 'redux';
import { reducer as merchants } from './merchants/reducer';
import { reducer as events } from './events/reducer';

export const reducer = combineReducers({
  merchants,
  events
});
