<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="childspace.aspx.cs" Inherits="HomeEconomyWeb.childspace" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Child Space</title>
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #e0f7fa; /* Light blue-green background color */
            margin: 0;
            padding: 0;
            color: #2e7d32; /* Dark green text color */
        }

        #container {
            display: flex;
            justify-content: space-between;
            align-items: flex-start;
            padding: 20px;
        }

        #tasks, #notifications {
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

        ul {
            list-style: none;
            padding: 0;
        }

        li {
            margin-bottom: 15px;
            padding: 15px;
            background-color: #ecf0f1;
            border-radius: 12px;
            transition: background-color 0.3s ease;
            cursor: pointer;
        }

        li:hover {
            background-color: #aed581; /* Light green hover color */
            color: #fff;
        }

        button {
            background-color: #43a047; /* Dark green button color */
            color: #fff;
            border: none;
            padding: 10px;
            border-radius: 6px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        button:hover {
            background-color: #2e7d32; /* Dark green hover color */
        }

        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
            table-layout: fixed;
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

        #notifications table {
            max-height: 400px;
            overflow-y: auto;
        }
        #notifications{
            margin-left:20px;
        }

        form {
            margin-top: 20px;
        }

        label {
            display: block;
            margin-bottom: 8px;
            color: #2e7d32; /* Dark green text color */
        }

        input[type="text"], .dropdown {
            width: 100%;
            padding: 10px;
            margin-bottom: 15px;
            border: 1px solid #ddd;
            border-radius: 6px;
            box-sizing: border-box;
        }

        #AddNotificationButton {
            background-color: #43a047; /* Dark green button color */
            color: #fff;
            border: none;
            padding: 10px 15px;
            border-radius: 6px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        #AddNotificationButton:hover {
            background-color: #2e7d32; /* Dark green hover color */
        }
        #taskDetailsModal {
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

   </div>
       <div id="taskDetailsModal" style="display: none; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%); background-color: #fff; padding: 20px; border: 1px solid #ddd; border-radius: 10px; z-index: 999;">
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
    <button onclick="closeTaskDetails();">Return</button>
</div>
    <div id="container">
        <div id="tasks">
            <h2>Your Tasks To Do!   <button onclick="openTaskDetails();">See history </button></h2>
            <ul>
                <% var child = Session["connect"] as homeeconomyback.Person; %>
                <% if (child != null) { %>
                    <% foreach (var task in child.GetMyTasks()) { %>
                        <li>
                            Parents: <%= homeeconomyback.Program.GetFullNameById(task.GetManager()) %>,
                            Type: <%= task.Gettype() %>,
                            Description: <%= task.GetDescription() %>,
                            Due: <%= task.GetUntil().ToShortDateString() %>
                            <button onclick="changeLabelText(<%= task.GetId().ToString()%>);">Set Done</button>
                        </li>
                    <% } %>
                <% } %>
            </ul>
           
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
            <td>    <input type="text" id="senderFilterInput" oninput="filterNotificationsBySender()" placeholder="filter sender" /></td>
            <td></td>
            <td>    <input type="text" id="targetFilterInput" oninput="filterNotificationsByTarget()" placeholder="filter target" /></td>
  

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
                <asp:TextBox ID="iddone" runat="server" OnTextChanged="TextBox1_TextChanged" AutoPostBack="true"></asp:TextBox>
            </form>
        </div>
    </div>
     <script>

         function changeLabelText(id) {
             var textBox = document.getElementById('<%= iddone.ClientID %>');
         textBox.value = id;
         __doPostBack('<%= iddone.ClientID %>', '');
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
     function openTaskDetails() {
         // Show the modal
         document.getElementById('taskDetailsModal').style.display = 'block';
          document.getElementById('notifications').style.display = 'none';
        document.getElementById('tasks').style.display = 'none';
     }
     function closeTaskDetails() {
         // Hide the modal
         document.getElementById('taskDetailsModal').style.display = 'none';
         document.getElementById('tasks').style.display = 'block';
         document.getElementById('notifications').style.display = 'block';
     }
     document.getElementById('<%= iddone.ClientID %>').style.display = 'none';
     </script>
</body>
</html>
