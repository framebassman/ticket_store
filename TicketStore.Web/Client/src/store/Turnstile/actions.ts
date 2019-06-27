import axios from 'axios';
import { verifyUrl } from './urls/prod';

export const verifyType = 'VERIFY';
export const resetType = 'RESET';

async function transfersFromBack(barcode: string) {
  try {
    const response = await axios.post(
      verifyUrl,
      { code: barcode },
      { headers: { Authorization: 'Bearer pkR9vfZ9QdER53mf'}}
    );
    setTimeout(() => {}, 2000);
    return response;
  }
  catch {
    return { data: {} };
  }
}

export const actionCreators = {
  verify: (code: string) => async (dispatch: any) => {
    const response = await transfersFromBack(code);
    dispatch({
      type: verifyType,
      payload: response
    });
    setTimeout(() => {
      dispatch({
        type: resetType
      })
    }, 2000);
  }
};
