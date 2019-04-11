import axios from 'axios';
import { verifyUrl } from './urls';

export const verifyType = 'VERIFY';

async function transfersFromBack(code: string) {
  try {
    return await axios.post(verifyUrl, code); 
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
    })
  }
};
