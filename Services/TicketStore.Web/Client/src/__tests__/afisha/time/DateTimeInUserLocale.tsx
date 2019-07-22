import React from 'react';
import { mount, ReactWrapper } from 'enzyme';
import {EventTime} from "../../../components/core/time/EventTime";

function setTimezoneToMsk(): void {
    Date.prototype.getTimezoneOffset = function() {
        return -180;
    }
}

function restoreTimezone(current): void {
    Date.prototype.getTimezoneOffset = current;
}

describe('<EventTime />', () => {
    describe('User locale', () => {
        let wrapper: ReactWrapper;
        const dateFromBackend = '2019-10-04T16:00:00Z';
        const currentTimezone = Date.prototype.getTimezoneOffset;

        beforeEach(() => {
            wrapper = mount(<EventTime startedAt={new Date(dateFromBackend)}/>);
            setTimezoneToMsk();
        });

        afterEach(() => {
            wrapper.unmount();
            restoreTimezone(currentTimezone);
        });

        it('Convert source time to user locale', () => {
            // Assert
            expect(wrapper.find('#when').hostNodes().text())
                .toEqual('4 октября 2019');
            expect(wrapper.find('#start').hostNodes().text())
                .toEqual('Начало в 19:00 часов');
        });
    });
});
