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
        string qryStringIndividualTask, qryStringGroupTask;
        if(Session["qryStringGroupTask"] != null && Session["qryStringIndividualTask"] == null)
        {
            qryStringGroupTask = Session["qryStringGroupTask"].ToString();
            Session.Abandon();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlDataAdapter sda2 = new SqlDataAdapter(qryStringGroupTask, con);
                DataTable dtGroupTask = new DataTable();
                sda2.Fill(dtGroupTask);
                RepeaterGroupTasks.DataSource = dtGroupTask;
                RepeaterGroupTasks.DataBind();
            }
        }
        else if(Session["qryStringIndividualTask"] != null && Session["qryStringGroupTask"] == null)
        {
            qryStringIndividualTask = Session["qryStringIndividualTask"].ToString();
            Session.Abandon();
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(qryStringIndividualTask, con);
                DataTable dtIndividualTask = new DataTable();
                sda.Fill(dtIndividualTask);
                RepeaterIndividualTasks.DataSource = dtIndividualTask;
                RepeaterIndividualTasks.DataBind();
            }
        }
        else
        {
            qryStringIndividualTask = "select * from [Individual task details] where [Phone no]=" + Int64.Parse(Request.Cookies["Phone_no"].Value.ToString()) + " ORDER BY [Task id] desc";
            qryStringGroupTask = "select t1.[Task id] AS TaskId, t1.[Task generator name] AS TaskGeneratorName, t1.[Task type] AS TaskType, t1.[Time] AS Time, t1.[Date] As Date, t1.[Group id] AS GroupId, t1.[Title] AS Title, t1.[Description] AS Description, t1.[Status] AS Status, t3.[Group name] AS GroupName from [Group task details] t1, [" + Int64.Parse(Request.Cookies["Phone_no"].Value.ToString()) + "] t2, [Groups list] t3 where t1.[Group id]=t2.[Group id] and t2.[Group id]=t3.[Group id] ORDER BY t1.[Task id] DESC";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(qryStringIndividualTask, con);
                DataTable dtIndividualTask = new DataTable();
                sda.Fill(dtIndividualTask);
                RepeaterIndividualTasks.DataSource = dtIndividualTask;
                RepeaterIndividualTasks.DataBind();

                SqlDataAdapter sda2 = new SqlDataAdapter(qryStringGroupTask, con);
                DataTable dtGroupTask = new DataTable();
                sda2.Fill(dtGroupTask);
                RepeaterGroupTasks.DataSource = dtGroupTask;
                RepeaterGroupTasks.DataBind();
            }
        }
        
    }

    protected void RepeaterIndividualTasks_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }
}