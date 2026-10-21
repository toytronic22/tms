# Final Surge — дипломный проект

Автотесты для сервиса тренировок [Final Surge](https://log.finalsurge.com/): UI, API и BDD.

## Стек

- C# / .NET 10, NUnit 4
- Selenium WebDriver 4, Page Object + Page Factory, Loadable Component
- RestSharp — API-тесты и создание тестовых данных через API
- Reqnroll — BDD-сценарии (преемник SpecFlow, сам SpecFlow больше не развивается)
- Allure — отчёт, log4net — логи
- Jenkins + Selenium Grid в Docker — CI

## Структура

```
FinalSurge/
├── FinalSurge.Core          фреймворк
│   ├── Api                  RestSharp-клиенты и модели запросов/ответов
│   ├── Configuration        чтение настроек (appsettings.json, user-secrets, переменные окружения)
│   ├── Drivers              DriverFactory: Chrome, Firefox, Selenium Grid
│   ├── Elements             обёртки над элементами: Button, Input, DropDown и др.
│   ├── Models               модели тестовых данных
│   ├── Pages                Page Object страниц
│   └── Utils                генератор тестовых данных
├── FinalSurge.Tests         UI- и API-тесты на NUnit
├── FinalSurge.Bdd           BDD-сценарии на Reqnroll
├── jenkins                  Dockerfile и docker-compose для Jenkins и Selenium Grid
├── Jenkinsfile              параллельный прогон
└── Jenkinsfile.crossbrowser кросс-браузерный прогон
```

## Что проверяют тесты

### UI — `FinalSurge.Tests/Ui`, 12 тестов

| Набор | Что проверяет |
|---|---|
| LoginTests | вход с верными данными, неверный пароль, выход |
| WorkoutTests | добавление тренировки, изменение названия, тренировка из API видна в календаре, удаление из календаря, отмена удаления |
| CalculatorTests | калькулятор калорий (два набора данных), калькулятор темпа для дистанции и для марафона |

### API — `FinalSurge.Tests/Api`, 15 тестов

| Набор | Что проверяет |
|---|---|
| AuthApiTests | вход возвращает токен, неверные данные (три варианта), запрос без токена — 401, настройки текущего пользователя |
| WorkoutApiTests | создание, изменение, удаление тренировки; удаление несуществующей; неизвестный тип активности |
| CalendarLabelApiTests | создание, изменение, удаление метки календаря; метка без названия |

### BDD — `FinalSurge.Bdd/Features`, 10 сценариев

| Фича | Сценарии |
|---|---|
| Login.feature | вход, неверный пароль, проверка email и пароля в форме, выход |
| Workouts.feature | добавление тренировки, изменение названия, тренировка из API в календаре, удаление |

### Виды тестирования

- **Функциональное** — все тесты проверяют поведение приложения.
- **Позитивное и негативное** — верные и неверные данные для входа, пустые поля, несуществующие сущности.
- **UI (end-to-end)** — сценарии пользователя в браузере.
- **API** — запросы к `https://beta.finalsurge.com/api/` без браузера.
- **Приёмочное (BDD)** — сценарии на Gherkin, понятные без кода.
- **Smoke и регрессионное** — категории `Smoke` и `Regression`.
- **Тестирование на разных данных (data-driven)** — `TestCase` в NUnit и `Scenario Outline` в Reqnroll.
- **Кросс-браузерное** — Chrome и Firefox.

Тестовые данные для UI-тестов создаются через API (RestSharp), после теста удаляются тоже через API. Все созданные
данные называются с префиксом `TMS`.

## Настройки

Настройки лежат в `appsettings.json` (секция `TestSettings`): браузер, headless, адреса сайта и API, Grid.

Почта и пароль в репозиторий не попадают. Локально их хранит user-secrets:

```bash
dotnet user-secrets set "TestSettings:Email" "<почта>" --project FinalSurge.Core
dotnet user-secrets set "TestSettings:Password" "<пароль>" --project FinalSurge.Core
```

Любую настройку можно переопределить переменной окружения: `TestSettings__Browser=Firefox`,
`TestSettings__Headless=true`, `TestSettings__GridUrl=http://localhost:4445`.

## Запуск

```bash
dotnet test                                                  # все тесты
dotnet test FinalSurge.Tests --filter Category=API           # только API
dotnet test FinalSurge.Tests --filter Category=UI            # только UI
dotnet test FinalSurge.Bdd                                   # BDD-сценарии
dotnet test --filter Category=Smoke                          # smoke
TestSettings__Browser=Firefox dotnet test FinalSurge.Bdd     # в Firefox
```

Отчёт Allure:

```bash
allure serve FinalSurge.Tests/bin/Debug/net10.0/allure-results
```

## Планы прогона

- **Параллельный** — внутри проекта тест-классы идут параллельно (`Parallelizable(ParallelScope.Fixtures)`,
  три потока). В Jenkins API, UI и BDD запускаются тремя параллельными стадиями (`Jenkinsfile`).
- **Кросс-браузерный** — `Jenkinsfile.crossbrowser`: matrix по браузеру, UI- и BDD-тесты одновременно в Chrome
  и Firefox через Selenium Grid. В отчёте Allure у каждого теста есть параметр `Browser`.
- **Smoke / Regression** — параметр `SUITE` в обеих задачах Jenkins.

## CI: Jenkins

```bash
cd jenkins
docker compose up -d --build
```

Поднимаются Jenkins (http://localhost:8082), Selenium Grid (http://localhost:4445) и две ноды: Chromium и Firefox.
Задачи `FinalSurge` и `FinalSurge-CrossBrowser` и инструмент `allure Jenkins` создаются сами. Код Jenkins берёт
из этого репозитория (папка смонтирована в контейнер), ветка `diploma`.

Перед первым запуском нужно добавить учётные данные Final Surge: Manage Jenkins → Credentials → System →
Global credentials → Add Credentials, тип Username with password, ID `final-surge`.

После прогона отчёт открывается по ссылке Allure Report в сборке.
