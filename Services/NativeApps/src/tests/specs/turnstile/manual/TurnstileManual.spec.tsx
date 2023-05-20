import React from 'react';
import { Provider } from 'react-redux';
import { mount, ReactWrapper } from 'enzyme';
import moxios from 'moxios';
import configureStore from '../../../../store/configureStore';
import { verifyUrl } from '../../../../store/Turnstile/urls/prod';
import TurnstileManual from '../../../../components/turnstile/manual/TurnstileManual';
import { SUBMIT, CHANGE } from '../../../model/enzyme/events';

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
  
  it('should send "Manual" verification method and barcode', done => {
    // Arrange
    const barcode = '1234';
    const textArea = turnstileManual.find('#ticket_number').hostNodes();
    const button = turnstileManual.find('#verify').hostNodes();
    moxios.stubRequest(verifyUrl, {
      status: 200,
      response: { message: 'OK' }
    });

    // Act
    textArea.instance().value = barcode;
    textArea.simulate(CHANGE);
    button.simulate(SUBMIT);

    // Assert
    moxios.wait(() => {
      const request = moxios.requests.mostRecent();
      const sentData = JSON.parse(request.config.data);
      expect(sentData).toEqual({
        code: barcode,
        method: 'Manual'
      });
      done();
    }, 10);
  });
});
