# Как добавить Bootsrtap 5 в проект

Мы настраиваем bootstrap для dev-server на порту 44309.

## 1. Установка Bootsrtap 5

```sh
npm install bootstrap@5
```

## 2. Добавьте ссылку на CSS Bootstrap

Чтобы подключить CSS Bootstrap, добавьте следующий тег `<link>` в секцию `<head>` файла `_Layout.cshtml`:

```html
<link rel="stylesheet" href="https://localhost:44309/bootstrap/bootstrap.min.css" />
```

## 3. Добавьте ссылку на JavaScript Bootstrap

Добавьте следующие скрипты перед закрывающим тегом `</body>` для подключения JavaScript-файла Bootstrap. Это важно делать в конце документа, чтобы убедиться, что весь HTML был загружен до выполнения скриптов.

```html
<script src="https://localhost:44309/bootstrap/bootstrap.bundle.min.js"></script>
```

## 4. Шаги по настройке Webpack

### Установите copy-webpack-plugin

```sh
npm install copy-webpack-plugin --save-dev
```

### Настройте Webpack

```javascript
const CopyPlugin = require("copy-webpack-plugin");
const path = require("path");

module.exports = {
  // ... другие настройки Webpack ...
  plugins: [
    // ... другие плагины ...
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
};
```

Важно добавить эту строку из предыдущего примера:

```javascript
const CopyPlugin = require("copy-webpack-plugin");
```

## 5. Запуск и проверка

Перед запуском проекта:

```sh
npm run serve
```

Потом:

- Запуск проекта из Visual Studio.
- Inspect Element.
- Убедиться, что всё загрузилось правильно.

## Пример файла _Layout.cshtml

```html
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>My Website</title>
    <link rel="stylesheet" href="https://localhost:44309/bootstrap/bootstrap.min.css" />
</head>
<body>
    <div class="container">
        <main role="main" class="pb-3">
            @RenderBody()
        </main>
    </div>

    <script src="https://localhost:44309/bootstrap/bootstrap.bundle.min.js"></script>
    <script src="https://localhost:44309/bundle.js" asp-append-version="true"></script>
</body>
</html>
```
