# OOP_PROJECT
 Проект по объектно ориентированному программированию, 2 курс, 2 полугодие

# oop_project.client

This template should help get you started developing with Vue 3 in Vite.

## Зависимости

1. NodeJS

## Recommended IDE Setup

[VSCode](https://code.visualstudio.com/) + [Volar](https://marketplace.visualstudio.com/items?itemName=Vue.volar) (and disable Vetur).

## Customize configuration

See [Vite Configuration Reference](https://vitejs.dev/config/).

## Выполнять следующие команды в папке oop_project.client

## Client project Setup 

```sh
npm install
```

### Compile and Hot-Reload for Development

```sh
npm run dev
```

### Compile and Minify for Production

```sh
npm run build
```

### Lint with [ESLint](https://eslint.org/)

```sh
npm run lint
```

## Server project Setup

### For .Net core CLI

Выполнять в папке ```OOP_PROJECT.Server```

```sh
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Or

### For Visual studio Package Manager Console

```sh
Install-Package Microsoft.EntityFrameworkCore
Add-Migration InitialCreate
Update-Database
```

### TroubleShoot

В случае проблем с сертификатами https:

Перейти по адресу ```%appdata%/ASP.NET/https``` и удалить все файлы в данной папке, саму папку оставить не тронутой
В командной строке выполнить:
```sh
dotnet dev-certs https --clean
dotnet dev-certs https --export-path %appdata%/ASP.NET/https/OOP_PROJECT.Server.pfx --trust
```
Перезапустить проект, браузер и т.д.

При необходимости после перезапуска сервера вновь перезапустить проект, и браузер.
