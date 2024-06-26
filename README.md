# OOP_PROJECT
 Проект по объектно ориентированному программированию, 2 курс, 2 полугодие

## Зависимости

1. NodeJS 
2. Docker

> Зависимости требуется поставить перед установкой и конфигурацией проекта

## Запуск проекта

### Первичная настройка

1. Скопируйте репозиторий через git clone
2. Откройте файл OOP_PROJECT.sln

### Debug

Для того что-бы запустить проект для отладки, без нам потребуется Docker для быстрой и простой развертки БД SQL Server.

1. Нажмите на стрелочку рядом с кнопкой запуска проекта
	![](README%20images/Configure%20project%20icon.png)
3. Выберите "Настройка начальных проектов"
4. В разделе "Запускаемый проект" выберите "Текущий выбор"
	![](README%20images/Current%20selection.png)
5. Для каждого сервера asp .net core выберите один из вариантов запуска
	1. http
	2. https
	3. IIS Express
	![](README%20images/Launch%20types.png)
6.  Повторите пункт 3, 4,
7. Выберите вариант "Несколько запускаемых проектов" в разделе "Запускаемый проект".
	Для каждого из проектов выберите действие "Запуск"
	![](README%20images/Launch%20all.png)
8. Проект готов к работе


### Release

Для того что-бы построить проект для выгрузки и запуска на сервере, нам потребуется Docker в котором будет каждый из наших серверов.


1. Нажмите на стрелочку рядом с кнопкой запуска проекта
	![[ConfigureProjectIcon.png]]
2. Выберите "Настройка начальных проектов"
3. . В разделе "Запускаемый проект" выберите "Текущий выбор"
	![](README%20images/Current%20selection.png)
4. Для каждого сервера asp .net core выберите вариант запуска Container (Dockerfile)
	![](README%20images/Launch%20types.png)
6. Повторите пункт 3, 4,
7. Выберите вариант "Несколько запускаемых проектов" в разделе "Запускаемый проект".
	Для каждого из проектов выберите действие "Запуск"
	![](README%20images/Launch%20all.png)
8. Проект готов к сборке
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
