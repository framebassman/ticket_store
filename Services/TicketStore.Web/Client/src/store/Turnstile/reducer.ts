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
    const { type, payload } = action;

    switch (type) {
        case verifyType : {
            console.log('before verifyType in reducer');
            const { message } = payload;

            console.log("message from backend: ", message);
            const pass = message === 'OK';

            if (pass) {
                const { concertLabel, used } = payload;
                return {
                    ...state,
                    pass,
                    scannedTicket: { concertLabel, used },
                    wait: true
                };
            } else {
                return { ...state, pass, scannedTicket: undefined, wait: true };
            }
        }
        case resetType: {
            console.log('before resetType in reducer');
            return { ...state, scannedTicket: undefined, wait: false };
        }
    }

    return state;
}