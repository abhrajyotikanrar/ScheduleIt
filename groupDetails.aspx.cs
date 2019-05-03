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
    int g_id;
    string connectionString = ConnectionString.getConnectionString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if ((Session["groupID"] != null) && (Session["groupName"] != null))
            {
                g_id = int.Parse(Session["groupID"].ToString());
                txtgroupname.Text = Session["groupName"].ToString();
                lblmatchfound.Visible = false;
            }
        }


        if ((Session["groupID"] == null) || (Session["groupName"] == null))
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Your session is timed out.')", true);
            Response.Redirect("groups.aspx");
        }

        g_id = int.Parse(Session["groupID"].ToString());

        if(Session["isMemberListWithSearchedElement"] != null)
        {
            if(Session["isMemberListWithSearchedElement"].ToString() == "true")
            {
                Session.Remove("isMemberListWithSearchedElement");
                return;
            }
            else
            {
                Session.Remove("isMemberListWithSearchedElement");
            }
        }
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            using (SqlDataAdapter sda = new SqlDataAdapter("select t1.[Member phone no] as phonoNo, SUBSTRING(t2.[First name], 1, 1) as name_initial, REPLACE(t2.[First name] + ' ' + t2.[Middle name] + ' ' + t2.[Last name], '  ', ' ') As name from [G" + g_id.ToString() + "] t1, [User details] t2 where t1.[Member phone no]=t2.[Phone no] ORDER BY REPLACE(t2.[First name] + ' ' + t2.[Middle name] + ' ' + t2.[Last name], '  ', ' ')", con))
            {
                DataTable dt = new DataTable();
                sda.Fill(dt);
                repeaterMember.DataSource = dt;
                repeaterMember.DataBind();
            }
            using (SqlCommand cmd = new SqlCommand("select * from [Groups list] where [Group id]=" + g_id, con))
            {
                SqlDataReader sdr = cmd.ExecuteReader();

                if (sdr.Read())
                {
                    lblid.Text = sdr["Group id"].ToString();
                    lblname.Text = sdr["Group name"].ToString();
                    lblnoofmembers.Text = sdr["No of members"].ToString();
                    lblcreatorname.Text = sdr["Creator name"].ToString();
                    lblcreatorno.Text = sdr["Creator no"].ToString();
                    lbldatetime.Text = sdr["Creating date"].ToString() + " " + sdr["Creating time"].ToString();
                    lblstatus.Text = sdr["Status"].ToString();
                    sdr.Close();
                }
            }
        }
        
    }

    protected void btnedit_Click(object sender, EventArgs e)
    {
        txtgroupname.Enabled = true;
        btnedit.Visible = false;
        btnsave.Visible = true;
        txtgroupname.Focus();
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        using(SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            using(SqlCommand cmd = new SqlCommand("update [Groups list] set [Group name]='" + txtgroupname.Text.Trim() + "' where [Group id]=" + g_id, con))
            {
                cmd.ExecuteNonQuery();
            }
        }
        Session["groupID"] = g_id.ToString();
        Session["groupName"] = txtgroupname.Text.Trim(); ;
        Session.Timeout = 30;
        Response.Redirect("groupDetails.aspx");
    }

    protected void btnaddmember_Click(object sender, EventArgs e)
    {
        using(SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            using(SqlCommand cmd = new SqlCommand("select [Group name] from [Groups list] where [Group id]=" + g_id, con))
            {
                Session["groupName"] = cmd.ExecuteScalar().ToString();
            }
        }
        Session["groupID"] = g_id.ToString();
        Session.Timeout = 30;
        Response.Redirect("addMember.aspx");
    }

    protected void btnsearchmember_Click(object sender, EventArgs e)
    {
        if(txtsearchmember.Text.Trim() != "")
        {
            Session["isMemberListWithSearchedElement"] = "true";
        }
        else
        {
            Session["isMemberListWithSearchedElement"] = "false";
        }
        using(SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            List<string> listMemberNo = new List<string>();

            using (SqlCommand cmd = new SqlCommand("select * from [G" + g_id.ToString() + "]", con))
            {
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    listMemberNo.Add(sdr["Member phone no"].ToString());
                }
                sdr.Close();
            }
            int indexOfMemberNo = listMemberNo.IndexOf(txtsearchmember.Text.Trim());

            if(indexOfMemberNo >= 0)
            {
                using (SqlDataAdapter sda = new SqlDataAdapter("select t1.[Member phone no] as phonoNo, SUBSTRING(t2.[First name], 1, 1) as name_initial, REPLACE(t2.[First name] + ' ' + t2.[Middle name] + ' ' + t2.[Last name], '  ', ' ') As name from [G" + g_id.ToString() + "] t1, [User details] t2 where t1.[Member phone no]=" + Int64.Parse(txtsearchmember.Text.Trim()) + " and t2.[Phone no]=" + Int64.Parse(txtsearchmember.Text.Trim()) + " ORDER BY REPLACE(t2.[First name] + ' ' + t2.[Middle name] + ' ' + t2.[Last name], '  ', ' ')", con))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    repeaterMember.DataSource = dt;
                    repeaterMember.DataBind();
                    lblmatchfound.Visible = false;
                    return;
                }
            }
            listMemberNo.Clear();

            int matchFound = 0;

            using(SqlCommand cmd = new SqlCommand("select t2.[Member phone no] as phonoNo, SUBSTRING(t1.[First name], 1, 1) as name_initial, REPLACE(t1.[First name] + ' ' + t1.[Middle name] + ' ' + t1.[Last name], '  ', ' ') As name from [User details] t1, [G" + g_id.ToString() + "] t2 where t1.[Phone no]=t2.[Member phone no] and REPLACE(t1.[First name] + ' ' + t1.[Middle name] + ' ' + t1.[Last name], '  ', ' ') LIKE '%' + '" + txtsearchmember.Text.Trim() + "' + '%' ORDER BY REPLACE(t1.[First name] + ' ' + t1.[Middle name] + ' ' + t1.[Last name], '  ', ' ')", con))
            {
                SqlDataReader sdr = cmd.ExecuteReader();
                

                if (sdr.Read())
                {
                    matchFound++;
                }

                sdr.Close();
            }
            using(SqlDataAdapter sda = new SqlDataAdapter("select t2.[Member phone no] as phonoNo, SUBSTRING(t1.[First name], 1, 1) as name_initial, REPLACE(t1.[First name] + ' ' + t1.[Middle name] + ' ' + t1.[Last name], '  ', ' ') As name from [User details] t1, [G" + g_id.ToString() + "] t2 where t1.[Phone no]=t2.[Member phone no] and REPLACE(t1.[First name] + ' ' + t1.[Middle name] + ' ' + t1.[Last name], '  ', ' ') LIKE '%' + '" + txtsearchmember.Text.Trim() + "' + '%' ORDER BY REPLACE(t1.[First name] + ' ' + t1.[Middle name] + ' ' + t1.[Last name], '  ', ' ')", con))
            {
                DataTable dt = new DataTable();
                sda.Fill(dt);
                repeaterMember.DataSource = dt;
                repeaterMember.DataBind();
            }
            if (matchFound != 0)
            {
                lblmatchfound.Visible = false;
            }
            else
            {
                lblmatchfound.Visible = true;
            }
        }
    }



    protected void repeaterMember_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "removeMember")
        {
            int repeterIndex = e.Item.ItemIndex;
            Label labelMemberNo = (Label)repeaterMember.Items[repeterIndex].FindControl("lblmemberno");
            Int64 memberNoToRemove = Int64.Parse(labelMemberNo.Text);

            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                int noOfMemberInGroup;

                using (SqlCommand cmd = new SqlCommand("delete from [G" + g_id.ToString() + "] where [Member phone no]=" + memberNoToRemove, con))
                {
                    cmd.ExecuteNonQuery();
                }
                using (SqlCommand cmd = new SqlCommand("delete from [" + memberNoToRemove.ToString() + "] where [Group id]=" + g_id, con))
                {
                    cmd.ExecuteNonQuery();
                }
                using(SqlCommand cmd = new SqlCommand("select [No of members] from [Groups list] where [Group id]=" + g_id, con))
                {
                    noOfMemberInGroup = int.Parse(cmd.ExecuteScalar().ToString());
                }
                noOfMemberInGroup--;
                using (SqlCommand cmd = new SqlCommand("update [Groups list] set [No of members]=" + noOfMemberInGroup + " where [Group id]=" + g_id, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            if(Request.Cookies["Phone_no"].Value.ToString() != memberNoToRemove.ToString())
            {
                Response.Redirect("groupDetails.aspx");
            }
        }
    }

}