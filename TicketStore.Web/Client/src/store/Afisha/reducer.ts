import { itemsFetchDataSuccessType, itemsHasErroredType, itemsIsLoadingType } from './actions';
import { AfishaState } from './state';
import { combineReducers } from 'redux';

const initialState: AfishaState = {
    isLoading: false,
    hasErrored: false,
    items: []
};

export function itemsHasErrored(state = false, action) {
    switch (action.type) {
        case itemsHasErroredType:
            return action.hasErrored;

        default:
            return state;
    }
}

export function itemsIsLoading(state = false, action) {
    switch (action.type) {
        case itemsIsLoadingType:
            console.log('isLoading action', action);
            return action.isLoading;

        default:
            return state;
    }
}

export function items(state = [], action) {
    switch (action.type) {
        case itemsFetchDataSuccessType:
            console.log('items action', action);
            return action.items;

        default:
            return state;
    }
}

export const reducer = combineReducers({
    items,
    itemsHasErrored,
    itemsIsLoading
});
