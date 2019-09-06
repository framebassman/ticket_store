import React from 'react';
import { Provider } from 'react-redux';
import { mount, ReactWrapper } from 'enzyme';
import moxios from 'moxios';
import configureStore from '../../../../store/configureStore';
import { verifyUrl } from '../../../../store/Turnstile/urls/prod';
import TurnstileManual from '../../../../components/turnstile/manual/TurnstileManual';
import { SUBMIT } from '../../../model/enzyme/events';

const store = configureStore();

describe('<TurnstileManual />', () => {
  let turnstileManual: ReactWrapper;

  beforeEach(() => {
    moxios.install();
    turnstileManual = mount(
      <Provider store={store}>
        <TurnstileManual />
      </Provider>
    );
  });
  
  afterEach(() => {
    moxios.uninstall();
    turnstileManual.unmount();
  });
  
  it('should send "Manual" verification method', done => {
    // Arrange
    const button = turnstileManual.find('#verify').hostNodes();
    moxios.stubRequest(verifyUrl, {
      status: 200,
      response: { message: 'OK' }
    });

    // Act
    button.simulate(SUBMIT);

    // Assert
    moxios.wait(() => {
      const request = moxios.requests.mostRecent();
      const sentData = JSON.parse(request.config.data);
      expect(sentData).toEqual({
        code: '',
        method: 'Manual'
      });
      done();
    }, 10);
  });
});
