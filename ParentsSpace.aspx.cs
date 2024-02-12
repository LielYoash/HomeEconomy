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

    }
}
