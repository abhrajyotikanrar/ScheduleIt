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
    int g_id = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) // If page loads for first time
        {
            Session["scheduleGroupTaskState"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }

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


    }

    protected void btnschedule_Click(object sender, EventArgs e)
    {
        if (Session["scheduleGroupTaskState"].ToString() != ViewState["scheduleGroupTaskState"].ToString())
        {
            return;
        }
        if (txttitle.Text.Trim() == "" || txtdescription.Text.Trim() == "" || txtdate.Text.Trim() == "" || txttime.Text.Trim() == "")
        {
            return;
        }
        long groupTaskId;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("select MAX(CAST([Task id] AS int)) from [Group task details]", con))
            {
                string res = cmd.ExecuteScalar().ToString();
                if (res == "")
                {
                    res = "0";
                }
                groupTaskId = Int64.Parse(res);
                groupTaskId++;
            }
            using (SqlCommand cmd = new SqlCommand("insert into [Group task details]([Task id],[Task generator no],[Task generator name],[Task type],[Time],[Date],[Group id],[Title],[Description],[Status]) values(@taskid, @generatorno, @generatorname, @tasktype, @time, @date, @gid, @title, @description, @status)", con))
            {
                cmd.Parameters.AddWithValue("taskid", groupTaskId);
                cmd.Parameters.AddWithValue("generatorno", Int64.Parse(Request.Cookies["Phone_no"].Value.ToString()));
                cmd.Parameters.AddWithValue("generatorname", Request.Cookies["Name"].Value.ToString());
                cmd.Parameters.AddWithValue("tasktype", type.Text);
                cmd.Parameters.AddWithValue("time", DateTime.Parse(txttime.Text).ToString("hh:mm tt"));
                cmd.Parameters.AddWithValue("date", DateTime.Parse(txtdate.Text).ToString("dd/MM/yyyy"));
                cmd.Parameters.AddWithValue("gid", g_id);
                cmd.Parameters.AddWithValue("title", txttitle.Text.Trim());
                cmd.Parameters.AddWithValue("description", txtdescription.Text.Trim());
                cmd.Parameters.AddWithValue("status", "Scheduled");
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "scheduleSuccessAlert()", true);
            }
        }
        Session["scheduleGroupTaskState"] = Server.UrlEncode(System.DateTime.Now.ToString());
    }

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["scheduleGroupTaskState"] = Session["scheduleGroupTaskState"];
    }
}