using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using homeeconomyback;


namespace HomeEconomyWeb
{
    public partial class LogIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSignup_Click(object sender, EventArgs e)
        {
            Response.Redirect("SingUp.aspx");
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Value;
            string password = txtPassword.Value;
            int login = Program.LogIn(email, password);
            if (login == 0)
            {
                Error.Text = "EMAIL does not exist";
            }
            else if(login== -1)
            {
                Error.Text = "Incorrect password";

            }
            else
            {
                Person connect = Program.GetUserByUserName(email, password);
                Session["connect"] = connect;
                if (connect is Parents)
                {   
                    Response.Redirect("ParentsSpace.aspx");
                }
                else
                {
                    Response.Redirect("ChildSpace.aspx");
                }

            }

        }
    }
}