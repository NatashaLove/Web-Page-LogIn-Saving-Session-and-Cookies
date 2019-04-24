using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nlove_inclass09
{
    public partial class GetPage2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string name = Request.QueryString["name"];
            string password = Request.QueryString["password"];

            Connection c = new Connection(name, password);

            if (c.UserExists)
            {
                lblName.Text = name + " has logged in.";
            }
            else
            {
                lblName.Text = "No one is logged in.";
            }
        }
    }
}