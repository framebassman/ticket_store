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

export const reducer = (state: TurnstileState = initialState, action: any): TurnstileState => {
    switch (action.type) {
        case verifyType : {
            console.log('before verifyType in reducer');
            const message = action.payload.data.message;

            console.log("message from backend: ", message);
            const pass = message === 'OK';

            return { ...state, pass, wait: true };
        }
        case resetType: {
            console.log('before resetType in reducer');
            return { ...state, wait: false };
        }
    }

    return state;
};
