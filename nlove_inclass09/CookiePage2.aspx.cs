using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nlove_inclass09
{// allows to stay logged in even after the browser was closed
    public partial class CookiePage2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["login"] !=null)
            {
                HttpCookie cookie = Request.Cookies["login"];
                lblName.Text = (string)cookie["name"] + " is logged in.";
            }
            else
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
                        HttpCookie cookie = new HttpCookie("login");
                        cookie["name"] = name;
                        cookie.Expires = DateTime.Now.AddDays(2);
                        Response.Cookies.Add(cookie);
                        

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
                    Response.Redirect("./CookieExample.aspx?error=" + error);

                }
            }
        }
    }
}