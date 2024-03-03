using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using homeeconomyback;

namespace HomeEconomyWeb
{
    public partial class SingUp : System.Web.UI.Page
    {
        static int LinearConversionToNumber(int encryptedValue)
        {
            // You can adjust the coefficients based on your needs
            // Extract relevant components from the current date and time
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            int hour = DateTime.Now.Hour;

            // You can customize the coefficients based on your requirements
            int a = 2, b = 3, c = 5, d = 7, e = 11;
            int encrypteddate = a * year + b * month + c * day + d * hour + e;


            return encryptedValue - encrypteddate;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Populate dropdown lists on the first page load
                PopulateRoleDropdown();
            }
        }

        private void PopulateRoleDropdown()
        {
            // Implement a function to get all role names
            // For demonstration purposes, a dummy list is used
            string[] roles = Program.GetAllRoleNames().ToArray();

            ddlRole.DataSource = roles;
            ddlRole.DataBind();
        }

   
        protected void btnAddFamily_Click(object sender, EventArgs e)
        {
            if (ddlRole.SelectedValue == "dad" || ddlRole.SelectedValue == "mom")
            {
                if (txtNewFamilyName.Visible == false)
                {
                    txtNewFamilyName.Visible = true;
                    txtNewFamilyName.Value = "add new family group";
                    btnAddFamily.Visible = false;
                    familycodetxt.Visible = false;
                    familycodetxt.Value = "";
                    error.Text = "";
                }
            }
            else
            {
                error.Text = "only parents can build new family";
            }


        }
        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            int i = 0;
            string familyName="";
            if (familycodetxt.Value != "")
            {
                int familyid = int.Parse(familycodetxt.Value);
                familyid = LinearConversionToNumber(familyid);
                familyName = Program.GetFamilyNameById(familyid);
            }
          

            if (btnAddFamily.Visible == false)
            {
                if (txtNewFamilyName.Value != "add new family group")
                {
                    i= Program.AddFamily(txtNewFamilyName.Value);
                    if (i == -1)
                    {
                        error.Text = " family group exict";
                        
                    }
                      familyName = txtNewFamilyName.Value;
                    
                }
            }

            if (i==0 || i == 1)
            {
                string roleName = ddlRole.SelectedValue;
                string email = txtEmail.Value;
                string password = txtPassword.Value;
                string firstName = txtFirstName.Value;
                string lastName = txtLastName.Value;

                if (DateTime.TryParse(txtBirthdate.Value, out DateTime birthdate))
                {
                     i = Program.AddUserToDb(Program.GetRoleIdByName(roleName), email, password, firstName, lastName, birthdate, familyName);
                    if (i == 1)
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
                    else if (i == -1)
                    {
                        error.Text = "Add failed. Try again later";
                    }
                }
            }
            

        }


    }
}