using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace HomeEconomy
{
    class Program
    {

        private const string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\נבו\\Source\\Repos\\HomeEconomyBack2\\HomeEconomy\\HomeEconomyDB.mdf;Integrated Security=True";

        //פונקציות שהולכות לבסיס הנתונים

        /// <summary>
        /// מקבל שם משתמש ומחזיר ID
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>



        public static string GetUserRoleByusername(string username)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT r.Permissions " +
                           "FROM users u " +
                           "INNER JOIN roles r ON u.RoleId = r.Id " +
                           "WHERE u.Email = @Username";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    object result = command.ExecuteScalar();

                    return result != null ? Convert.ToString(result) : ""; // Return -1 if user not found
                }
            }
        }

        public static string dbGetpassword(string username)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT Password FROM Users WHERE Email = @Username";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    object result = command.ExecuteScalar();

                    return result as string; // Return null if username not found, or the password string if found
                }
            }
        }

        public static Person dbGetUserByUserName(string username)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT Email, FirstName, LastName, Birthdate FROM Users WHERE Email = @Username";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // User found in the database
                            if (GetUserRoleByusername(username) == "ALL_PERMISSIONS")
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
        public static int AddUsertodb(int roleid, string username, string password, string firstname, string lastname, DateTime birth)
        {
            string rightpassword = dbGetpassword(username);
            if (rightpassword == null)
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {


                    connection.Open();

                    string query = "INSERT INTO Users (RoleId, Email, Password, FirstName, LastName, Birthdate, CreatedAt, UpdatedAt) " +
                                   "VALUES (@role, @Username, @Password, @FirstName, @LastName, @Birthdate, @createdat, @updateat);";

                    try
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@role", roleid);
                            command.Parameters.AddWithValue("@Username", username);
                            command.Parameters.AddWithValue("@Password", password);
                            command.Parameters.AddWithValue("@FirstName", firstname);
                            command.Parameters.AddWithValue("@LastName", lastname);
                            string date = birth.Year.ToString() + "-" + birth.Month.ToString() + "-" + birth.Day.ToString();
                            command.Parameters.AddWithValue("@Birthdate", date);
                            date = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
                            command.Parameters.AddWithValue("@createdat", date);
                            command.Parameters.AddWithValue("@updateat", date);
                            string queryWithValues = command.CommandText;
                            foreach (SqlParameter parameter in command.Parameters)
                            {
                                queryWithValues = queryWithValues.Replace(parameter.ParameterName, parameter.Value.ToString());
                            }

                            Console.WriteLine(queryWithValues);
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
        public static Person LogIn(string username, string password)
        {
            string rightpassword = dbGetpassword(username);
            if (rightpassword != null)
            {
                if (password == rightpassword)
                {
                    //שליפת הנתונים של הבן אדם  והחזרתו למשתמש
                    Person newperson = dbGetUserByUserName(username);
                    return newperson;
                }
                return null;
            }
            return null;
        }



        static void Main(string[] args)
        {
            Person connect = LogIn("guygadassi@gmail.com", "guy12345678");
            if (connect is Parents)
            {
                Parents dad = (Parents)connect;

                int taskid = dad.GetMytask()[0].getid();
                dad.updatethistask(taskid, "Sport", "run around the city", new DateTime(2024, 2, 2), 2);

            }
        }
    
    }
}
