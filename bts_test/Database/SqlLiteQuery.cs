using System;
using System.Data.SQLite;
using bts_test.Models;

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
}