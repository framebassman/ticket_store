import { verifyType, resetType } from './actions';
import { TurnstileState } from '../../components/turnstile/TurnstileState';

const initialState: TurnstileState = {
    isTicketFound: false,
    isTicketScanned: false,
};

export const reducer = (state: TurnstileState = initialState, action: any): TurnstileState => {
    const { type, payload } = action;

    switch (type) {
        case verifyType : {
            console.log('before verifyType in reducer');
            const { message } = payload;

            console.log("message from backend: ", message);
            const isTicketFound = message === 'OK';

            if (isTicketFound) {
                const { concertLabel, used } = payload;
                return {
                    ...state,
                    isTicketScanned: true,
                    isTicketFound: true,
                    scannedTicket: { concertLabel, used },
                };
            } else {
                return {
                    ...state,
                    isTicketScanned: true,
                    isTicketFound: false,
                    scannedTicket: undefined,
                };
            }
        }
        case resetType: {
            console.log('before resetType in reducer');
            return {
                ...state,
                isTicketScanned: false,
                isTicketFound: false,
                scannedTicket: undefined,
            };
        }
    }

    return state;
}