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
        if (!Page.IsPostBack)
        { //do something 
        }
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string qryString;
            string phno = Request.Cookies["Phone_no"].Value.ToString();

            if (Session["qryString"] != null)
            {
                qryString = Session["qryString"].ToString();
                Session.Abandon();
            }
            else
            {
                qryString = "select t2.[Group name], t1.[Group id], t2.[Creator name], t2.[No of members], t2.[Creating date] from [" + phno + "] t1, [Groups list] t2 where t1.[Group id]=t2.[Group id] and t2.[Status]='Active'";
            }
            
            SqlDataAdapter sda = new SqlDataAdapter(qryString, con);
            DataTable dtGroupList = new DataTable();
            sda.Fill(dtGroupList);
            RepeaterGroupList.DataSource = dtGroupList;
            RepeaterGroupList.DataBind();
            
        }
    }

    protected void RepeaterGroupList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "scheduleTask")
        { 
            int repeterIndex = e.Item.ItemIndex;

            Label labelGroupId = (Label)RepeaterGroupList.Items[repeterIndex].FindControl("lblgroupid");
            Label labelGroupName = (Label)RepeaterGroupList.Items[repeterIndex].FindControl("lblgroupname");

            Session["groupID"] = labelGroupId.Text;
            Session["groupName"] = labelGroupName.Text;
            Session.Timeout = 30;

            Response.Redirect("scheduleGroupTask.aspx");
        }
        if (e.CommandName == "addMember")
        {
            int repeterIndex = e.Item.ItemIndex;

            Label labelGroupId = (Label)RepeaterGroupList.Items[repeterIndex].FindControl("lblgroupid");
            Label labelGroupName = (Label)RepeaterGroupList.Items[repeterIndex].FindControl("lblgroupname");

            Session["groupID"] = labelGroupId.Text;
            Session["groupName"] = labelGroupName.Text;
            Session.Timeout = 30;

            Response.Redirect("addMember.aspx");
        }
        if (e.CommandName == "deleteGroup")
        {
            int repeterIndex = e.Item.ItemIndex;

            Label labelGroupId = (Label)RepeaterGroupList.Items[repeterIndex].FindControl("lblgroupid");
            Label labelGroupName = (Label)RepeaterGroupList.Items[repeterIndex].FindControl("lblgroupname");

            List<string> membersOfGroup = new List<string>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("update [Groups list] set [Status]='Deleted' where [Group id]=" + Int64.Parse(labelGroupId.Text), con))
                {
                    cmd.ExecuteNonQuery();
                }
                
                using (SqlCommand cmd = new SqlCommand("update [Group task details] set [Status]='Group deleted' where [Group id]=" + Int64.Parse(labelGroupId.Text), con))
                {
                    cmd.ExecuteNonQuery();
                }

                using (SqlCommand cmd = new SqlCommand("select [Member phone no] from [G" + labelGroupId.Text + "]", con))
                {
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        membersOfGroup.Add(sdr["Member phone no"].ToString());
                    }
                    sdr.Close();
                }

                foreach(string item in membersOfGroup)
                {
                    using(SqlCommand cmd = new SqlCommand("delete from [" + item + "] where [Group id]=" + labelGroupId.Text, con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                membersOfGroup.Clear();

                using (SqlCommand cmd = new SqlCommand("DROP TABLE [G" + labelGroupId.Text + "]", con))
                {
                    cmd.ExecuteNonQuery();
                }
                Response.Redirect("groups.aspx");
            }
        }
        if (e.CommandName == "viewDetails")
        {
            int repeterIndex = e.Item.ItemIndex;

            Label labelGroupId = (Label)RepeaterGroupList.Items[repeterIndex].FindControl("lblgroupid");
            Label labelGroupName = (Label)RepeaterGroupList.Items[repeterIndex].FindControl("lblgroupname");

            Session["groupID"] = labelGroupId.Text;
            Session["groupName"] = labelGroupName.Text;
            Session.Timeout = 30;

            Response.Redirect("groupDetails.aspx");
        }
        if (e.CommandName == "taskHistory")
        {
            int repeterIndex = e.Item.ItemIndex;

            Label labelGroupId = (Label)RepeaterGroupList.Items[repeterIndex].FindControl("lblgroupid");
            Label labelGroupName = (Label)RepeaterGroupList.Items[repeterIndex].FindControl("lblgroupname");

            Session["groupID"] = labelGroupId.Text;
            Session["groupName"] = labelGroupName.Text;
            Session.Timeout = 30;

            Response.Redirect("taskHistory.aspx");
        }
    }
    
}