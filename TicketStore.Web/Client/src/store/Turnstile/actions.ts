import axios from 'axios';
const url = require(`./urls/${env()}`);

function env(): string {
  if (process.env.ASPNETCORE_ENVIRONMENT === 'production' ||
      process.env.NODE_ENV === 'production') {
    return 'prod';
  } else {
    return 'dev';
  }
}

export const verifyType = 'VERIFY';
export const resetType = 'RESET';

async function transfersFromBack(barcode: string) {
  try {
    return await axios.post(url.verifyUrl, {code: barcode}); 
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
