import axios from 'axios';
import { merchantsUrl } from '../urls';
import { merchantsFetchDataSuccessType, merchantsHasErroredType, merchantsIsLoadingType } from './types';

export function merchantsFetchData() {
  return (dispatch) => {
    dispatch(merchantsIsLoading(true));

    axios.get(merchantsUrl)
      .then((response) => {
        if (response.status !== 200) {
          throw Error(response.statusText);
        }

        dispatch(merchantsIsLoading(false));

        return response;
      })
      .then((response) => response.data)
      .then((items) => dispatch(merchantsFetchDataSuccess(items)))
      .catch(() => dispatch(merchantsHasErrored(true)));
  };
}

function merchantsHasErrored(bool: boolean) {
  return {
    type: merchantsHasErroredType,
    hasErrored: bool
  };
}

function merchantsIsLoading(bool: boolean) {
  return {
    type: merchantsIsLoadingType,
    isLoading: bool
  };
}

function merchantsFetchDataSuccess(merchants) {
  return {
    type: merchantsFetchDataSuccessType,
    merchants
  };
}
