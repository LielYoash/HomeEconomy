using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace HomeEconomy
{
    public class Task
    {
        private const string ConnectionString = "YourConnectionStringHere"; // Replace with your actual connection string

        private string type;
        private string description;
        private DateTime until;
        private bool done;
        private int managerId;// מחלק המשימה
        private int dotaskId;// מבצע המשימה


        public Task(string type, string description, DateTime until, bool done, int managerid, int dotaskid)
        {
            this.type = type;
            this.description = description;
            this.until = until;
            this.done = done;
            this.managerId = managerid;
            this.dotaskId  = dotaskid;
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
        public DateTime getUntil()
        {
            return this.until;
        }
        public void setUntil(DateTime date)
        {
            this.until = date;
        }
        public bool getDone()
        {
            return this.done;
        }
        public void setDone()
        {
            this.done = true;
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "UPDATE Tasks SET Done = 1 WHERE Type = @Type AND TaskDescription = @TaskDescription";

                try
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Type", this.type);
                        command.Parameters.AddWithValue("@TaskDescription", this.description);

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
        public int getManger()
        {
            return this.managerId;
        }
        public void setManger(int manager)
        {
            this.managerId = manager;
        }
        public int getDotask()
        {
            return this.dotaskId;
        }
        public void setDotask(int dotask)
        {
            this.dotaskId = dotask;
        }
        public bool isexist()
        {
            return true;
        }
        public void updatealltask()
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "UPDATE Tasks SET UserId = @UserId, Type = @Type, Done = @Done, " +
                               "TaskDescription = @TaskDescription, ManagerId = @ManagerId, Until = @Until";

                try
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", this.dotaskId);
                        command.Parameters.AddWithValue("@Type", this.type);
                        command.Parameters.AddWithValue("@Done", this.done);
                        command.Parameters.AddWithValue("@TaskDescription", this.description);
                        command.Parameters.AddWithValue("@ManagerId", this.managerId);
                        command.Parameters.AddWithValue("@Until", this.until);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Log or handle the exception as needed
                    throw new Exception("Error updating all task values in the database.", ex);
                }
            }
        }

    }
}
