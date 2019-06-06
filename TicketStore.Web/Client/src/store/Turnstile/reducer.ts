import { verifyType, resetType } from './actions';
import { TurnstileState } from '../../components/turnstile/TurnstileState';
import { DetectedBarcode } from '../../components/turnstile/camera/DetectedBarcode';

const initialState: TurnstileState = {
    scanning: false,
    result: new DetectedBarcode(),
    pass: false,
    wait: false,
    isRequested: false
};

export const reducer = (state: any, action: any): TurnstileState => {
    state = state || initialState;
    switch(action.type) {
        case verifyType : {
            console.log('before verifyType in reducer')
            const message = action.payload.data;
            let result: boolean;
            console.log("message from backend: ", message);
            if (message === 'OK') {
                result = true;
            } else {
                result = false;
            }

            return { ...state, pass: result, wait: true };
        }
        case resetType: {
            console.log('before resetType in reducer')
            return { ...state, wait: false };
        }
    }

    return state;
}