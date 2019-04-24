using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nlove_inclass09
{
    // Here the password is NOT HASHED!
    public partial class GetExample : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {//display error message
            string error = Request.QueryString["error"]; // in [] - because it's an array in the method
            // the "error" - from the query line below -?error - looks for it and displays the message -which is under the variable "error"
            if (!(String.IsNullOrWhiteSpace(error)))
            {
                lblError.Text = error;
                lblError.Visible = true;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string error = "";

            try
            {
                //hashing password
                PasswordHash p = new PasswordHash(txtPassword.Text);
                string hash = p.Hash;// random hash is added to the password - we or user or hacker can't see it

                Connection c = new Connection(txtName.Text, hash);// (username and password) - in the form to log in
                if (c.UserExists) // UserExists -  custom created class - 
                    // -it compares entered passw AND login to existing in the db - both
                {
                    // no space : ?name=
                    Response.Redirect("./GetPage2.aspx?name=" + txtName.Text + // ? - means it's a query
                   "&password=" + hash);// & -separates 2 - name and password in url
                }
                else
                {
                    error = "User and password do not match";
                }
            }
            catch (EmptyPasswordException ex)
            {
                error = ex.Message;
            }
            catch(ConnectionException ex)
            {
                error = ex.Message;
            }
            catch(Exception ex)
            {
                error = ex.Message;
            }

            Response.Redirect("./GetExample.aspx?error=" + error);
        }

    }
}