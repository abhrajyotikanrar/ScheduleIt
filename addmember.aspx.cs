using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    string connectionString = ConnectionString.getConnectionString();
    int g_id;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack) // If page loads for first time
        {
            Session["addGroupMemberState"] = Server.UrlEncode(System.DateTime.Now.ToString());
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

    protected void btnaddmember_Click(object sender, EventArgs e)
    {
        if (Session["addGroupMemberState"].ToString() != ViewState["addGroupMemberState"].ToString())
        {
            return;
        }
        if (txtphno.Text.Trim() == "")
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Please enter phone number.')", true);
            return;
        }
        if(txtphno.Text.Trim() == Request.Cookies["Phone_no"].Value.ToString())
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Please enter phone number except yours.')", true);
            return;
        }

        List<string> memberNoListFromDb = new List<string>();
        List<string> groupMemberNoListFromDb = new List<string>();
        int noOfMembers;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("select [Phone no] from [User details]", con))
            {
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    memberNoListFromDb.Add(sdr["Phone no"].ToString());
                }
                sdr.Close();
            }
            if (memberNoListFromDb.IndexOf(txtphno.Text.Trim()) < 0)
            {
                divstatus.Visible = true;
                lblstatus.Text = "Phone number "+txtphno.Text.Trim()+" cannot be added as the number is not registered in Scheduleit.";
                lblstatus.ForeColor = System.Drawing.Color.Red;
                txtphno.Text = "";
                return;
            }
            memberNoListFromDb.Clear();

            using (SqlCommand cmd = new SqlCommand("select [Member phone no] from [G" + g_id.ToString() +"]" , con))
            {
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    groupMemberNoListFromDb.Add(sdr["Member phone no"].ToString());
                }
                sdr.Close();
            }

            if (groupMemberNoListFromDb.IndexOf(txtphno.Text.Trim()) >= 0)
            {
                divstatus.Visible = true;
                lblstatus.Text = "Phone number " + txtphno.Text.Trim() + " is already been added to this group.";
                lblstatus.ForeColor = System.Drawing.Color.Red;
                txtphno.Text = "";
                return;
            }
            groupMemberNoListFromDb.Clear();

            using (SqlCommand cmd = new SqlCommand("insert into [" + txtphno.Text.Trim() + "]([Group id],[Joining time],[Joining date]) values(@groupid, @jtime, @jdate)", con))
            {
                cmd.Parameters.AddWithValue("groupid", g_id);
                cmd.Parameters.AddWithValue("jtime", DateTime.Now.ToString("hh:mm tt"));
                cmd.Parameters.AddWithValue("jdate", DateTime.Now.ToString("dd/MM/yyyy"));
                cmd.ExecuteNonQuery();
            }

            using (SqlCommand cmd = new SqlCommand("select [No of members] from [Groups list] where [Group id]=" + g_id, con))
            {
                noOfMembers = int.Parse(cmd.ExecuteScalar().ToString());
                noOfMembers++;
            }

            using (SqlCommand cmd = new SqlCommand("update [Groups list] set [No of members]=" + noOfMembers + " where [Group id]=" + g_id, con))
            {
                cmd.ExecuteNonQuery();
            }

            using (SqlCommand cmd = new SqlCommand("insert into [G" + g_id.ToString() + "] ([Member phone no],[Joining time],[Joining date]) values(@memberno, @jtime, @jdate)", con))
            {
                cmd.Parameters.AddWithValue("memberno", Int64.Parse(txtphno.Text.Trim()));
                cmd.Parameters.AddWithValue("jtime", DateTime.Now.ToString("hh:mm tt"));
                cmd.Parameters.AddWithValue("jdate", DateTime.Now.ToString("dd/MM/yyyy"));
                cmd.ExecuteNonQuery();

                divstatus.Visible = true;
                lblstatus.Text = "Phone number " + txtphno.Text.Trim() + " is added successfully.";
                lblstatus.ForeColor = System.Drawing.Color.DarkGreen;
                txtphno.Text = "";
            }
        }

        Session["addGroupMemberState"] = Server.UrlEncode(System.DateTime.Now.ToString());
    }

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["addGroupMemberState"] = Session["addGroupMemberState"];
    }

}