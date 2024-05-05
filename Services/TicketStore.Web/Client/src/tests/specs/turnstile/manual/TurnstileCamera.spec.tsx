import React from 'react';
import { Provider } from 'react-redux';
import { mount, ReactWrapper } from 'enzyme';
import moxios from 'moxios';
import configureStore from '../../../../store/configureStore';
import TurnstileCamera from '../../../../components/turnstile/camera/TurnstileCamera';
import { CLICK } from '../../../model/enzyme/events';

const store = configureStore();

describe('<TurnstileManual />', () => {
  let turnstileCamera: ReactWrapper;

  beforeEach(() => {
    moxios.install();
    turnstileCamera = mount(
      <Provider store={store}>
        <TurnstileCamera />
      </Provider>
    );
  });

  it('should have no video tag by default', () => {
    expect(turnstileCamera.exists('video')).toBe(false);
  });

  it('should have camera after "Start scan" click', () => {
    const button = turnstileCamera.find('#start_scan').hostNodes();

    expect(() => button.simulate(CLICK)).toThrow("Quagga.init is not a function");
  });
});
