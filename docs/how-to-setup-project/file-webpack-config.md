# Файл "webpack.config.js"

~/webpack.config.js

```js
// webpack.config.js

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
        new VueLoaderPlugin()
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
```

---

Порт, который я буду использовать: 44309
Это будет использоваться в коде так:

```html
<script src="https://localhost:44309/bundle.js" asp-append-version="true" defer></script>
```
