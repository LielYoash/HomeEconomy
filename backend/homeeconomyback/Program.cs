using System;
using System.Collections.Generic;
using Npgsql;
using NpgsqlTypes;

namespace homeeconomyback
{
    public class Program
    {
        private const string ConnectionString = "Host=dpg-cms30kol5elc73erq0pg-a.singapore-postgres.render.com;Port=5432;Username=user;Password=zglVIbTqI7VVHenTuPcZAQGAYSi5GgxI;Database=homeeconomy_neod;SSL Mode=Require;Trust Server Certificate=true;";

        public static List<string> GetAllFamilyNames()
        {
            List<string> familyNames = new List<string>();

            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT FamilyName FROM families";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string familyName = reader["FamilyName"].ToString();
                            familyNames.Add(familyName);
                        }
                    }
                }
            }

            return familyNames;
        }

        public static int AddFamily(string familyName)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                string checkIfExistsQuery = $"SELECT COUNT(*) FROM families WHERE FamilyName = '{familyName}';";
                NpgsqlCommand checkCommand = new NpgsqlCommand(checkIfExistsQuery, connection);
                int existingFamilyCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (existingFamilyCount == 0)
                {
                    string insertQuery = $"INSERT INTO families (FamilyName) VALUES ('{familyName}');";
                    NpgsqlCommand insertCommand = new NpgsqlCommand(insertQuery, connection);
                    insertCommand.ExecuteNonQuery();

                    return 1;
                }
                return -1;
            }
        }

        public static List<string> GetAllRoleNames()
        {
            List<string> roleNames = new List<string>();

            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT Name FROM roles";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string roleName = reader["Name"].ToString();
                            roleNames.Add(roleName);
                        }
                    }
                }
            }

            return roleNames;
        }

        public static int GetRoleIdByName(string roleName)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT Id FROM roles WHERE Name = @roleName";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@roleName", roleName);

                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            return -1;
        }

        public static int GetFamilyIdByName(string familyName)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT Id FROM families WHERE FamilyName = @FamilyName";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FamilyName", familyName);

                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }

            return -1;
        }
        public static string GetFamilyNameById(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT FamilyName FROM families WHERE Id = @Familyid";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Familyid", id);

                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        return result.ToString(); // Cast the result to string
                    }
                    else
                    {
                        // Handle the case where the result is null or DBNull.Value
                        // You might want to throw an exception, return a default value, or handle it accordingly
                        return ""; // Change this according to your logic
                    }
                }
            }
        }

        public static string GetFullNameById(int userId)
        {
            string fullName = null;

            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT FirstName, LastName FROM users WHERE Id = @UserId";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string firstName = reader["FirstName"].ToString();
                            string lastName = reader["LastName"].ToString();
                            fullName = $"{firstName} {lastName}";
                        }
                    }
                }
            }

            return fullName;
        }

        public static string GetUserRoleByusername(string username)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT r.Permissions " +
                           "FROM users u " +
                           "INNER JOIN roles r ON u.RoleId = r.Id " +
                           "WHERE u.Email = @Username";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    object result = command.ExecuteScalar();

                    return result != null ? Convert.ToString(result) : "";
                }
            }
        }

        public static string dbGetpassword(string username)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT Password FROM Users WHERE Email = @Username";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    object result = command.ExecuteScalar();

                    return result as string;
                }
            }
        }

        public static Person dbGetUserByUserName(string username)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT Email, FirstName, LastName, Birthdate, FamilyID FROM Users WHERE Email = @Username";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (GetUserRoleByusername(username) == "ALL_PERMISSIONS")
                            {
                                return new Parents(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3), reader.GetInt32(4));
                            }
                            return new Person(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3), reader.GetInt32(4));

                        }
                    }
                }
            }

            return null;
        }

        public static int AddUserToDb(int roleId, string username, string password, string firstname, string lastname, DateTime birth, string familyname)
        {
            string rightpassword = dbGetpassword(username);
            if (rightpassword == null)
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Users (RoleId, Email, Password, FirstName, LastName, Birthdate, CreatedAt, UpdatedAt, FamilyID) " +
                                   "VALUES (@role, @Username, @Password, @FirstName, @LastName, @Birthdate, @createdat, @updateat, @familyid );";

                    try
                    {
                        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@role", roleId);
                            command.Parameters.AddWithValue("@Username", username);
                            command.Parameters.AddWithValue("@Password", password);
                            command.Parameters.AddWithValue("@FirstName", firstname);
                            command.Parameters.AddWithValue("@LastName", lastname);
                        
                            command.Parameters.AddWithValue("@Birthdate", birth);
                            command.Parameters.AddWithValue("@createdat", DateTime.Now);
                            command.Parameters.AddWithValue("@updateat", DateTime.Now);
                            command.Parameters.AddWithValue("@familyid", GetFamilyIdByName(familyname));

                            command.ExecuteNonQuery();
                            return 1;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log or handle the exception as needed
                        throw new Exception("Error adding user to the database.", ex);
                    }
                }
            }
            return -1;
        }

        public static int LogIn(string username, string password)
        {
            string rightpassword = dbGetpassword(username);
            if (rightpassword != null)
            {
                if (password == rightpassword)
                {
                    return 1;
                }
                return -1;
            }
            return 0;
        }

        public static Person GetUserByUserName(string username, string password)
        {
            string rightpassword = dbGetpassword(username);
            if (rightpassword != null)
            {
                if (password == rightpassword)
                {
                    Person newperson = dbGetUserByUserName(username);
                    return newperson;
                }
                return null;
            }
            return null;
        }

        static void Main(string[] args)
        {
            // Your main program logic goes here
        }
    }
}
