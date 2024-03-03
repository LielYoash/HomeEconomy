using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using homeeconomyback;


namespace HomeEconomyWeb
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            // Apply styles dynamically
            container.Style["max-width"] = "800px";
            container.Style["margin"] = "50px auto";
            container.Style["background-color"] = "#fff";
            container.Style["padding"] = "20px";
            container.Style["border-radius"] = "8px";
            container.Style["box-shadow"] = "0 0 10px rgba(0, 0, 0, 0.1)";
            btnLogin.Style["padding"] = "10px 20px";
            btnLogin.Style["font-size"] = "16px";
            btnLogin.Style["margin-right"] = "10px";
            btnLogin.Style["cursor"] = "pointer";
            btnLogin.Style["background-color"] = "#4CAF50";
            btnLogin.Style["color"] = "#fff";
            btnLogin.Style["border"] = "none";
            btnLogin.Style["border-radius"] = "4px";
            btnLogin.Style["text-align"] = "center";
            btnLogin.Style["text-decoration"] = "none";
            btnLogin.Style["display"] = "inline-block";
            btnLogin.Style["transition-duration"] = "0.4s";

            btnSignup.Style["padding"] = "10px 20px";
            btnSignup.Style["font-size"] = "16px";
            btnSignup.Style["cursor"] = "pointer";
            btnSignup.Style["background-color"] = "#4CAF50";
            btnSignup.Style["color"] = "#fff";
            btnSignup.Style["border"] = "none";
            btnSignup.Style["border-radius"] = "4px";
            btnSignup.Style["text-align"] = "center";
            btnSignup.Style["text-decoration"] = "none";
            btnSignup.Style["display"] = "inline-block";
            btnSignup.Style["transition-duration"] = "0.4s";
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // Handle login button click event
            // Redirect to your login page
            Response.Redirect("LogIn.aspx");
        }

        protected void btnSignup_Click(object sender, EventArgs e)
        {
            // Handle signup button click event
            // Redirect to your signup page
            Response.Redirect("SingUp.aspx");
        }
    }
}