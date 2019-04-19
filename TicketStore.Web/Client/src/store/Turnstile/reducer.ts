import { verifyType, cancelType } from './actions';
import { VerifyState } from './state';
import { TurnstileState } from '../../components/turnstile/TurnstileState';

const initialState: TurnstileState = {
    scanning: false,
    result: undefined,
    pass: false,
    wait: true,
    isRequested: false,
    myArray: [""]
};

export const reducer = (state: any, action: any): TurnstileState => {
    state = state || initialState;
    if (action.type == verifyType) {
        const message = action.payload.data;
        let result: boolean;
        let current;
        if (message == 'OK') {
            current = state.myArray.concat("OK")
            result = true;
        } else {
            current = state.myArray.concat("NOT")
            result = false;
        }

        return { ...state, pass: result, wait: false, myArray: current }
    }

    return state;
}