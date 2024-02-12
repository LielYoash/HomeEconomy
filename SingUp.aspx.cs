﻿using System;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Populate dropdown lists on the first page load
                PopulateRoleDropdown();
                PopulateFamilyDropdown();
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

        private void PopulateFamilyDropdown()
        {
            // Implement a function to get all family names
            // For demonstration purposes, a dummy list is used
            string[] families = Program.GetAllFamilyNames().ToArray();

            ddlFamilyName.DataSource = families;
            ddlFamilyName.DataBind();
        }
        protected void btnAddFamily_Click(object sender, EventArgs e)
        {
            if (txtNewFamilyName.Visible == false)
            {
                txtNewFamilyName.Visible = true;
                txtNewFamilyName.Value = "add new family group";
                btnAddFamily.Visible = false;
                ddlFamilyName.Visible=false;

            }


        }
        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            int i = 0;
            string familyName = ddlFamilyName.SelectedValue;
            if (btnAddFamily.Visible == false)
            {
                if (txtNewFamilyName.Value != "add new family group")
                {
                    i= Program.AddFamily(txtNewFamilyName.Value);
                    if (i == -1)
                    {
                        error.Text = "new family group exict";
                        
                    }
                      familyName = txtNewFamilyName.Value;
                    
                }
            }
            if(i==0 || i == 1)
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