using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class registration : System.Web.UI.Page
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
    }

    protected void btnregister_Click(object sender, EventArgs e)
    {
        if (chkagree.Checked == false)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Please check the tearms and conditions')", true);
            return;
        }

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("select COUNT(*) from [User details] where [Phone no]=" + Int64.Parse(txtphno.Text), con))
            {
                int count = (int)cmd.ExecuteScalar();
                if (count != 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Phone number already exists. Chenge your phone number.')", true);
                    return;
                }
            }
            Random rnd = new Random();
            int OTP = rnd.Next(1000, 10000);

            string smsToSend = "Hi, " + OTP.ToString() + " is your OTP for your scheduleit account verification.";
            SendSms sndsms = new SendSms();
            sndsms.send(smsToSend, txtphno.Text.Trim());

            Session["Phone_no"] = txtphno.Text.Trim();
            Session["fname"] = txtfname.Text.Trim();
            Session["mname"] = txtmname.Text.Trim();
            Session["lname"] = txtlname.Text.Trim();
            Session["email"] = txtmail.Text.Trim();
            Session["dob"] = txtdob.Text.Trim();
            Session["gender"] = gender.SelectedValue;
            Session["password"] = txtpassword.Text;
            Session["OTPinPhone"] = OTP.ToString();
            Session.Timeout = 5;
            Response.Redirect("phone_no_verification.aspx");
        }
    }
}