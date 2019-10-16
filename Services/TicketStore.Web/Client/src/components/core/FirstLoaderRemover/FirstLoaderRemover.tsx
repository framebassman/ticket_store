import { Component } from 'react';

export class FirstLoaderRemover extends Component {
  componentDidMount() {
    const firstLoader = document.getElementById('first-loader');
    if (firstLoader) {
      firstLoader.remove();
    }
  }

  render() {
    return null;
  }
}
