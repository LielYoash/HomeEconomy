<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ParentsSpace.aspx.cs" Inherits="HomeEconomyWeb.ParentsSpace" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Parent Space</title>
    <style>
  body {
    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    background-color: #e0f7fa; /* Light blue-green background color */
    margin: 0;
    padding: 0;
    color: #2e7d32; /* Dark green text color */
}

#container {
  display: grid;
  grid-template-columns: 60% 40%; /* 60% for tasks, 40% for notifications */
  grid-gap: 20px; /* Add spacing between divs */
  padding: 20px;
}



#tasks {
    flex: 1;
    padding: 20px;
    background-color: #fff;
    box-shadow: 0 0 15px rgba(0, 0, 0, 0.1);
    border-radius: 12px;
}

h2 {
    margin-bottom: 20px;
    color: #2e7d32; /* Dark green header text color */
}

table {
    width: 100%;
    border-collapse: collapse;
    margin-top: 20px;
}

th, td {
    border: 1px solid #ddd;
    padding: 12px;
    text-align: left;
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
    color: #2e7d32; /* Dark green text color */
}

tr {
    transition: background-color 0.3s ease;
}

tr:hover {
    background-color: #f0f0f0;
}

th {
    background-color: #43a047; /* Dark green header background color */
    color: #fff;
}

td {
    border: none;
}

td:first-child {
    width: 30%;
    font-weight: bold;
    color: #2e7d32; /* Dark green text color */
}

td:nth-child(2), td:nth-child(3), td:nth-child(4) {
    width: 23%;
}
#AddNotificationButton
{
    background-color: #43a047; /* Dark green button color */
    color: #fff;
    border: none;
    padding: 10px;
    border-radius: 6px;
    cursor: pointer;
    transition: background-color 0.3s ease;
    margin-bottom:0px;
}
button,
#update,
#AddTaskButton {
    background-color: #43a047; /* Dark green button color */
    color: #fff;
    border: none;
    padding: 10px;
    border-radius: 6px;
    cursor: pointer;
    transition: background-color 0.3s ease;
}
#notifications {
  padding: 20px; 
  background-color: #fff;
  box-shadow: 0 0 15px rgba(0, 0, 0, 0.1);
  border-radius: 12px;
  margin-left: 20px; /* Add margin to separate it from the tasks div */
  margin-bottom: 0px;
  flex-grow: 1; /* Stretch to fill available space */
}


button:hover,
#update:hover,
#AddNotificationButton:hover,
#AddTaskButton:hover {
    background-color: #2e7d32; /* Dark green hover color */
}

form {
    margin-top: 20px;
}

label {
    display: block;
    margin-bottom: 8px;
    color: #2e7d32; /* Dark green text color */
}

input[type="text"],
.dropdown,
input[type="date"] {
    width: 100%;
    padding: 10px;
    margin-bottom: 15px;
    border: 1px solid #ddd;
    border-radius: 6px;
    box-sizing: border-box;
}

#taskDetailsModal,#taskhistory {
    display: none;
    position: fixed;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    background-color: #fff;
    padding: 20px;
    border: 1px solid #ddd;
    border-radius: 10px;
    z-index: 999;
}

    </style>
</head>
<body>
                    <form runat="server">

         <div id="greeting">
         <asp:Label ID="hellolbl" runat="server" Text="" style="font-weight: bold; color: #2e7d32;"></asp:Label>
                 <asp:Button ID="invitebt" runat="server" Text="invite new family member" OnClick="invitebt_Click" style="background-color: #43a047; color: #fff; border: none; padding: 10px; border-radius: 6px; cursor: pointer; transition: background-color 0.3s ease;" />
   </div>
          <div id="taskhistory" style="display: none; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%); background-color: #fff; padding: 20px; border: 1px solid #ddd; border-radius: 10px; z-index: 999;">
          <table>
              <tr> <td colspan="5">History tasks</td></tr>
     <tr>
         <th>Parents</th>
         <th>Type</th>
         <th>Description</th>
         <th>Due Date</th>
         <th> Expired At</th>
     </tr>
     <% var history = Session["connect"] as homeeconomyback.Person; %>
     <% if (history != null) { %>
         <% foreach (var task in history.GetHistoryTasks()) { %>
             <tr>
                 <td><%= homeeconomyback.Program.GetFullNameById(task.GetManager()) %></td>
                 <td><%= task.Gettype() %></td>
                 <td style="white-space: pre-line;"><%= task.GetDescription() %></td>
                 <td><%= task.GetUntil().ToShortDateString() %></td>
                 <td><%= task.GetDone().ToShortDateString() %></td>
             </tr>
         <% } %>
     <% } %>
 </table>
    <button onclick="closeHistoryTaskDetails();">Return</button>
</div>

   <div id="taskDetailsModal" style="display: none; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%); background-color: #fff; padding: 20px; border: 1px solid #ddd; border-radius: 10px; z-index: 999;">
    <h2>Task Details</h2>
    <p> <input type="text" id="taskidlable" runat="server"  /> </p>
    <p><strong>Child:</strong><asp:DropDownList ID="childDetail" CssClass="dropdown" runat="server"></asp:DropDownList></p>
    <p><strong>Type:</strong> <input type="text" id="typeDetail" runat="server"  /></p>
    <p><strong>Description:</strong> <input type="text" id="descriptionDetail" runat="server"  /></p>
    <p><strong>Due Date:</strong><input name="dueDateDetail" type="date" runat="server" id="dueDateDetail"  /></p>
    <asp:Button ID="update" runat="server" Text="Update Task" OnClientClick="return validateForm();" OnClick="UpdateTaskButton_Click" />

    <button onclick="closeTaskDetails();">Close</button>
</div>
    
        <div id="container">
            <div id="tasks">
                <h2>Your Tasks  <button onclick="return openTaskHistoryDetails();">See history </button></h2>
                <table>
                    <tr>
                        <th>Parents</th>
                        <th>Type</th>
                        <th>Description</th>
                        <th>Due Date</th>
                        <th></th>
                    </tr>
                    <% var child = Session["connect"] as homeeconomyback.Person; %>
                    <% if (child != null) { %>
                        <% foreach (var task in child.GetMyTasks()) { %>
                            <tr>
                                <td><%= homeeconomyback.Program.GetFullNameById(task.GetManager()) %></td>
                                <td><%= task.Gettype() %></td>
                                <td style="white-space: pre-line;"><%= task.GetDescription() %></td>
                                <td><%= task.GetUntil().ToShortDateString() %></td>
                                <td><button onclick="changeLabelText(<%= task.GetId().ToString() %>);">Set Done</button></td>
                            </tr>
                        <% } %>
                    <% } %>
                </table>
                <h2>Tasks you manage </h2>
                <table>
                    <tr>
                        <th>Child</th>
                        <th>Type</th>
                        <th>Description</th>
                        <th>Due Date</th>
                    </tr>
                    <% var parent = Session["connect"] as homeeconomyback.Parents; %>
                    <% if (parent != null) { %>
                        <% foreach (var task in parent.GetManageTask()) { %>
                            <tr onclick="openTaskDetails('<%= task.GetId() %>','<%= homeeconomyback.Program.GetFullNameById(task.GetDotask()) %>', '<%= task.Gettype() %>', '<%= task.GetDescription() %>', '<%= task.GetUntil().ToShortDateString() %>');">
                                <td><%= homeeconomyback.Program.GetFullNameById(task.GetDotask()) %></td>
                                <td><%= task.Gettype() %></td>
                                <td style="white-space: pre-line;"><%= task.GetDescription() %></td>
                                <td><%= task.GetUntil().ToShortDateString() %></td>
                            </tr>
                        <% } %>
                    <% } %>
                </table>

                <div id="addTaskForm">
                    <h2>Add Task</h2>
                    <label for="childList">Assign to family member:</label>
                    <asp:DropDownList ID="FamilyList" CssClass="dropdown" runat="server"></asp:DropDownList>
                    <label for="taskType">Task Type:</label>
                    <input type="text" id="taskType" runat="server" />
                    <label for="taskDescription">Task Description:</label>
                    <input type="text" id="taskDescription" runat="server" />
                    <label for="dueDate">Due Date:</label>
                    <input type="date" id="dueDate" runat="server" />
                    <asp:Button ID="AddTaskButton" runat="server" Text="Add Task" OnClick="AddTaskButton_Click" />
                    <asp:TextBox ID="iddone" runat="server" OnTextChanged="TextBox1_TextChanged" AutoPostBack="true" ></asp:TextBox>
                </div>
            </div>
           
            <div id="notifications">
                <h2>Notifications</h2>
             
    <table id="notificationsTable">
                    <tr>
                        <th>Date</th>
                        <th>Sender</th>
                        <th>Message</th>
                        <th>Target</th>
                    </tr>
        <tr>
<td></td>
            <td>    <input type="text" id="senderFilterInput" oninput="filterNotificationsBySender()" placeholder="Filter sender" /></td>
            <td></td>
            <td>    <input type="text" id="targetFilterInput" oninput="filterNotificationsByTarget()" placeholder="Filter target" /></td>
  

        </tr>
                    <% foreach (var notification in child.GetNotifications()) { %>
                        <tr>
                            <td><%= notification.DateCreated.ToString() %></td>
                            <td><%= (homeeconomyback.Program.GetFullNameById(notification.SenderId)) %></td>
                            <td style="white-space: pre-line;"><%= notification.Message %></td>
                            <td><%= (homeeconomyback.Program.GetFullNameById(notification.TargetId)) %></td>
                        </tr>
                    <% } %>
                </table>
                <h2>Add Notification</h2>
                <label for="notificationText">Notification Text:</label>
                <input type="text" id="notificationText" runat="server" />
                <label for="sendtolist">Send to:</label>
                <asp:DropDownList ID="sendtolist" CssClass="dropdown" runat="server"></asp:DropDownList>
                <asp:Button ID="AddNotificationButton" runat="server" Text="Add Notification" OnClick="AddNotificationButton_Click" />
            </div>
        </div>
            
    </form>
            <script>
                document.getElementById('<%= iddone.ClientID %>').style.display = 'none';
                function validateForm() {
                    var taskidlable = document.getElementById("taskidlable").value;
                    var childDetail = document.getElementById("childDetail").value;
                    var typeDetail = document.getElementById("typeDetail").value;
                    var descriptionDetail = document.getElementById("descriptionDetail").value;
                    var dueDateDetail = document.getElementById("dueDateDetail").value;

                    // Validate DropDownList
                    if (childDetail === "") {
                        alert("Please select a value for the Child field.");
                        return false;
                    }

                    // Validate Date
                    var currentDate = new Date().toISOString().split('T')[0];
                    if (dueDateDetail === "" || dueDateDetail < currentDate) {
                        alert("Please select a valid Due Date in the future.");
                        return false;
                    }

                    // Validate other required fields
                    if (taskidlable === "" || typeDetail === "" || descriptionDetail === "") {
                        alert("Please fill out all required fields.");
                        return false;
                    }

                    // Add more specific validation if needed

                    return true; // Form is valid, allow submission
                }
                function changeLabelText(id) {
                    var textBox = document.getElementById('<%= iddone.ClientID %>');
    textBox.value = id;
    __doPostBack('<%= iddone.ClientID %>', '');
}

function openTaskDetails(id,child, type, description, dueDate) {
    // Set task details in the modal
    document.getElementById('childDetail').value = child;
    document.getElementById('typeDetail').value = type;
    document.getElementById('descriptionDetail').value = description;
    document.getElementById('dueDateDetail').value = dueDate;
    document.getElementById('taskidlable').value = id;

    // Show the modal
    document.getElementById('taskDetailsModal').style.display = 'block';
    document.getElementById('<%= taskidlable.ClientID %>').style.display = 'none';
                    document.getElementById('notifications').style.display = 'none';
                    document.getElementById('tasks').style.display = 'none';
                    document.getElementById('addTaskForm').style.display = 'none';
                }


                function closeTaskDetails() {
                    // Hide the modal
                    document.getElementById('taskDetailsModal').style.display = 'none';
                    document.getElementById('tasks').style.display = 'block';
                    document.getElementById('notifications').style.display = 'block';
                    document.getElementById('addTaskForm').style.display = 'block';
                }


                function openTaskHistoryDetails() {
                    // Show the modal
                    document.getElementById('taskhistory').style.display = 'block';
                    document.getElementById('taskDetailsModal').style.display = 'none';
                    document.getElementById('notifications').style.display = 'none';
                    document.getElementById('tasks').style.display = 'none';
                    document.getElementById('addTaskForm').style.display = 'none';
                    return false;
                }
                function closeHistoryTaskDetails() {
                    // Hide the modal
                    document.getElementById('taskhistory').style.display = 'none';
                    document.getElementById('taskDetailsModal').style.display = 'none';
                    document.getElementById('tasks').style.display = 'block';
                    document.getElementById('notifications').style.display = 'block';
                    document.getElementById('addTaskForm').style.display = 'block';
                }
                function filterNotificationsBySender() {
                    // Get input value
                    var inputText = document.getElementById('senderFilterInput').value.toUpperCase();

                    // Get the table and rows
                    var table = document.getElementById('notificationsTable');
                    var rows = table.getElementsByTagName('tr');

                    // Loop through all table rows, and hide those that don't match the search query
                    for (var i = 2; i < rows.length; i++) { // Start from index 1 to skip the header row
                        var senderCell = rows[i].getElementsByTagName('td')[1]; // Assuming "Sender" is the second column (index 1)

                        // Get the sender cell value
                        var senderValue = senderCell.textContent || senderCell.innerText;

                        // Toggle row visibility based on the filter
                        rows[i].style.display = senderValue.toUpperCase().includes(inputText) ? '' : 'none';
                    }
                }
                function filterNotificationsByTarget() {
                    // Get input value
                    var inputText = document.getElementById('targetFilterInput').value.toUpperCase();

                    // Get the table and rows
                    var table = document.getElementById('notificationsTable');
                    var rows = table.getElementsByTagName('tr');

                    // Loop through all table rows, and hide those that don't match the search query
                    for (var i = 2; i < rows.length; i++) { // Start from index 1 to skip the header row
                        var senderCell = rows[i].getElementsByTagName('td')[3]; // Assuming "Sender" is the second column (index 1)

                        // Get the sender cell value
                        var senderValue = senderCell.textContent || senderCell.innerText;

                        // Toggle row visibility based on the filter
                        rows[i].style.display = senderValue.toUpperCase().includes(inputText) ? '' : 'none';
                    }
                }
            </script>
</body>
</html>
