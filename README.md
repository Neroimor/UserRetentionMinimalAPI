# UserRetentionMinimalAPI

Лёгкий REST API на .NET 9 для управления пользователями с использованием PostgreSQL и EF Core.

## Содержание

* [Функциональность](#функциональность)
* [Технологии](#технологии)
* [Установка и настройка](#установка-и-настройка)
* [Миграции и базе данных](#миграции-и-база-данных)
* [Запуск приложения](#запуск-приложения)
* [Краткое описание эндпоинтов](#краткое-описание-эндпоинтов)
* [Тестирование](#тестирование)

## Функциональность

* Добавление нового пользователя
* Получение пользователя по email
* Обновление данных пользователя
* Получение списка всех пользователей
* Удаление пользователя

## Технологии

* .NET 9 Minimal API
* Entity Framework Core + Npgsql (PostgreSQL)
* HTTP Logging Middleware
* In-memory DB + Moq для модульных тестов

## Установка и настройка

1. Клонировать репозиторий:

   ```bash
   git clone https://github.com/Neroimor/UserRetentionMinimalAPI.git
   cd UserRetentionMinimalAPI
   git checkout development
   ```

2. Установить зависимости:

   ```bash
   dotnet restore
   ```

3. Настроить строку подключения в `appsettings.json`:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Port=5432;Database=UserRetentionDb;Username=postgres;Password=your_password"
   }
   ```

## Миграции и база данных

Используются пакеты:

* `Npgsql.EntityFrameworkCore.PostgreSQL`
* `Microsoft.EntityFrameworkCore`
* `Microsoft.EntityFrameworkCore.Design` (миграции)

### Создание первой миграции

```bash
 dotnet ef migrations add InitialCreate
```

### Применение миграции

```bash
 dotnet ef database update
```

### Откат миграции

```bash
 dotnet ef migrations remove
```

## Запуск приложения

```bash
 dotnet run --project src/UserRetentionMinimalAPI
```

По умолчанию API будет доступен на `https://localhost:7045`.

При запуске в режиме Development будут подключены Swagger UI (`/swagger`) и логирование HTTP.

## Краткое описание эндпоинтов

| Метод  | URL               | Тело запроса (JSON)                   | Описание                  |
| ------ | ----------------- | ------------------------------------- | ------------------------- |
| POST   | `/added`          | `{ "name": string, "email": string }` | Добавить пользователя     |
| GET    | `/get/{email}`    | —                                     | Получить по email         |
| PUT    | `/update/{email}` | `{ "name": string, "email": string }` | Обновить данные           |
| GET    | `/getall`         | —                                     | Список всех пользователей |
| DELETE | `/delete/{email}` | —                                     | Удалить пользователя      |

## Тестирование

В проекте `TestUserManagment` настроены модульные тесты с использованием:

* `Moq` для mock-объектов
* `Microsoft.EntityFrameworkCore.InMemory` для In-Memory БД

Примеры команд:

```bash
cd tests/TestUserManagment
dotnet test
```
