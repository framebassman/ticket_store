import App from './index.svelte'
import { render } from '@testing-library/svelte'

describe('App', () => {
    it('should render the app', () => {
        const { container, getAllByText } = render(App);

        expect(container.getElementsByClassName('farewell').length).toBe(1);
        expect(container.getElementsByClassName('title').length).toBe(1);
        expect(container.getElementsByClassName('image-content').length).toBe(1);
        expect(container.getElementsByClassName('smile-image').length).toBe(1);
        expect(container.getElementsByClassName('description').length).toBe(1);
        expect(container.getElementsByClassName('team').length).toBe(1);

        expect(getAllByText('Чертополох').length).toBe(1);
        expect(getAllByText('Команда Kolenka Inc.').length).toBe(1);

        expect(container).toMatchSnapshot();
    })
})
