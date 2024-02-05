using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;  


namespace HomeEconomy
{
    class Program
    {
        private const string ConnectionString = "";

        //פונקציות שהולכות לבסיס הנתונים

        /// <summary>
        /// מקבל שם משתמש ומחזיר ID
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>



        public static int GetUserRoleByusername(string username)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT Role FROM Users WHERE Username = @Username";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    object result = command.ExecuteScalar();

                    return result != null ? Convert.ToInt32(result) : -1; // Return -1 if user not found
                }
            }
        }

        public static string dbGetpassword(string username)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT Password FROM Users WHERE Username = @Username";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    object result = command.ExecuteScalar();

                    return result as string; // Return null if username not found, or the password string if found
                }
            }
        }

        public static Person dbGetUserByUserName(string username, int role)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT UserName, FirstName, LastName, Birth FROM Users WHERE UserName = @Username";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // User found in the database
                            if (role == 1)
                            {
                                return new Parents(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3));
                            }
                            return new Person(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3));

                        }
                    }
                }
            }
            // Return null if user not found
            return null;
        }

        /// <summary>
        /// הוספת משתמש
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="birth"></param>
        /// <returns>-1 אם שם המשתמש קיים
        /// 1 אם הצליח להוסיף משתמש</returns>
        public static int AddUsertodb(string username, string password, string firstname, string lastname, DateTime birth)
        {
            string rightpassword = dbGetpassword(username);
            if (rightpassword == null)
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {


                    connection.Open();

                    string query = "INSERT INTO Users (Password, FirstName, LastName, Birthdate) " +
                                   "VALUES (@Username, @Password, @FirstName, @LastName, @Birthdate)";

                    try
                    {
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Password", password);
                            command.Parameters.AddWithValue("@FirstName", firstname);
                            command.Parameters.AddWithValue("@LastName", lastname);
                            command.Parameters.AddWithValue("@Birthdate", birth);

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


        //פונקציות התחברות והרשמה


        /// <summary>
        /// אם הוחזר 1 שם משתמש לא קיים
        /// אם הוחזר -1 סיסמה לא נכונה
        /// </summary>
        /// <param name="username">קלט שם משתמש</param>
        /// <param name="password">קלט סיסמה</param>
        /// <returns>משתמש בהתאם וההתחברות הצליחה</returns>
        public static object LogIn(string username, string password)
        {
            string rightpassword = dbGetpassword(username);
            if (rightpassword != null)
            {
                if (password == rightpassword)
                {
                    //שליפת הנתונים של הבן אדם  והחזרתו למשתמש
                    Person newperson = dbGetUserByUserName(username, GetUserRoleByusername(username));
                    return newperson;
                }
                return -1;
            }
            return 1;
        }



        static void Main(string[] args)
        {
            Person yuval = new Person("yuval", "nevso", "gadassi", new DateTime(2003, 10, 01));
            Console.WriteLine(yuval.Getage());
            AddUsertodb("nevogad4", "123456", "Nevo", "Gadassi", new DateTime(2003, 10, 1));
        }

        // בפעולת התחברות להחזיר משתמש Person אם משימות שעדיין לא בוצעו
    }
}
