// webpack.config.js

const CopyPlugin = require("copy-webpack-plugin");
const path = require('path');
const { VueLoaderPlugin } = require('vue-loader');
const fs = require('fs');

module.exports = {
    entry: './VueJs/main.js',
    output: {
        path: path.resolve(__dirname, './wwwroot/dist'),
        filename: 'bundle.js',
    },
    module: {
        rules: [
            {
                test: /\.vue$/,
                loader: 'vue-loader',
            },
            {
                test: /\.js$/,
                loader: 'babel-loader',
            },
            {
                test: /\.css$/,
                use: [
                    'vue-style-loader',
                    'css-loader'
                ]
            }
        ],
    },
    plugins: [
        new VueLoaderPlugin(),
        new CopyPlugin({
            patterns: [
                {
                    from: path.resolve(__dirname, 'node_modules/bootstrap/dist/css/bootstrap.min.css'),
                    to: path.resolve(__dirname, 'wwwroot/dist/bootstrap/bootstrap.min.css')
                },
                {
                    from: path.resolve(__dirname, 'node_modules/bootstrap/dist/js/bootstrap.bundle.min.js'),
                    to: path.resolve(__dirname, 'wwwroot/dist/bootstrap/bootstrap.bundle.min.js')
                }
            ],
        }),
    ],
    resolve: {
        alias: {
            'vue$': 'vue/dist/vue.esm-bundler.js'
        }
    },
    devServer: {
        https: {
            key: fs.readFileSync(path.join(__dirname, 'localhost-key.pem')),
            cert: fs.readFileSync(path.join(__dirname, 'localhost-cert.pem')),
        },
        static: {
            directory: path.join(__dirname, './wwwroot/dist'),
        },
        port: 44309
    }
};