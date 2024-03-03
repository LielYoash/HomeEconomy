using System;
using System.Collections.Generic;
using Npgsql;

namespace homeeconomyback
{
    public class Task
    {
        private const string ConnectionString = "Host=dpg-cms30kol5elc73erq0pg-a.singapore-postgres.render.com;Port=5432;Username=user;Password=zglVIbTqI7VVHenTuPcZAQGAYSi5GgxI;Database=homeeconomy_neod;SSL Mode=Require;Trust Server Certificate=true;";

        private int id;
        private string type;
        private string description;
        private DateTime until;
        private DateTime done;
        private int managerId;
        private int dotaskId;

        public Task(int id, string type, string description, DateTime until, DateTime done, int managerid, int dotaskid)
        {
            this.id = id;
            this.type = type;
            this.description = description;
            this.until = until;
            this.done = done;
            this.managerId = managerid;
            this.dotaskId = dotaskid;
        }

        public string Gettype()
        {
            return this.type;
        }

        public void SetType(string type)
        {
            this.type = type;
        }

        public string GetDescription()
        {
            return this.description;
        }

        public void SetDescription(string description)
        {
            this.description = description;
        }

        public DateTime GetUntil()
        {
            return this.until;
        }

        public void SetUntil(DateTime date)
        {
            this.until = date;
        }

        public DateTime GetDone()
        {
            return this.done;
        }

        public static void SetDone(int idtask)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "UPDATE Tasks SET ExpiredAt = CURRENT_DATE WHERE Id = @idtask;";

                try
                {
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idtask", idtask);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Log or handle the exception as needed
                    throw new Exception("Error updating task status in the database.", ex);
                }
            }
        }

        public int GetManager()
        {
            return this.managerId;
        }

        public void SetManager(int manager)
        {
            this.managerId = manager;
        }

        public int GetDotask()
        {
            return this.dotaskId;
        }

        public void SetDotask(int dotask)
        {
            this.dotaskId = dotask;
        }

        public int GetId()
        {
            return this.id;
        }

        public bool IsExistent()
        {
            bool result = false;

            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM tasks WHERE Type = @TaskType AND Description = @TaskDescription";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TaskType", this.Gettype());
                    command.Parameters.AddWithValue("@TaskDescription", this.GetDescription());

                    int count = int.Parse(command.ExecuteScalar().ToString());
                    result = count > 0;
                }
            }

            return result;
        }
    }
}
