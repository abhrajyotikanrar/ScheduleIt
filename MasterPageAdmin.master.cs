using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPageAdmin : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["admin_username"] == null)
        {
            Response.Redirect("AdminLogin.aspx");
        }
    }

    protected void btnlogout_Click(object sender, EventArgs e)
    {
        if (Request.Cookies["admin_username"] != null)
        {
            Response.Cookies["admin_username"].Expires = DateTime.Now.AddDays(-1);
        }
        Session.Abandon();
        Response.Redirect("AdminLogin.aspx");
    }
}
