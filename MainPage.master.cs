using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class MainPage : System.Web.UI.MasterPage
{
    string connectionString = ConnectionString.getConnectionString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) // If page loads for first time
        {
            // Assign the Session["update"] with unique value
            Session["feedbackUpdateState"] = Server.UrlEncode(System.DateTime.Now.ToString());
            Session["passwordChangeState"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }
        if (Request.Cookies["Phone_no"] != null)
        {
            lblname.Text = Request.Cookies["Name"].Value;
        }
        else
        {
            Response.Redirect("login.aspx");
        }
        
    }

    protected void btnsettings_Click(object sender, EventArgs e)
    {
        Response.Redirect("settings.aspx");
    }

    protected void btnchangepassword_Click(object sender, EventArgs e)
    {
        // If page is Refreshed
        if (Session["passwordChangeState"].ToString() != ViewState["passwordChangeState"].ToString())
        {
            return;
        }
        if(txtcurrentpw.Text=="" || txtnewpw.Text=="" || txtconfirmpw.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Please enter all the field.')", true);
            return;
        }

        using(SqlConnection con = new SqlConnection(connectionString))
        {
            string curr_pw;
            con.Open();
            using(SqlCommand cmd = new SqlCommand("select [Password] from [User details] where [Phone no]=" + Int64.Parse(Request.Cookies["Phone_no"].Value), con))
            {
                curr_pw = cmd.ExecuteScalar().ToString();
            }
            if(txtcurrentpw.Text != curr_pw)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Enter your current password properly.')", true);
                return;
            }
            using(SqlCommand cmd = new SqlCommand("update [User details] set [Password]='" + txtnewpw.Text + "' where [Phone no]=" + Int64.Parse(Request.Cookies["Phone_no"].Value), con))
            {
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "passwordChangeSuccessAlert()", true);
            }
        }

        Session["passwordChangeState"] = Server.UrlEncode(System.DateTime.Now.ToString());
    }

    protected void btnlogout_Click(object sender, EventArgs e)
    {
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

    protected void btnpostfeedback_Click(object sender, EventArgs e)
    {
        // If page is Refreshed
        if (Session["feedbackUpdateState"].ToString() != ViewState["feedbackUpdateState"].ToString())
        {
            return;
        }

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            using(SqlCommand cmd = new SqlCommand("select MAX(CAST([Serial no] AS int)) from [Feedback]", con))
            {
                string res = cmd.ExecuteScalar().ToString();
                if(res == "")
                {
                    res = "0";
                }
                int slno = int.Parse(res);
                slno = slno + 1;
                using(SqlCommand cmd2 = new SqlCommand("insert into [Feedback] ([Serial no], [Phone no], [Name], [Date and Time], [Title], [Description]) values(@serialno, @phoneno, @name, @datetime, @title, @description)", con))
                {
                    cmd2.Parameters.AddWithValue("serialno", slno);
                    cmd2.Parameters.AddWithValue("phoneno", Int64.Parse(Request.Cookies["Phone_no"].Value));
                    cmd2.Parameters.AddWithValue("name", Request.Cookies["Name"].Value);
                    cmd2.Parameters.AddWithValue("datetime", DateTime.Now.ToString());
                    cmd2.Parameters.AddWithValue("title", txttitle.Text.Trim().ToString());
                    cmd2.Parameters.AddWithValue("description", txtdescription.Text.Trim().ToString());
                    cmd2.ExecuteNonQuery();
                    txttitle.Text = "";
                    txtdescription.Text = "";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "feedbackSuccessAlert()", true);
                }
            }
        }
        Session["feedbackUpdateState"] = Server.UrlEncode(System.DateTime.Now.ToString());
    }


    protected override void OnPreRender(EventArgs e)
    {
        ViewState["feedbackUpdateState"] = Session["feedbackUpdateState"];
        ViewState["passwordChangeState"] = Session["passwordChangeState"];
        ViewState["scheduleIndividualTaskState"] = Session["scheduleIndividualTaskState"];
    }

    protected void btnsearch_Click(object sender, ImageClickEventArgs e)
    {
        if (txtsearch.Text.Trim() == "")
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Please enter data to be searched.')", true);
            return;
        }
        if(txtsearch.Text.Trim().Substring(0,1) == "#")
        {
            long n;
            if(!long.TryParse(txtsearch.Text.Trim().Substring(1, (txtsearch.Text.Trim().Length - 1)), out n))
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Please enter a valid Group ID.')", true);
                return;
            }
            string qryString = "select t2.[Group name], t1.[Group id], t2.[Creator name], t2.[No of members], t2.[Creating date] from[" + Request.Cookies["Phone_no"].Value.ToString() + "] t1, [Groups list] t2 where t1.[Group id]=" + txtsearch.Text.Trim().Substring(1,(txtsearch.Text.Trim().Length - 1)) + " and t2.[Status]='Active' and t2.[Group id]=" + txtsearch.Text.Trim().Substring(1, (txtsearch.Text.Trim().Length - 1));
            Session["qryString"] = qryString;
            Response.Redirect("groups.aspx");
        }
        else if (txtsearch.Text.Trim().Substring(0, 2).ToUpper() == "IT")
        {
            long n;
            if (!long.TryParse(txtsearch.Text.Trim().Substring(2, (txtsearch.Text.Trim().Length - 2)), out n))
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Please enter a valid Task ID.')", true);
                return;
            }

            string qryStringIndividualTask = "select * from [Individual task details] where [Phone no]=" + Int64.Parse(Request.Cookies["Phone_no"].Value.ToString()) + " and [Task id]=" + long.Parse(txtsearch.Text.Trim().Substring(2, (txtsearch.Text.Trim().Length - 2)));
            Session["qryStringIndividualTask"] = qryStringIndividualTask;
            Response.Redirect("home.aspx");
        }
        else if (txtsearch.Text.Trim().Substring(0, 2).ToUpper() == "GT")
        {
            long n;
            if (!long.TryParse(txtsearch.Text.Trim().Substring(2, (txtsearch.Text.Trim().Length - 2)), out n))
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Please enter a valid Task ID.')", true);
                return;
            }
            string qryStringGroupTask = "select t1.[Task id] AS TaskId, t1.[Task generator name] AS TaskGeneratorName, t1.[Task type] AS TaskType, t1.[Time] AS Time, t1.[Date] As Date, t1.[Group id] AS GroupId, t1.[Title] AS Title, t1.[Description] AS Description, t1.[Status] AS Status, t3.[Group name] AS GroupName from [Group task details] t1, [" + Int64.Parse(Request.Cookies["Phone_no"].Value.ToString()) + "] t2, [Groups list] t3 where t1.[Group id]=t2.[Group id] and t2.[Group id]=t3.[Group id] and t1.[Task id]=" + long.Parse(txtsearch.Text.Trim().Substring(2, (txtsearch.Text.Trim().Length - 2)));
            Session["qryStringGroupTask"] = qryStringGroupTask;
            Response.Redirect("home.aspx");
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Sorry! Unable to proceed with your query.')", true);
            return;
        }
    }
}
