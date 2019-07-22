import React from 'react';
import { mount } from 'enzyme';
import {EventTime} from "../../../components/core/time/EventTime";

describe('<EventTime />', () => {
   it('Convert source time to user locale', async () => {
       // Arrange
       const dateFromBackend = '2019-10-04T16:00:00Z';
       
       // Act
       const component = mount(<EventTime startedAt={new Date(dateFromBackend)}/>);
       
       // Assert
       expect(component.find('#when').hostNodes().text())
           .toEqual('4 октября 2019');
       expect(component.find('#start').hostNodes().text())
           .toEqual('Начало в 19:00 часов');
   });
});
