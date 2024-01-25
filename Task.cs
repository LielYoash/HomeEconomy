using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Org.BouncyCastle.Asn1.Mozilla;


namespace HomeEconomy
{
    public class Task
    {
        private const string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\נבו\\Source\\Repos\\HomeEconomyBack2\\HomeEconomy\\HomeEconomyDB.mdf;Integrated Security=True";

        private int id;
        private string type;
        private string description;
        private DateTime until;
        private DateTime done;
        private int managerId;// מחלק המשימה
        private int dotaskId;// מבצע המשימה


        public Task(int id,  string type, string description, DateTime until, DateTime done, int managerid, int dotaskid)
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
        public DateTime getUntil()
        {
            return this.until;
        }
        public void setUntil(DateTime date)
        {
            this.until = date;
        }
        public DateTime getDone()
        {
            return this.done;
        }
        public void setDone()
        {
            this.done = DateTime.Now;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "UPDATE Tasks SET Done = GETDATE() WHERE Id = @idtask ;";

                try
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idtask", this.id);
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
        public int getid()
        {
            return this.id;
        }
        public bool isexict()
        {
            bool result = false;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM tasks WHERE Type = @TaskType AND Description = @TaskDescription";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TaskType", this.Gettype());
                    command.Parameters.AddWithValue("@TaskDescription", this.GetDescription());

                    int count = (int)command.ExecuteScalar();
                    result = count > 0;
                }
            }

            return result;
        }
       

    }
}
