import {
    merchantsFetchDataSuccessType, merchantsHasErroredType, merchantsIsLoadingType
} from './types';
import { combineReducers } from 'redux';

function merchantsHasErrored(state = false, action) {
    switch (action.type) {
      case merchantsHasErroredType:
        return action.hasErrored;
  
      default:
        return state;
    }
  }
  
  function merchantsIsLoading(state = false, action) {
    switch (action.type) {
      case merchantsIsLoadingType:
        return action.isLoading;
  
      default:
        return state;
    }
  }
  
  function merchants(state = [], action) {
    switch (action.type) {
      case merchantsFetchDataSuccessType:
        return action.events;
  
      default:
        return state;
    }
  }
  
  export const reducer = combineReducers({
    merchants,
    merchantsHasErrored,
    merchantsIsLoading
  });
