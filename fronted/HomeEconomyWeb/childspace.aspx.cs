using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using homeeconomyback;


namespace HomeEconomyWeb
{
    public partial class childspace : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve the child object from the session
                Person child = Session["connect"] as Person;

                if (child != null)
                {
                    // Display tasks for the child
                    foreach (Task task in child.GetMyTasks())
                    {
                        // Display task details (replace with your actual display logic)
                        string taskDetails = $"{task.Gettype()} - {task.GetDescription()} - Due: {task.GetUntil().ToShortDateString()}";
                        // Add logic to display taskDetails as needed
                    }

                    // Display notifications for the child
                    foreach (Notification notification in child.GetNotifications())
                    {
                        // Display notification details (replace with your actual display logic)
                        string notificationDetails = notification.ToString();
                        // Add logic to display notificationDetails as needed
                    }
                    List<int> familyid = child.GetFamily();
                    foreach (int id in familyid)
                    {
                        ListItem listItem = new ListItem(Program.GetFullNameById(id), id.ToString());
                        sendtolist.Items.Add(listItem);
                    }
                    hellolbl.Text = "Hello " + child.GetFirstname();
                    if (child.GetBirth().Month==DateTime.Now.Month && child.GetBirth().Day == DateTime.Now.Day)
                    {
                        hellolbl.Text += " Happy " + child.GetAge().ToString() + " Birthday";
                    }
                    if (child.GetMyTasks().Count > 0)
                    {
                        hellolbl.Text += " You have " + child.GetMyTasks().Count.ToString() + " Tasks to do ";
                    }
                }

            }
                
      
            
        }
    

        protected void AddNotificationButton_Click(object sender, EventArgs e)
        {
            // Retrieve the child object from the session
            Person child = Session["connect"] as Person;

            if (child != null)
            {
                int sendto = Convert.ToInt32(sendtolist.SelectedItem.Value);
                child.AddNotification(sendto, notificationText.Value);
                Response.Redirect(Request.RawUrl);
            }
        }
        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            string id = iddone.Text;
            if (id != "")
            {
                Task.SetDone(int.Parse(id));
                iddone.Text = "id has done";
                Person child = Session["connect"] as Person;

                if (child != null)
                {
                    child.RefreshMyTasks();
                }
                    Response.Redirect(Request.RawUrl);
            }
        }

    }
}