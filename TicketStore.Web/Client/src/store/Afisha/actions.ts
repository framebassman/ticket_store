import axios, { AxiosResponse } from 'axios';
import { eventsUrl } from './urls';

export const fetchEventsType = 'FETCH_EVENTS';
export const eventsHasErroredType = 'EVENTS_HAS_ERRORED';

async function transfersFromBack(): Promise<AxiosResponse<any>> {
  return await axios.get(eventsUrl);
}

export const actionCreators = {
  fetchEvents: () => async (dispatch: any) => {
    const response = await transfersFromBack();
    if (response.status === 200) {
      dispatch({
        type: fetchEventsType,
        payload: response.data
      });
    } else {
      dispatch({
        type: eventsHasErroredType
      })
    }
  }
};
