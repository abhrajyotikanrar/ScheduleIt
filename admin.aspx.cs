using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

public partial class _Default : System.Web.UI.Page
{
    string connectionString = ConnectionString.getConnectionString();
    System.Timers.Timer tm = new System.Timers.Timer();

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void timerSmsSending_Tick(object sender, EventArgs e)
    {
        //Thread smsToIndividual = new Thread(delegate ()
        //{
            sendSmsToIndividualUser();
        //});

        //Thread smsTogroups = new Thread(delegate ()
        //{
            sendSmsToGroupMembers();
        //});

        Response.Redirect("admin.aspx");
    }

    public void sendSmsToIndividualUser()
    {
        SendSms sndsms = new SendSms();
        SqlConnection con = new SqlConnection(connectionString);

        con.Open();
        using (SqlCommand cmd = new SqlCommand("select [Phone no], [Title], [Time] from [Individual task details] where [Date]='" + DateTime.Now.AddMinutes(10).ToString("dd/MM/yyyy") + "' and [Time]='" + DateTime.Now.AddMinutes(10).ToString("hh:mm tt") + "'", con))
        {
            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                string msg_title = sdr["Title"].ToString();
                string num = sdr["Phone no"].ToString();
                string time = sdr["Time"].ToString();
                string msg;

                if (msg_title.Length > 50)
                {
                    msg = msg_title.Substring(0, 47) + "...";
                }
                else
                {
                    msg = msg_title;
                }

                string msgToSend = "Hi, you have scheduled your task \"" + msg + "\" on " + time + ".%nFrom Scheduleit.";

                sndsms.send(msgToSend, num);

                using (SqlConnection con2 = new SqlConnection(connectionString))
                {
                    con2.Open();
                    using (SqlCommand cmd2 = new SqlCommand("update [Individual task details] set [Notification status]='Sent' where [Date]='" + DateTime.Now.AddMinutes(10).ToString("dd/MM/yyyy") + "' and [Time]='" + DateTime.Now.AddMinutes(10).ToString("hh:mm tt") + "'", con2))
                    {
                        cmd2.ExecuteNonQuery();
                    }
                }
            }

            sdr.Close();
        }

        using (SqlCommand cmd = new SqlCommand("update [Individual task details] set [Task status]='Outdated' where [Date]='" + DateTime.Now.ToString("dd/MM/yyyy") + "' and [Time]='" + DateTime.Now.ToString("hh:mm tt") + "'", con))
        {
            cmd.ExecuteNonQuery();
        }
        con.Close();
    }

    public void sendSmsToGroupMembers()
    {
        string curr_time = DateTime.Now.AddMinutes(10).ToString("hh:mm tt");
        string curr_date = DateTime.Now.AddMinutes(10).ToString("dd/MM/yyyy");
        SendSms sndsms = new SendSms();
        SqlConnection con = new SqlConnection(connectionString);
        List<string> listGroupsId = new List<string>();
        List<string> listGroupMemberNo = new List<string>();

        con.Open();
        using (SqlCommand cmd = new SqlCommand("select [Group id] from [Groups list] where [Status]='Active'", con))
        {
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                listGroupsId.Add(sdr["Group id"].ToString());
            }
            sdr.Close();
        }
        

        foreach(string item in listGroupsId)
        {
            using (SqlCommand cmd = new SqlCommand("select [Member phone no] from [G" + item + "]", con))
            {
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    listGroupMemberNo.Add("91"+sdr["Member phone no"].ToString());
                }
                sdr.Close();
            }

            string numsOfUsers = string.Join(",", listGroupMemberNo);
            listGroupMemberNo.Clear();
            string msg_title, msg_time, group_name, msg, group;

            using (SqlCommand cmd = new SqlCommand("select t1.[Title] As title, t1.[Time] As time, t2.[Group name] as groupname from [Group task details] t1, [Groups list] t2 where t1.[Group id]=" + int.Parse(item) + " and t2.[Group id]=" + int.Parse(item) + " and t1.[Date]='" + curr_date + "' and t1.[Time]='" + curr_time + "'", con))
            {
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    msg_title = sdr["title"].ToString();
                    msg_time = sdr["time"].ToString();
                    group_name = sdr["groupname"].ToString();

                    if (msg_title.Length > 50)
                    {
                        msg = msg_title.Substring(0, 47) + "...";
                    }
                    else
                    {
                        msg = msg_title;
                    }

                    if (group_name.Length > 30)
                    {
                        group = group_name.Substring(0, 27) + "...";
                    }
                    else
                    {
                        group = group_name;
                    }

                    string msgToSend = "Hi, your task \"" + msg + "\" scheduled on " + msg_time + " in the group \"" + group + "\".%nFrom Scheduleit.";
                    
                    sndsms.send(msgToSend, numsOfUsers);
                    
                    numsOfUsers = "";
                }
                
                sdr.Close();
            }
            
        }
        listGroupsId.Clear();

        using(SqlCommand cmd = new SqlCommand("update [Group task details] set [Status]='Outdated' where [Time]='" + DateTime.Now.ToString("hh:mm tt") + "' and [Date]='" + DateTime.Now.ToString("dd/MM/yyyy") + "'", con))
        {
            cmd.ExecuteNonQuery();
        }

        con.Close();
    }

    protected void btnStartSms_Click(object sender, EventArgs e)
    {
        timerSmsSending.Enabled = true;
    }

    protected void btnStopSms_Click(object sender, EventArgs e)
    {
        timerSmsSending.Enabled = false;
    }
}