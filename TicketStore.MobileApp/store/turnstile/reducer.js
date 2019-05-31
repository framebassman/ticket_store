import { verifyType, resetType } from './actions';
import { DetectedBarcode } from '../../components/Turnstile/camera/DetectedBarcode';

const initialState = {
    scanning: false,
    result: new DetectedBarcode(),
    pass: false,
    wait: false,
    isRequested: false
};

export const reducer = (state = initialState, action) => {
    switch(action.type) {
        case verifyType : {
            console.log('before verifyType in reducer')
            const message = action.payload.data;
            console.log("message from backend: ", message);

            let result;
            if (message == 'OK') {
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
