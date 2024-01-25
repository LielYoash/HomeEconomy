using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace HomeEconomy
{
    public class Parents : Person
    {
        
        private const string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\נבו\\Source\\Repos\\HomeEconomyBack2\\HomeEconomy\\HomeEconomyDB.mdf;Integrated Security=True";

        protected List<Task> taskmanage;
        protected List<int> myfamilyid;//all the id that the parents can give tasks
        protected string familyrole; //name of role in the house dad\mom

        public Parents(string username, string firstname, string lastname, DateTime birth) : base(username, firstname, lastname, birth)
        {
            this.myfamilyid = new List<int>();
            this.taskmanage = new List<Task>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string getUserIdQuery = "SELECT Id FROM Users WHERE Email = @Username";

                using (SqlCommand getUserIdCommand = new SqlCommand(getUserIdQuery, connection))
                {
                    getUserIdCommand.Parameters.AddWithValue("@Username", this.email);
                    int userId = Convert.ToInt32(getUserIdCommand.ExecuteScalar());

                    string getTasksQuery = "SELECT Id, ResponsibleUser, Type,  Description, CreatedBy, DueDate FROM Tasks " +
                                           "WHERE CreatedBy = @UserId AND ExpiredAt is null ";

                    using (SqlCommand getTasksCommand = new SqlCommand(getTasksQuery, connection))
                    {
                        getTasksCommand.Parameters.AddWithValue("@UserId", userId);

                        using (SqlDataReader reader = getTasksCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                this.taskmanage.Add(new Task(reader.GetInt32(0), reader.GetString(2), reader.GetString(3), reader.GetDateTime(5), DateTime.MinValue, reader.GetInt32(4), reader.GetInt32(1)));

                            }
                        }
                    }


                }
                string getLasksQuery = "SELECT r.Name " +
                                             " FROM users u " +
                                           "INNER JOIN roles r ON u.RoleId = r.Id" +
                              " WHERE u.Email = @username; ";
                using (SqlCommand getTasksCommand = new SqlCommand(getLasksQuery, connection))
                {
                    getTasksCommand.Parameters.AddWithValue("@username", username);

                    this.familyrole = Convert.ToString(getTasksCommand.ExecuteScalar());
                }
                string getmyfamilystr = "SELECT Id FROM users WHERE LastName = @LastName";

                using (SqlCommand getmyfamily = new SqlCommand(getmyfamilystr, connection))
                {
                    getmyfamily.Parameters.AddWithValue("@LastName",lastname);
                    using (SqlDataReader reader = getmyfamily.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int userId = reader.GetInt32(reader.GetOrdinal("Id"));
                            this.myfamilyid.Add(userId);
                        }
                    }
                }
            }

        }
        public List<int> GetFamily()
        {
            return this.myfamilyid;
        }

        public Task Addnewtask(int familyid ,int Responsible, string type, string TaskDescription, int managerusername, DateTime until)
        {
            Task task = new Task(0,type, TaskDescription, until, DateTime.MinValue, managerusername, Responsible);

            if (!(task.isexict())) {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Tasks (FamilyId, Description, DueDate, CreatedBy,Type , ResponsibleUser, CreatedAt) " +
                                   "VALUES (@familyid,  @Description, @DueDate, @CreatedBy, @Type, @ResponsibleUser , @CreatedAt);";

                    try
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@familyid", familyid);
                            command.Parameters.AddWithValue("@Description", TaskDescription);
                            string date = until.Year.ToString() + "-" + until.Month.ToString() + "-" + until.Day.ToString();
                            command.Parameters.AddWithValue("@DueDate", date);
                            command.Parameters.AddWithValue("@CreatedBy", managerusername);
                            command.Parameters.AddWithValue("@Type", type);
                            command.Parameters.AddWithValue("@ResponsibleUser", Responsible);
                            date = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
                            command.Parameters.AddWithValue("@CreatedAt", date);

                            
                            command.ExecuteNonQuery();

                            return task;
                        }

                    }
                    catch (Exception ex)
                    {
                        // Log or handle the exception as needed
                        throw new Exception("Error adding task to the database.", ex);
                    }
                }
            }
            return null;
        }
        public void updatethistask(int id, string type, string description, DateTime until, int ResponsibleUser)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "UPDATE Tasks SET Type = @Type, Description = @description, " +
                               "DueDate = @until , ResponsibleUser = @ResponsibleUser " +
                               "WHERE id = @id";

                try
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Type", type);
                        command.Parameters.AddWithValue("@description", description);
                        string date = until.Year.ToString() + "-" + until.Month.ToString() + "-" + until.Day.ToString();
                        command.Parameters.AddWithValue("@until", until);
                        command.Parameters.AddWithValue("@ResponsibleUser", ResponsibleUser);
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Log or handle the exception as needed
                    throw new Exception("Error updating specific task values in the database.", ex);
                }
            }
        }


    }
}