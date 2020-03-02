import Farewell from './index.svelte';
import { render } from '@testing-library/svelte'

describe('Sample', () => {
    it('renders without failing', () => {
        const { container, getAllByText } = render(Farewell)

        expect(getAllByText('Чертополох').length).toBe(1)
        expect(getAllByText('Команда Kolenka Inc.').length).toBe(1)
        expect(container).toMatchSnapshot()
    });
});