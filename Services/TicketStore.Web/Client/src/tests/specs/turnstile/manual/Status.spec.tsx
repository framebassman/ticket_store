import React from 'react';
import { Provider } from 'react-redux';
import configureStore from '../../../../store/configureStore';
import { mount, ReactWrapper } from 'enzyme';
import moxios from 'moxios';
import { verifyUrl } from '../../../../store/Turnstile/urls/prod';
import TurnstileManual from '../../../../components/turnstile/manual/TurnstileManual';
import { SUBMIT } from '../../../model/enzyme/events';
import { cooldown } from '../../../../store/Turnstile/timeouts';

const initialState = (window as any).initialReduxState;
const store = configureStore(initialState);

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
    
    // Assert
    expect(description.text()).toEqual('Готов к проверке!');
  });
  
  it('should declare ticket valid if backend returns OK', done => {
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
    
      // Assert
      const description = turnstileManual.find('#status-description');
      expect(description.text()).toEqual('Билет Действителен');
      done();
    }, 100);
  });

  it('should declare ticket used if backend returns OK and used ticket ', done => {
    // Arrange
    const button = turnstileManual.find('#verify').hostNodes();
    moxios.stubRequest(verifyUrl, {
      status: 200,
      response: { message: 'OK', used: true }
    });

    // Act
    button.simulate(SUBMIT);
    moxios.wait(() => {
      turnstileManual.update();
    
      // Assert
      const description = turnstileManual.find('#status-description');
      expect(description.text()).toEqual('Билет Использован');
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
      expect(description.text()).toEqual('Билет не найден');
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
      expect(description.text()).toEqual('Билет Действителен');
    }, 100);

      // Assert
      button.simulate(SUBMIT);
      moxios.wait(() => {
        turnstileManual.update();
  
        const description = turnstileManual.find('#status-description');
        expect(description.text()).toEqual('Готов к проверке!');
        done();
      }, cooldown);
  });
});
