using System;
using System.Data.SQLite;
using Microsoft.Extensions.Configuration;

namespace bts_test.Database;
public class SqlLiteConnection
{
    private string connectionString;
    private readonly IConfiguration _config;

    public SqlLiteConnection(IConfiguration config) {
        _config = config;
        connectionString = _config.GetConnectionString("bts_Connection");
    }

    public void CreateTable()
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string[] createTableQueries = new string[]
            {
                @"
                CREATE TABLE IF NOT EXISTS LoginRequest (
                    LoginID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Username TEXT NOT NULL,
                    Password TEXT NOT NULL
                );",
                
                @"
                CREATE TABLE IF NOT EXISTS RegistrationRequest (
                    RegistrationID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Email TEXT NOT NULL,
                    Username TEXT NOT NULL,
                    Password TEXT NOT NULL
                );",
                
                @"
                CREATE TABLE IF NOT EXISTS Titles (
                    TitleID INTEGER PRIMARY KEY AUTOINCREMENT,
                    TitleName TEXT
                );",
                
                @"
                CREATE TABLE IF NOT EXISTS Tasks (
                    TaskID INTEGER PRIMARY KEY AUTOINCREMENT,
                    TaskDescription TEXT,
                    IsCompleted INTEGER,
                    TitleID INTEGER,
                    FOREIGN KEY (TitleID) REFERENCES Titles(TitleID)
                );"
            };

            foreach (var query in createTableQueries)
            {
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Tables created successfully.");
        }
    }

    public void InsertDummyData()
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string[] insertQueries = new string[]
            {
                @"
                INSERT INTO LoginRequest (Username, Password)
                VALUES (@Username, @Password);",

                @"
                INSERT INTO RegistrationRequest (Email, Username, Password)
                VALUES (@Email, @Username, @Password);",

                @"
                INSERT INTO Titles (TitleName)
                VALUES (@TitleName);",

                @"
                INSERT INTO Tasks (TaskDescription, IsCompleted, TitleID)
                VALUES (@TaskDescription, @IsCompleted, @TitleID);"
            };

            using (var command = new SQLiteCommand(insertQueries[0], connection))
            {
                command.Parameters.AddWithValue("@Username", "user1");
                command.Parameters.AddWithValue("@Password", "password1");
                command.ExecuteNonQuery();

                command.Parameters["@Username"].Value = "user2";
                command.Parameters["@Password"].Value = "password2";
                command.ExecuteNonQuery();
            }

            using (var command = new SQLiteCommand(insertQueries[1], connection))
            {
                command.Parameters.AddWithValue("@Email", "email1@example.com");
                command.Parameters.AddWithValue("@Username", "reguser1");
                command.Parameters.AddWithValue("@Password", "regpassword1");
                command.ExecuteNonQuery();

                command.Parameters["@Email"].Value = "email2@example.com";
                command.Parameters["@Username"].Value = "reguser2";
                command.Parameters["@Password"].Value = "regpassword2";
                command.ExecuteNonQuery();
            }

            using (var command = new SQLiteCommand(insertQueries[2], connection))
            {
                command.Parameters.AddWithValue("@TitleName", "Title1");
                command.ExecuteNonQuery();

                command.Parameters["@TitleName"].Value = "Title2";
                command.ExecuteNonQuery();
            }

            using (var command = new SQLiteCommand(insertQueries[3], connection))
            {
                command.Parameters.AddWithValue("@TaskDescription", "Task 1");
                command.Parameters.AddWithValue("@IsCompleted", 0);
                command.Parameters.AddWithValue("@TitleID", 1);
                command.ExecuteNonQuery();

                command.Parameters["@TaskDescription"].Value = "Task 2";
                command.Parameters["@IsCompleted"].Value = 1;
                command.Parameters["@TitleID"].Value = 2;
                command.ExecuteNonQuery();
            }

            Console.WriteLine("Dummy data inserted successfully.");
        }
    }
}
