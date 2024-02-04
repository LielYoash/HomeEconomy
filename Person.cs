using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace HomeEconomy
{
    public class Person
    {
        private const string ConnectionString = "";
        protected string username;
        protected string firstname;
        protected string lastname;
        protected DateTime birth;
        protected List<Task> mytask;

        public Person()
        {
            this.firstname = "";
            this.lastname = "";
            this.birth = new DateTime();
            this.mytask = new List<Task>();
        }
        public Person(string username, string firstname, string lastname, DateTime birth)
        {
            this.username = username;
            this.firstname = firstname;
            this.lastname = lastname;
            this.birth = birth;
            this.mytask = new List<Task>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string getUserIdQuery = "SELECT UserId FROM Users WHERE Username = @Username";

                using (SqlCommand getUserIdCommand = new SqlCommand(getUserIdQuery, connection))
                {
                    getUserIdCommand.Parameters.AddWithValue("@Username", this.username);
                    int userId = Convert.ToInt32(getUserIdCommand.ExecuteScalar());

                    string getTasksQuery = "SELECT TaskId, UserId, Type, done,  TaskDescription, managerid Until FROM Tasks " +
                                           "WHERE UserId = @UserId AND Until > GETDATE()";

                    using (SqlCommand getTasksCommand = new SqlCommand(getTasksQuery, connection))
                    {
                        getTasksCommand.Parameters.AddWithValue("@UserId", userId);

                        using (SqlDataReader reader = getTasksCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                this.mytask.Add(new Task(reader.GetString(2), reader.GetString(4), reader.GetDateTime(5), reader.GetBoolean(3), reader.GetInt32(5), reader.GetInt32(1)));

                            }
                        }
                    }
                }
            }
        }
        public string GetUsername()
        {
            return this.username;
        }
        public void SetUserName(string username)
        {
            this.username = username;
        }
        public string GetFirstname()
        {
            return this.firstname;
        }
        public void SetFirstname(string firstname)
        {
            this.firstname = firstname;
        }
        public string GetLastname()
        {
            return this.lastname;
        }
        public void SetLastname(string lastname)
        {
            this.lastname = lastname;
        }
        public DateTime GetBirth()
        {
            return this.birth;
        }
        public void SetBirth(DateTime birth)
        {
            this.birth = birth;
        }

        public int Getage()
        {
            DateTime now = DateTime.Now;
            DateTime birth = this.birth;
            if (now.Month - birth.Month < 0)
            {
                return now.Year - birth.Year - 1;
            }
            else if (now.Month - birth.Month > 0)
            {
                return now.Year - birth.Year;
            }
            else if (now.Day - birth.Day < 0)
            {
                return now.Year - birth.Year - 1;
            }
            else
            {
                return now.Year - birth.Year;
            }

        }
        public void addtolist(Task newtask)
        {
            this.mytask.Add(newtask);
        }
        public static int getidfromusername(string name)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT UserId FROM Users WHERE Username = @Username";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", name);

                    object result = command.ExecuteScalar();

                    return result != null ? Convert.ToInt32(result) : -1; // Return -1 if username not found
                }
            }
        }

    }
}
