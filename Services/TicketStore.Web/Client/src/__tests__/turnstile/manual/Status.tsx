import React from 'react';
import { Provider } from 'react-redux';
import configureStore from '../../../store/configureStore';
import { mount, ReactWrapper } from 'enzyme';
import moxios from 'moxios';
import { verifyUrl } from '../../../store/Turnstile/urls/prod';
import TurnstileManual from '../../../components/turnstile/manual/TurnstileManual';

const initialState = (window as any).initialReduxState;
const store = configureStore(initialState);

describe('Status of <TurnstileManual />', () => {
  let turnstileManual: ReactWrapper;

  beforeEach(() => {
    moxios.install();
    turnstileManual = mount(
      <Provider store={store}>
        <TurnstileManual pass={false} wait={false} verify={false}/>
      </Provider>
    );

  });
  
  afterEach(() => {
    moxios.uninstall();
    turnstileManual.unmount();
  });
  
  it('Yellow status', () => {
    // Act
    const description = turnstileManual.find('#status-description');
    
    // Assert
    expect(description.text()).toEqual('Готов сканировать!');
  });
  
  it('Green status', done => {
    // Arrange
    const button = turnstileManual.find('#verify').hostNodes();
    moxios.stubRequest(verifyUrl, {
      status: 200,
      response: { message: 'OK'}
    });

    // Act
    button.simulate('submit');
    moxios.wait(() => {
      turnstileManual.update();
    
      // Assert
      const description = turnstileManual.find('#status-description');
      expect(description.text()).toEqual('Успешно!');
      done();
    }, 100);
  });
  
  it('Red status', done => {
    // Arrange
    const button = turnstileManual.find('#verify').hostNodes();
    moxios.stubRequest(verifyUrl, {
      status: 200,
      response: { message: 'cannot find code in database'}
    });
  
    // Act
    button.simulate('submit');
    moxios.wait(() => {
      turnstileManual.update();
  
      // Assert
      const description = turnstileManual.find('#status-description');
      expect(description.text()).toEqual('Ошибочка вышла!');
      done();
    }, 100);
  });
});
