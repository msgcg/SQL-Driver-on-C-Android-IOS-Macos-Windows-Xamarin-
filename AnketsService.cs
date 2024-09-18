using SWSU_Dating;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace SWSU_Dating
{
    public class AnketsService
    {
        // Строка подключения к SQL Server
        private readonly string connectionString = "Server=YOUR_ADRESS;Database=NAME_OF_DATABASE;User Id=LOGIN;Password=PASSWORD";

        // Метод для получения всех данных из таблицы ANKETS
        public async Task<List<Ankets>> GetAllAnketsAsync()
        {
            // Создаем список для хранения результатов
            var ankets = new List<Ankets>();

            // Создаем подключение к SQL Server
            using (var connection = new SqlConnection(connectionString))
            {
                // Открываем подключение
                await connection.OpenAsync();

                // Создаем команду для выполнения запроса
                using (var command = new SqlCommand("SELECT * FROM ANKETS", connection))
                {
                    // Выполняем запрос и получаем объект SqlDataReader для чтения данных
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        // Пока есть данные, читаем их и добавляем в список
                        while (await reader.ReadAsync())
                        {
                            // Создаем объект Ankets и заполняем его данными из текущей строки
                            var anket = new Ankets
                            {
                                id = reader.GetInt32(0),
                                name = reader.GetString(1),
                                age = reader.GetInt32(2),
                                zodiac = reader.GetString(3),
                                interests = reader.GetString(4),
                                aboutMe = reader.GetString(5),
                                photoUrl = reader.GetString(6),
                                liked = reader.GetString(7),
                                disliked = reader.GetString(8),
                                banned = reader.GetString(9),
                                chatWith = reader.GetString(10),
                                subscribe = reader.GetBoolean(11),
                                sex = reader.GetString(12),
                                password = reader.GetString(13),
                                tgusername = reader.GetString(14),
                                latitude = reader.GetString(15),
                                longtitude = reader.GetString(16),
                                badhabbits = reader.GetString(17)
                            };
                            if (string.IsNullOrEmpty(anket.badhabbits) || (!(anket.badhabbits == "false" || anket.badhabbits == "true"))) anket.badhabbits = "false";
                                // Добавляем объект Ankets в список
                                ankets.Add(anket);
                        }
                    }
                }
            }

            // Возвращаем список с данными
            return ankets;
        }

        // Метод для получения одной записи из таблицы ANKETS по id
        public async Task<Ankets> GetAnketByIdAsync(int id)
        {
            Ankets anket = new Ankets(); // Создаем объект с пустыми значениями

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT * FROM ANKETS WHERE id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            // Заполнение объекта Ankets данными из текущей строки, если они есть
                            anket.id = reader.GetInt32(0);
                            anket.name = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                            anket.age = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                            anket.zodiac = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                            anket.interests = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                            anket.aboutMe = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                            anket.photoUrl = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                            anket.liked = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                            anket.disliked = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                            anket.banned = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                            anket.chatWith = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                            anket.subscribe = reader.IsDBNull(11) ? false : reader.GetBoolean(11);
                            anket.sex = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                            anket.password = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
                            anket.tgusername = reader.IsDBNull(14) ? string.Empty : reader.GetString(14);
                            anket.latitude = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);
                            anket.longtitude = reader.IsDBNull(16) ? string.Empty : reader.GetString(16);
                            anket.badhabbits = reader.IsDBNull(16) ? string.Empty : reader.GetString(17);
                            if (string.IsNullOrEmpty(anket.badhabbits) || (!(anket.badhabbits == "false" || anket.badhabbits == "true"))) anket.badhabbits = "false";
                        }
                        else
                        {
                            // Если строки с указанным id нет, возвращаем пустой объект Ankets
                            return anket;
                        }
                    }
                }
            }

            return anket; // Возвращаем объект Ankets с данными из базы данных или пустыми значениями
        }

        public async Task<Ankets> GetAnketByTgusernameAsync(string tgusername)
        {
            Ankets anket = new Ankets(); // Создаем объект с пустыми значениями

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT * FROM ANKETS WHERE CONVERT(NVARCHAR(MAX), tgusername) = CONVERT(NVARCHAR(MAX), @tgusername)", connection))
                {
                    command.Parameters.Add("@tgusername", SqlDbType.NVarChar).Value = tgusername;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            // Заполнение объекта Ankets данными из текущей строки, если они есть
                            anket.id = reader.GetInt32(0);
                            anket.name = reader.IsDBNull(1) ? string.Empty : reader.GetSqlValue(1).ToString();
                            anket.age = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                            anket.zodiac = reader.IsDBNull(3) ? string.Empty : reader.GetSqlValue(3).ToString();
                            anket.interests = reader.IsDBNull(4) ? string.Empty : reader.GetSqlValue(4).ToString();
                            anket.aboutMe = reader.IsDBNull(5) ? string.Empty : reader.GetSqlValue(5).ToString();
                            anket.photoUrl = reader.IsDBNull(6) ? string.Empty : reader.GetSqlValue(6).ToString();
                            anket.liked = reader.IsDBNull(7) ? string.Empty : reader.GetSqlValue(7).ToString();
                            anket.disliked = reader.IsDBNull(8) ? string.Empty : reader.GetSqlValue(8).ToString();
                            anket.banned = reader.IsDBNull(9) ? string.Empty : reader.GetSqlValue(9).ToString();
                            anket.chatWith = reader.IsDBNull(10) ? string.Empty : reader.GetSqlValue(10).ToString();
                            anket.subscribe = reader.IsDBNull(11) ? false : reader.GetBoolean(11);
                            anket.sex = reader.IsDBNull(12) ? string.Empty : reader.GetSqlValue(12).ToString();
                            anket.password = reader.IsDBNull(13) ? string.Empty : reader.GetSqlValue(13).ToString();
                            anket.tgusername = reader.IsDBNull(14) ? string.Empty : reader.GetSqlValue(14).ToString();
                            anket.latitude = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);
                            anket.longtitude = reader.IsDBNull(16) ? string.Empty : reader.GetString(16);
                            anket.badhabbits = reader.IsDBNull(16) ? string.Empty : reader.GetString(17);
                            if (string.IsNullOrEmpty(anket.badhabbits) || (!(anket.badhabbits == "false" || anket.badhabbits == "true"))) anket.badhabbits = "false";
                        }
                    }
                }
            }
            return anket;
        }



        // Метод для добавления новой записи в таблицу ANKETS
        public async Task AddAnketAsync(Ankets anket)
        {
            int newId;

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                // Получаем максимальный id
                using (var command = new SqlCommand("SELECT MAX(id) FROM ANKETS", connection))
                {
                    var maxIdObj = await command.ExecuteScalarAsync();
                    if (maxIdObj == DBNull.Value)
                    {
                        newId = 1;
                    }
                    else
                    {
                        newId = (int)maxIdObj + 1;
                    }
                }

                using (var command = new SqlCommand("INSERT INTO ANKETS " +
                                                   "(id, name, age, zodiac, interests, aboutme, photourl, liked, disliked, banned, chatwith, subscribe, sex, password, tgusername,latitude,longtitude,badhabbits) " +
                                                   "VALUES " +
                                                   "(@id, @name, @age, @zodiac, @interests, @aboutme, @photourl, @liked, @disliked, @banned, @chatwith, @subscribe, @sex, @password, @tgusername,@latitude,@longtitude,@badhabbits)", connection))
                {
                    // Добавляем параметры и устанавливаем их значения из объекта Ankets
                    command.Parameters.Add("@id", SqlDbType.Int).Value = newId;
                    command.Parameters.Add("@name", SqlDbType.NText).Value = anket.name;
                    command.Parameters.Add("@age", SqlDbType.Int).Value = anket.age;
                    command.Parameters.Add("@zodiac", SqlDbType.NText).Value = anket.zodiac;
                    command.Parameters.Add("@interests", SqlDbType.NText).Value = anket.interests;
                    command.Parameters.Add("@aboutme", SqlDbType.NText).Value = anket.aboutMe;
                    command.Parameters.Add("@photourl", SqlDbType.Text).Value = anket.photoUrl;
                    command.Parameters.Add("@liked", SqlDbType.Text).Value = anket.liked;
                    command.Parameters.Add("@disliked", SqlDbType.Text).Value = anket.disliked;
                    command.Parameters.Add("@banned", SqlDbType.Text).Value = anket.banned;
                    command.Parameters.Add("@chatwith", SqlDbType.Text).Value = anket.chatWith;
                    command.Parameters.Add("@subscribe", SqlDbType.Bit).Value = anket.subscribe;
                    command.Parameters.Add("@sex", SqlDbType.NText).Value = anket.sex;
                    command.Parameters.Add("@password", SqlDbType.Text).Value = anket.password;
                    command.Parameters.Add("@tgusername", SqlDbType.Text).Value = anket.tgusername;
                    command.Parameters.Add("@latitude", SqlDbType.Text).Value = anket.latitude;
                    command.Parameters.Add("@longtitude", SqlDbType.Text).Value = anket.longtitude;
                    if (string.IsNullOrEmpty(anket.badhabbits) || (!(anket.badhabbits == "false" || anket.badhabbits == "true")))
                    {
                        command.Parameters.Add("@badhabbits", SqlDbType.Text).Value = "false";

                    }
                    else command.Parameters.Add("@badhabbits", SqlDbType.Text).Value = anket.badhabbits;

                    // Выполняем запрос
                    await command.ExecuteNonQueryAsync();
                }
            }
        }


        // Метод для обновления существующей записи в таблице ANKETS по id
        public async Task UpdateAnketAsync(Ankets anket)
        {
            // Создаем подключение к SQL Server
            using (var connection = new SqlConnection(connectionString))
            {
                // Открываем подключение и выполняем запрос
                await connection.OpenAsync();
                if (!(anket.age<=0))
                {
                    using (var command = new SqlCommand("UPDATE ANKETS SET age = @age WHERE id = @id", connection))
                    {
                        command.Parameters.Add("@id", SqlDbType.Int).Value = anket.id;
                        command.Parameters.Add("@age", SqlDbType.Int).Value = anket.age;
                        await command.ExecuteNonQueryAsync();
                    }
                    
                }
                if (!(anket.name == null))
                {
                    using (var command = new SqlCommand("UPDATE ANKETS SET name = @name WHERE id = @id", connection))
                    {
                        command.Parameters.Add("@id", SqlDbType.Int).Value = anket.id;
                        command.Parameters.Add("@name", SqlDbType.NText).Value = anket.name;
                        await command.ExecuteNonQueryAsync();
                    }

                }
                if (!(anket.zodiac == null))
                {
                    using (var command = new SqlCommand("UPDATE ANKETS SET zodiac = @zodiac WHERE id = @id", connection))
                    {
                        command.Parameters.Add("@id", SqlDbType.Int).Value = anket.id;
                        command.Parameters.Add("@zodiac", SqlDbType.NText).Value = anket.zodiac;
                        await command.ExecuteNonQueryAsync();
                    }

                }
                if (!(anket.interests == null))
                {
                    using (var command = new SqlCommand("UPDATE ANKETS SET interests = @interests WHERE id = @id", connection))
                    {
                        command.Parameters.Add("@id", SqlDbType.Int).Value = anket.id;
                        command.Parameters.Add("@interests", SqlDbType.NText).Value = anket.interests;
                        await command.ExecuteNonQueryAsync();
                    }

                }
                if (!(anket.aboutMe == null))
                {
                    using (var command = new SqlCommand("UPDATE ANKETS SET aboutme = @aboutme WHERE id = @id", connection))
                    {
                        command.Parameters.Add("@id", SqlDbType.Int).Value = anket.id;
                        command.Parameters.Add("@aboutme", SqlDbType.NText).Value = anket.aboutMe;
                        await command.ExecuteNonQueryAsync();
                    }

                }
                if (!(anket.photoUrl == null))
                {
                    using (var command = new SqlCommand("UPDATE ANKETS SET photourl = @photourl WHERE id = @id", connection))
                    {
                        command.Parameters.Add("@id", SqlDbType.Int).Value = anket.id;
                        command.Parameters.Add("@photourl", SqlDbType.Text).Value = anket.photoUrl;
                        await command.ExecuteNonQueryAsync();
                    }

                }
                if (!(anket.liked == null))
                {
                    using (var command = new SqlCommand("UPDATE ANKETS SET liked = @liked WHERE id = @id", connection))
                    {
                        command.Parameters.Add("@id", SqlDbType.Int).Value = anket.id;
                        command.Parameters.Add("@liked", SqlDbType.Text).Value = anket.liked;
                        await command.ExecuteNonQueryAsync();
                    }

                }
                if (!(anket.disliked == null))
                {
                    using (var command = new SqlCommand("UPDATE ANKETS SET disliked = @disliked WHERE id = @id", connection))
                    {
                        command.Parameters.Add("@id", SqlDbType.Int).Value = anket.id;
                        command.Parameters.Add("@disliked", SqlDbType.Text).Value = anket.disliked;
                        await command.ExecuteNonQueryAsync();
                    }

                }
                if (!(anket.banned == null))
                {
                    using (var command = new SqlCommand("UPDATE ANKETS SET banned = @banned WHERE id = @id", connection))
                    {
                        command.Parameters.Add("@id", SqlDbType.Int).Value = anket.id;
                        command.Parameters.Add("@banned", SqlDbType.Text).Value = anket.banned;
                        await command.ExecuteNonQueryAsync();
                    }

                }
                if (!(anket.chatWith == null))
                {
                    using (var command = new SqlCommand("UPDATE ANKETS SET chatwith = @chatwith WHERE id = @id", connection))
                    {
                        command.Parameters.Add("@id", SqlDbType.Int).Value = anket.id;
                        command.Parameters.Add("@chatwith", SqlDbType.Text).Value = anket.chatWith;
                        await command.ExecuteNonQueryAsync();
                    }

                }
                if (!(anket.subscribe ==false ))
                {
                    using (var command = new SqlCommand("UPDATE ANKETS SET subscribe = @subscribe WHERE id = @id", connection))
                    {
                        command.Parameters.Add("@id", SqlDbType.Int).Value = anket.id;
                        command.Parameters.Add("@subscribe", SqlDbType.Bit).Value = anket.subscribe;
                        await command.ExecuteNonQueryAsync();
                    }

                }
                if (!(anket.sex == null))
                {
                    using (var command = new SqlCommand("UPDATE ANKETS SET sex = @sex WHERE id = @id", connection))
                    {
                        command.Parameters.Add("@id", SqlDbType.Int).Value = anket.id;
                        command.Parameters.Add("@sex", SqlDbType.NText).Value = anket.sex;
                        await command.ExecuteNonQueryAsync();
                    }

                }
                if (!(anket.password == null))
                {
                    using (var command = new SqlCommand("UPDATE ANKETS SET password = @password WHERE id = @id", connection))
                    {
                        command.Parameters.Add("@id", SqlDbType.Int).Value = anket.id;
                        command.Parameters.Add("@password", SqlDbType.Text).Value = anket.password;
                        await command.ExecuteNonQueryAsync();
                    }

                }
                if (!(anket.tgusername == null))
                {
                    using (var command = new SqlCommand("UPDATE ANKETS SET tgusername = @tgusername WHERE id = @id", connection))
                    {
                        command.Parameters.Add("@id", SqlDbType.Int).Value = anket.id;
                        command.Parameters.Add("@tgusername", SqlDbType.Text).Value = anket.tgusername;
                        await command.ExecuteNonQueryAsync();
                    }

                }
                if (!(anket.latitude == null))
                {
                    using (var command = new SqlCommand("UPDATE ANKETS SET latitude = @latitude WHERE id = @id", connection))
                    {
                        command.Parameters.Add("@id", SqlDbType.Int).Value = anket.id;
                        command.Parameters.Add("@latitude", SqlDbType.Text).Value = anket.latitude;
                        await command.ExecuteNonQueryAsync();
                    }

                }
                if (!(anket.longtitude == null))
                {
                    using (var command = new SqlCommand("UPDATE ANKETS SET longtitude = @longtitude WHERE id = @id", connection))
                    {
                        command.Parameters.Add("@id", SqlDbType.Int).Value = anket.id;
                        command.Parameters.Add("@longtitude", SqlDbType.Text).Value = anket.longtitude;
                        await command.ExecuteNonQueryAsync();
                    }

                }
                if (!(anket.badhabbits == null))
                {
                    using (var command = new SqlCommand("UPDATE ANKETS SET badhabbits = @badhabbits WHERE id = @id", connection))
                    {
                        command.Parameters.Add("@id", SqlDbType.Int).Value = anket.id;
                        command.Parameters.Add("@badhabbits", SqlDbType.Text).Value = anket.badhabbits;
                        await command.ExecuteNonQueryAsync();
                    }

                }

            }
        }

        // Метод для удаления записи из таблицы ANKETS по id
        public async Task DeleteAnketAsync(int id)
        {
            // Создаем подключение к SQL Server
            using (var connection = new SqlConnection(connectionString))
            {
                // Открываем подключение
                await connection.OpenAsync();

                // Создаем команду для выполнения запроса с параметром @id
                using (var command = new SqlCommand("DELETE FROM ANKETS WHERE id = @id", connection))
                {
                    // Добавляем параметр @id и устанавливаем его значение
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    // Выполняем запрос
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}