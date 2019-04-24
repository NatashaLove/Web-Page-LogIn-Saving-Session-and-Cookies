using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nlove_inclass09
{// allows to leave the page and return and stayed logged in
    public partial class SessionExample : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["name"] !=null)
            {
                Response.Redirect("./SessionPage2.aspx");
            }
        }
    }
}