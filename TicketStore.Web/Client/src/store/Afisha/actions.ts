import axios from 'axios';
import { eventsUrl } from './urls';

export const eventsFetchDataSuccessType = 'EVENTS_FETCH_DATA_SUCCESS';
export const eventsHasErroredType = 'EVENTS_HAS_ERRORED';
export const eventsIsLoadingType = 'EVENTS_IS_LOADING';

export function eventsFetchData() {
  return (dispatch) => {
    dispatch(eventsIsLoading(true));

    axios.get(eventsUrl)
      .then((response) => {
        if (response.status !== 200) {
          throw Error(response.statusText);
        }

        dispatch(eventsIsLoading(false));

        return response;
      })
      .then((response) => response.data)
      .then((items) => dispatch(eventsFetchDataSuccess(items)))
      .catch(() => dispatch(eventsHasErrored(true)));
  };
}

export function errorAfterFiveSeconds() {
  // We return a function instead of an action object
  return (dispatch) => {
    setTimeout(() => {
      // This function is able to dispatch other action creators
      dispatch(eventsHasErrored(true));
    }, 5000);
  };
}

function eventsHasErrored(bool: boolean) {
  return {
    type: eventsHasErroredType,
    hasErrored: bool
  };
}

function eventsIsLoading(bool: boolean) {
  return {
    type: eventsIsLoadingType,
    isLoading: bool
  };
}

function eventsFetchDataSuccess(events) {
  return {
    type: eventsFetchDataSuccessType,
    events
  };
}
