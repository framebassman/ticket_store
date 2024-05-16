const path = require('path');
const { merge } = require('webpack-merge');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const CopyPlugin = require('copy-webpack-plugin');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');

const baseConfig = {
  entry: './src/index.js',
  output: {
    path: path.resolve(__dirname, '..', 'Model', 'PdfDocument', 'Templates', 'Dynamic'),
    filename: 'bundle.js',
    assetModuleFilename: 'assets/[hash][ext]',
  },
  module: {
    rules: [
      // Images
      {
        test: /\.(?:ico|gif|png|jpg|jpeg|svg)$/i,
        type: 'asset/resource',
      },
      // Fonts
      {
        test: /\.(woff(2)?|eot|ttf|otf)$/i,
        type: 'asset/resource',
      },
      {
        test: /\.css$/i,
        use: [
          {
            loader: 'style-loader',
            options: {
              insert: 'head', // insert style tag inside of <head>
              injectType: 'singletonStyleTag' // this is for wrap all your style in just one style tag
            },
          },
          "css-loader"
        ],
      },
      {
        test: /\.geojson$/,
        type: 'json',
      }
    ]
  },
  resolve: {
    extensions: ['.js']
  },
  plugins: [
    new MiniCssExtractPlugin({
       filename: '[name].[contenthash].css'
    }),
    new HtmlWebpackPlugin({
      template: './src/index.html'
    }),
    new CopyPlugin({
      patterns: [{
        from: 'public',
        noErrorOnMissing: true,
      }],
    }),
    new CleanWebpackPlugin({ cleanStaleWebpackAssets: false }),
  ]
};


module.exports = ({ mode }) => {
  const isProductionMode = mode === 'prod';
  const envConfig = isProductionMode ? require('./webpack.production.config') : require('./webpack.development.config');

  return merge(baseConfig, envConfig);
}
