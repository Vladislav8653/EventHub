﻿# EventHub

## Overview

**EventHub** is an API for working with events.

## Startup instructions

- **Step 0**: Make sure you have .NET 8, PostgreSQL installed
- **Step 1**: Clone repository:
```bash
git clone https://github.com/Vladislav8653/EventHub
```
- **Step 2**: Find `EventHub/EventHub/appsettings.json` and configure field **sqlConnection** in **ConnectionStrings** option. This is connection string to your database.
- **Step 3**: Create Migrations via terminal from project root:
```bash
dotnet ef migrations add initial --project DataLayer --startup-project EventHub
```
- **Step 4**: Update database via terminal from project root:
```bash
dotnet ef database update --startup-project EventHub
```
- **Step 5**: Enjoy😋

## Use cases
### Use Case: Управление категориями
**Просмотр категорий**
- Актор: любой пользователь
- Путь: **GET /categories**
- Основной поток:
  - Пользователь отправляет HTTP-запрос
  - Система обрабатывает запрос 
  - Система обращается в БД для получения всех категорий
  - Система формирует JSON со списком и отправляет ответ с кодом **200 OK**
- Результаты: отображение списка категорий

**Добавление категории**
- Актор: Администратор
- Путь: **POST /categories**
- Основной поток:
  - Пользователь отправляет HTTP-запрос с телом
  - Система проверяет права пользователя
  - Система обрабатывает запрос 
  - Система обращается в БД для проверки уникальности имени категории
  - Если имя уникально, система создает новую категорию в базе данных
  - Система отправляет созданную категорию с кодом **200 OK**
- Альтернативный поток:
  - Система обращается в БД для проверки уникальности имени категории
  - Если имя не уникально, система возвращает ошибку с кодом **400 Bad Request**
- Альтернативный поток:
  - Если пользователь не имеет достаточных прав, система возвращает ошибку с кодом **401 Unauthorized**
- Результаты: отображение созданной категории

**Удаление категории**
- Актор: Администратор
- Путь: **DELETE /categories**
- Основной поток:
  - Пользователь отправляет HTTP-запрос с ID
  - Система проверяет права пользователя
  - Система обрабатывает запрос
  - Система обращается в БД для проверки существования категории
  - Если существует, система удаляет категорию базе данных
  - Система отправляет созданную категорию с кодом **200 OK**
- Альтернативный поток:
  - Система обращается в БД для проверки существования категории
  - Если не существует, система возвращает ошибку с кодом **404 Not Found**
- Альтернативный поток:
  - Если пользователь не имеет достаточных прав, система возвращает ошибку с кодом **401 Unauthorized** 
- Результаты: отображение удаленной категории

### Use Case: Управление событиями
### Use Case: Управление участниками
### Use Case: Управление пользователями
