using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized; // added this line

namespace nlove_inclass09
{
    public partial class SessionPage2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["name"] !=null)
            {
                lblName.Text = (string)Session["name"] + " is logged in.";
            }
            else
            {
                NameValueCollection nvc = Request.Form;

                string name = nvc["txtName"];// txtName- is an index in associative array (like 0)
                string password = nvc["txtPassword"];// txtPassword - index in associative array (like 1)
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
                        Session["name"] = name;
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
                    Response.Redirect("./SessionExample.aspx?error=" + error);

                }
            }
        }
    }
}