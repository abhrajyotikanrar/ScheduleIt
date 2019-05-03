using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class AdminLogin : System.Web.UI.Page
{
    string connectionString = ConnectionString.getConnectionString();

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void btnLogIn_Click(object sender, EventArgs e)
    {
        if (txtusername.Text.Trim() == "")
        {
            Response.Write("<script>alert('Please enter the username')</script>");
            return;   
        }
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            using(SqlCommand cmd = new SqlCommand("select COUNT(*) from [admin] where [username]='" + txtusername.Text.Trim() + "' and [password]='" + txtpassword.Text + "'", con))
            {
                int rowCount = int.Parse(cmd.ExecuteScalar().ToString());
                if(rowCount == 1)
                {
                    Response.Cookies["admin_username"].Value = txtusername.Text.Trim();
                    Response.Redirect("admin.aspx");
                }
                else
                {
                    Response.Write("<script>alert('Wrong credentials')</script>");
                }
            }
        }
    }
}