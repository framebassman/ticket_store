import React from 'react';
import { mount, ReactWrapper } from 'enzyme';
import {EventTime} from "../../../../components/core/time/EventTime";

function setTimezoneToMinsk(): void {
    Date.prototype.getTimezoneOffset = function() {
        return -120;
    }
}

function restoreTimezone(current): void {
    Date.prototype.getTimezoneOffset = current;
}

describe('<EventTime />', () => {
    describe('Date in UTC', () => {
        let wrapper: ReactWrapper;
        const dateFromBackend = '2019-10-04T16:00:00Z';
        const currentTimezone = Date.prototype.getTimezoneOffset;

        beforeEach(() => {
            setTimezoneToMinsk();
            wrapper = mount(<EventTime origin={dateFromBackend}/>);
        });

        afterEach(() => {
            wrapper.unmount();
            restoreTimezone(currentTimezone);
        });

        it('Convert source time to user locale', () => {
            // Assert
            expect(wrapper.find('#when').hostNodes().text())
                .toEqual('4 Oct 2019');
            expect(wrapper.find('#start').hostNodes().text())
                .toEqual('Doors Open: 18:00');
        });
    });

    describe('Date without timezone', () => {
        let wrapper: ReactWrapper;
        const dateFromBackend = '2019-10-04T16:00:00';
        const currentTimezone = Date.prototype.getTimezoneOffset;

        beforeEach(() => {
            setTimezoneToMinsk();
            wrapper = mount(<EventTime origin={dateFromBackend}/>);
        });

        afterEach(() => {
            wrapper.unmount();
            restoreTimezone(currentTimezone);
        });

        it('Convert source time to user locale', () => {
            // Assert
            expect(wrapper.find('#when').hostNodes().text())
                .toEqual('4 Oct 2019');
            expect(wrapper.find('#start').hostNodes().text())
                .toEqual('Doors Open: 18:00');
        });
    });
});
