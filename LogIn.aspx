<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="HomeEconomyWeb.LogIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>Login</title>
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

        .btn-enter {
            background-color: #008CBA;
        }

        .btn-enter:hover {
            background-color: #0077a3;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnLogin">
        <div id="container">
            <h1>Login</h1>
            
            <label for="txtEmail">Email:</label>
            <input type="text" id="txtEmail" name="txtEmail" runat="server" placeholder="Enter your email" />

            <label for="txtPassword">Password:</label>
            <input type="password" id="txtPassword" name="txtPassword" runat="server" placeholder="Enter your password" />

            <div class="btn-container">
                <!-- Signup Button -->
                <asp:Button ID="btnSignup" runat="server" Text="Signup" OnClick="btnSignup_Click" CssClass="btn" />

                <!-- Login Button -->
                <asp:Button ID="btnLogin" runat="server" Text="Enter" OnClick="btnLogin_Click" CssClass="btn btn-enter" />
            </div>
            <asp:Label ID="Error" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
