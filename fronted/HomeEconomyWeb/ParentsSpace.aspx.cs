using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using homeeconomyback;


namespace HomeEconomyWeb
{
    public partial class ParentsSpace : System.Web.UI.Page
    {
        static int EncryptWithDateTime(int inputNumber)
        {
            DateTime currentDate = DateTime.Now;

            // Extract relevant components from the current date and time
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            int hour = DateTime.Now.Hour;

            // You can customize the coefficients based on your requirements
            int a = 2, b = 3, c = 5, d = 7, e = 11;

            // Linear encryption function: ax + by + cz + dw + ex
            int encryptedValue = a * year + b * month + c * day + d * hour + e;

            // Combine with the inputNumber (you can adjust the combination logic)
            encryptedValue += inputNumber;

            return encryptedValue;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Parents parents = Session["connect"] as Parents;

                if (parents != null)
                {
                    foreach (Task task in parents.GetMyTasks())
                    {
                        // Display task details (replace with your actual display logic)
                        string taskDetails = $"{task.Gettype()} - {task.GetDescription()} - Due: {task.GetUntil().ToShortDateString()}";
                        // Add logic to display taskDetails as needed
                    }

                    foreach (Notification notification in parents.GetNotifications())
                    {
                        // Display notification details (replace with your actual display logic)
                        string notificationDetails = notification.ToString();
                        // Add logic to display notificationDetails as needed
                    }
                    List<int> familyid = parents.GetFamily();
                    foreach (int id in familyid)
                    {
                        ListItem listItem = new ListItem(Program.GetFullNameById(id), id.ToString());
                        FamilyList.Items.Add(listItem);
                        sendtolist.Items.Add(listItem);
                        childDetail.Items.Add(listItem);

                    }
                    hellolbl.Text = "Hello " + parents.GetFirstname() ;
                    if (parents.GetBirth().Equals(DateTime.Now))
                    {
                        hellolbl.Text += " Happy " + parents.GetAge().ToString() +" Birthday";
                    }
                    if (parents.GetMyTasks().Count>0)
                    {
                        hellolbl.Text += " You have " + parents.GetMyTasks().Count.ToString() + " Tasks to do ";                            
                    }
                }

            }
        }

        // Event handler for the "Add Task" button click
        protected void AddTaskButton_Click(object sender, EventArgs e)
        {
            int dotaskid = Convert.ToInt32(FamilyList.SelectedValue);
            string Type = taskType.Value;
            string Description = taskDescription.Value;

            if (DateTime.TryParse(dueDate.Value, out DateTime date))
            {
                Parents parents = Session["connect"] as Parents;

                if (parents != null)
                {
                    parents.AddNewTask(parents.GetFamilyId(), dotaskid, Type, Description, Person.GetIdFromUsername(parents.GetEmail()), date);
                    Response.Redirect(Request.RawUrl);
                }
            }


        }
        protected void UpdateTaskButton_Click(object sender, EventArgs e)
        {
            int dotaskid = Convert.ToInt32(childDetail.SelectedValue);
            string Type = typeDetail.Value;
            string Description = descriptionDetail.Value;
            string str = taskidlable.Value;
            int taskid = int.Parse(str);
            if (DateTime.TryParse(dueDateDetail.Value, out DateTime date))
            {
                Parents parents = Session["connect"] as Parents;

                if (parents != null)
                {
                    parents.UpdateThisTask(taskid, Type, Description, date, dotaskid);
                    Response.Redirect(Request.RawUrl);
                }
            }


        }
        protected void AddNotificationButton_Click(object sender, EventArgs e)
        {
            Parents parent = Session["connect"] as Parents;

            if (parent != null)
            {
                int sendto = Convert.ToInt32(sendtolist.SelectedItem.Value);
                parent.AddNotification(sendto, notificationText.Value);
                Response.Redirect(Request.RawUrl);
            }
        }


        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            string id = iddone.Text;
            if (id != "")
            {
                Task.SetDone(int.Parse(id));
                iddone.Text = id + " has done";
                Parents child = Session["connect"] as Parents;

                if (child != null)
                {
                    child.RefreshMyTasks();
                }
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void invitebt_Click(object sender, EventArgs e)
        {
            Parents parents = Session["connect"] as Parents;

            if (parents != null)
            {
                if (invitebt.Text == "invite new family member")
                {
                    hellolbl.Text = "Temporary family code for an hour: " + EncryptWithDateTime(parents.GetFamilyId());
                    invitebt.Text = "hide code";
                }
                 else if(invitebt.Text == "hide code")
                {
                    hellolbl.Text = "Hello " + parents.GetFirstname();
                    if (parents.GetBirth().Equals(DateTime.Now))
                    {
                        hellolbl.Text += " Happy " + parents.GetAge().ToString() + " Birthday";
                    }
                    if (parents.GetMyTasks().Count > 0)
                    {
                        hellolbl.Text += " You have " + parents.GetMyTasks().Count.ToString() + " Tasks to do ";
                    }
                    invitebt.Text = "invite new family member";
                }
            }

            
        }
    }
}
