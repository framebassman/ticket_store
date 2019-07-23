import {
  eventsFetchDataSuccessType, eventsHasErroredType, eventsIsLoadingType
} from './types';
import { combineReducers } from 'redux';

function eventsHasErrored(state = false, action) {
  switch (action.type) {
    case eventsHasErroredType:
      return action.hasErrored;

    default:
      return state;
  }
}

function eventsIsLoading(state = false, action) {
  switch (action.type) {
    case eventsIsLoadingType:
      return action.isLoading;

    default:
      return state;
  }
}

function events(state = [], action) {
  switch (action.type) {
    case eventsFetchDataSuccessType:
      return action.events;

    default:
      return state;
  }
}

export const reducer = combineReducers({
  events,
  eventsHasErrored,
  eventsIsLoading
});
