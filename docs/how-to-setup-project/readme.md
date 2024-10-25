# Настройка проекта .NET Razor Pages, VueJs и WebPack

## Предварительная подготовка

- Создать новый проект Razor Pages
- Откройте терминал в папке проекта.

```sh
npm init
```

## Настройка проекта Razor Pages

- В *.csproj добавить:

```html
<ItemGroup>
  <PackageReference Include="Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation" Version="7.0.11" />
</ItemGroup>
```

- Правый клик на Dependencies в Solution Exporer и после этого Restore - это установит NuGet

- В startup.cs добавить:

```csharp
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
```

- Добавить в /Pages/Index.cshtml:

```html
<div id="vue-app"></div>
<script src="https://localhost:44309/bundle.js" asp-append-version="true" defer></script>
```

## Настройка VueJs и WebPack


- [Создайте файл ".gitignore"](./file-gitignore.md)
- [Создайте файл "packege.json"](./file-packege-json.md)
- [Создайте файл "webpack.config.js"](./file-webpack-config.md)
- [Создайте файл "main.js"](./file-main-js.md)
- [Создайте файл "VueApp.vue"](./file-vueapp-vue.md)



## Настройка сертификатов для локального тестирования

Краткое описание процесса создания самоподписанного сертификата:

- Генерация приватного ключа.
- Создание запроса на подпись сертификата (CSR).
- Генерация самоподписанного сертификата с использованием CSR и приватного ключа.
- Удаление файла CSR после создания сертификата.

```sh
openssl genrsa -out localhost-key.pem 2048
openssl req -new -key localhost-key.pem -out localhost.csr
openssl x509 -req -days 365 -in localhost.csr -signkey localhost-key.pem -out localhost-cert.pem
rm localhost.csr
```

## Запуск и тестирования

```sh
npm install
npm run serve
```

- Run project from Visual Studio.
- Inspect element
- Find error: Failed to load resource: The certificate for this server is invalid. You might be connecting to a server that is pretending to be “localhost”, which could put your confidential information at risk.
- Copy link: https://localhost:44309/bundle.js
- Open in a new tab https://localhost:44309/bundle.js
- In certificate details: Always trust, visit web page
- Endure file is not empty
- Reload Home Page.
- Ensure you see “Привет, это тестовое Vue-приложение!”
- Готово.
- Commit to git.
