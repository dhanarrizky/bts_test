using System;
using System.Data.SQLite;

public class SqlLiteConnection
{
    private string connectionString = "Data Source=bts_test_db.db";

    public SqlLiteConnection() {}

    // createing tables if not exsist
    public void CreateTable()
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string createLoginRequestTable = @"
                CREATE TABLE IF NOT EXISTS LoginRequest (
                    LoginID INTEGER PRIMARY KEY AUTOINCREMENT,  
                    Username TEXT NOT NULL,        
                    Password TEXT NOT NULL         
                );";

            string createRegistrationRequestTable = @"
                CREATE TABLE IF NOT EXISTS RegistrationRequest (
                    RegistrationID INTEGER PRIMARY KEY AUTOINCREMENT,  
                    Email TEXT NOT NULL,                  
                    Username TEXT NOT NULL,               
                    Password TEXT NOT NULL                
                );";

            string createTitlesTable = @"
                CREATE TABLE IF NOT EXISTS Titles (
                    TitleID INTEGER PRIMARY KEY AUTOINCREMENT,
                    TitleName TEXT
                );";

            string createTasksTable = @"
                CREATE TABLE IF NOT EXISTS Tasks (
                    TaskID INTEGER PRIMARY KEY AUTOINCREMENT,
                    TaskDescription TEXT,
                    IsCompleted INTEGER,
                    TitleID INTEGER,
                    FOREIGN KEY (TitleID) REFERENCES Titles(TitleID)
                );";

            using (var command = connection.CreateCommand())
            {
                command.CommandText = createLoginRequestTable;
                command.ExecuteNonQuery();

                command.CommandText = createRegistrationRequestTable;
                command.ExecuteNonQuery();

                command.CommandText = createTitlesTable;
                command.ExecuteNonQuery();

                command.CommandText = createTasksTable;
                command.ExecuteNonQuery();
            }

            Console.WriteLine("Tables created successfully.");
        }
    }

    // insert dummy data
    public void InsertDummyData()
    {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string insertLoginRequest = @"
                INSERT INTO LoginRequest (Username, Password)
                VALUES (@Username, @Password);";

            using (var command = new SQLiteCommand(insertLoginRequest, connection))
            {
                command.Parameters.AddWithValue("@Username", "user1");
                command.Parameters.AddWithValue("@Password", "password1");
                command.ExecuteNonQuery();

                command.Parameters["@Username"].Value = "user2";
                command.Parameters["@Password"].Value = "password2";
                command.ExecuteNonQuery();
            }

            string insertRegistrationRequest = @"
                INSERT INTO RegistrationRequest (Email, Username, Password)
                VALUES (@Email, @Username, @Password);";

            using (var command = new SQLiteCommand(insertRegistrationRequest, connection))
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

            string insertTitles = @"
                INSERT INTO Titles (TitleName)
                VALUES (@TitleName);";

            using (var command = new SQLiteCommand(insertTitles, connection))
            {
                command.Parameters.AddWithValue("@TitleName", "Title1");
                command.ExecuteNonQuery();

                command.Parameters["@TitleName"].Value = "Title2";
                command.ExecuteNonQuery();
            }

            string insertTasks = @"
                INSERT INTO Tasks (TaskDescription, IsCompleted, TitleID)
                VALUES (@TaskDescription, @IsCompleted, @TitleID);";

            using (var command = new SQLiteCommand(insertTasks, connection))
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
