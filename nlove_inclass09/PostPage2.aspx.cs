using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;

namespace nlove_inclass09
{
    //Here the password is HASHED!
    public partial class PostPage2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NameValueCollection nvc = Request.Form;

            string name = nvc["txtName"];
            string password = nvc["txtPassword"];
            string error = "";
            bool hasError = false;

            try
            {
                //hashing password
                PasswordHash p = new PasswordHash(password);
                string hash = p.Hash;

                Connection c = new Connection(name, hash);// (username and password) - in the form to l og in
                if (c.UserExists)
                {
                    lblName.Text = name + " has logged in.";
                
                }
                else
                {
                    hasError = true;
                       error = "User and password do not match";
                }
            }
            catch (EmptyPasswordException ex)
            {
                hasError = true;
                error = ex.Message;
            }
            catch (ConnectionException ex)
            {
                hasError = true;
                error = ex.Message;
            }
            catch (Exception ex)
            {
                hasError = true;
                error = ex.Message;
            }

            if (hasError)
            {
            Response.Redirect("./GetExample.aspx?error=" + error);

            }

        }
    }
}