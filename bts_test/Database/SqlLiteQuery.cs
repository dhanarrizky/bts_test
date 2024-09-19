using System;
using System.Data.SQLite;
using bts_test.Models;

namespace bts_test.Database;
public class SqlLiteQuery {
    private string connectionString;
    private readonly IConfiguration _config;
    public SqlLiteQuery(IConfiguration config) {
        _config = config;
        connectionString = _config.GetConnectionString("bts_Connection");
    }
    
    // authentication
    public UserModel GetUserByUsername(string username){
        UserModel user = null;

        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string query = @"
                SELECT RegistrationID, Email, Username, Password
                FROM RegistrationRequest
                WHERE Username = @Username;";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel
                        {
                            RegistrationID = reader.GetInt32(0),
                            Email = reader.GetString(1),
                            Username = reader.GetString(2),
                            Password = reader.GetString(3)
                        };
                    }
                }
            }
        }

        return user;
    }
    // task

    public void RegisterUser(UserModel user) {
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            var insertUser = @"
                INSERT INTO RegistrationRequest (Email, Username, Password)
                VALUES (@Email, @Username, @Password);";

            using (var command = new SQLiteCommand(insertUser, connection))
            {
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Password", user.Password);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new InvalidOperationException("Failed to insert user into the database.");
                }
            }
        }
    }

}