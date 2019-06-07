import { eventsFetchDataSuccessType, eventsHasErroredType, eventsIsLoadingType } from './actions';
import { AfishaState } from './state';
import { combineReducers } from 'redux';

const initialState: AfishaState = {
    isLoading: false,
    hasErrored: false,
    items: []
};

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
            console.log('isLoading action', action);
            return action.isLoading;

        default:
            return state;
    }
}

function events(state = [], action) {
    switch (action.type) {
        case eventsFetchDataSuccessType:
            console.log('items action', action);
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
