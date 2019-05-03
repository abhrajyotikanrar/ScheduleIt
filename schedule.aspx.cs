using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class _Default : System.Web.UI.Page
{
    string connectionString = ConnectionString.getConnectionString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) // If page loads for first time
        {
            Session["scheduleIndividualTaskState"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }
    }

    protected void btnschedule_Click(object sender, EventArgs e)
    {
        if (Session["scheduleIndividualTaskState"].ToString() != ViewState["scheduleIndividualTaskState"].ToString())
        {
            return;
        }
        if (txttitle.Text.Trim() == "" || txtdescription.Text.Trim() == "" || txtdate.Text.Trim()=="" || txttime.Text.Trim() == "")
        {
            return;
        }
        long individualTaskId;
        string mailAddress;

        using(SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            using(SqlCommand cmd = new SqlCommand("select MAX(CAST([Task id] as int)) from [Individual task details]", con))
            {
                string res = cmd.ExecuteScalar().ToString();
                if (res == "")
                {
                    res = "0";
                }
                individualTaskId = long.Parse(res);
                individualTaskId++;
            }
            using(SqlCommand cmd= new SqlCommand("insert into [Individual task details] ([Task id], [Task type], [Phone no], [Time], [Date], [Title], [Description], [Task status], [Notification status]) values(@taskid, @tasktype, @phoneno, @time, @date, @title, @description, @taskstatus, @notificationstatus)", con))
            {
                cmd.Parameters.AddWithValue("taskid", individualTaskId);
                cmd.Parameters.AddWithValue("tasktype", type.Text);
                cmd.Parameters.AddWithValue("phoneno", Int64.Parse(Request.Cookies["Phone_no"].Value.ToString()));
                cmd.Parameters.AddWithValue("time", DateTime.Parse(txttime.Text).ToString("hh:mm tt"));
                cmd.Parameters.AddWithValue("date", DateTime.Parse(txtdate.Text).ToString("dd/MM/yyyy"));
                cmd.Parameters.AddWithValue("title", txttitle.Text.Trim());
                cmd.Parameters.AddWithValue("description", txtdescription.Text.Trim());
                cmd.Parameters.AddWithValue("taskstatus", "Scheduled");
                cmd.Parameters.AddWithValue("notificationstatus", "Pending");
                cmd.ExecuteNonQuery();
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "scheduleSuccessAlert()", true);
            }
            using(SqlCommand cmd = new SqlCommand("select [Email] from [User details] where [Phone no]=" + Int64.Parse(Request.Cookies["Phone_no"].Value.ToString()) + " and [Email verified]='true'", con))
            {
                mailAddress = cmd.ExecuteScalar().ToString();
            }
            if(mailAddress != null)
            {
                //Send mail to user regarding scheduled task details 
                string msgBodyToSend = "<h3>Hey! Greetings from Scheduleit. <br/><br/>";
                msgBodyToSend = msgBodyToSend + "Your task \"<b>" + txttitle.Text.Trim() + "</b>\" with the <b>ID: " + individualTaskId.ToString() + "</b> scheduled successfuly.<br/><br/>";
                msgBodyToSend = msgBodyToSend + "<b>Task Details:</b><br/><br/>";
                msgBodyToSend = msgBodyToSend + "<b>Task Id: </b>" + individualTaskId.ToString() + "<br/>";
                msgBodyToSend = msgBodyToSend + "<b>Task Type: </b>" + type.Text + "<br/>";
                msgBodyToSend = msgBodyToSend + "<b>Title: </b>" + txttitle.Text.Trim() + "<br/>";
                msgBodyToSend = msgBodyToSend + "<b>Description: </b>" + txtdescription.Text.Trim() + "<br/>";
                msgBodyToSend = msgBodyToSend + "<b>Scheduled Date: </b>" + DateTime.Parse(txtdate.Text).ToString("dd/MM/yyyy") + "<br/>";
                msgBodyToSend = msgBodyToSend + "<b>Scheduled Time: </b>" + DateTime.Parse(txttime.Text).ToString("hh:mm tt") + "<br/>";
                msgBodyToSend = msgBodyToSend + "<b>Task Status: </b>" + "Scheduled" + "<br/>";
                msgBodyToSend = msgBodyToSend + "<b>Notification Status: </b>" + "Pending" + "<br/>";
                msgBodyToSend = msgBodyToSend + "<br/><br/>";
                msgBodyToSend = msgBodyToSend + "You will be notified before 10 minutes on your registered phone number i.e. <b>" + Request.Cookies["Phone_no"].Value.ToString() + "</b><br/>";
                msgBodyToSend = msgBodyToSend + "Thank you being with us<br/>";
                msgBodyToSend = msgBodyToSend + "<b>Scheduleit</b></h3>";
                string msgSubjectToSend = "Task Id: " + individualTaskId + " scheduled successfully.";
                SendMail mail = new SendMail();
                mail.sendMailWithInfo(msgSubjectToSend, msgBodyToSend, mailAddress);
            }
        }
        Session["scheduleIndividualTaskState"] = Server.UrlEncode(System.DateTime.Now.ToString());
    }

    protected override void OnPreRender(EventArgs e)
    {
        ViewState["scheduleIndividualTaskState"] = Session["scheduleIndividualTaskState"];
    }
}