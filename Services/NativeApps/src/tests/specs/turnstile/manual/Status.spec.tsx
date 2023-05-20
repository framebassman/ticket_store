import React from 'react';
import { Provider } from 'react-redux';
import { mount, ReactWrapper } from 'enzyme';
import moxios from 'moxios';
import configureStore from '../../../../store/configureStore';
import { verifyUrl } from '../../../../store/Turnstile/urls/prod';
import TurnstileManual from '../../../../components/turnstile/manual/TurnstileManual';
import { SUBMIT } from '../../../model/enzyme/events';
import { cooldown } from '../../../../store/Turnstile/timeouts';

const store = configureStore();

describe('Status of <TurnstileManual />', () => {
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
  
  it('should be in ready state by default', () => {
    // Act
    const description = turnstileManual.find('#status-description');
    const info = turnstileManual.find('#ticket-info');
    
    // Assert
    expect(description.text()).toEqual('Ready');
    expect(info.text()).toEqual(`Event: `);
  });
  
  it('should declare ticket valid if backend returns OK and ticket not used', done => {
    // Arrange
    const concertLabel = 'test';
    const button = turnstileManual.find('#verify').hostNodes();
    moxios.stubRequest(verifyUrl, {
      status: 200,
      response: {
        message: 'OK',
        used: false,
        concertLabel: concertLabel
      }
    });

    // Act
    button.simulate(SUBMIT);
    moxios.wait(() => {
      turnstileManual.update();
    
      // Assert
      const description = turnstileManual.find('#status-description');
      expect(description.text()).toEqual('The ticket is valid');
      const info = turnstileManual.find('#ticket-info');
      expect(info.text()).toEqual(`Event: ${concertLabel}`);
      done();
    }, 100);
  });

  it('should declare ticket used if backend returns OK and used ticket', done => {
    // Arrange
    const concertLabel = 'test';
    const button = turnstileManual.find('#verify').hostNodes();
    moxios.stubRequest(verifyUrl, {
      status: 200,
      response: {
        message: 'OK',
        used: true,
        concertLabel: concertLabel
      }
    });

    // Act
    button.simulate(SUBMIT);
    moxios.wait(() => {
      turnstileManual.update();
    
      // Assert
      const description = turnstileManual.find('#status-description');
      expect(description.text()).toEqual('The ticket is used');
      const info = turnstileManual.find('#ticket-info');
      expect(info.text()).toEqual(`Event: ${concertLabel}`);
      done();
    }, 100);
  });
  
  it('should declare ticket not found if backend returns error', done => {
    // Arrange
    const button = turnstileManual.find('#verify').hostNodes();
    moxios.stubRequest(verifyUrl, {
      status: 200,
      response: { message: 'cannot find code in database'}
    });
  
    // Act
    button.simulate(SUBMIT);
    moxios.wait(() => {
      turnstileManual.update();
  
      // Assert
      const description = turnstileManual.find('#status-description');
      expect(description.text()).toEqual('Cannot find the ticket');
      const info = turnstileManual.find('#ticket-info');
      expect(info.text()).toEqual(`Event: `);
      done();
    }, 100);
  });

  it(`should stay ready state after cooldown (${cooldown} ms)`, done => {
    // Arrange
    const button = turnstileManual.find('#verify').hostNodes();
    moxios.stubRequest(verifyUrl, {
      status: 200,
      response: { message: 'OK'}
    });

    // Act
    button.simulate(SUBMIT);
    moxios.wait(() => {
      turnstileManual.update();

      const description = turnstileManual.find('#status-description');
      expect(description.text()).toEqual('The ticket is valid');
    }, 100);

      // Assert
      button.simulate(SUBMIT);
      moxios.wait(() => {
        turnstileManual.update();
  
        const description = turnstileManual.find('#status-description');
        expect(description.text()).toEqual('Ready');
        done();
      }, cooldown);
  });
});
