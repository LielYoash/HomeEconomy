using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Security.Cryptography;


namespace HomeEconomy
{
    public class Person
    {
        private const string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\נבו\\Source\\Repos\\HomeEconomyBack2\\HomeEconomy\\HomeEconomyDB.mdf;Integrated Security=True";
        protected string email;
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
        public Person(string email, string firstname, string lastname, DateTime birth)
        {
            this.email = email;
            this.firstname = firstname;
            this.lastname = lastname;
            this.birth = birth;
            this.mytask = new List<Task>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string getUserIdQuery = "SELECT Id FROM Users WHERE Email = @email";

                using (SqlCommand getUserIdCommand = new SqlCommand(getUserIdQuery, connection))
                {

                    getUserIdCommand.Parameters.AddWithValue("@email", this.email);
                    int userId = Convert.ToInt32(getUserIdCommand.ExecuteScalar());

                    string getTasksQuery = "SELECT Id, ResponsibleUser, Type,  Description, CreatedBy, DueDate FROM Tasks " +
                                           "WHERE ResponsibleUser = @UserId AND DueDate > GETDATE() AND ExpiredAt is null";

                    using (SqlCommand getTasksCommand = new SqlCommand(getTasksQuery, connection))
                    {
                        getTasksCommand.Parameters.AddWithValue("@UserId", userId);

                        using (SqlDataReader reader = getTasksCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                this.mytask.Add(new Task(reader.GetInt32(0),reader.GetString(2), reader.GetString(3), reader.GetDateTime(5), DateTime.MinValue, reader.GetInt32(4), reader.GetInt32(1)));

                            }
                        }
                    }
                }
            }
        }
        public string GetEmail()
        {
            return this.email;
        }
        public void SetEmail(string email)
        {
            this.email = email;
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
        public List<Task> GetMytask()
        {
            return this.mytask;
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

                string query = "SELECT Id FROM Users WHERE Email = @email";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@email", name);

                    object result = command.ExecuteScalar();

                    return result != null ? Convert.ToInt32(result) : -1; // Return -1 if username not found
                }
            }
        }

    }
}
