using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class phone_no_verification : System.Web.UI.Page
{
    string connectionString = ConnectionString.getConnectionString();
    string OTP;
    string phno, fname, mname, lname, email, dob, gender, pass;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["OTPinPhone"] == null)
            {
                Response.Redirect("registration.aspx");
            }
            else
            {
                try
                {
                    lblPhoneNo.Text = Session["Phone_no"].ToString();
                }
                catch (Exception ex)
                {
                    Response.Redirect("registration.aspx");
                }

            }
        }
        
    }

    protected void btnverify_Click(object sender, EventArgs e)
    {
        if (Session["OTPinPhone"] == null)
        {
            Response.Redirect("registration.aspx");
        }

        OTP = Session["OTPinPhone"].ToString();

        if (txtotp.Text.ToString() != OTP)
        {
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Enter correct OTP.')", true);
            return;
        }

        phno = Session["Phone_no"].ToString();
        fname = Session["fname"].ToString();
        mname = Session["mname"].ToString();
        lname = Session["lname"].ToString();
        email = Session["email"].ToString();
        dob = Session["dob"].ToString();
        gender = Session["gender"].ToString();
        pass = Session["password"].ToString();
        Session.Abandon();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string cerateUserTable = "create table [" + phno + "] ([Group id] numeric(18, 0) primary key, [Joining time] varchar(MAX), [Joining date] varchar(MAX))";
            using (SqlCommand cmd = new SqlCommand(cerateUserTable, con))
            {
                cmd.ExecuteNonQuery();
            }

            string insertQuery = "insert into [User details] ([Phone no],[First name],[Middle name],[Last name],[Email],[DOB],[Gender],[Password],[Email verified]) values (@ph_no, @fname, @mname, @lname, @email, @dob, @gender, @password, @emailverified)";
            using (SqlCommand cmd = new SqlCommand(insertQuery, con))
            {
                cmd.Parameters.AddWithValue("@ph_no", Int64.Parse(phno));
                cmd.Parameters.AddWithValue("@fname", fname);
                cmd.Parameters.AddWithValue("@mname", mname);
                cmd.Parameters.AddWithValue("@lname", lname);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@dob", dob);
                cmd.Parameters.AddWithValue("@gender", gender);
                cmd.Parameters.AddWithValue("@password", pass);
                cmd.Parameters.AddWithValue("@emailverified", "false");


                cmd.ExecuteNonQuery();
                Response.Cookies["Phone_no"].Value = phno;
                Response.Cookies["Name"].Value = fname + " " + mname + " " + lname;
                Response.Cookies["Password"].Value = pass;
                Response.Cookies["Login_time"].Value = DateTime.Now.ToString();

                Response.Redirect("Welcome.aspx");
            }
        }
    }
}