const path = require('path');

module.exports = {
    mode: 'development',
    devtool: 'inline-source-map', 
    devServer: {
        static: {
            directory: path.join(__dirname, '..', 'Mode', 'PdfDocument', 'DynamicTemplates'),
          },
        historyApiFallback: true,
        hot: true,
        port: 8080,
    },
};
