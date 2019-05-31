import axios from 'axios';
import { verifyUrl } from './urls/prod';

export const verifyType = 'VERIFY';
export const resetType = 'RESET';

async function transfersFromBack(barcode) {
  try {
    return await axios.post(verifyUrl, { code: barcode }); 
  }
  catch {
    return { data: {} };
  }
}

export const actionCreators = {
  verify: (code) => async (dispatch) => {
    const response = await transfersFromBack(code);

    console.log('before verifyType')
    dispatch({
      type: verifyType,
      payload: response
    });

    setTimeout(() => {
      console.log('before resetType')
      dispatch({
        type: resetType
      })
    }, 2000);
  }
};
