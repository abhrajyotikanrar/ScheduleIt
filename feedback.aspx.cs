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

    protected void Page_Load(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            SqlDataAdapter sda = new SqlDataAdapter("select * from [Feedback] ORDER BY [Serial no] desc", con);
            DataTable dtGroupList = new DataTable();
            sda.Fill(dtGroupList);
            repeaterfeedbacklist.DataSource = dtGroupList;
            repeaterfeedbacklist.DataBind();
        }
    }
}