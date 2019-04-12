import axios from 'axios';
const url = require(`./urls/${env()}`)

function env(): string {
  if (process.env.ASPNETCORE_ENVIRONMENT === 'production'
      || process.env.NODE_ENV === 'production') {
    return 'prod';
  } else {
    return 'dev';
  }
}

export const verifyType = 'VERIFY';

async function transfersFromBack(code: string) {
  try {
    return await axios.post(url.verifyUrl, code); 
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
