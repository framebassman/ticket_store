import axios from 'axios';
import { verifyUrl } from './urls/prod';
import { cooldown } from './timeouts'; 

export const waitingType = 'TURNSTILE_WAITING';
export const verifyType = 'TURNSTILE_VERIFY';
export const resetType = 'TURNSTILE_RESET';

async function transfersFromBack(barcode: string) {
  try {
    return await axios.post(
      verifyUrl,
      { code: barcode },
      { headers: { Authorization: 'Bearer pkR9vfZ9QdER53mf'}}
    );
  }
  catch (e) {
    return e.response;
  }
}

export const actionCreators = {
  verify: (barcode: string) => async (dispatch: any) => {
    dispatch({ type: waitingType });
  
    const response = await transfersFromBack(barcode);
    dispatch({
      type: verifyType,
      payload: response
    });
    setTimeout(() => {
      dispatch({ type: resetType })
    }, cooldown);
  }
};
