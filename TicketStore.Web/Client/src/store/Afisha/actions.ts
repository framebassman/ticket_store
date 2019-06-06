import axios from 'axios';
import { eventsUrl } from './urls';

export const fetchEventsType = 'FETCH_EVENTS';

async function transfersFromBack() {
  try {
    const eventsFromBack = await axios.get(eventsUrl);
    return { events: eventsFromBack };
  }
  catch {
    return { events: [] };
  }
}

export const actionCreators = {
  fetchEvents: () => async (dispatch: any) => {
    const response = await transfersFromBack();
    console.log('before fetchEventsType');
    dispatch({
      type: fetchEventsType,
      payload: response
    });
  }
};
