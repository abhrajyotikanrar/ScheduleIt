using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

public partial class _Default : System.Web.UI.Page
{
    string connectionString = ConnectionString.getConnectionString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            return;
        }
        lblphno.Text = Request.Cookies["Phone_no"].Value;
        lbllogondatetime.Text = "Logged on " + Request.Cookies["Login_time"].Value;


        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("select * from [User details] where [Phone no]=" + Int64.Parse(lblphno.Text), con))
            {
                SqlDataReader sdr = cmd.ExecuteReader();
                
                if (sdr.Read())
                {
                    lblname.Text = sdr["First name"].ToString() + " " + sdr["Middle name"].ToString() + " " + sdr["Last name"].ToString();
                    lblemail.Text = sdr["Email"].ToString();
                    txtfname.Text = sdr["First name"].ToString();
                    txtmname.Text = sdr["Middle name"].ToString();
                    txtlname.Text = sdr["Last name"].ToString();
                    txtemail.Text= sdr["Email"].ToString();
                    if (sdr["Email verified"].ToString() == "false")
                    {
                        imgverified.Visible = false;
                        imgnotverified.Visible = true;
                        btnverify.Visible = true;
                    }
                    else
                    {
                        imgverified.Visible = true;
                        imgnotverified.Visible = false;
                        btnverify.Visible = false;
                    }
                    lbldob.Text = sdr["DOB"].ToString();
                    lblgender.Text = sdr["Gender"].ToString();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Kindly login again.')", true);
                    Response.Redirect("login.aspx");
                    return;
                }

            }
        }
    }

    protected void btnverify_Click(object sender, EventArgs e)
    {
        sendVerificationMailToUser();
    }

    protected void btnupdateprofile_Click(object sender, EventArgs e)
    {
        bool changeEmail = false;

        if (txtemail.Text.Trim() != "")
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("select [Email] from [User details] where [Phone no]=" + Int64.Parse(lblphno.Text), con))
                {
                    //SqlDataReader sdr = cmd.ExecuteReader();
                    string mailFromDb = (string)cmd.ExecuteScalar();

                    if (mailFromDb != null)
                    {
                        string newMail = txtemail.Text.Trim();
                        if (mailFromDb != newMail)
                        {
                            changeEmail = true;
                        }
                    }
                    //sdr.Close();
                }

                using (SqlCommand cmd = new SqlCommand("update [User details] set [First name]=@fname, [Middle name]=@mname, [Last name]=@lname where [Phone no]=" + Int64.Parse(lblphno.Text), con))
                {
                    cmd.Parameters.AddWithValue("fname", txtfname.Text.Trim());
                    cmd.Parameters.AddWithValue("mname", txtmname.Text.Trim());
                    cmd.Parameters.AddWithValue("lname", txtlname.Text.Trim());
                    cmd.ExecuteNonQuery();
                    Response.Cookies["Name"].Value = txtfname.Text.Trim() + " " + txtmname.Text.Trim() + " " + txtlname.Text.Trim();
                }

                string name = txtfname.Text.Trim() + " " + txtmname.Text.Trim() + " " + txtlname.Text.Trim();
                using (SqlCommand cmd = new SqlCommand("update [Groups list] set [Creator name]='" + name.Trim() + "' where [Creator no]=" + Int64.Parse(lblphno.Text), con))
                {
                    cmd.ExecuteNonQuery();
                }
                using (SqlCommand cmd = new SqlCommand("update [Group task details] set [Task generator name]='" + name.Trim() + "' where [Task generator no]=" + Int64.Parse(lblphno.Text), con))
                {
                    cmd.ExecuteNonQuery();
                }
                using (SqlCommand cmd = new SqlCommand("update [Feedback] set [Name]='" + name.Trim() + "' where [Phone no]=" + Int64.Parse(lblphno.Text), con))
                {
                    cmd.ExecuteNonQuery();
                }

                if (changeEmail)
                {
                    using (SqlCommand cmd = new SqlCommand("update [User details] set [Email]=@mail, [Email verified]=@emailverified where [Phone no]=" + Int64.Parse(lblphno.Text), con))
                    {
                        cmd.Parameters.AddWithValue("mail", txtemail.Text.Trim());
                        cmd.Parameters.AddWithValue("emailverified", "false");
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "profileupdateSuccessAlert()", true);
            Response.Redirect("profile.aspx");
        }      
    }

    private void sendVerificationMailToUser()
    {
        SendMail sendVerificationMail = new SendMail();
        Random rnd = new Random();
        int OTP = rnd.Next(100000, 1000000);

        string bodyOfMail = "<center><h4>Your OTP for your account verification is</h4></center><br><br><center><div style='background - color: yellow; padding: 20px; color: red; width: 100px;'><h2><b>"+ OTP.ToString() + "</b></h2></div></center><br><br>Enter your OTP in verification window.";
        string subjectOfMail = "Verify your scheduleit account";
        string mailTo = lblemail.Text.Trim();
        bool mailStatus = sendVerificationMail.sendMailWithInfo(subjectOfMail, bodyOfMail, mailTo);

        if (mailStatus == true)
        {
            Session["otpformail"] = OTP.ToString();
            Session["mailid"] = lblemail.Text.Trim();
            Session.Timeout = 5;
            Response.Redirect("verifyemail.aspx");
        }
        else if (mailStatus == false)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('OTP sending failed. Check your email address and network connection.')", true);
        }
        
    }
}