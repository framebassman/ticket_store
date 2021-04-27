import {
    merchantsFetchDataSuccessType, merchantsHasErroredType, merchantsIsLoadingType
} from './types';
import { AnyAction, combineReducers } from 'redux';

function merchantsHasErrored(state = false, action: AnyAction) {
    switch (action.type) {
      case merchantsHasErroredType:
        return action.hasErrored;
  
      default:
        return state;
    }
  }
  
  function merchantsIsLoading(state = false, action: AnyAction) {
    switch (action.type) {
      case merchantsIsLoadingType:
        return action.isLoading;
  
      default:
        return state;
    }
  }
  
  function merchants(state = [], action: AnyAction) {
    switch (action.type) {
      case merchantsFetchDataSuccessType:
        return action.merchants;
  
      default:
        return state;
    }
  }
  
  export const reducer = combineReducers({
    merchants,
    merchantsHasErrored,
    merchantsIsLoading
  });
