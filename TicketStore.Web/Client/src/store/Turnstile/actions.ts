import axios from 'axios';
import { verifyUrl } from './urls/prod';

export const verifyType = 'VERIFY';
export const resetType = 'RESET';

const authHeader = {
  Authorization: "Bearer pkR9vfZ9QdER53mf"
}

async function transfersFromBack(barcode: string) {
  const params = {
    code: barcode
  };
  const headers = authHeader;
  try {
    return await axios.post(verifyUrl, {params, headers}); 
  }
  catch {
    return { data: {} };
  }
}

export const actionCreators = {
  verify: (code: string) => async (dispatch: any) => {
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
