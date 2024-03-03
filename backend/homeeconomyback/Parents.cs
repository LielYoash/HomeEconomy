using System;
using System.Collections.Generic;
using Npgsql;
using System.Net;

namespace homeeconomyback
{
    public class Parents : Person
    {
        private const string ConnectionString = "Host=dpg-cms30kol5elc73erq0pg-a.singapore-postgres.render.com;Port=5432;Username=user;Password=zglVIbTqI7VVHenTuPcZAQGAYSi5GgxI;Database=homeeconomy_neod;SSL Mode=Require;Trust Server Certificate=true;";

        protected List<Task> taskmanage;

        public Parents(string username, string firstname, string lastname, DateTime birth, int familyid) : base(username, firstname, lastname, birth, familyid)
        {
            this.taskmanage = new List<Task>();
            this.history = new List<Task>();
            this.RefreshMyTasks();
        }

        public Task AddNewTask(int familyid, int Responsible, string type, string TaskDescription, int managerusername, DateTime until)
        {
            Task task = new Task(0, type, TaskDescription, until, DateTime.MinValue, managerusername, Responsible);

            if (!(task.IsExistent()))
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Tasks (FamilyId, Description, DueDate, CreatedBy, Type, ResponsibleUser, CreatedAt) " +
                                   "VALUES (@familyid,  @Description, @DueDate, @CreatedBy, @Type, @ResponsibleUser , @CreatedAt);";

                    try
                    {
                        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@familyid", familyid);
                            command.Parameters.AddWithValue("@Description", TaskDescription);
                            command.Parameters.AddWithValue("@DueDate", until);
                            command.Parameters.AddWithValue("@CreatedBy", managerusername);
                            command.Parameters.AddWithValue("@Type", type);
                            command.Parameters.AddWithValue("@ResponsibleUser", Responsible);
                            command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                            command.ExecuteNonQuery();
                            this.RefreshMyTasks();
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

        public void UpdateThisTask(int id, string type, string description, DateTime until, int ResponsibleUser)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "UPDATE Tasks SET Type = @Type, Description = @description, " +
                               "DueDate = @until , ResponsibleUser = @ResponsibleUser " +
                               "WHERE id = @id";

                try
                {
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Type", type);
                        command.Parameters.AddWithValue("@description", description);
                        command.Parameters.AddWithValue("@until", until);
                        command.Parameters.AddWithValue("@ResponsibleUser", ResponsibleUser);
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                    }
                    this.RefreshMyTasks();
                }
                catch (Exception ex)
                {
                    // Log or handle the exception as needed
                    throw new Exception("Error updating specific task values in the database.", ex);
                }
            }
        }

        public List<Task> GetManageTask()
        {
            return this.taskmanage;
        }

        public override void RefreshMyTasks()
        {
            if (mytask == null)
            {
                this.mytask = new List<Task>();
            }
            if (taskmanage == null)
            {
                this.taskmanage = new List<Task>();
            }
            if (history == null)
            {
                this.history = new List<Task>();
            }
            mytask.Clear();
            taskmanage.Clear();
            history.Clear();

            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                int userId = GetIdFromUsername(this.email);
                string getManageTasksQuery = "SELECT Id, ResponsibleUser, Type,  Description, CreatedBy, DueDate FROM Tasks " +
                                             "WHERE CreatedBy = @UserId AND ExpiredAt is null AND DueDate > CURRENT_DATE ";

                using (NpgsqlCommand getTasksCommand = new NpgsqlCommand(getManageTasksQuery, connection))
                {
                    getTasksCommand.Parameters.AddWithValue("@UserId", userId);

                    using (NpgsqlDataReader reader = getTasksCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.taskmanage.Add(new Task(reader.GetInt32(0), reader.GetString(2), reader.GetString(3), reader.GetDateTime(5), DateTime.MinValue, reader.GetInt32(4), reader.GetInt32(1)));
                        }
                    }
                }

                string getMyTasksQuery = "SELECT Id, ResponsibleUser, Type,  Description, CreatedBy, DueDate FROM Tasks " +
                                         "WHERE ResponsibleUser = @UserId AND DueDate > CURRENT_DATE AND ExpiredAt is null";

                using (NpgsqlCommand getTasksCommand = new NpgsqlCommand(getMyTasksQuery, connection))
                {
                    getTasksCommand.Parameters.AddWithValue("@UserId", userId);

                    using (NpgsqlDataReader reader = getTasksCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.mytask.Add(new Task(reader.GetInt32(0), reader.GetString(2), reader.GetString(3), reader.GetDateTime(5), DateTime.MinValue, reader.GetInt32(4), reader.GetInt32(1)));
                        }
                    }
                }
                 getMyTasksQuery = "SELECT Id, ResponsibleUser, Type,  Description, CreatedBy, DueDate, ExpiredAt FROM Tasks " +
                                        "WHERE ResponsibleUser = @UserId AND ExpiredAt IS NOT NULL";

                using (NpgsqlCommand getTasksCommand = new NpgsqlCommand(getMyTasksQuery, connection))
                {
                    getTasksCommand.Parameters.AddWithValue("@UserId", userId);

                    using (NpgsqlDataReader reader = getTasksCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.history.Add(new Task(reader.GetInt32(0), reader.GetString(2), reader.GetString(3), reader.GetDateTime(5), reader.GetDateTime(6), reader.GetInt32(4), reader.GetInt32(1)));
                        }
                    }
                }
            }
        }
    }
}
