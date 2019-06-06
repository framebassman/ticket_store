import { fetchEventsType, eventsHasErroredType } from './actions';
import { AfishaState } from './state';

const initialState: AfishaState = {
    isLoading: true,
    hasErrored: false,
    events: []
};

export const reducer = (state: any, action: any): AfishaState => {
    state = state || initialState;
    switch(action.type) {
        case fetchEventsType : {
            console.log('before fetchEventsType in reducer');
            const newEvents = [];
            newEvents.concat(action.payload);
            console.log('after fetchEventsType in reducer');
            console.log('fetched events: ', newEvents);
            return { ...state, events: newEvents, isLoading: false };
        }
        case eventsHasErroredType : {
            return { ...state, hasErrored: true };
        }
    }

    return state;
}