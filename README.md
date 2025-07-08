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

## Скриншоты

![Снимок экрана 2025-07-08 111320](https://github.com/user-attachments/assets/5315f3a2-0810-4ee0-9630-cb19161b9987)
![Снимок экрана 2025-07-08 111358](https://github.com/user-attachments/assets/01dd4b3a-ddb0-47f3-9b17-33d948b0e162)
![Снимок экрана 2025-07-08 111421](https://github.com/user-attachments/assets/d6281469-56bd-4658-81b6-124cb1f1d65d)
![Снимок экрана 2025-07-08 111452](https://github.com/user-attachments/assets/aa0af03d-e194-4637-92dc-12f41c349a9e)
![Снимок экрана 2025-07-08 111510](https://github.com/user-attachments/assets/520fca6b-34c6-4cbf-ad61-1d72b858b4d0)
