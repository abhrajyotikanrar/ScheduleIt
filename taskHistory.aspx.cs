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
    int g_id;

    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["groupID"] != null) && (Session["groupName"] != null))
        {
            g_id = int.Parse(Session["groupID"].ToString());
            lblgroupname.Text = Session["groupName"].ToString();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Your session is timed out.')", true);
            Response.Redirect("groups.aspx");
        }

        using(SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            SqlDataAdapter sda2 = new SqlDataAdapter("select t1.[Task id] AS TaskId, t1.[Task generator name] AS TaskGeneratorName, t1.[Task type] AS TaskType, t1.[Time] AS Time, t1.[Date] As Date, t1.[Title] AS Title, t1.[Description] AS Description, t1.[Status] AS Status from [Group task details] t1, [" + Int64.Parse(Request.Cookies["Phone_no"].Value.ToString()) + "] t2 where t1.[Group id]=" + g_id + " and t2.[Group id]=" + g_id + " ORDER BY t1.[Task id] DESC", con);
            DataTable dtGroupTask = new DataTable();
            sda2.Fill(dtGroupTask);
            repeaterTaskList.DataSource = dtGroupTask;
            repeaterTaskList.DataBind();
        }
    }
}