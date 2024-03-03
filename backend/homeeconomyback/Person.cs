using System;
using System.Collections.Generic;
using Npgsql;
using System.Security.Cryptography;

namespace homeeconomyback
{
    public struct Notification
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int TargetId { get; set; }
        public string Message { get; set; }
        public DateTime DateCreated { get; set; }

        public override string ToString()
        {
            return $" Sender: {Program.GetFullNameById(SenderId)}, Target: {Program.GetFullNameById(TargetId)}, Message: {Message}, Date: {DateCreated}";
        }
    }

    public class Person
    {
        private const string ConnectionString = "Host=dpg-cms30kol5elc73erq0pg-a.singapore-postgres.render.com;Port=5432;Username=user;Password=zglVIbTqI7VVHenTuPcZAQGAYSi5GgxI;Database=homeeconomy_neod;SSL Mode=Require;Trust Server Certificate=true;";

        protected string email;
        protected string firstname;
        protected string lastname;
        protected int family;
        protected DateTime birth;
        protected List<int> myfamiliesid;
        protected List<Task> mytask;
        protected List<Task> history;
        protected List<Notification> mynotifications;

        public Person()
        {
            this.firstname = "";
            this.lastname = "";
            this.birth = new DateTime();
            this.mytask = new List<Task>();
        }

        public Person(string email, string firstname, string lastname, DateTime birth, int familyid)
        {
            this.family = familyid;
            this.myfamiliesid = new List<int>();
            this.email = email;
            this.firstname = firstname;
            this.lastname = lastname;
            this.birth = birth;
            this.mytask = new List<Task>();
            this.history = new List<Task>();
            mynotifications = new List<Notification>();
            UpdateNotifications();
            RefreshMyTasks();
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();
                string getmyfamilystr = "SELECT Id FROM users WHERE FamilyID = @FamilyID";

                using (NpgsqlCommand getmyfamily = new NpgsqlCommand(getmyfamilystr, connection))
                {
                    getmyfamily.Parameters.AddWithValue("@FamilyID", familyid);
                    using (NpgsqlDataReader reader = getmyfamily.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int userId = reader.GetInt32(reader.GetOrdinal("Id"));
                            this.myfamiliesid.Add(userId);
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

        public List<int> GetFamily()
        {
            return this.myfamiliesid;
        }

        public int GetFamilyId()
        {
            return this.family;
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

        public int GetAge()
        {
            DateTime now = DateTime.Now;
            DateTime birth = this.birth;

            if (now.Month < birth.Month || (now.Month == birth.Month && now.Day < birth.Day))
            {
                return now.Year - birth.Year - 1;
            }
            else
            {
                return now.Year - birth.Year;
            }
        }

        public List<Task> GetMyTasks()
        {
            return this.mytask;
        }
        public List<Task> GetHistoryTasks()
        {
            return this.history;
        }
        public static int GetIdFromUsername(string name)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT Id FROM Users WHERE Email = @email";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@email", name);

                    object result = command.ExecuteScalar();

                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }

        public List<Notification> GetNotifications()
        {
            return this.mynotifications;
        }

        public void AddNotification(int target, string message)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                string query = @"INSERT INTO notifications (sender_id, target_id, message, date_created)
                             VALUES (@SenderId, @TargetId, @Message, @DateCreated)";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SenderId", GetIdFromUsername(this.email));
                    command.Parameters.AddWithValue("@TargetId", target);
                    command.Parameters.AddWithValue("@Message", message);
                    command.Parameters.AddWithValue("@DateCreated", DateTime.Now);

                    command.ExecuteNonQuery();
                    UpdateNotifications();
                }
            }
        }

        public void UpdateNotifications()
        {
            mynotifications.Clear();

            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                string query = @"SELECT * FROM notifications
                             WHERE sender_id = @UserId OR target_id = @UserId";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", GetIdFromUsername(this.email));

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Notification notification = new Notification
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                SenderId = Convert.ToInt32(reader["sender_id"]),
                                TargetId = Convert.ToInt32(reader["target_id"]),
                                Message = reader["message"].ToString(),
                                DateCreated = Convert.ToDateTime(reader["date_created"])
                            };

                            mynotifications.Add(notification);
                        }
                    }
                }
            }
        }

        public virtual void RefreshMyTasks()
        {
            mytask.Clear();

            using (NpgsqlConnection connection = new NpgsqlConnection(ConnectionString))
            {
                mytask.Clear();
                history.Clear();
                connection.Open();

                int userId = GetIdFromUsername(this.email);

                string getmyTasksQuery = "SELECT Id, ResponsibleUser, Type,  Description, CreatedBy, DueDate FROM Tasks " +
                                  "WHERE ResponsibleUser = @UserId AND DueDate > CURRENT_DATE AND ExpiredAt is null";

                using (NpgsqlCommand getTasksCommand = new NpgsqlCommand(getmyTasksQuery, connection))
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
                 getmyTasksQuery = "SELECT Id, ResponsibleUser, Type,  Description, CreatedBy, DueDate, ExpiredAt  FROM Tasks " +
                                  "WHERE ResponsibleUser = @UserId  AND ExpiredAt IS NOT NULL";

                using (NpgsqlCommand getTasksCommand = new NpgsqlCommand(getmyTasksQuery, connection))
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
