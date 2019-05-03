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
    SendMail mail = new SendMail();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnverify_Click(object sender, EventArgs e)
    {
        string userNum = Request.Cookies["Phone_no"].Value.ToString();
        if (Session["otpformail"] != null && Session["mailid"] != null)
        {
            if (Session["otpformail"].ToString() == txtotp.Text.Trim())
            {
                using(SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using(SqlCommand cmd =new SqlCommand("update [User details] set [Email verified]='true' where [Phone no]="+ Int64.Parse(userNum), con))
                    {
                        cmd.ExecuteNonQuery();

                        string bodyOfMail = "Hi <b>" + Session["mailid"].ToString() + "</b>,<br/><br/><br/>Greetings from Scheduleit Ltd.<br/>Your email address <b>" + Session["mailid"].ToString() + "</b> has been verified successfully in your scheduleit account.";
                        string subjectOfMail = "Congratulations! Your email address has been verified.";
                        string mailTo = Session["mailid"].ToString();

                        mail.sendMailWithInfo(subjectOfMail, bodyOfMail, mailTo);
                        ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "emailVerificationSuccessAlert()", true);
                        Response.Redirect("profile.aspx");
                    }
                }
                
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Please enter your OTP')", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Sorry, your session for OTP verification is out. Kindly click on verify button again for OTP.')", true);
            Response.Redirect("profile.aspx");
        }
    }
}