using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    string connectionString = ConnectionString.getConnectionString();
    string phoneNo = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        phoneNo = Request.Cookies["Phone_no"].Value.ToString();
    }

    protected void btndeleteaccount_Click(object sender, EventArgs e)
    {
        List<string> listGroupId = new List<string>();

        using(SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("select [Group id] from [" + phoneNo + "]", con))
            {
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    listGroupId.Add(sdr["Group id"].ToString());
                }
                sdr.Close();
            }

            using (SqlCommand cmd = new SqlCommand("drop table [" + phoneNo + "]", con))
            {
                cmd.ExecuteNonQuery();
            }
            
            foreach(string item in listGroupId)
            {
                using(SqlCommand cmd = new SqlCommand("delete from [G" + item + "] where [Member phone no]=" + phoneNo, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            listGroupId.Clear();
            using(SqlCommand cmd = new SqlCommand("delete from [User details] where [Phone no]=" + phoneNo))
            {
                cmd.ExecuteNonQuery();
            }
        }

        if (Request.Cookies["Phone_no"] != null)
        {
            Response.Cookies["Phone_no"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["Name"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["Login_time"].Expires = DateTime.Now.AddDays(-1);
        }
        Session.Abandon();
        Response.Redirect("login.aspx");
    }
}