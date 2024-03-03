<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SingUp.aspx.cs" Inherits="HomeEconomyWeb.SingUp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Sign Up</title>
    <style>
        body {
            font-family: 'Arial', sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }

        #container {
            max-width: 400px;
            margin: 50px auto;
            background-color: #fff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);

        }

        h1 {
            color: #333;
            text-align: center;
        }

        label {
            display: block;
            margin-bottom: 8px;
            color: #333;
        }

        input[type="text"],
        input[type="password"] {
            width: 100%;
            padding: 10px;
            margin-bottom: 15px;
            box-sizing: border-box;
        }

        .dropdown {
            width: 100%;
            padding: 10px;
            margin-bottom: 15px;
            box-sizing: border-box;
        }

        .btn-container {
            text-align: center;
        }

        .btn {
            padding: 10px 20px;
            font-size: 16px;
            cursor: pointer;
            background-color: #4CAF50;
            color: #fff;
            border: none;
            border-radius: 4px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            transition-duration: 0.4s;
        }

        .btn:hover {
            background-color: #45a049;
            color: white;
        }
    </style>
    <script>
        function validateForm() {
            var roleName = document.getElementById('<%= ddlRole.ClientID %>').value;
            var email = document.getElementById('<%= txtEmail.ClientID %>').value;
            var password = document.getElementById('<%= txtPassword.ClientID %>').value;
            var firstName = document.getElementById('<%= txtFirstName.ClientID %>').value;
            var lastName = document.getElementById('<%= txtLastName.ClientID %>').value;
            var birthdate = document.getElementById('<%= txtBirthdate.ClientID %>').value;
            var familyCode = document.getElementById('<%= familycodetxt.ClientID %>').value;

            if (roleName === "" || email === "" || password === "" || firstName === "" || lastName === "" || birthdate === "" || familyCode === "") {
                alert("Please fill in all the required fields.");
                return false;
            }
            if (!isValidDate(birthdate)) {
                alert("Please enter a valid birthdate.");
                return false;
            }

            // Additional validation checks (e.g., password strength)
            return validatePassword();
        }

        function validatePassword() {
            var password = document.getElementById('<%= txtPassword.ClientID %>').value;

            // Add your password strength criteria here
            var strongRegex = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$");

            if (!strongRegex.test(password)) {
                alert("Password should be at least 8 characters long and include at least one lowercase letter, one uppercase letter, one number, and one special character.");
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="container">
            <h1>
                Sign Up</h1>

            &nbsp;

            <label for="ddlRole">Role Name:</label>
            <asp:DropDownList ID="ddlRole" CssClass="dropdown" runat="server"></asp:DropDownList>

            <label for="txtEmail">Email:</label>
            <input type="text" id="txtEmail" name="txtEmail" runat="server" />

            <label for="txtPassword">Password:</label>
            <input type="password" id="txtPassword" name="txtPassword" runat="server" onblur="validatePassword()"/>

            <label for="txtFirstName">First Name:</label>
            <input type="text" id="txtFirstName" name="txtFirstName" runat="server" />

            <label for="txtLastName">Last Name:</label>
            <input type="text" id="txtLastName" name="txtLastName" runat="server" />

            <label for="txtBirthdate">Birthdate:</label>
            <input type="text" id="txtBirthdate" name="txtBirthdate" runat="server" placeholder="YYYY-MM-DD" />
               <div id="addFamilySection">


            </div>
            <label for="ddlFamilyName">Family Name:</label>
             <input type="text" id="txtNewFamilyName" runat="server" Visible="false" />
            <label for="txtNewFamilyName">Family Code:</label>
               <input type="text" id="familycodetxt" name="txtNewFamilyName" runat="server" />
            <div class="btn-container">
                <asp:Label ID="error" runat="server"></asp:Label>
                <asp:Button ID="btnSignUp" runat="server" Text="Sign Up" OnClick="btnSignUp_Click" CssClass="btn" OnClientClick="return validateForm();" />
                 <asp:Button ID="btnAddFamily" runat="server" Text="Add NEW Family" CssClass="btn" OnClick="btnAddFamily_Click" />

            </div>
        </div>
    </form>
</body>
</html>