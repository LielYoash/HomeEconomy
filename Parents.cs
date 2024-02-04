using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace HomeEconomy
{
    public class Parents : Person
    {
        private const string ConnectionString = "";

        protected List<Task> taskmanage;

        public Parents(string username, string firstname, string lastname, DateTime birth) : base(username, firstname, lastname, birth)
        {
            this.taskmanage = new List<Task>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string getUserIdQuery = "SELECT UserId FROM Users WHERE Username = @Username";

                using (SqlCommand getUserIdCommand = new SqlCommand(getUserIdQuery, connection))
                {
                    getUserIdCommand.Parameters.AddWithValue("@Username", this.username);
                    int userId = Convert.ToInt32(getUserIdCommand.ExecuteScalar());

                    string getTasksQuery = "SELECT TaskId, UserId, Type, done,  TaskDescription, managerid Until FROM Tasks " +
                                           "WHERE managerid = @UserId AND done = 0";

                    using (SqlCommand getTasksCommand = new SqlCommand(getTasksQuery, connection))
                    {
                        getTasksCommand.Parameters.AddWithValue("@UserId", userId);

                        using (SqlDataReader reader = getTasksCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                this.taskmanage.Add(new Task(reader.GetString(2), reader.GetString(4), reader.GetDateTime(5), reader.GetBoolean(3), reader.GetInt32(5), reader.GetInt32(1)));

                            }
                        }
                    }
                }
            }

        }


        public Task Addnewtask(string username, string type, string TaskDescription, string managerusername, DateTime until)
        {

            Task task = new Task(type, TaskDescription, until, false, getidfromusername(managerusername), getidfromusername(username));
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "INSERT INTO Tasks (UserId, Type, Done, TaskDescription, ManagerId, Until) " +
                               "VALUES (@UserId, @Type, @Done, @TaskDescription, @ManagerId, @Until)";

                try
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", task.getDotask());
                        command.Parameters.AddWithValue("@Type", task.Gettype());
                        command.Parameters.AddWithValue("@Done", false);
                        command.Parameters.AddWithValue("@TaskDescription", task.GetDescription());
                        command.Parameters.AddWithValue("@ManagerId", task.getManger());
                        command.Parameters.AddWithValue("@Until", task.getUntil());

                        command.ExecuteNonQuery();
                    }
                    return task;
                }
                catch (Exception ex)
                {
                    // Log or handle the exception as needed
                    throw new Exception("Error adding task to the database.", ex);
                }

            }
        }


    }
}