import React from 'react';
import { Provider } from 'react-redux';
import { mount, ReactWrapper } from 'enzyme';
import configureStore from '../../store/configureStore';
import Afisha from '../../components/afisha/Afisha';

const initialState = (window as any).initialReduxState;
const store = configureStore(initialState);

export let afisha: ReactWrapper;

beforeEach(() => {
  afisha = mount(
    <Provider store={store}>
      <Afisha />
    </Provider>
  );
});

it('empty', () => {})
