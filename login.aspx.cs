using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class login : System.Web.UI.Page
{
    string connectionString = ConnectionString.getConnectionString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["Phone_no"] != null)
        {
            string ph_no = Request.Cookies["Phone_no"].Value.ToString();
            string password = Request.Cookies["Password"].Value.ToString();

            if (ph_no != null)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("select * from [User details] where [Phone no]=" + Int64.Parse(ph_no), con))
                    {
                        SqlDataReader sdr = cmd.ExecuteReader();

                        if (sdr.Read())
                        {
                            if (sdr["Password"].ToString() == password)
                            {
                                Response.Redirect("Welcome.aspx");
                            }
                        }
                    }
                }
            }
        }

        txtphonenoforforgotpw.Visible = true;
        txtotpforforgotpw.Visible = false;
        txtnewpw.Visible = false;
        txtconfirmpw.Visible = false;

        btnsubmit.Visible = true;
        btnverifyotp.Visible = false;
        btnchangepw.Visible = false;

        if (Session["pwChangeStatus"] != null)
        {
            divpwchangestatus.Visible = true;
            Session.Abandon();
        }
        else
        {
            divpwchangestatus.Visible = false;
        }
    }

    protected void btnlogin_Click(object sender, EventArgs e)
    {

        string phno = txtphno.Text.Trim();
        string password = txtpassword.Text;
        
        if (phno.Length != 10)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Please enter a valid phone number')", true);
            return;
        }
        if(password.Length == 0)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Please enter your password')", true);
            return;
        }
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("select * from [User details] where [Phone no]=" + Int64.Parse(txtphno.Text), con))
            {
                SqlDataReader sdr = cmd.ExecuteReader();

                if (sdr.Read())
                {
                    if (sdr["Password"].ToString() == txtpassword.Text)
                    {
                        Response.Cookies["Phone_no"].Value = txtphno.Text.Trim();
                        Response.Cookies["Name"].Value = sdr["First name"].ToString() + " " + sdr["Middle name"].ToString() + " " + sdr["Last name"].ToString();
                        Response.Cookies["Password"].Value = txtpassword.Text;
                        Response.Cookies["Login_time"].Value = DateTime.Now.ToString();

                        Response.Redirect("Welcome.aspx");
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Enter your password correctly to login.')", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Check your phone number and enter again.')", true);
                    return;
                }
                
            }
        }

    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if(txtphonenoforforgotpw.Text.Length == 0)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Enter your phone number to get OTP.')", true);
            return;
        }
        Random rnd = new Random();
        SendSms sndsms = new SendSms();

        
        using(SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            using(SqlCommand cmd = new SqlCommand("select COUNT(*) from [User details] where [Phone no]=" + Int64.Parse(txtphonenoforforgotpw.Text), con))
            {
                int countrow = int.Parse(cmd.ExecuteScalar().ToString());

                if(countrow == 1)
                {
                    int OTP = rnd.Next(1000, 10000);

                    string msg = "Hi, your OTP for account password recovery is "+OTP.ToString()+".";
                    sndsms.send(msg, txtphonenoforforgotpw.Text);
                    Session["OTP_ForgetPw"] = OTP.ToString();
                    Session.Timeout = 5;
                    
                    txtphonenoforforgotpw.Visible = false;
                    txtotpforforgotpw.Visible = true;
                    txtnewpw.Visible = false;
                    txtconfirmpw.Visible = false;

                    btnsubmit.Visible = false;
                    btnverifyotp.Visible = true;
                    btnchangepw.Visible = false;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Enter your registered phone number only.')", true);
                    return;
                }
            }
        }
    }

    protected void btnverifyotp_Click(object sender, EventArgs e)
    {
        string otpFromSession = Session["OTP_ForgetPw"].ToString();
        if(txtotpforforgotpw.Text.Trim() == otpFromSession)
        {
            Session.Abandon();
            txtphonenoforforgotpw.Visible = false;
            txtotpforforgotpw.Visible = false;
            txtnewpw.Visible = true;
            txtconfirmpw.Visible = true;

            btnsubmit.Visible = false;
            btnverifyotp.Visible = false;
            btnchangepw.Visible = true;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Enter OTP sent to your registered phone number.')", true);
            txtphonenoforforgotpw.Visible = false;
            txtotpforforgotpw.Visible = true;
            txtnewpw.Visible = false;
            txtconfirmpw.Visible = false;

            btnsubmit.Visible = false;
            btnverifyotp.Visible = true;
            btnchangepw.Visible = false;
            return;
        }
    }

    protected void btnchangepw_Click(object sender, EventArgs e)
    {
        if(txtnewpw.Text.Length == 0)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Please enter your new password.')", true);
            txtphonenoforforgotpw.Visible = false;
            txtotpforforgotpw.Visible = false;
            txtnewpw.Visible = true;
            txtconfirmpw.Visible = true;

            btnsubmit.Visible = false;
            btnverifyotp.Visible = false;
            btnchangepw.Visible = true;
            return;
        }
        if (txtconfirmpw.Text != txtnewpw.Text)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Please Re-enter your new password.')", true);
            txtphonenoforforgotpw.Visible = false;
            txtotpforforgotpw.Visible = false;
            txtnewpw.Visible = true;
            txtconfirmpw.Visible = true;

            btnsubmit.Visible = false;
            btnverifyotp.Visible = false;
            btnchangepw.Visible = true;
            return;
        }

        using(SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("update [User details] set [Password]='" + txtnewpw.Text + "' where [Phone no]=" + txtphonenoforforgotpw.Text, con))
            {
                cmd.ExecuteNonQuery();
                Session["pwChangeStatus"] = "true";
                Session.Timeout = 5;
                Response.Redirect("login.aspx");
            }
        }
    }
}