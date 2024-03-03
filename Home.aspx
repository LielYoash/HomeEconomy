<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="HomeEconomyWeb.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
 <form id="form1" runat="server">
        <div id="container" runat="server">
            <h1 runat="server">Home Economy</h1>
            <p runat="server">
                Welcome to our website! We are dedicated to providing valuable information and services.
            </p>
            <!-- Add more information about your organization/company here -->

            <div runat="server" class="btn-container">
                <!-- Login Button -->
                <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click"  />

                <!-- Signup Button -->
                <asp:Button ID="btnSignup" runat="server" Text="Signup" OnClick="btnSignup_Click" />
            </div>
        </div>
    </form>
</body>
</html>
