import axios from 'axios';
import { verifyUrl } from './urls/prod';
import { cooldown } from './timeouts'; 
import { VerificationMethod } from './verificationMethods'; 

export const waitingType = 'TURNSTILE_WAITING';
export const verifyType = 'TURNSTILE_VERIFY';
export const resetType = 'TURNSTILE_RESET';

async function transfersFromBack(barcode: string, method: VerificationMethod) {
  try {
    const result = await axios.post(
      verifyUrl,
      { code: barcode, method },
      { headers: { Authorization: 'Bearer pkR9vfZ9QdER53mf'}}
    );
    return result;
  }
  catch (e) {
    console.log(e.response);
    return e.response;
  }
}

export type TurnstileActions = {
  verify: (barcode: string, method: VerificationMethod) => any,
};

export const actionCreators: TurnstileActions = {
  verify: (barcode: string, method: VerificationMethod) => async (dispatch: any) => {
    dispatch({ type: waitingType });
  
    const { data } = await transfersFromBack(barcode, method);

    dispatch({
      type: verifyType,
      payload: data
    });
    setTimeout(() => {
      dispatch({ type: resetType })
    }, cooldown);
  }
};
