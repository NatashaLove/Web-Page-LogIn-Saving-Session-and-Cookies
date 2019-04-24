using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace nlove_inclass09

   
{
    public partial class PostExample : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string error = Request.QueryString["error"]; // in [] - because it's an array in the method
            // the "error" - from the query line below -?error - looks for it and displays the message -which is under the variable "error"
            if (!(String.IsNullOrWhiteSpace(error)))
            {
                lblError.Text = error;
                lblError.Visible = true;
            }
        }
    }
}