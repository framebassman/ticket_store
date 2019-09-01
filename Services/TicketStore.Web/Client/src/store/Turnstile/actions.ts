import axios from 'axios';
import { verifyUrl } from './urls/prod';

export const waitingType = 'TURNSTILE_WAITING';
export const verifyType = 'TURNSTILE_VERIFY';
export const resetType = 'TURNSTILE_RESET';

async function transfersFromBack(barcode: string) {
  try {
    const result = await axios.post(
      verifyUrl,
      { code: barcode },
      { headers: { Authorization: 'Bearer pkR9vfZ9QdER53mf'}}
    );

    console.log(result);
    return result;
  }
  catch (e) {
    console.log(e.response);
    return e.response;
  }
}

export const actionCreators = {
  verify: (barcode: string) => async (dispatch: any) => {
    dispatch({ type: waitingType });
  
    const { data } = await transfersFromBack(barcode);

    dispatch({
      type: verifyType,
      payload: data
    });
    setTimeout(() => {
      dispatch({ type: resetType })
    }, 2000);
  }
};

