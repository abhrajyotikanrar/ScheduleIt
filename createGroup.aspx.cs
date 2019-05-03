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
        {
            Session.Abandon();
        }

    }

    protected void RepeaterMemberList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "cross")
        {
            if (Session["ListOfNumbers"] != null)
            {
                List<string> memberNoList = new List<string>();
                int repeterIndex = e.Item.ItemIndex;

                string stringOfNumbers = Session["ListOfNumbers"].ToString();
                memberNoList = stringOfNumbers.Split(',').ToList<string>();
                
                if (memberNoList.Count == 1)
                {
                    memberNoList.Clear();
                    RepeaterMemberList.DataSource = null;
                    RepeaterMemberList.DataBind();
                    Session["ListOfNumbers"] = null;
                }
                else
                {
                    memberNoList.RemoveAt(repeterIndex);
                    RepeaterMemberList.DataSource = from i in memberNoList select new { newMemberPhoneNo = i};
                    RepeaterMemberList.DataBind();
                    Session["ListOfNumbers"] = string.Join(",", memberNoList);
                }
                Session.Timeout = 30;
            }
        }
    }
    
    protected void btnaddmember_Click(object sender, EventArgs e)
    {
        List<string> memberNoList = new List<string>();

        if (txtphno.Text != "")
        {
            if (txtphno.Text == Request.Cookies["Phone_no"].Value.ToString())
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Add members number excluding you.')", true);
                return;
            }
            if (Session["ListOfNumbers"] != null)
            {
                string stringOfNumbers = Session["ListOfNumbers"].ToString();
                memberNoList = stringOfNumbers.Split(',').ToList<string>();
            }
            int pos = memberNoList.IndexOf(txtphno.Text.Trim());
            if (pos >= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Number already added')", true);
                return;
            }

            memberNoList.Add(txtphno.Text.Trim());
            RepeaterMemberList.DataSource = from c in memberNoList select new { newMemberPhoneNo = c };
            RepeaterMemberList.DataBind();
            Session["ListOfNumbers"] = string.Join(",", memberNoList);
            Session.Timeout = 30;
            txtphno.Text = "";
            txtphno.Focus();
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        
    }

    protected void btncreategroup_Click(object sender, EventArgs e)
    {
        if (txtgroupname.Text.Trim() == "")
        {
            Response.Write("<script>alert('Please enter the group name.')</script>");
            return;
        }
        List<string> memberNoListFromDb = new List<string>();
        List<string> memberNoList = new List<string>();
        List<string> memberFailedToAdd = new List<string>();
        long totalMemberAdded = 0;

        long gid;

        if (Session["ListOfNumbers"] != null)
        {
            string stringOfNumbers = Session["ListOfNumbers"].ToString();
            memberNoList = stringOfNumbers.Split(',').ToList<string>();
        }
        else
        {
            Response.Write("<script>alert('Please add group members.')</script>");
            return;
        }

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("select MAX(CAST([Group id] as int)) from [Groups list]", con))
            {
                string res = cmd.ExecuteScalar().ToString();
                if (res == "")
                {
                    res = "0";
                }
                gid = long.Parse(res);
                gid++;
            }
            using (SqlCommand cmd = new SqlCommand("insert into [Groups list]([Group id], [Group name], [Creator no], [Creator name], [Creating time], [Creating date], [Status]) values(@gid, @gname, @creatorno, @creatorname, @creatingtime, @creatingdate, @status)", con))
            {
                //No of members not added. It will be updated later.
                cmd.Parameters.AddWithValue("gid", gid);
                cmd.Parameters.AddWithValue("gname", txtgroupname.Text.Trim());
                cmd.Parameters.AddWithValue("creatorno", Int64.Parse(Request.Cookies["Phone_no"].Value.ToString()));
                cmd.Parameters.AddWithValue("creatorname", Request.Cookies["Name"].Value.ToString());
                cmd.Parameters.AddWithValue("creatingtime", DateTime.Now.ToString("hh:mm tt"));
                cmd.Parameters.AddWithValue("creatingdate", DateTime.Now.ToString("dd/MM/yyyy"));
                cmd.Parameters.AddWithValue("status", "Active");
                cmd.ExecuteNonQuery();
            }
            using (SqlCommand cmd = new SqlCommand("create table [G" + gid.ToString()+ "]([Member phone no] numeric(18) primary key, [Joining time] varchar(MAX), [Joining date] varchar(MAX))",con))
            {
                cmd.ExecuteNonQuery();
            }

            using (SqlCommand cmd = new SqlCommand("insert into [G" + gid.ToString() + "]([Member phone no], [Joining time], [Joining date]) values(@memberno, @jtime, @jdate)", con))
            {
                cmd.Parameters.AddWithValue("memberno",Int64.Parse(Request.Cookies["Phone_no"].Value.ToString()));
                cmd.Parameters.AddWithValue("jtime", DateTime.Now.ToString("hh:mm tt"));
                cmd.Parameters.AddWithValue("jdate", DateTime.Now.ToString("dd/MM/yyyy"));
                cmd.ExecuteNonQuery();
                totalMemberAdded++;
            }
            using (SqlCommand cmd = new SqlCommand("select [Phone no] from [User details]", con))
            {
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    memberNoListFromDb.Add(sdr["Phone no"].ToString());
                }
                sdr.Close();
            }
            foreach (string item in memberNoList)
            {
                if (memberNoListFromDb.IndexOf(item) >= 0)
                {
                    using (SqlCommand cmd = new SqlCommand("insert into [G" + gid.ToString() + "]([Member phone no], [Joining time], [Joining date]) values(@memberno, @jtime, @jdate)", con))
                    {
                        cmd.Parameters.AddWithValue("memberno", item);
                        cmd.Parameters.AddWithValue("jtime", DateTime.Now.ToString("hh:mm tt"));
                        cmd.Parameters.AddWithValue("jdate", DateTime.Now.ToString("dd/MM/yyyy"));
                        cmd.ExecuteNonQuery();
                        totalMemberAdded++;
                    }
                }
                else
                {
                    memberFailedToAdd.Add(item);
                }
                
            }
            memberNoListFromDb.Clear();
            foreach(string item in memberFailedToAdd)
            {
                if (memberNoList.IndexOf(item) >= 0)
                {
                    memberNoList.Remove(item);
                }
            }
            using (SqlCommand cmd = new SqlCommand("insert into [" + Request.Cookies["Phone_no"].Value.ToString() + "]([Group id], [Joining time], [Joining date]) values(@groupid, @jtime, @jdate)", con))
            {
                cmd.Parameters.AddWithValue("groupid", gid);
                cmd.Parameters.AddWithValue("jtime", DateTime.Now.ToString("hh:mm tt"));
                cmd.Parameters.AddWithValue("jdate", DateTime.Now.ToString("dd/MM/yyyy"));
                cmd.ExecuteNonQuery();
            }
            foreach (string item in memberNoList)
            {
                using (SqlCommand cmd = new SqlCommand("insert into [" + item + "]([Group id], [Joining time], [Joining date]) values(@groupid, @jtime, @jdate)", con))
                {
                    cmd.Parameters.AddWithValue("groupid", gid);
                    cmd.Parameters.AddWithValue("jtime", DateTime.Now.ToString("hh:mm tt"));
                    cmd.Parameters.AddWithValue("jdate", DateTime.Now.ToString("dd/MM/yyyy"));
                    cmd.ExecuteNonQuery();
                }
            }
            using (SqlCommand cmd = new SqlCommand("update [Groups list] set [No of members]=" + totalMemberAdded + " where [Group id]=" + gid, con))
            {
                cmd.ExecuteNonQuery();
            }
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "groupCreateSuccessAlert()", true);
        Response.Redirect("groups.aspx");
    }

}