import axios from 'axios';
import { eventsUrl } from './urls';
import { eventsFetchDataSuccessType, eventsHasErroredType, eventsIsLoadingType } from './types';

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
