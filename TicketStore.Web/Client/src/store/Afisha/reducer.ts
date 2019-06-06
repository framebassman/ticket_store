import { fetchEventsType } from './actions';

const initialState = [];

export const reducer = (state: any, action: any) => {
    state = state || initialState;
    switch(action.type) {
        case fetchEventsType : {
            console.log('before fetchEventsType in reducer');
            const events = action.payload.data;
            console.log('after fetchEventsType in reducer');
            return events;
        }
    }

    return state;
}