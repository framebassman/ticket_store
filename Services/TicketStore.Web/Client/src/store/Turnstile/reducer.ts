import { verifyType, resetType } from './actions';
import { TurnstileState } from '../../components/turnstile/TurnstileState';

const initialState: TurnstileState = {
    ticketFound: false,
    wait: false,
};

export const reducer = (state: TurnstileState = initialState, action: any): TurnstileState => {
    const { type, payload } = action;

    switch (type) {
        case verifyType : {
            console.log('before verifyType in reducer');
            const { message } = payload;

            console.log("message from backend: ", message);
            const ticketFound = message === 'OK';

            if (ticketFound) {
                const { concertLabel, used } = payload;
                return {
                    ...state,
                    ticketFound,
                    scannedTicket: { concertLabel, used },
                    wait: true
                };
            } else {
                return { ...state, ticketFound, scannedTicket: undefined, wait: true };
            }
        }
        case resetType: {
            console.log('before resetType in reducer');
            return { ...state, scannedTicket: undefined, wait: false };
        }
    }

    return state;
}