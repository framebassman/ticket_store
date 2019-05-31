module.exports = function(api) {
  api.cache(true);
  return {
    presets: ['babel-preset-expo', 'bable-preset-flow'],
  };
};
