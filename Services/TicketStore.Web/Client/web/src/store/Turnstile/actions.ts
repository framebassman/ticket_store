import { verifyUrl } from './urls/prod';
import { cooldown } from './timeouts';
import { VerificationMethod } from './verificationMethods';

export const waitingType = 'TURNSTILE_WAITING';
export const verifyType = 'TURNSTILE_VERIFY';
export const resetType = 'TURNSTILE_RESET';

async function transfersFromBack(barcode: string, method1: VerificationMethod) {
  try {
    const request = new Request(verifyUrl, {
      method: 'post',
      body: JSON.stringify(
        {
          code: barcode,
          method1
        }
      ),
      headers: { Authorization: 'Bearer pkR9vfZ9QdER53mf'}
    });
    const result = await fetch(request);
    return result.body;
  }
  catch (e) {
    console.log(e);
    return e;
  }
}

export type TurnstileActions = {
  verify: (barcode: string, method: VerificationMethod) => any,
};

export const actionCreators: TurnstileActions = {
  verify: (barcode: string, method: VerificationMethod) => async (dispatch: any) => {
    dispatch({ type: waitingType });

    const data = await transfersFromBack(barcode, method);

    dispatch({
      type: verifyType,
      payload: data
    });
    setTimeout(() => {
      dispatch({ type: resetType })
    }, cooldown);
  }
};
