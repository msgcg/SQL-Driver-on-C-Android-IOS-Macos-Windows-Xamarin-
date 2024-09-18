# SQL-Driver-on-CSharp

SQL-Driver-on-CSharp — это библиотека для работы с базой данных SQL Server на C#. Она предоставляет методы для выполнения операций CRUD (создание, чтение, обновление, удаление) в таблице `ANKETS`. Протестировано на Xamarin.

## Установка

1. **Клонируйте репозиторий:**

    ```bash
    git clone https://github.com/msgcg/SQL-Driver-on-CSharp
    ```

2. **Откройте проект в Visual Studio или другом подходящем IDE.**

3. **Настройте строку подключения к вашей базе данных:**

    ```csharp
    private readonly string connectionString = "Server=YOUR_ADDRESS;Database=NAME_OF_DATABASE;User Id=LOGIN;Password=PASSWORD";
    ```

4. **Создайте таблицу `ANKETS` на сервере SQL. Структура таблицы должна выглядеть следующим образом:**

    ```sql
    CREATE TABLE [dbo].[ANKETS](
        [id] [int] NOT NULL,
        [name] [ntext] NOT NULL,
        [age] [int] NOT NULL,
        [zodiac] [ntext] NOT NULL,
        [interests] [ntext] NOT NULL,
        [aboutme] [ntext] NOT NULL,
        [photourl] [text] NOT NULL,
        [liked] [text] NOT NULL,
        [disliked] [text] NOT NULL,
        [banned] [text] NOT NULL,
        [chatwith] [text] NOT NULL,
        [subscribe] [bit] NOT NULL,
        [sex] [ntext] NOT NULL,
        [password] [text] NOT NULL,
        [tgusername] [text] NOT NULL,
        [latitude] [text] NOT NULL,
        [longtitude] [text] NOT NULL,
        [badhabbits] [text] NOT NULL
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
    ```

5. **Лицензия:** Этот проект лицензирован под [GPL-3.0](https://opensource.org/licenses/GPL-3.0).

## Примеры использования

### Получение всех анкет

```csharp
var service = new AnketsService();
List<Ankets> allAnkets = await service.GetAllAnketsAsync();

foreach (var anket in allAnkets)
{
    Console.WriteLine($"ID: {anket.id}, Имя: {anket.name}, Возраст: {anket.age}");
}
```

### Получение анкеты по ID

```csharp
var service = new AnketsService();
int id = 1;
Ankets anket = await service.GetAnketByIdAsync(id);

if (anket != null)
{
    Console.WriteLine($"Имя: {anket.name}, Возраст: {anket.age}, Зодиак: {anket.zodiac}");
}
else
{
    Console.WriteLine("Анкета не найдена");
}
```

### Получение анкеты по Telegram username

```csharp
var service = new AnketsService();
string tgUsername = "example_username";
Ankets anket = await service.GetAnketByTgusernameAsync(tgUsername);

if (anket != null)
{
    Console.WriteLine($"Имя: {anket.name}, Возраст: {anket.age}, Зодиак: {anket.zodiac}");
}
else
{
    Console.WriteLine("Анкета не найдена");
}
```

### Добавление новой анкеты

```csharp
var service = new AnketsService();
var newAnket = new Ankets
{
    name = "Jane Doe",
    age = 28,
    zodiac = "Leo",
    interests = "Reading, Traveling",
    aboutMe = "Passionate about life",
    photoUrl = "http://example.com/janedoe.jpg",
    liked = "Books, Beaches",
    disliked = "Crowds",
    banned = "",
    chatWith = "987654321",
    subscribe = true,
    sex = "Female",
    password = "securepassword",
    tgusername = "@janedoe",
    latitude = "40.7128",
    longtitude = "-74.0060",
    badhabbits = "false"
};

await service.AddAnketAsync(newAnket);
Console.WriteLine("Анкета добавлена");
```

### Обновление существующей анкеты

```csharp
var service = new AnketsService();
var anketToUpdate = new Ankets
{
    id = 1, // ID существующей анкеты
    name = "Jane Smith",
    age = 29
    // Другие поля по необходимости
};

await service.UpdateAnketAsync(anketToUpdate);
Console.WriteLine("Анкета обновлена");
```

### Удаление анкеты

```csharp
var service = new AnketsService();
int idToDelete = 1;
await service.DeleteAnketAsync(idToDelete);
Console.WriteLine("Анкета удалена");
```
