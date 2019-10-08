const rewriteReactHotLoader = require('react-app-rewire-hot-loader');

module.exports = function override(config, env) {
  config = rewriteReactHotLoader(config, env);

  // see https://github.com/gaearon/react-hot-loader#react--dom
  config.resolve.alias = {
    ...config.resolve.alias,
    'react-dom': '@hot-loader/react-dom'
  }

  return config;
}
