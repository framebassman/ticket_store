import axios from 'axios';
import { eventsUrl } from './urls';

export const itemsFetchDataSuccessType = 'ITEMS_FETCH_DATA_SUCCESS';
export const itemsHasErroredType = 'EVENTS_HAS_ERRORED';
export const itemsIsLoadingType = 'ITEMS_IS_LOADING';

export function itemsFetchData() {
  return (dispatch) => {
      dispatch(itemsIsLoading(true));

      axios.get(eventsUrl)
          .then((response) => {
              if (response.status !== 200) {
                throw Error(response.statusText);
              }

              dispatch(itemsIsLoading(false));

              return response;
          })
          .then((response) => response.data)
          .then((items) => dispatch(itemsFetchDataSuccess(items)))
          .catch(() => dispatch(itemsHasErrored(true)));
  };
}

export function errorAfterFiveSeconds() {
  // We return a function instead of an action object
  return (dispatch) => {
      setTimeout(() => {
          // This function is able to dispatch other action creators
          dispatch(itemsHasErrored(true));
      }, 5000);
  };
}

export function itemsHasErrored(bool: boolean) {
  return {
      type: itemsHasErroredType,
      hasErrored: bool
  };
}

export function itemsIsLoading(bool: boolean) {
  return {
      type: itemsIsLoadingType,
      isLoading: bool
  };
}

export function itemsFetchDataSuccess(items) {
  return {
      type: itemsFetchDataSuccessType,
      items
  };
}
