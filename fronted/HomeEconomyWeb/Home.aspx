<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="HomeEconomyWeb.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home Economy</title>
    <style>
        body {
            font-family: 'Arial', sans-serif;
            background-color: #f7f7f7;
            margin: 0;
            padding: 0;
            color: #333;
        }

        #container {
            max-width: 800px;
            margin: 50px auto;
            padding: 20px;
            background-color: #fff;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            border-radius: 10px;
            opacity: 0;
            animation: fadeIn 1s forwards;
        }

        @keyframes fadeIn {
            to {
                opacity: 1;
            }
        }

        h1, h2 {
            color: #2c3e50;
            animation: bounce 1s infinite;
        }

        @keyframes bounce {
            0%, 20%, 50%, 80%, 100% {
                transform: translateY(0);
            }
            40% {
                transform: translateY(-20px);
            }
            60% {
                transform: translateY(-10px);
            }
        }

        p {
            line-height: 1.6;
        }

        ul {
            list-style-type: none;
            padding: 0;
        }

        li {
            margin-bottom: 10px;
        }

        .btn-container {
            margin-top: 20px;
            text-align: center;
        }

        #btnLogin, #btnSignup {
            background-color: #3498db;
            color: #fff;
            padding: 10px 20px;
            font-size: 16px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            transition: transform 0.3s, background-color 0.3s;
        }

        #btnLogin:hover, #btnSignup:hover {
            background-color: #2980b9;
            transform: scale(1.1);
        }
    </style>
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            // Add JavaScript code if needed
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="container" runat="server">
            <h1 runat="server">Home Economy</h1>
            <p runat="server">
                Welcome to Home Economy, your reliable companion in managing and simplifying tasks within your family. At Home Economy, we understand the intricate dynamics of a household and the countless responsibilities that come with it. Our mission is to empower you with the tools and resources to streamline tasks, foster collaboration, and enhance the overall well-being of your family.
            </p>
            
            <h2 runat="server">Our Vision</h2>
            <p runat="server">
                We envision a world where every family experiences a harmonious balance in managing their day-to-day activities. Home Economy strives to be the go-to platform that not only eases the burden of domestic responsibilities but also strengthens the bonds within your family.
            </p>
            
            <h2 runat="server">What We Offer</h2>
            
            <ul runat="server">
                <li>Task Management: Tired of juggling multiple to-do lists? Our intuitive task management system helps you organize chores, events, and responsibilities efficiently. Stay on top of your family's schedule with ease.</li>
                
                <li>Collaboration: Foster teamwork within your family. Our collaborative features enable seamless communication, ensuring that everyone is on the same page when it comes to household tasks and activities.</li>
                
                <li>Support System: Home Economy is more than just a platform; it's a support system for your family. Connect with like-minded individuals, share experiences, and learn from one another.</li>
            </ul>
            
            <h2 runat="server">Why Choose Home Economy?</h2>
            
            <ul runat="server">
                <li>Simplicity: We prioritize user-friendly interfaces, making it easy for every family member, regardless of age or tech-savviness, to actively participate in managing household tasks.</li>
                
                <li>Empowerment: We empower you to take control of your home economy, allowing you to focus on what truly matters – the well-being and happiness of your family.</li>
                
                <li>Privacy and Security: Your family's data and information are our top priorities. We employ the latest security measures to ensure a safe and secure environment for all users.</li>
            </ul>
            
            <div runat="server" class="btn-container">
                <!-- Login Button -->
                <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
                
                <!-- Signup Button -->
                <asp:Button ID="btnSignup" runat="server" Text="Signup" OnClick="btnSignup_Click" />
            </div>
        </div>
    </form>
</body>
</html>
